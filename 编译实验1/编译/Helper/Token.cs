using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LLanguageComplier
{
    public class Token //token文件结构
    {
        public int Label { set; get; } //单词序号
        public string Name { set; get; } //单词本身
        public int Code { set; get; } //单词种别编码
        public int Addr { set; get; } //单词在符号表中登记项的指针，仅用于标识符或常数，其他情况下为-1
        public Token()
        {
        }
    }
}
