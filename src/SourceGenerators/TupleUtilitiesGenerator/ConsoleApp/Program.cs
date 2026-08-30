using EverModern.Memory;
using MemoryPack;
using System.Runtime.Intrinsics;

var contract = new ExampleContract();

var formatters = contract.CreateFormatters();

var tasks = (4, 5, 6, 7).Select(x => Task.FromResult(x));
var (a, b, c, d) = await tasks;

var dataFormatters = new DataFormatter().CreateFormatters();

MemoryPackFormatterProvider.RegisterMany(formatters);
MemoryPackFormatterProvider.RegisterMany(dataFormatters);

var alice = new Person("Alice", 30);

var bob = new Person("Bob", 25);

var fluffy = new Pet("Fluffy", 3, alice);

var input = fluffy;

var bytes = MemoryPackSerializer.Serialize<Creature>(input);

var output = MemoryPackSerializer.Deserialize<Creature>(bytes);

var pass = input == output;

Vector128<float> vector = Vector128.Create(1.0f, 2.0f, 3.0f, 4.0f);

return 0;

// ── existing Creature hierarchy ─────────────────────────────────────

public abstract record Creature(string Name, int Age);

public record Person(string Name, int Age, Creature[] Friends) : Creature(Name, Age)
{
    public Person(string name, int age)
        : this(name, age, []) { }
}

public record Pet(string Name, int Age, Person Owner) : Creature(Name, Age);

public partial class ExampleContract : IAutoPack<(Creature, Pet)>; // this is a hierarachy example. Here all the specified heirs of Creature are processed, because it's an abstract type coming first.

// Abstract type can only come first and all consequent heirs must be derived types and their order sets their priority when they are assigned their ids in the hierarchy.
// If an heir type is not present in the tuple, its place in the order is determined based on alphabetic ordering.
// Can't have more than one implemented interface of this type.
// Normally the analyser should take all the types in the assembly, but if additional heirs are added to the tuple, they must be processed too.

// ── single-type (non-tuple) hierarchy ────────────────────────────────

public abstract record DataItem;

public record Employee(string Name, DateTime Birthdate) : DataItem;

public record Product(string Name, decimal Price) : DataItem;

public partial class DataFormatter : IAutoPack<DataItem>;
