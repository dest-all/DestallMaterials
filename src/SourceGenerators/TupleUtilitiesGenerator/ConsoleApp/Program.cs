using DestallMaterials.Extensions.Tuples;

var a = (1,2,3,4,5,6);

var b = a.ToDictionary();

Console.WriteLine(b.First().Key);

return 0;