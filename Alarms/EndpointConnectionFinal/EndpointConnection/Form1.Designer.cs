
namespace ArsServer2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Passwordlabel = new System.Windows.Forms.Label();
            this.UNLabel = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.ServerName = new System.Windows.Forms.TextBox();
            this.UserName = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Endpoint = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Exit = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(328, 16);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(264, 31);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Windows Authentication";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(25, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 27);
            this.label5.TabIndex = 20;
            this.label5.Text = "Server Name";
            // 
            // Passwordlabel
            // 
            this.Passwordlabel.AutoSize = true;
            this.Passwordlabel.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Passwordlabel.Location = new System.Drawing.Point(25, 208);
            this.Passwordlabel.Name = "Passwordlabel";
            this.Passwordlabel.Size = new System.Drawing.Size(100, 27);
            this.Passwordlabel.TabIndex = 14;
            this.Passwordlabel.Text = "Password";
            // 
            // UNLabel
            // 
            this.UNLabel.AutoSize = true;
            this.UNLabel.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UNLabel.Location = new System.Drawing.Point(25, 154);
            this.UNLabel.Name = "UNLabel";
            this.UNLabel.Size = new System.Drawing.Size(113, 27);
            this.UNLabel.TabIndex = 13;
            this.UNLabel.Text = "User Name";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(428, 265);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(182, 58);
            this.button3.TabIndex = 0;
            this.button3.Text = "Submit";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ServerName
            // 
            this.ServerName.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerName.Location = new System.Drawing.Point(228, 106);
            this.ServerName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ServerName.Name = "ServerName";
            this.ServerName.Size = new System.Drawing.Size(382, 34);
            this.ServerName.TabIndex = 22;
            this.ServerName.Text = "DESKTOP-EPRNPCE";
            // 
            // UserName
            // 
            this.UserName.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserName.Location = new System.Drawing.Point(228, 154);
            this.UserName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(382, 34);
            this.UserName.TabIndex = 23;
            this.UserName.Text = "sa";
            // 
            // Password
            // 
            this.Password.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Password.Location = new System.Drawing.Point(228, 208);
            this.Password.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(382, 34);
            this.Password.TabIndex = 24;
            this.Password.Text = "dacpl@123";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Endpoint);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.Password);
            this.groupBox1.Controls.Add(this.UserName);
            this.groupBox1.Controls.Add(this.UNLabel);
            this.groupBox1.Controls.Add(this.ServerName);
            this.groupBox1.Controls.Add(this.Passwordlabel);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(26, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(692, 342);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server Connections (Alarms)";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // Endpoint
            // 
            this.Endpoint.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Endpoint.Location = new System.Drawing.Point(228, 52);
            this.Endpoint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Endpoint.Name = "Endpoint";
            this.Endpoint.ReadOnly = true;
            this.Endpoint.Size = new System.Drawing.Size(382, 34);
            this.Endpoint.TabIndex = 26;
            this.Endpoint.Text = "opcda://localhost/OPCServer.WinCC";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(228, 265);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(173, 58);
            this.button4.TabIndex = 25;
            this.button4.Text = "Test Connection";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 27);
            this.label1.TabIndex = 29;
            this.label1.Text = "Enter Endpoint URL";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.Exit);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(26, 362);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(692, 288);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Connections Status";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(219, 162);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 27);
            this.label9.TabIndex = 28;
            this.label9.Text = " None";
            // 
            // Exit
            // 
            this.Exit.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exit.Location = new System.Drawing.Point(224, 224);
            this.Exit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(122, 58);
            this.Exit.TabIndex = 27;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(224, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 27);
            this.label6.TabIndex = 24;
            this.label6.Text = "None";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(414, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 27);
            this.label7.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(224, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 27);
            this.label8.TabIndex = 26;
            this.label8.Text = "None";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 27);
            this.label2.TabIndex = 21;
            this.label2.Text = "Data Destination : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 27);
            this.label3.TabIndex = 22;
            this.label3.Text = "Last Logged on :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 27);
            this.label4.TabIndex = 23;
            this.label4.Text = "Data Source : ";
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(85, 687);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(694, 27);
            this.label10.TabIndex = 29;
            this.label10.Text = "Developed by Dhruva Automation &&  Controls Pvt Ltd | All Rights Reserved.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ArsServer2.Properties.Resources.D111;
            this.pictureBox1.Location = new System.Drawing.Point(29, 674);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(819, 746);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ARS Application";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Passwordlabel;
        private System.Windows.Forms.Label UNLabel;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox ServerName;
        private System.Windows.Forms.TextBox UserName;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox Endpoint;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

