using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LLanguageComplier
{
    public class Semantic
    {
        public List<FourPart> fps = new List<FourPart>();
        public List<E> es = new List<E>();
        List<Token> tokenList;
        public List<symble> symbles = new List<symble>();
        public string error = "";
        int i = 0;
        int ti = 1;
        string tt = "";

        public Semantic(Morphology m)//将词法分析产生的token文件和符号表文件继续进行语义分析
        {
            tokenList = m.tokenList;
            symbles = m.symbles;
            Dispose();//执行主要函数
        }

        private void Next()
        {
            if (i < tokenList.Count - 1)
            {
                i++;
            }
        }

        private void Before()
        {
            if (i > 0)
            {
                i--;
            }
        }

        #region 创建临时变量NewTemp()
        private string NewTemp()
        {
            string temp = "T" + ti.ToString();
            ti++;
            symble s = new symble();
            s.Name = temp;
            symbles.Add(s);
            return temp;
        }
        #endregion

        #region 回填函数BackPatch(int addr, int addr2)//完成四元式转移目标的回填
        private void BackPatch(int addr, int addr2)
        {
            fps[addr].JumpNum = addr2.ToString();//把链首addr所链接的每个四元式的第四分量都改写为地址addr2
        }
        #endregion

        #region 产生四元式Emit(string op, string strLeft, string strRight, string jumpNum)
        private void Emit(string op, string strLeft, string strRight, string jumpNum)
        {
            FourPart fp = new FourPart(op, strLeft, strRight, jumpNum);
            fps.Add(fp);//将新生成的四元式表项fp添加到四元式列表fps中
        }
        #endregion

        #region 主要函数Dispose()//〈程序〉→ program〈标识符〉〈程序体〉
        private void Dispose()
        {
            if (tokenList[i].Code == 12)//如果是program，判断下一个单词
            {
                Next();
                if (tokenList[i].Code == 18)//如果是标识符，生成四元式
                {
                    Emit("program", tokenList[i].Name, "_", "_");
                    Next();
                    ProBody();//执行程序体
                }
                else
                {
                    error = "该程序program缺少方法名";
                }
            }
            else
            {
                error = "该程序缺少关键字：program";
            }
        }
        #endregion

        #region 程序体ProBody()//〈程序体〉→〈变量说明〉〈复合句〉//〈变量说明〉→ var〈变量定义〉|ε
        private void ProBody()
        {
            if (tokenList[i].Code == 16)//如果是var，指向下一个单词
            {
                Next();
                VarDef();//执行变量定义
            }
            else if (tokenList[i].Code == 2)//如果是begin，指向下一个单词
            {
                Next();
                ComSent();//执行复合句
            }
            else//否则报错
            {
                error = "程序体缺少var或begin";
            }
        }
        #endregion

        #region 变量定义VarDef()//〈变量定义〉→〈标识符表〉：〈类型〉；｜〈标识符表〉：〈类型〉；〈变量定义〉
        private void VarDef()
        {
            if (IsIdlist())//若该字符为标识符，则判断下一个字符，若为冒号，继续判断类型定义是否正确
            {
                Next();
                if (tokenList[i].Code == 29) //冒号
                {
                    Next();
                    if (tokenList[i].Code == 9 || tokenList[i].Code == 3 || tokenList[i].Code == 13) //类型为integer、bool、real
                    {
                        int j = i;
                        j = j - 2;
                        symbles[tokenList[j].Addr].Type = tokenList[i].Code;//类型定义正确，在符号表中记录该标识符的类型
                        j--;
                        while (tokenList[j].Code == 28) //若标识符后面有逗号，表示同时定义了几个相同类型的变量，把它们都添加到符号表中
                        {
                            j--;
                            symbles[tokenList[j].Addr].Type = tokenList[i].Code;
                        }
                        Next();
                        if (tokenList[i].Code == 30) //如果是分号，判断下一个单词，若为begin，执行复合句；否则继续循环执行变量定义
                        {
                            Next();
                            if (tokenList[i].Code == 2) // begin
                            {
                                Next();
                                ComSent();//执行复合句
                            }
                            else
                            {
                                VarDef();//继续循环执行
                            }
                        }
                        else
                        {
                            error = "变量定义后面缺少；";
                        }
                    }
                    else
                    {
                        error = "变量定义缺少类型或类型定义错误";
                        return;
                    }
                }
                else
                {
                    error = "var后面缺少冒号";
                }
            }
            else
            {
                error = "变量定义标识符出错";
            }
        }
        #endregion

        #region 判断是不是标识符表IsIdlist()//〈标识符表〉→〈标识符〉，〈标识符表〉｜〈标识符〉//是标识符表返回true
        private bool IsIdlist()
        {
            if (tokenList[i].Code == 18)//标识符
            {
                Next();
                if (tokenList[i].Code == 28)//逗号
                {
                    Next();
                    return IsIdlist();//下一个字符若为逗号，则继续循环执行判断是否为标识符表
                }
                else
                {
                    Before();//指向前一个字符，为标识符，返回true
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 复合句ComSent()//〈复合句〉→ begin〈语句表〉end
        private void ComSent()
        {
            SentList();//执行语句表
            if (error == "")
            {
                if (tokenList[i].Code == 30 && tokenList[i+1].Code == 6) //含有end
                {
                    Emit("sys", "_", "_", "_");//生成四元式
                }
                else
                {
                    error = "复合句末尾缺少end";
                }
            }
        }
        #endregion

        #region 语句表SentList()//〈语句表〉→〈执行句〉；〈语句表〉｜〈执行句〉
        private void SentList()
        {
            S s = new S();
            ExecSent(ref s);//执行执行句，ref传址引用
            if (error == "")
            {
                Next();
                if (tokenList[i].Code == 30) //若为分号，继续循环执行语句表
                {
                    Next();
                    SentList();
                    //ExecSent(ref s);
                }
            }
        }
        #endregion

        #region 执行句ExecSent(ref S s)//〈执行句〉→〈简单句〉｜〈结构句〉//〈简单句〉→〈赋值句〉
        private void ExecSent(ref S s)
        {
            if (tokenList[i].Code == 18)//如果是标识符，为简单句
            {
                Next();
                AssiSent();//执行赋值句
            }
            else if (tokenList[i].Code == 2 || tokenList[i].Code == 8 || tokenList[i].Code == 17)//如果是begin或while或if
            {
                StructSent(ref s);//执行结构句
            }
            else//否则返回到前一个字符
            {
                Before();
            }
        }
        #endregion

        #region 赋值句AssiSent()//〈赋值句〉→〈变量〉：＝〈表达式〉
        private void AssiSent()
        {
            if (tokenList[i].Code == 31)//如果为:=
            {
                string temp = tokenList[i - 1].Name;//temp记录上一个token文件项的名字
                Next();
                Expression();//执行表达式
                Emit(":=", tt, "_", temp);//生成四元式，即temp：=tt
            }
            else
            {
                error = "赋值句变量后缺少：=";
            }
        }
        #endregion


        #region 表达式Expression()//〈表达式〉→〈算术表达式〉｜〈布尔表达式〉
        private void Expression()
        {
            if (tokenList[i].Code == 7 || tokenList[i].Code == 15 || (tokenList[i].Addr != -1 && symbles[tokenList[i].Addr].Type == 3))
            {//如果是false或true或单词为保留字且在符号表中的类型为bool型
                E e = new E();
                BoolExp(ref e);//执行布尔表达式
            }
            else
            {
                AritExp();//执行算术表达式
            }
        }
        #endregion

        /*优先级关系为not>and>or,在方法布尔表达式（or）、布尔项（and）、布尔因子（not）中有所体现*/
        #region 布尔表达式BoolExp(ref E e)//〈布尔表达式〉→〈布尔表达式〉or〈布尔项〉｜〈布尔项〉
        private void BoolExp(ref E e)
        {
            E e1 = new E();
            BoolItem(ref e1);//执行布尔项
            if (error == "")
            {
                Next();
                if (tokenList[i].Code == 11)//如果为or
                {
                    int m = fps.Count;//m记录四元式表项的数量值，即地址M.quad
                    E e2 = new E();
                    Next();
                    BoolExp(ref e2);//执行布尔表达式
                    e.t.Concat(e1.t);//Concat连接两个序列e1.t和e2.t,即e.t={e1.t,e2.t}
                    e.t.Concat(e2.t);
                    e.f = e2.f;//即e.f={e2.f}
                    foreach (int k in e.f)//foreach是一个迭代器，从int型数组k中循环读取数据，并将每次循环到的值赋值给e.f
                    {
                        BackPatch(k, m);//执行回填函数,把链首k所链接的每个四元式的第四分量都改写为地址m;即k=e1.f,m=M.q
                    }
                }
                else//如果不是or，则e.t=e1.t;e.f=e1.f
                {
                    e = e1;
                    Before();
                }
            }
            else
            {
                e = e1;
            }
        }
        #endregion

        #region 布尔项BoolItem(ref E e)//〈布尔项〉→〈布尔项〉and〈布尔因子〉｜〈布尔因子〉
        private void BoolItem(ref E e)
        {
            E e1 = new E();
            BoolFactor(ref e1);//执行布尔因子
            if (error == "")
            {
                Next();
                if (tokenList[i].Code == 1)//如果是and
                {
                    Next();
                    int m = fps.Count;
                    E e2 = new E();
                    BoolItem(ref e2);//执行布尔项
                    e.t = e2.t; //即e.t=e2.t
                    e.f.Concat(e1.f);//即e.f={e1.f,e2.f}
                    e.f.Concat(e2.f);
                    foreach (int k in e.t)
                    {
                        BackPatch(k, m);//执行回填函数
                    }
                }
                else//如果不是and,则e.t=e1.t,e.f=e1.f
                {
                    e = e1;
                    Before();
                }
            }
        }
        #endregion

        #region 布尔因子BoolFactor(ref E e)// 〈布尔因子〉→not〈布尔因子〉｜〈布尔量〉
        private void BoolFactor(ref E e)
        {
            if (tokenList[i].Code == 10)//如果是not
            {
                Next();
                E e1 = new E();
                BoolFactor(ref e1);//执行布尔因子
                e.t = e1.f;//即e.t=e1.t;e.f=e1.f
                e.f = e1.t;
            }
            else//不是not
            {
                E e1 = new E();
                BoolValue(ref e1);//执行布尔量
                e = e1;
            }
        }
        #endregion

        #region 布尔量BoolValue(ref E e)//〈布尔量〉→〈布尔常数〉｜〈标识符〉｜（〈布尔表达式〉）｜〈关系表达式〉
        private void BoolValue(ref E e)
        {
            if (tokenList[i].Code == 15 || tokenList[i].Code == 7)//如果是true或false，则e.t=fps.Count，e.f=fps.Count+1
            {//fps.Count指向下一条将要产生但尚未产生的四元式的编号，其初值为1，每执行一次emit过程，fps.Count将会自动加1
                e.t.Add(fps.Count);
                e.f.Add(fps.Count + 1);
                tt = tokenList[i].Name;//tt记录名字
            }
            else if (tokenList[i].Code == 18)//如果是标识符
            {
                Next();
                if (tokenList[i].Code == 34 || tokenList[i].Code == 33 || tokenList[i].Code == 32 || tokenList[i].Code == 37 || tokenList[i].Code == 36 || tokenList[i].Code == 35)
                {//如果为<、<=、=、>=、>、<>，即关系表达式
                    Next();
                    if (tokenList[i].Code == 18)//如果是标识符,则e.t=fps.Count，e.f=fps.Count+1
                    {
                        e.t.Add(fps.Count);
                        e.f.Add(fps.Count + 1);
                        Emit("j" + tokenList[i - 1].Name, tokenList[i - 2].Name, 
                            tokenList[i].Name, "0");//生成四元式，即a<b的四元式为(j<,a,b,0)
                        Emit("j", "_", "_", "0");
                    }
                    else
                    {
                        Before();
                    }
                }
                else//只有一个标识符，说明该标识符为布尔型，则e.t=fps.Count，e.f=fps.Count+1
                {
                    Before();
                    e.t.Add(fps.Count);
                    e.f.Add(fps.Count + 1);
                    Emit("jnz", tokenList[i].Name, "_", "0");//生成四元式,即E—>a的四元式为(jnz,a,_,0)
                    Emit("j", "_", "_", "0");
                    Next();
                }

            }
            else if (tokenList[i].Code == 21)//如果为（
            {
                E e1 = new E();//定义E—>(E1)
                BoolExp(ref e1);//执行布尔表达式
                e.t = e1.t;
                e.f = e1.f;
                if (tokenList[i].Code == 22)//如果为），返回
                {
                    return;
                }
            }
        }
        #endregion

        /*运算符优先级关系为()>*或\>+或-,在方法算术表达式（+或-）、项（*或\）、因子（()）中有所体现*/
        #region 算术表达式AritExp()//〈算术表达式〉→〈算术表达式〉＋〈项〉｜〈算术表达式〉－〈项〉｜〈项〉
        private void AritExp()
        {
            Item();//执行项
            if (error == "")
            {
                Next();
                if (tokenList[i].Code == 23 || tokenList[i].Code == 24)//如果为+或-
                {
                    string[] temp = { tokenList[i - 1].Name, tokenList[i].Name };//temp记录运算符和它前面的变量名字
                    if (tokenList[i - 1].Code == 22)//符号为）
                    {
                        temp[0] = tt;
                    }
                    Next();
                    AritExp();//执行算术表达式
                    Emit(temp[1], temp[0], tt, NewTemp());//生成四元式，即x:=y+z的四元式为(+,y,z,T1)
                    tt = "T" + (ti - 1).ToString();
                }
                else
                {
                    Before();
                    return;
                }
            }
            else
            {
                return;
            }
        }
        #endregion

        #region 项Item()//〈项〉→〈项〉＊〈因子〉｜〈项〉／〈因子〉｜〈因子〉
        private void Item()
        {
            Factor();//执行因子
            if (error == "")
            {
                Next();
                if (tokenList[i].Code == 25 || tokenList[i].Code == 26)//如果为*或/
                {
                    string[] temp = { tokenList[i - 1].Name, tokenList[i].Name };//temp记录运算符和它前面的变量名字
                    if (tokenList[i - 1].Code == 22)//token文件的上一表项为）
                    {
                        temp[0] = tt;
                    }
                    Next();
                    Item();//执行项
                    Emit(temp[1], temp[0], tt, NewTemp());//生成四元式，即x:=y*z的四元式为(*,y,z,T1)
                    tt = "T" + (ti - 1).ToString();
                }
                else
                {
                    Before();
                }
            }
            else
            {
            }
        }
        #endregion

        #region 因子Factor()//〈因子〉→〈算术量〉｜（〈算术表达式〉）
        private void Factor()
        {
            if (tokenList[i].Code == 21)//如果为（
            {
                Next();
                AritExp();//执行算数表达式
                Next();
                if (tokenList[i].Code == 22)//如果为）
                {
                }
                else
                {
                    error = "因子中算数表达式缺少）";
                }
            }
            else
            {
                CalQua();//执行算术量
            }
        }
        #endregion

        #region 算术量CalQua()//〈算术量〉→〈标识符〉｜〈整数〉｜〈实数〉
        private void CalQua()
        {
            if (tokenList[i].Code == 18 || tokenList[i].Code == 19 || 
                tokenList[i].Code == 20)//标识符、整数、实数
            {
                tt = tokenList[i].Name;//记录变量名
            }
            else
            {
                error = "算术量出错";
            }
        }
        #endregion

        #region 结构句StructSent(ref S s)//〈结构句〉→〈复合句〉｜〈if句〉｜〈WHILE句〉
        private void StructSent(ref S s)
        {
            if (tokenList[i].Code == 2)//如果是begin
            {
                Next();
                ComSent();//执行复合句
            }
            else if (tokenList[i].Code == 8)//if
            {
                Next();
                IfSent(ref s);//执行if语句
            }
            else if (tokenList[i].Code == 17)//while
            {
                Next();
                WhileSent(ref s);//执行whlie语句
            }
        }
        #endregion

        #region if语句IfSent(ref S s) //〈if句〉→if〈布尔表达式〉then〈执行句〉| if〈布尔表达式〉then〈执行句〉else〈执行句〉
        private void IfSent(ref S s)
        {
            E e = new E();
            BoolExp(ref e);//执行布尔表达式
            if (error == "")
            {
                Next();
                if (tokenList[i].Code == 14)//then
                {
                    int m1 = fps.Count;
                    S s1 = new S();
                    Next();
                    ExecSent(ref s1);//执行执行句
                    Next();
                    Next();
                    if (tokenList[i].Code == 5)//如果是else，即控制语句产生式为S—>if E then M1S1N else M2S2，则s.next={s1.next,n.next,s2.next},并回填e.t和e.f
                    {
                        S n = new S();//若N—>ε,n.next=fps.Count,并生成四元式(j,_,_,0)
                        n.next.Add(fps.Count);
                        Emit("j", "_", "_", "0");
                        S s2 = new S();
                        int m2 = fps.Count;
                        Next();
                        ExecSent(ref s2);//执行执行句
                        s.next = s1.next;
                        s.next.Concat(n.next);
                        s.next.Concat(s2.next);

                        foreach (int k in e.t)
                        {
                            BackPatch(k, m1);//执行回填函数
                        }
                        foreach (int k in e.f)
                        {
                            BackPatch(k, m2);
                        }
                    }
                    else//即控制语句产生式为S—>if E then MS1,则s.next={e.f,s1.next},并回填e.t
                    {
                        s.next = e.f;
                        s.next.Concat(s1.next);
                        foreach (int k in e.t)
                        {
                            BackPatch(k, m1);
                        }
                        Before();
                    }
                }
                else
                {
                    error = "if...then语句缺少then";
                }
            }
            else
            {
                error = "if语句布尔表达式出错";
            }
        }
        #endregion

        #region while语句WhileSent(ref S s)//〈while句〉→while〈布尔表达式〉do〈执行句〉
        private void WhileSent(ref S s)
        {
            int m1 = fps.Count;
            E e = new E();
            BoolExp(ref e);//执行布尔表达式
            Next();
            if (tokenList[i].Code == 4)//如果是do，即控制语句产生式为S—>while M1E do M2S1,则s.next=e.f,生成四元式(j,_,_,m1.q),并回填e.t和s1.next
            {
                int m2 = fps.Count;
                S s1 = new S();
                Next();
                ExecSent(ref s1);//执行执行句
                s.next = e.f;
                Emit("j", "_", "_", m1.ToString());//生成四元式
                foreach (int k in e.t)
                {
                    BackPatch(k, m2);
                }
                foreach (int k in s1.next)
                {
                    BackPatch(k, m1);
                }
            }

        }
        #endregion




    }
}
