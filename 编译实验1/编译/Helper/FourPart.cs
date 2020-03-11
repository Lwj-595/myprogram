using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LLanguageComplier
{
    public class FourPart
    {
        public string Op { get; set; }
        public string StrLeft { get; set; }
        public string StrRight { get; set; }
        public string JumpNum { get; set; }
        public bool Input { get; set; }
        public FourPart() { }
        public FourPart(string op, string strLeft, string strRight, string jumpNum)//四元式
        {
            this.Op = op;
            this.StrLeft = strLeft;
            this.StrRight = strRight;
            this.JumpNum = jumpNum;
            this.Input = false;
        }
    }
}
