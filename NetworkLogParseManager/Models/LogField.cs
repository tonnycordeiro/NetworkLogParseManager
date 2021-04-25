using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLogParseManager.Models
{
    public class LogField
    {
        private int _index;
        private string _id;
        private string _value;

        public int Index { get => _index; set => _index = value; }
        public string Id { get => _id; set => _id = value; }
        public string Value { get => _value; set => _value = value; }
    }
}
