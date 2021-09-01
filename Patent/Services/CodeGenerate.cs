using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patent.Services
{
    public class CodeGenerate
    {
        public string Code{ get; set; }
        private readonly Random generator;
        public CodeGenerate()
        {
            generator = new Random();
            Code = generator.Next(0, 1000000).ToString("D6");
        }

        public void ChangeCode()
        {
            Code = generator.Next(0, 1000000).ToString("D6");
        }

        public override bool Equals(object obj)
        {
            return Code.Equals(obj);
        }
    }
}
