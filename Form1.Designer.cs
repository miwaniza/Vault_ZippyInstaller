namespace WindowsFormsApplication1
    {
    partial class Form1
        {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose ( bool disposing )
            {
            if (disposing && (components != null))
                {
                components.Dispose();
                }
            base.Dispose( disposing );
            }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent ()
            {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonUAC = new System.Windows.Forms.Button();
            this.buttonASP = new System.Windows.Forms.Button();
            this.buttonIIS = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonReboot = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonTimeout = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonUAC
            // 
            this.buttonUAC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonUAC.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonUAC.Location = new System.Drawing.Point(3, 3);
            this.buttonUAC.Name = "buttonUAC";
            this.buttonUAC.Size = new System.Drawing.Size(287, 25);
            this.buttonUAC.TabIndex = 10;
            this.buttonUAC.Text = "UAC state";
            this.buttonUAC.UseVisualStyleBackColor = true;
            this.buttonUAC.Click += new System.EventHandler(this.buttonUAC_Click);
            // 
            // buttonASP
            // 
            this.buttonASP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonASP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonASP.Location = new System.Drawing.Point(3, 65);
            this.buttonASP.Name = "buttonASP";
            this.buttonASP.Size = new System.Drawing.Size(287, 27);
            this.buttonASP.TabIndex = 12;
            this.buttonASP.Text = "Re-register ASP .Net filters";
            this.buttonASP.UseVisualStyleBackColor = true;
            this.buttonASP.Click += new System.EventHandler(this.buttonASP_Click);
            // 
            // buttonIIS
            // 
            this.buttonIIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonIIS.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonIIS.Location = new System.Drawing.Point(3, 34);
            this.buttonIIS.Name = "buttonIIS";
            this.buttonIIS.Size = new System.Drawing.Size(287, 25);
            this.buttonIIS.TabIndex = 11;
            this.buttonIIS.Text = "Install Vault-related IIS features";
            this.buttonIIS.UseVisualStyleBackColor = true;
            this.buttonIIS.Click += new System.EventHandler(this.buttonIIS_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.buttonReboot, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.buttonUAC, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonASP, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonIIS, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(293, 302);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // buttonReboot
            // 
            this.buttonReboot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonReboot.Location = new System.Drawing.Point(3, 98);
            this.buttonReboot.Name = "buttonReboot";
            this.buttonReboot.Size = new System.Drawing.Size(287, 27);
            this.buttonReboot.TabIndex = 14;
            this.buttonReboot.Text = "Hack \"Reboot Needed\"";
            this.buttonReboot.UseVisualStyleBackColor = true;
            this.buttonReboot.Click += new System.EventHandler(this.buttonReboot_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonTimeout);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 151);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 148);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IIS sites with timeout < 900s";
            // 
            // buttonTimeout
            // 
            this.buttonTimeout.Location = new System.Drawing.Point(6, 106);
            this.buttonTimeout.Name = "buttonTimeout";
            this.buttonTimeout.Size = new System.Drawing.Size(272, 42);
            this.buttonTimeout.TabIndex = 17;
            this.buttonTimeout.Text = "Increase IIS timeout for selected site";
            this.buttonTimeout.UseVisualStyleBackColor = true;
            this.buttonTimeout.Click += new System.EventHandler(this.buttonTimeout_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 18);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(272, 82);
            this.listBox1.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 302);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Vault Zippy Installer";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form1_HelpButtonClicked);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

            }

        #endregion

        private System.Windows.Forms.Button buttonUAC;
        private System.Windows.Forms.Button buttonASP;
        private System.Windows.Forms.Button buttonIIS;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonReboot;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonTimeout;



        }
    }

