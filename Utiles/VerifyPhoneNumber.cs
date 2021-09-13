using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utiles
{
    public class VerifyPhoneNumber : IVerifyPhoneNumber
    {
        private IDictionary<string, string> _dic;
        private CodeGenerate _codeGenerate;
        public VerifyPhoneNumber()
        {
            _codeGenerate = new CodeGenerate();
            _dic = new Dictionary<string, string>();
        }
        public bool CheckCode(string phoneNumber, string code)
        {
            if (!_dic.ContainsKey(phoneNumber)) return false;

            if (_dic[phoneNumber] == code) return true;

            return false;
        }

        public string GetCodeByPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber)) return null;

            string code = _codeGenerate.GetChangedCode();

            if (!_dic.ContainsKey(phoneNumber)) _dic.Add(phoneNumber, code);
            else
            {
                _dic[phoneNumber] = code;
            }

            Console.WriteLine("verify code: " + code);

            return _dic[phoneNumber];
        }
    }
}
