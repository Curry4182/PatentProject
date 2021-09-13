using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utiles
{
    public class SaveAndLoad : ISaveAndLoad
    {
        IDictionary<string, object> _dic;
        public SaveAndLoad()
        {
            _dic = new Dictionary<string, object>();
        }

        public object Save(string key, object item)
        {
            if (!_dic.ContainsKey(key))
            {
                _dic.Add(key, item);
            }
            else
            {
                _dic[key] = item;
            }

            return _dic[key];
        }

        public object Load(string key)
        {
            if (key == null) return null;
            if (!_dic.ContainsKey(key)) return null;

            return _dic[key];
        }
    }
}
