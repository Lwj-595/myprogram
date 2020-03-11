using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LLanguageComplier
{
    public class Assembly
    {
        public string Op { set; get; }
        public string PL { set; get; }
        public string PR { set; get; }
        public string Num { set; get; }
        public Assembly(string op, string pl, string pr) //三元式
        {
            Op = op;
            PL = pl;
            PR = pr;
        }
    }
}
