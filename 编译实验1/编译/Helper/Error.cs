using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LLanguageComplier
{
    public class Error
    {
        public int Row { set; get; }
        public string ErrorSrc { set; get; }
        public string ErrorType { set; get; }
        public Error() { }
        public Error(int row, string errorSrc, string errorType)
        {
            this.Row = row;
            this.ErrorType = errorType;
            this.ErrorSrc = errorSrc;
        }
    }
}
