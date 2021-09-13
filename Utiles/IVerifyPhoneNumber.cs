using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utiles
{
    public interface IVerifyPhoneNumber
    {
        string GetCodeByPhoneNumber(string phoneNumber);

        bool CheckCode(string phoneNumber, string code);
    }
}
