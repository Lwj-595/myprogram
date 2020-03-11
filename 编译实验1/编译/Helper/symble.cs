using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LLanguageComplier
{
    public class symble //符号表文件结构
    {
        public int Number { set; get; } //序号
        public int Type { set; get; } //类型
        public string Name { set; get; } //名字
        public string Value { set; get; } //值
        public List<string[]> Wait { set; get; } 
        public symble() //初始化
        {
            Value = "";
            Wait = new List<string[]>();
            Wait.Add(new string[2] { "0", "N" });
        }
    }
}
