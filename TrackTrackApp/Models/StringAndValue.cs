using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTrackApp.Models
{
    public class StringAndValue
    {
        public string String { get; set; }
        public int Value { get; set; }
        public StringAndValue(string s, int v)
        {
            String = s;
            Value = v;
        }
        public StringAndValue() { }
    }
}
