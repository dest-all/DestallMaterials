using DestallMaterials.CodeGeneration.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerationTests
{
    public class Auxiliary
    {
        [Test]
        public void TextRemoval()
        {
            const string input = @" CallAsync(Protocol.Models.ChangePasswordRequest.IChangePasswordRequestModel passwords <text >) => await ChangePasswordInnerDispatcher.RunInvocationAsync(_source, passwords); < /
        text > <text > < /text >}";

            var result = FileWriter.RemoveTextTags(input);

        }
    }
}
