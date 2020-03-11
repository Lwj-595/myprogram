using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 编译;
using System.IO;

namespace LLanguageComplier
{
    public partial class LLC : Form
    {
        //Morphology m = new Morphology("");
        public LLC()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e) { }

        #region 词法分析
        /// <summary>
        /// 输出词法分析结果和词法错误统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Morphology_Click(object sender, EventArgs e)
        {
            Morphology m = new Morphology(txtInput.Text);//传入字符串
            string str = "";
            string strError = "";
            foreach (Token token in m.tokenList)
            {
                str += "(" + token.Label + ")(" + token.Code + ",\"" + token.Name + "\")\r\n";
            }
            txtOutput.Text = str;
            strError += "共有" + m.errors.Count + "个错误：\r\n";

            foreach (Error error in m.errors)
            {
                strError += "第" + error.Row + "行出现错误\r\n";
            }
            txtError.Text = strError;
        }
        #endregion

        #region 输出token文件
        /// <summary>
        /// 输出token文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void token_Click(object sender, EventArgs e)
        {
            Morphology m = new Morphology(txtInput.Text);
            string str = "";
            foreach (Token token in m.tokenList)
            {
                str += "(" + token.Label + ") (" + token.Code + ",\"" + token.Name + "\"," + token.Addr + ")\r\n";
            }
            txtOutput.Text = str;
        }
        #endregion

        #region 输出symble文件
        /// <summary>
        /// 输出symble文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void symble_Click(object sender, EventArgs e)
        {
            Morphology m = new Morphology(txtInput.Text);
            string str = "";
            foreach (symble s in m.symbles)
            {
                str += "(" + s.Number + ") (" + s.Type + ",\"" + s.Name + "\")\r\n";
            }
            txtOutput.Text = str;
        }
        #endregion

        #region 语法分析
        /// <summary>
        /// 语法分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grammar_Click(object sender, EventArgs e)
        {
            Morphology m = new Morphology(txtInput.Text);
            Grammar g = new Grammar(m);
            if (g.error != "")
            {
                txtOutput.Text = g.error;
            }
            else
            {
                txtOutput.Text = "正确";
            }
        }
        #endregion

        #region 语义分析
        /// <summary>
        /// 语义分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Semantic_Click(object sender, EventArgs e)
        {
            Morphology m = new Morphology(txtInput.Text);
            Semantic s = new Semantic(m);
            string str = "";
            int i = 0;
            foreach (FourPart f in s.fps)
            {
                str += "(" + i + ")(" + f.Op + "," + f.StrLeft + "," + f.StrRight + "," + f.JumpNum + ")\r\n";
                i++;
            }
            txtOutput.Text = str;
        }
        #endregion 

        #region 生成目标代码
        /// <summary>
        /// 生成目标代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void create_Click(object sender, EventArgs e)
        {
            Morphology m = new Morphology(txtInput.Text);
            Semantic s = new Semantic(m);
            Create c = new Create(s);
            string str = "";
            foreach (Assembly a in c.assemblys)
            {
                if (a.Num != null)
                {
                    str += "L" + a.Num + ": ";
                    if (a.PR == "")
                    {
                        str += a.Op + " " + a.PL + "\r\n";
                    }
                    else
                    {
                        str += a.Op + " " + a.PL + "," + a.PR + "\r\n";
                    }
                }
                else
                {
                    str += "    ";
                    if (a.PR == "")
                    {
                        str += a.Op + " " + a.PL + "\r\n";
                    }
                    else
                    {
                        str += a.Op + " " + a.PL + "," + a.PR + "\r\n";
                    }
                }

            }
            txtOutput.Text = str;
        }
        #endregion

        #region 打开文件
        private void openFile_Click(object sender, EventArgs e)
        {
            string strText = "";
            OpenFileDialog fileLaction = new OpenFileDialog();
            fileLaction.Filter = "文本文件|*.txt";
            if (fileLaction.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = File.OpenText(fileLaction.FileName);
                while (sr.EndOfStream != true)
                {
                    strText += sr.ReadLine()+"\r\n";

                }
                txtInput.Text = strText;
            }
        }
        #endregion

        //private void label3_Click(object sender, EventArgs e)
        //{

        //}
        //private void input_TextChanged(object sender, EventArgs e){}

        //private void output_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void statistics_TextChanged(object sender, EventArgs e)
        //{

        //}

    }
}
