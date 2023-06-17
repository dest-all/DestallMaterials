using CodeGenerationExample.ClientDependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerationExample.Target;

[TargetClass]
class AttributeCarrier
{
    public string Hello() => nameof(Hello);

    public char Comma() => ',';

    public string World() => nameof(World);

    public char ExclamationMark() => '!';  

}

public static partial class InfoProvider
{
}


