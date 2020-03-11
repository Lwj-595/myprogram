using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LLanguageComplier
{
    public class Grammar
    {
        List<Token> tokenList;
        List<symble> symbles;
        public string error = "";
        int i = 0;
        public Grammar(Morphology m)//传入词法分析产生的Token文件和符号表文件
        {
            tokenList = m.tokenList;
            symbles = m.symbles;
            Dispose();//执行主要函数
        }
        private void Next()//在token文件表中新建一项
        {
            if (i < tokenList.Count - 1)
            {
                i++;
            }
        }

        private void Before()//前一项
        {
            if (i > 0)
            {
                i--;
            }
        }

        #region 主要函数Dispose//〈程序〉→program〈标识符〉〈程序体〉
        private void Dispose()
        {
            if (tokenList[i].Code == 12)//含有program
            {
                Next();
                if (tokenList[i].Code == 18)//是标识符
                {

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

        #region 程序体ProBody //〈程序体〉→〈变量说明〉〈复合句〉//〈变量说明〉→ var〈变量定义〉|ε
        private void ProBody()
        {
            if (tokenList[i].Code == 16)//如果是var
            {
                Next();
                VarDef();//执行变量定义
            }
            else if (tokenList[i].Code == 2)//如果是begin
            {
                Next();
                ComSent();//执行复合句
            }
            else
            {
                error = "程序体缺少var或begin";
            }
        }
        #endregion
        //缺少var或begin, 报错
        #region 变量定义VarDef//〈变量定义〉→〈标识符表〉：〈类型〉；｜〈标识符表〉：〈类型〉；〈变量定义〉
        private void VarDef()
        {
            if (IsIdlist())//若该字符为标识符，则判断下一个字符，若为冒号，继续判断类型定义是否正确
            {
                Next();
                if (tokenList[i].Code == 29)//冒号
                {
                    Next();
                    if (tokenList[i].Code == 9 || tokenList[i].Code == 3 || tokenList[i].Code == 13)//类型为integer或bool或real
                    {
                        int j = i;
                        j = j - 2;
                        symbles[tokenList[j].Addr].Type = tokenList[i].Code;//类型定义正确，在符号表中记录该标识符的类型
                        j--;
                        while (tokenList[j].Code == 28)// 若标识符后面有逗号，表示同时定义了几个相同类型的变量，把它们都添加到符号表中
                        {
                            j--;
                            symbles[tokenList[j].Addr].Type = tokenList[i].Code;
                        }
                        Next();
                        if (tokenList[i].Code == 30)//如果是分号，判断下一个单词，若为begin，执行复合句；否则继续循环执行变量定义
                        {
                            Next();
                            if (tokenList[i].Code == 2)//含有begin
                            {
                                Next();
                                ComSent();//执行复合句
                            }
                            else
                            {
                                VarDef();//继续执行变量定义
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

        #region 判断是不是标识符表 IsIdlist//〈标识符表〉→〈标识符〉，〈标识符表〉｜〈标识符〉
        private bool IsIdlist()
        {
            if (tokenList[i].Code == 18)//标识符
            {//若是标识符，判断下一个字符，如果是逗号，继续判断下一个字符，如果不是逗号，指向前一个字符，返回true，否则返回false——此方法用来判断是否将几个变量定义为同一个类型
                Next();
                if (tokenList[i].Code == 28)//逗号
                {
                    Next();
                    return IsIdlist();//下一个字符若为逗号，则继续循环执行判断是否为标识符表
                }
                else
                {//指向前一个字符，为标识符，返回true
                    Before();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 复合句ComSent//〈复合句〉→ begin〈语句表〉end
        private void ComSent()
        {
            SentList();//执行语句表
            Next();
            if (error == "")
            {
                if (tokenList[i].Code == 30)  //end前必须有一个分号 ；
                {
                    Next();
                    if (tokenList[i].Code == 2 || tokenList[i].Code == 8 || tokenList[i].Code == 17)
                    {
                        StructSent();
                    }
                    else if (tokenList[i].Code == 6) //end
                    {
                        return;
                    }
                    else
                    {
                        error = "必须以end结尾";
                    }
                }
               // if (tokenList[i].Code == 30)// && tokenList[i+1].Code == 6)//end
               // {
               //     return;
               // }
               // else
               // {
                   // error = "复合句末尾缺少end";
               // }
            }
        }
        #endregion

        #region 语句表SentList//〈语句表〉→〈执行句〉；〈语句表〉｜〈执行句〉
        private void SentList()
        {
            ExecSent();//执行句
            if (error == "")
            {
                Next();
                if (tokenList[i].Code == 30)//若为分号，继续循环执行语句表
                {
                    Next();
                    SentList();
                }
                else
                {
                    Before();
                }
            }
        }
        #endregion

        #region 执行句ExecSent//〈执行句〉→〈简单句〉｜〈结构句〉//〈简单句〉→〈赋值句〉
        private void ExecSent()
        {
            if (tokenList[i].Code == 18)//标识符如果是标识符，为简单句
            {
                Next();
                AssiSent();//赋值句
            }
            else if (tokenList[i].Code == 2 || tokenList[i].Code == 8 || tokenList[i].Code == 17)//begin，if，句号的机内码
            {
                StructSent();//结构句
            }
            else
            {
                Before();//回退一个
            }
        }
        #endregion

        #region 赋值句AssiSent//〈赋值句〉→〈变量〉：＝〈表达式〉
        private void AssiSent()
        {
            if (tokenList[i].Code == 31)//:=
            {
                Next();
                Expression();//表达式
            }
            else
            {
                error = "赋值句变量后缺少：=";
            }
        }
        #endregion

        #region 表达式Expression//〈表达式〉→〈算术表达式〉｜〈布尔表达式〉
        private void Expression()
        {
            if (tokenList[i].Code == 7 || tokenList[i].Code == 15 || 
                (tokenList[i].Addr != -1 && symbles[tokenList[i].Addr].Type == 3))
                //false或true或单词为保留字且在符号表中的类型为bool型
            {
                BoolExp();//布尔表达式
            }
            else
            {
                AritExp();//算术表达式
            }
        }
        #endregion

        #region 布尔表达式BoolExp//〈布尔表达式〉→〈布尔表达式〉or〈布尔项〉｜〈布尔项〉
        private void BoolExp()
        {
            BoolItem();//布尔项
            if (error == "")
            {
                Next();
                if (tokenList[i].Code == 11)//or
                {
                    Next();
                    BoolExp();
                }
                else
                {
                    Before();
                }
            }
            else
            {
                return;
            }
        }
        #endregion

        #region 布尔项BoolItem//〈布尔项〉→〈布尔项〉and〈布尔因子〉｜〈布尔因子〉
        private void BoolItem()
        {
            BoolFactor();//布尔因子
            if (error == "")
            {
                Next();
                if (tokenList[i].Code == 1)//and
                {
                    Next();
                    BoolItem();//布尔项
                }
                else
                {
                    Before();
                }
            }
        }
        #endregion

        #region 布尔因子BoolFactor//〈布尔因子〉→ not〈布尔因子〉｜〈布尔量〉
        private void BoolFactor()
        {
            if (tokenList[i].Code == 10)//not
            {
                Next();
                BoolFactor();//布尔因子
            }
            else
            {
                BoolValue();//布尔量
            }
        }
        #endregion

        #region 布尔量BoolValue//〈布尔量〉→〈布尔常数〉｜〈标识符〉｜（〈布尔表达式〉）｜〈关系表达式〉
        private void BoolValue()
        {
            if (tokenList[i].Code == 15 || tokenList[i].Code == 7)//true或false
            {
                return;
            }
            else if (tokenList[i].Code == 18)//标识符（关系表达式）
            {
                Next();
                if (tokenList[i].Code == 34 || tokenList[i].Code == 33 || tokenList[i].Code == 32 ||
                    tokenList[i].Code == 37 || tokenList[i].Code == 36 || tokenList[i].Code == 35)
                {//==或>或<或<>或<=或>=
                    Next();
                    if (tokenList[i].Code == 18)//标识符
                    {
                    }
                    else
                    {
                        error = "关系运算符后缺少标识符";
                    }
                }
                else
                {
                    Before();
                }
            }
            else if (tokenList[i].Code == 21)//字符为（，即布尔表达式
            {
                BoolExp();//执行布尔表达式

                if (tokenList[i].Code == 22)//字符为）
                {
                    return;
                }
                else
                {
                    error = "布尔量中的布尔表达式缺少一个）";
                }
            }
            else
            {
                error = "布尔量出错";
            }
        }
        #endregion

        #region 算术表达式 AritExp//〈算术表达式〉→〈算术表达式〉＋〈项〉｜〈算术表达式〉－〈项〉｜〈项〉
        private void AritExp()
        {
            Item();//执行项
            if (error == "")
            {
                Next();
                if (tokenList[i].Code == 23 || tokenList[i].Code == 24)//符号为+或-
                {
                    Next();
                    AritExp();//执行算术表达式
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

        #region 项 Item//〈项〉→〈项〉＊〈因子〉｜〈项〉／〈因子〉｜〈因子〉
        private void Item()
        {
            Factor();//执行因子
            if (error == "")
            {
                Next();
                if (tokenList[i].Code == 25 || tokenList[i].Code == 26)//符号为*或/
                {
                    Next();
                    Item();//执行项
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

        #region 因子Factor//〈因子〉→〈算术量〉｜（〈算术表达式〉）
        private void Factor()
        {
            if (tokenList[i].Code == 21)//字符为（
            {
                Next();
                AritExp();//执行算术表达式
                Next();
                if (tokenList[i].Code == 22)//字符为)
                {
                    return;
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

        #region 算术量CalQua//〈算术量〉→〈标识符〉｜〈整数〉｜〈实数〉
        private void CalQua()
        {
            if (tokenList[i].Code == 18 || tokenList[i].Code == 19 || 
                tokenList[i].Code == 20)//标识符或整数或实数
            {
                return;
            }
            else
            {
                error = "算术量出错";
            }
        }
        #endregion

        #region 结构句 StructSent//〈结构句〉→〈复合句〉｜〈if句〉｜〈WHILE句〉
        private void StructSent()
        {
            if (tokenList[i].Code == 2)//begin
            {
                Next();
                ComSent();//执行复合句
            }
            else if (tokenList[i].Code == 8)//if
            {
                Next();
                IfSent();//执行if语句
            }
            else if (tokenList[i].Code == 17)//while
            {
                Next();
                WhileSent();//执行while语句
            }
        }
        #endregion

        #region if语句IfSent// 〈if句〉→if〈布尔表达式〉then〈执行句〉| if〈布尔表达式〉then〈执行句〉else〈执行句〉
        private void IfSent()
        {
            BoolExp();//布尔表达式
            if (error == "")
            {
                Next();
                if (tokenList[i].Code == 14)//then
                {
                    Next();
                    ExecSent();//执行句
                    Next();
                    if (tokenList[i].Code == 5)//else
                    {
                        Next();
                        ExecSent();//执行句
                    }
                    else
                    {
                        Before();
                        return;
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

        #region while语句 WhileSent//〈while句〉→while〈布尔表达式〉do〈执行句〉
        private void WhileSent()
        {
            BoolExp();//布尔表达式
            if (error == "")
            {
                Next();
                if (tokenList[i].Code == 4)//do
                {
                    Next();
                    ExecSent();//执行句
                }
                else
                {
                    error = "while语句缺少do";
                }
            }
        }
        #endregion
    }
}
