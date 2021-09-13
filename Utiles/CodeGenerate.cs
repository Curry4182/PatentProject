using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utiles
{
    public class CodeGenerate
    {
        private string _code;
        public string Code{
            get
            {
                return _code;
            }
        }
        private readonly Random generator;
        public CodeGenerate()
        {
            generator = new Random();
            _code = generator.Next(0, 1000000).ToString("D6");
        }

        public string GetChangedCode()
        {
            ChangeCode();
            return _code;
        }

        public void ChangeCode()
        {
            _code = generator.Next(0, 1000000).ToString("D6");
        }
    }
}
