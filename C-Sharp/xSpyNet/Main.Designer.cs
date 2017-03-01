namespace xSpyNet
{
    partial class Main
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
            this.txtColor = new System.Windows.Forms.TextBox();
            this.SendDataTimer = new System.Windows.Forms.Timer(this.components);
            this.lblTime = new System.Windows.Forms.Label();
            this.LocationTimer = new System.Windows.Forms.Timer(this.components);
            this.OnlineTimer = new System.Windows.Forms.Timer(this.components);
            this.lblHello = new System.Windows.Forms.TextBox();
            this.QueryTimer = new System.Windows.Forms.Timer(this.components);
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtColor
            // 
            this.txtColor.Location = new System.Drawing.Point(355, 189);
            this.txtColor.Multiline = true;
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(127, 86);
            this.txtColor.TabIndex = 1;
            this.txtColor.Click += new System.EventHandler(this.textBox1_Click);
            // 
            // SendDataTimer
            // 
            this.SendDataTimer.Interval = 5000;
            this.SendDataTimer.Tick += new System.EventHandler(this.SendDataTimer_Tick);
            // 
            // lblTime
            // 
            this.lblTime.Location = new System.Drawing.Point(203, 9);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(135, 23);
            this.lblTime.TabIndex = 3;
            this.lblTime.Text = "0";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LocationTimer
            // 
            this.LocationTimer.Tick += new System.EventHandler(this.LocationTimer_Tick);
            // 
            // OnlineTimer
            // 
            this.OnlineTimer.Tick += new System.EventHandler(this.OnlineTimer_Tick);
            // 
            // lblHello
            // 
            this.lblHello.BackColor = System.Drawing.SystemColors.Control;
            this.lblHello.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblHello.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHello.Location = new System.Drawing.Point(17, 43);
            this.lblHello.Multiline = true;
            this.lblHello.Name = "lblHello";
            this.lblHello.Size = new System.Drawing.Size(519, 41);
            this.lblHello.TabIndex = 4;
            this.lblHello.Text = "Hello World!";
            this.lblHello.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // QueryTimer
            // 
            this.QueryTimer.Tick += new System.EventHandler(this.QueryTimer_Tick);
            // 
            // txtQuery
            // 
            this.txtQuery.BackColor = System.Drawing.SystemColors.Control;
            this.txtQuery.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuery.Location = new System.Drawing.Point(17, 131);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(519, 41);
            this.txtQuery.TabIndex = 4;
            this.txtQuery.Text = "Hello World!";
            this.txtQuery.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(120, 0);
            this.Controls.Add(this.txtQuery);
            this.Controls.Add(this.lblHello);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.txtColor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Microsoft";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Timer SendDataTimer;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer LocationTimer;
        private System.Windows.Forms.Timer OnlineTimer;
        private System.Windows.Forms.TextBox lblHello;
        private System.Windows.Forms.Timer QueryTimer;
        private System.Windows.Forms.TextBox txtQuery;
    }
}

