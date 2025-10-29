namespace Урок__2.Манипулирование_процессами
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
            this.availableAssemblies = new System.Windows.Forms.ListBox();
            this.startedAssemblies = new System.Windows.Forms.ListBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_closeWindow = new System.Windows.Forms.Button();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.btn_run = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // availableAssemblies
            // 
            this.availableAssemblies.FormattingEnabled = true;
            this.availableAssemblies.Location = new System.Drawing.Point(12, 12);
            this.availableAssemblies.Name = "availableAssemblies";
            this.availableAssemblies.Size = new System.Drawing.Size(293, 316);
            this.availableAssemblies.TabIndex = 0;
            this.availableAssemblies.SelectedIndexChanged += new System.EventHandler(this.availableAssemblies_SelectedIndexChanged);
            // 
            // startedAssemblies
            // 
            this.startedAssemblies.FormattingEnabled = true;
            this.startedAssemblies.Location = new System.Drawing.Point(495, 12);
            this.startedAssemblies.Name = "startedAssemblies";
            this.startedAssemblies.Size = new System.Drawing.Size(293, 316);
            this.startedAssemblies.TabIndex = 1;
            this.startedAssemblies.SelectedIndexChanged += new System.EventHandler(this.startedAssemblies_SelectedIndexChanged);
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(321, 12);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(159, 46);
            this.btn_start.TabIndex = 2;
            this.btn_start.Text = "START";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(321, 77);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(159, 46);
            this.btn_stop.TabIndex = 3;
            this.btn_stop.Text = "STOP";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_closeWindow
            // 
            this.btn_closeWindow.Location = new System.Drawing.Point(321, 145);
            this.btn_closeWindow.Name = "btn_closeWindow";
            this.btn_closeWindow.Size = new System.Drawing.Size(159, 46);
            this.btn_closeWindow.TabIndex = 4;
            this.btn_closeWindow.Text = "CLOSE WINDOW";
            this.btn_closeWindow.UseVisualStyleBackColor = true;
            this.btn_closeWindow.Click += new System.EventHandler(this.btn_closeWindow_Click);
            // 
            // btn_refresh
            // 
            this.btn_refresh.Location = new System.Drawing.Point(321, 214);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(159, 46);
            this.btn_refresh.TabIndex = 5;
            this.btn_refresh.Text = "REFRESH";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // btn_run
            // 
            this.btn_run.Location = new System.Drawing.Point(321, 282);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(159, 46);
            this.btn_run.TabIndex = 6;
            this.btn_run.Text = "RUN CALCULATOR";
            this.btn_run.UseVisualStyleBackColor = true;
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 340);
            this.Controls.Add(this.btn_run);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.btn_closeWindow);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.startedAssemblies);
            this.Controls.Add(this.availableAssemblies);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox availableAssemblies;
        private System.Windows.Forms.ListBox startedAssemblies;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_closeWindow;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.Button btn_run;
    }
}

