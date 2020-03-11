namespace LLanguageComplier
{
    partial class LLC
    {
        #region 定义对象变量
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button grammar;
        private System.Windows.Forms.Button semantic;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label input;
        private System.Windows.Forms.Label result;
        private System.Windows.Forms.Button Morphology;
        private System.Windows.Forms.Button symbleShow;
        private System.Windows.Forms.Button tokenShow;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.Label error;
        private System.Windows.Forms.Button create;
        private System.Windows.Forms.Button openFile;
        private System.Windows.Forms.Button saveFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        #endregion

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtInput = new System.Windows.Forms.TextBox();
            this.grammar = new System.Windows.Forms.Button();
            this.semantic = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.input = new System.Windows.Forms.Label();
            this.result = new System.Windows.Forms.Label();
            this.Morphology = new System.Windows.Forms.Button();
            this.symbleShow = new System.Windows.Forms.Button();
            this.tokenShow = new System.Windows.Forms.Button();
            this.txtError = new System.Windows.Forms.TextBox();
            this.error = new System.Windows.Forms.Label();
            this.create = new System.Windows.Forms.Button();
            this.openFile = new System.Windows.Forms.Button();
            this.saveFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInput.Location = new System.Drawing.Point(42, 133);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInput.Size = new System.Drawing.Size(806, 275);
            this.txtInput.TabIndex = 0;
            this.txtInput.Text = "\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r" +
    "\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n";
            // 
            // grammar
            // 
            this.grammar.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grammar.Location = new System.Drawing.Point(463, 33);
            this.grammar.Name = "grammar";
            this.grammar.Size = new System.Drawing.Size(109, 35);
            this.grammar.TabIndex = 2;
            this.grammar.Text = "语法分析";
            this.grammar.UseVisualStyleBackColor = true;
            this.grammar.Click += new System.EventHandler(this.Grammar_Click);
            // 
            // semantic
            // 
            this.semantic.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.semantic.Location = new System.Drawing.Point(612, 33);
            this.semantic.Name = "semantic";
            this.semantic.Size = new System.Drawing.Size(114, 35);
            this.semantic.TabIndex = 3;
            this.semantic.Text = "语义分析";
            this.semantic.UseVisualStyleBackColor = true;
            this.semantic.Click += new System.EventHandler(this.Semantic_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOutput.Location = new System.Drawing.Point(498, 450);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(350, 189);
            this.txtOutput.TabIndex = 4;
            this.txtOutput.Text = "\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r" +
    "\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n";
            // 
            // input
            // 
            this.input.AutoSize = true;
            this.input.Font = new System.Drawing.Font("楷体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.input.Location = new System.Drawing.Point(38, 98);
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(93, 20);
            this.input.TabIndex = 5;
            this.input.Text = "程序输入";
            // 
            // result
            // 
            this.result.AutoSize = true;
            this.result.Font = new System.Drawing.Font("楷体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.result.Location = new System.Drawing.Point(494, 426);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(93, 20);
            this.result.TabIndex = 6;
            this.result.Text = "结果显示";
            // 
            // Morphology
            // 
            this.Morphology.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Morphology.Location = new System.Drawing.Point(33, 32);
            this.Morphology.Name = "Morphology";
            this.Morphology.Size = new System.Drawing.Size(109, 35);
            this.Morphology.TabIndex = 1;
            this.Morphology.Text = "词法分析";
            this.Morphology.UseVisualStyleBackColor = true;
            this.Morphology.Click += new System.EventHandler(this.Morphology_Click);
            // 
            // symbleShow
            // 
            this.symbleShow.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.symbleShow.Location = new System.Drawing.Point(318, 33);
            this.symbleShow.Name = "symbleShow";
            this.symbleShow.Size = new System.Drawing.Size(109, 35);
            this.symbleShow.TabIndex = 7;
            this.symbleShow.Text = "符号表显示";
            this.symbleShow.UseVisualStyleBackColor = true;
            this.symbleShow.Click += new System.EventHandler(this.symble_Click);
            // 
            // tokenShow
            // 
            this.tokenShow.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tokenShow.Location = new System.Drawing.Point(178, 33);
            this.tokenShow.Name = "tokenShow";
            this.tokenShow.Size = new System.Drawing.Size(109, 34);
            this.tokenShow.TabIndex = 8;
            this.tokenShow.Text = "token显示";
            this.tokenShow.UseVisualStyleBackColor = true;
            this.tokenShow.Click += new System.EventHandler(this.token_Click);
            // 
            // txtError
            // 
            this.txtError.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtError.Location = new System.Drawing.Point(42, 450);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.ReadOnly = true;
            this.txtError.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtError.Size = new System.Drawing.Size(372, 189);
            this.txtError.TabIndex = 11;
            this.txtError.Text = "\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r" +
    "\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n";
            // 
            // error
            // 
            this.error.AutoSize = true;
            this.error.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.error.Location = new System.Drawing.Point(38, 427);
            this.error.Name = "error";
            this.error.Size = new System.Drawing.Size(93, 19);
            this.error.TabIndex = 12;
            this.error.Text = "词法错误";
            // 
            // create
            // 
            this.create.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.create.Location = new System.Drawing.Point(750, 34);
            this.create.Name = "create";
            this.create.Size = new System.Drawing.Size(121, 34);
            this.create.TabIndex = 13;
            this.create.Text = "目标代码生成";
            this.create.UseVisualStyleBackColor = true;
            this.create.Click += new System.EventHandler(this.create_Click);
            // 
            // openFile
            // 
            this.openFile.Location = new System.Drawing.Point(178, 94);
            this.openFile.Name = "openFile";
            this.openFile.Size = new System.Drawing.Size(109, 33);
            this.openFile.TabIndex = 14;
            this.openFile.Text = "打开文件";
            this.openFile.UseVisualStyleBackColor = true;
            this.openFile.Click += new System.EventHandler(this.openFile_Click);
            // 
            // saveFile
            // 
            this.saveFile.Location = new System.Drawing.Point(318, 94);
            this.saveFile.Name = "saveFile";
            this.saveFile.Size = new System.Drawing.Size(109, 33);
            this.saveFile.TabIndex = 15;
            this.saveFile.Text = "保存文件";
            this.saveFile.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // LLC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 686);
            this.Controls.Add(this.saveFile);
            this.Controls.Add(this.openFile);
            this.Controls.Add(this.create);
            this.Controls.Add(this.error);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.tokenShow);
            this.Controls.Add(this.symbleShow);
            this.Controls.Add(this.result);
            this.Controls.Add(this.input);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.semantic);
            this.Controls.Add(this.grammar);
            this.Controls.Add(this.Morphology);
            this.Controls.Add(this.txtInput);
            this.Name = "LLC";
            this.Text = "L Language Compiler";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}

