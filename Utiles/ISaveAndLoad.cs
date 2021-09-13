using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utiles
{
    public interface ISaveAndLoad
    {
        public object Save(string key, object value);
        public object Load(string key);
    }
}
