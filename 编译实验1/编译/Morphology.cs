using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LLanguageComplier
{
    public class Morphology
    {
      public  string[] machineCodes = new string[38];//定义一个数组存放机内码
        public List<Token> tokenList = new List<Token>();
        public List<symble> symbles = new List<symble>();
        public List<Error> errors = new List<Error>();
        string input;
        int i = 0;
        int rowNum = 1;//行值

        public Morphology(string s)//传入字符串
        {
            input = s + " ";
            NewKeyWord();//定义机内码
            Dispose();//主程序处理输入的代码
        }

        #region 定义机内码NewKeyWord()
        private void NewKeyWord()
        {
            machineCodes[0] = "";
            machineCodes[1] = "and";
            machineCodes[2] = "begin";
            machineCodes[3] = "bool";
            machineCodes[4] = "do";
            machineCodes[5] = "else";
            machineCodes[6] = "end";
            machineCodes[7] = "false";
            machineCodes[8] = "if";
            machineCodes[9] = "integer";
            machineCodes[10] = "not";
            machineCodes[11] = "or";
            machineCodes[12] = "program";
            machineCodes[13] = "real";
            machineCodes[14] = "then";
            machineCodes[15] = "true";
            machineCodes[16] = "var";
            machineCodes[17] = "while";
            machineCodes[18] = "标识符";
            machineCodes[19] = "整数";
            machineCodes[20] = "实数";
            machineCodes[21] = "(";
            machineCodes[22] = ")";
            machineCodes[23] = "+";
            machineCodes[24] = "-";
            machineCodes[25] = "*";
            machineCodes[26] = "/";
            machineCodes[27] = ".";
            machineCodes[28] = ",";
            machineCodes[29] = ":";
            machineCodes[30] = ";";
            machineCodes[31] = ":=";
            machineCodes[32] = "=";
            machineCodes[33] = "<=";
            machineCodes[34] = "<";
            machineCodes[35] = "<>";
            machineCodes[36] = ">";
            machineCodes[37] = ">=";
        }
        #endregion

        #region 主程序处理输入的代码Dispose()
        public void Dispose()
        {
            while (i < input.Length)//只要不超出字符串长度就继续执行
            {
                if (IsAlpha(input[i]))
                {
                    RecogId();//如果是字母，判断是标识符还是关键字，并处理
                }
                else if (IsDight(input[i]))
                {
                    RecogCons();//如果是数字，判断是否是常数并处理
                }
                else if (input[i] == '/')//如果是/，判断下一个字符
                {
                    i++;
                    if (input[i] == '/')//如果下一个字符是/，说明是注释，一直往后到下一行
                    {
                        while (input[i] != '\r' && i < input.Length)
                        {
                            i++;
                        }
                    }
                    else//如果是除号，指向前一个字符
                    {
                        i--;
                    }
                }
                else if (input[i] == '\r' && input[i + 1] == '\n')//如果字符为回车且下一字符为换行时，行数加一，字符加2
                {
                    rowNum++;
                    i++;
                    i++;
                }
                else if (IsDelimiter(input[i]))//如果为其他界符
                {
                    RecogSym();//判断是否为符号并处理
                }
                else if (input[i] == ' ')//如果是空格，判断下一个字符
                {
                    i++;
                }
                else
                {
                    Error e = new Error(rowNum, input[i].ToString(), "(1)非法字符");//否则报错
                    errors.Add(e);//把产生的错误添加到错误列表中，继续判断下一个字符
                    i++;
                }

            }
        }
        #endregion

        #region 判断是标识符还是关键字并处理RecogId()//〈标识符〉→〈字母〉｜〈标识符〉〈字母〉｜〈标识符〉〈数字〉
        private void RecogId()
        {
            string str = "";
            int code;//标志位，记录是标识符还是关键字
            do
            {
                str = str + input[i];
                i++;
            } while (IsAlpha(input[i]) || IsDight(input[i]));//判断是不是字母或数字，是的话继续执行
            code = Reserve(str);//匹配关键字机内码
            Token token = new Token();//新建一个token表项
            token.Label = tokenList.Count;
            token.Name = str;
            if (code == 0)//如果为标识符，将属性值添加到token表项中
            {
                token.Code = 18;
                token.Addr = symbles.Count;
                symble s = new symble();//新建一个符号表表项，记录各个属性
                s.Number = token.Addr;
                s.Name = str;
                s.Type = 18;
                symbles.Add(s);
            }
            else//如果为关键字
            {
                token.Code = code;
                token.Addr = -1;//关键字在token文件中的地址为-1
            }
            tokenList.Add(token);
        }
        #endregion

        #region 判断是不是常数并处理RecogCons()
        private void RecogCons()
        {//看流程图
            string str = input[i].ToString();
            bool flag = true;//设置两个标志位，flag记录是否发生错误，point记录为实数（true）还是整数（flase）
            bool point = true;
            while (flag)
            {
                i++;
                if (IsDight(input[i]))
                {
                    str += input[i];
                }
                else if (input[i] == '.')
                {
                    if (point)
                    {
                        str += input[i];
                        point = false;//如果有小数点，说明是实数，point置为false
                    }
                    else//若小数点后还有小数点，则发生错误，flag置为flase
                    {
                        Error e = new Error(rowNum, str, "出现第二个'.'号");
                        errors.Add(e);
                        while (input[i] != ';')//处理错误，将第二个小数点之后的数字去掉，只显示第二个小数点之前的数字，即3.14.15转换为3.14
                        {
                            i++;
                        }
                        flag = false;
                    }
                }
                else if (IsAlpha(input[i]))//如果是字母
                {
                    i++;
                    if (IsDight(input[i]))//下一个字符为数字
                    {
                        i--;
                        i--;
                        if (point)//如果是整数，发生错误为出现数字开头的数字、字母串
                        {
                            Error e = new Error(rowNum, str, "数字开头的数字、字母串");
                            errors.Add(e);
                            while (input[i] != ';')//处理错误，将第一个出现的字母之后的数字或字母串去掉，只显示第一个字母之前的数字，即3a56转换为3
                            {
                                i++;
                            }
                            i--;
                        }
                        else//如果是实数，发生错误为实数的小数部分出现字母
                        {
                            Error e = new Error(rowNum, str, "实数的小数部分出现字母");
                            errors.Add(e);
                            while (input[i] != ';')//处理错误，将第一个出现的字母之后的数字或字母串去掉，只显示第一个字母之前的数字，即5.26B78转换为5.26
                            {
                                i++;
                            }
                            i--;
                        }
                    }
                }
                else //以上情况都不是，则发生错误，停止执行
                {
                    flag = false;
                }
            }
            if (point)//point为true，说明是整数，把各个属性填到token文件和符号表文件中
            {
                Token t = new Token();
                t.Label = tokenList.Count;
                t.Name = str;
                t.Code = 19;
                t.Addr = symbles.Count;
                symble s = new symble();
                s.Number = t.Addr;
                s.Name = str;
                s.Type = 19;
                symbles.Add(s);
                tokenList.Add(t);
            }
            else//point为flase，说明是实数，把各个属性填到token文件和符号表文件中
            {
                Token t = new Token();
                t.Label = tokenList.Count;
                t.Name = str;
                t.Code = 20;
                t.Addr = symbles.Count;
                symble s = new symble();
                s.Number = t.Addr;
                s.Name = str;
                s.Type = 20;
                symbles.Add(s);
                tokenList.Add(t);
            }
        }
        #endregion

        #region 判断是否是符号并处理RecogSym()
        private void RecogSym()
        {
            string str = "" + input[i];
            if (str == ":" || str == "<" || str == ">")//如果为：或<或>，判断下一个字符
            {
                i++;
                if (input[i] == '=')//如果为=，则符号为：=或<=或>=
                {
                    str += input[i];
                }
                else if (input[i] == '>' && str == "<")//如果str为<且当前字符为>,说明是尖括号<>
                {
                    str += input[i];
                }
                else//如果都不是，指向前一个字符
                {
                    i--;
                }
            }
            for (int j = 21; j <= 37; j++)//判断是哪个符号
            {
                if (str == machineCodes[j])//如果匹配成功，将各个属性添加到新建的token文件表项中，地址为-1，然后继续判断下一个
                {
                    Token t = new Token();
                    t.Label = tokenList.Count;
                    t.Name = str;
                    t.Code = j;
                    t.Addr = -1;
                    tokenList.Add(t);
                    i++;
                }
            }



        }
        #endregion

        #region 判断是不是字母IsAlpha(char c)//是字母返回true
        private bool IsAlpha(char c)
        {
            if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 判断是不是数字IsDight(char c)//是数字返回true
        private bool IsDight(char c)
        {
            if (c >= '0' && c <= '9')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 判断是不是其他界符IsDelimiter(char c)//匹配成功，返回true
        private bool IsDelimiter(char c)
        {
            switch (c)
            {
                case '(': return true;
                case ')': return true;
                case '+': return true;
                case '-': return true;
                case '*': return true;
                case '.': return true;
                case ',': return true;
                case ':': return true;
                case ';': return true;
                case '=': return true;
                case '<': return true;
                case '>': return true;
                default: return false;
            }
        }
        #endregion

        #region 匹配关键字机内码Reserve(string str)//匹配成功，返回关键字的机内码；匹配失败，则为标识符，返回0
        private int Reserve(string str)
        {
            for (int i = 1; i <= 17; i++)
            {
                if (str == machineCodes[i])
                {
                    return i;//返回匹配成功的关键字的机内码
                }
            }
            return 0;//匹配失败，则为标识符
        }
        #endregion
    }
}
