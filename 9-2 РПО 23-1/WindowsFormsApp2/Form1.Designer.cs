namespace WindowsFormsApp2
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label2 = new System.Windows.Forms.Label();
            this.ck3 = new System.Windows.Forms.CheckBox();
            this.ck4 = new System.Windows.Forms.CheckBox();
            this.rd3 = new System.Windows.Forms.RadioButton();
            this.rd4 = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(353, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Расскажите о себе";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // ck3
            // 
            this.ck3.AutoSize = true;
            this.ck3.Location = new System.Drawing.Point(217, 145);
            this.ck3.Name = "ck3";
            this.ck3.Size = new System.Drawing.Size(96, 17);
            this.ck3.TabIndex = 1;
            this.ck3.Text = "Программист";
            this.ck3.UseVisualStyleBackColor = true;
            // 
            // ck4
            // 
            this.ck4.AutoSize = true;
            this.ck4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ck4.Location = new System.Drawing.Point(526, 145);
            this.ck4.Name = "ck4";
            this.ck4.Size = new System.Drawing.Size(85, 17);
            this.ck4.TabIndex = 2;
            this.ck4.Text = "Дизайнер";
            this.ck4.UseVisualStyleBackColor = true;
            this.ck4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // rd3
            // 
            this.rd3.AutoSize = true;
            this.rd3.Location = new System.Drawing.Point(217, 87);
            this.rd3.Name = "rd3";
            this.rd3.Size = new System.Drawing.Size(71, 17);
            this.rd3.TabIndex = 3;
            this.rd3.TabStop = true;
            this.rd3.Text = "Мужской";
            this.rd3.UseVisualStyleBackColor = true;
            this.rd3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // rd4
            // 
            this.rd4.AutoSize = true;
            this.rd4.Location = new System.Drawing.Point(526, 87);
            this.rd4.Name = "rd4";
            this.rd4.Size = new System.Drawing.Size(72, 17);
            this.rd4.TabIndex = 4;
            this.rd4.TabStop = true;
            this.rd4.Text = "Женский";
            this.rd4.UseVisualStyleBackColor = true;
            this.rd4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(231, 350);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(367, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(367, 265);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(75, 23);
            this.btn.TabIndex = 6;
            this.btn.Text = "ОК";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.button2_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog2_FileOk);
            // 
            // fontDialog1
            // 
            this.fontDialog1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fontDialog1.Apply += new System.EventHandler(this.fontDialog1_Apply);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(816, 442);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.rd4);
            this.Controls.Add(this.rd3);
            this.Controls.Add(this.ck4);
            this.Controls.Add(this.ck3);
            this.Controls.Add(this.label2);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ck3;
        private System.Windows.Forms.CheckBox ck4;
        private System.Windows.Forms.RadioButton rd3;
        private System.Windows.Forms.RadioButton rd4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.FontDialog fontDialog1;
    }
}

