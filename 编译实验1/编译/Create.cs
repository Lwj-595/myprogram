using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LLanguageComplier;

namespace 编译
{
    public class Create
    {
        List<FourPart> fps = new List<FourPart>();
        public List<symble> symbles = new List<symble>();
        public List<Assembly> assemblys = new List<Assembly>();
        string bx = "";
        string dx = "";
        bool ism = false;

        public Create(Semantic s)
        {
            fps = s.fps;
            symbles = s.symbles;
            Dispose();
            Back();
            Translate();
        }




        #region 从尾到头的回填待用信息
        private void Back()
        {
            int i = fps.Count - 2;
            for (; i == 0; i--)
            {
                if (!fps[i].Op.Contains("j"))
                {
                    string str = fps[i].StrLeft;
                    foreach (symble s in symbles)
                    {
                        if (s.Name == str)
                        {
                            s.Wait.Add(new string[2] { "" + i + 1 + "", "Y" });
                        }
                    }
                    if (fps[i].StrRight != "_")
                    {
                        str = fps[i].StrRight;
                        foreach (symble s in symbles)
                        {
                            if (s.Name == str)
                            {
                                s.Wait.Add(new string[2] { "" + i + 1 + "", "Y" });
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 划分基本块
        private void Dispose()
        {
            fps[1].Input = true;
            int i = 0;
            foreach (FourPart f in fps)
            {
                if (f.Op.Contains("j") && f.JumpNum != "-1")
                {
                    fps[Convert.ToInt32(f.JumpNum)].Input = true;
                    if (f.Op != "j")
                    {
                        fps[i + 1].Input = true;
                    }
                }
                i++;
            }
        }
        #endregion

        #region 寻找参数的value值
        private string GetValue(string str)
        {
            foreach (symble s in symbles)
            {
                if (s.Name == str)
                {
                    return s.Value;
                }
            }
            return "";
        }
        #endregion

        #region 寄存器分配策略
        private string GetReg(FourPart f)
        {
            if (bx == "" || GetValue(f.StrLeft) == "bx")
            {
                bx = f.StrLeft;
                foreach (symble s in symbles)
                {
                    if (s.Name == f.StrLeft)
                    {
                        s.Value = "bx";
                    }
                }
                return "bx";
            }
            else
            {
                if (dx == "" || GetValue(f.StrLeft) == "dx")
                {
                    dx = f.StrLeft;
                    foreach (symble s in symbles)
                    {
                        if (s.Name == f.StrLeft)
                        {
                            s.Value = "dx";
                        }
                    }
                    return "dx";
                }
                else
                {
                    ism = true;
                    int bxNum = 0;
                    int dxNum = 0;
                    foreach (symble s in symbles)
                    {
                        if (s.Name == bx)
                        {
                            bxNum = Convert.ToInt32(s.Wait.Last()[0]);
                        }
                        if (s.Name == dx)
                        {
                            dxNum = Convert.ToInt32(s.Wait.Last()[0]);
                        }
                    }
                    if (bxNum > dxNum)
                    {
                        dx = f.StrLeft;
                        foreach (symble s in symbles)
                        {
                            if (s.Name == f.StrLeft)
                            {
                                s.Value = "dx";
                            }
                        }
                        return "dx";
                    }
                    else
                    {
                        bx = f.StrLeft;
                        foreach (symble s in symbles)
                        {
                            if (s.Name == f.StrLeft)
                            {
                                s.Value = "bx";
                            }
                        }
                        return "bx";
                    }
                }
            }
        }
        #endregion

        #region 产生目标代码
        private void Translate()
        {
            int i = 0;
            foreach (FourPart f in fps)
            {
                if (f.Op == "j")
                {
                    Assembly a = new Assembly("JMP", "L" + f.JumpNum, "");
                    a.Num = i.ToString();
                    assemblys.Add(a);
                }
                else if (f.Op == "j>")
                {
                    string s = GetReg(f);
                    Assembly a1 = new Assembly("MOV", s, f.StrLeft);
                    a1.Num = i.ToString();
                    Assembly a2 = new Assembly("CMP", s, f.StrRight);
                    Assembly a3 = new Assembly("JG", "L" + f.JumpNum, "");
                    assemblys.Add(a1);
                    assemblys.Add(a2);
                    assemblys.Add(a3);
                    foreach (symble sym in symbles)
                    {
                        if (sym.Name == f.StrLeft)
                        {
                            if (sym.Wait.Count > 1)
                            {
                                sym.Wait.RemoveAt(sym.Wait.Count - 1);
                            }
                        }
                        if (sym.Name == f.StrRight)
                        {
                            if (sym.Wait.Count > 1)
                            {
                                sym.Wait.RemoveAt(sym.Wait.Count - 1);
                            }
                        }
                    }
                }
                else if (f.Op == "j<")
                {
                    string s = GetReg(f);
                    Assembly a1 = new Assembly("MOV", s, f.StrLeft);
                    a1.Num = i.ToString();
                    Assembly a2 = new Assembly("CMP", s, f.StrRight);
                    Assembly a3 = new Assembly("JL", "L" + f.JumpNum, "");
                    assemblys.Add(a1);
                    assemblys.Add(a2);
                    assemblys.Add(a3);
                    foreach (symble sym in symbles)
                    {
                        if (sym.Name == f.StrLeft)
                        {
                            if (sym.Wait.Count > 1)
                            {
                                sym.Wait.RemoveAt(sym.Wait.Count - 1);
                            }
                        }
                        if (sym.Name == f.StrRight)
                        {
                            if (sym.Wait.Count > 1)
                            {
                                sym.Wait.RemoveAt(sym.Wait.Count - 1);
                            }
                        }
                    }
                }
                else if (f.Op == "j=")
                {
                    string s = GetReg(f);
                    Assembly a1 = new Assembly("MOV", s, f.StrLeft);
                    a1.Num = i.ToString();
                    Assembly a2 = new Assembly("CMP", s, f.StrRight);
                    Assembly a3 = new Assembly("JZ", "L" + f.JumpNum, "");
                    assemblys.Add(a1);
                    assemblys.Add(a2);
                    assemblys.Add(a3);
                    foreach (symble sym in symbles)
                    {
                        if (sym.Name == f.StrLeft)
                        {
                            if (sym.Wait.Count > 1)
                            {
                                sym.Wait.RemoveAt(sym.Wait.Count - 1);
                            }
                        }
                        if (sym.Name == f.StrRight)
                        {
                            if (sym.Wait.Count > 1)
                            {
                                sym.Wait.RemoveAt(sym.Wait.Count - 1);
                            }
                        }
                    }
                }
                else if (f.Op == "j<>")
                {
                    string s = GetReg(f);
                    Assembly a1 = new Assembly("MOV", s, f.StrLeft);
                    a1.Num = i.ToString();
                    Assembly a2 = new Assembly("CMP", s, f.StrRight);
                    Assembly a3 = new Assembly("JNZ", "L" + f.JumpNum, "");
                    assemblys.Add(a1);
                    assemblys.Add(a2);
                    assemblys.Add(a3);
                    foreach (symble sym in symbles)
                    {
                        if (sym.Name == f.StrLeft)
                        {
                            if (sym.Wait.Count > 1)
                            {
                                sym.Wait.RemoveAt(sym.Wait.Count - 1);
                            }
                        }
                        if (sym.Name == f.StrRight)
                        {
                            if (sym.Wait.Count > 1)
                            {
                                sym.Wait.RemoveAt(sym.Wait.Count - 1);
                            }
                        }
                    }
                }
                else if (f.Op == "j<=")
                {
                    string s = GetReg(f);
                    Assembly a1 = new Assembly("MOV", s, f.StrLeft);
                    a1.Num = i.ToString();
                    Assembly a2 = new Assembly("CMP", s, f.StrRight);
                    Assembly a3 = new Assembly("JLE", "L" + f.JumpNum, "");
                    assemblys.Add(a1);
                    assemblys.Add(a2);
                    assemblys.Add(a3);
                    foreach (symble sym in symbles)
                    {
                        if (sym.Name == f.StrLeft)
                        {
                            if (sym.Wait.Count > 1)
                            {
                                sym.Wait.RemoveAt(sym.Wait.Count - 1);
                            }
                        }
                        if (sym.Name == f.StrRight)
                        {
                            if (sym.Wait.Count > 1)
                            {
                                sym.Wait.RemoveAt(sym.Wait.Count - 1);
                            }
                        }
                    }
                }
                else if (f.Op == "j>=")
                {
                    string s = GetReg(f);
                    Assembly a1 = new Assembly("MOV", s, f.StrLeft);
                    a1.Num = i.ToString();
                    Assembly a2 = new Assembly("CMP", s, f.StrRight);
                    Assembly a3 = new Assembly("JGE", "L" + f.JumpNum, "");
                    assemblys.Add(a1);
                    assemblys.Add(a2);
                    assemblys.Add(a3);
                    foreach (symble sym in symbles)
                    {
                        if (sym.Name == f.StrLeft)
                        {
                            if (sym.Wait.Count > 1)
                            {
                                sym.Wait.RemoveAt(sym.Wait.Count - 1);
                            }
                        }
                        if (sym.Name == f.StrRight)
                        {
                            if (sym.Wait.Count > 1)
                            {
                                sym.Wait.RemoveAt(sym.Wait.Count - 1);
                            }
                        }
                    }
                }
                else if (f.Op == "+")
                {
                    string s = GetReg(f);
                    Assembly a1 = new Assembly("MOV", s, f.StrLeft);
                    a1.Num = i.ToString();
                    Assembly a2 = new Assembly("ADD", s, f.StrRight);
                    assemblys.Add(a1);
                    assemblys.Add(a2);
                    foreach (symble sym in symbles)
                    {
                        if (sym.Name == f.StrLeft)
                        {
                            if (sym.Wait.Count > 1)
                            {
                                sym.Wait.RemoveAt(sym.Wait.Count - 1);
                            }
                        }
                        if (sym.Name == f.StrRight)
                        {
                            if (sym.Wait.Count > 1)
                            {
                                sym.Wait.RemoveAt(sym.Wait.Count - 1);
                            }
                        }
                    }
                }
                else if (f.Op == "-")
                {
                    string s = GetReg(f);
                    Assembly a1 = new Assembly("MOV", s, f.StrLeft);
                    a1.Num = i.ToString();
                    Assembly a2 = new Assembly("SUB", s, f.StrRight);
                    assemblys.Add(a1);
                    assemblys.Add(a2);
                    foreach (symble sym in symbles)
                    {
                        if (sym.Name == f.StrLeft)
                        {
                            if (sym.Wait.Count > 1)
                            {
                                sym.Wait.RemoveAt(sym.Wait.Count - 1);
                            }
                        }
                        if (sym.Name == f.StrRight)
                        {
                            if (sym.Wait.Count > 1)
                            {
                                sym.Wait.RemoveAt(sym.Wait.Count - 1);
                            }
                        }
                    }
                }
                else if (f.Op == "*")
                {
                    string s = GetReg(f);
                    Assembly a1 = new Assembly("MOV", s, f.StrLeft);
                    a1.Num = i.ToString();
                    Assembly a2 = new Assembly("MUL", s, f.StrRight);
                    assemblys.Add(a1);
                    assemblys.Add(a2);
                    foreach (symble sym in symbles)
                    {
                        if (sym.Name == f.StrLeft)
                        {
                            if (sym.Wait.Count > 1)
                            {
                                sym.Wait.RemoveAt(sym.Wait.Count - 1);
                            }
                        }
                        if (sym.Name == f.StrRight)
                        {
                            if (sym.Wait.Count > 1)
                            {
                                sym.Wait.RemoveAt(sym.Wait.Count - 1);
                            }
                        }
                    }
                }
                else if (f.Op == "/")
                {
                    string s = GetReg(f);
                    Assembly a1 = new Assembly("MOV", s, f.StrLeft);
                    a1.Num = i.ToString();
                    Assembly a2 = new Assembly("DIV", s, f.StrRight);
                    assemblys.Add(a1);
                    assemblys.Add(a2);
                    foreach (symble sym in symbles)
                    {
                        if (sym.Name == f.StrLeft)
                        {
                            if (sym.Wait.Count > 1)
                            {
                                sym.Wait.RemoveAt(sym.Wait.Count - 1);
                            }
                        }
                        if (sym.Name == f.StrRight)
                        {
                            if (sym.Wait.Count > 1)
                            {
                                sym.Wait.RemoveAt(sym.Wait.Count - 1);
                            }
                        }
                    }
                }
                else if (f.Op == ":=")
                {
                    string s = GetReg(f);
                    Assembly a1 = new Assembly("MOV", s, f.StrLeft);
                    a1.Num = i.ToString();
                    assemblys.Add(a1);
                    foreach (symble sym in symbles)
                    {
                        if (sym.Name == f.StrLeft)
                        {
                            if (sym.Wait.Count > 1)
                            {
                                sym.Wait.RemoveAt(sym.Wait.Count - 1);
                            }
                        }
                    }
                }
                else
                {
                }
                i++;
            }
        }
        #endregion
    }
}
