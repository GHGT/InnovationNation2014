namespace DroneSimulator
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
            this.btnLoadWayPoints = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.lblPath = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.cpu_timer = new System.Windows.Forms.Timer(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.lblDEBUG = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadWayPoints
            // 
            this.btnLoadWayPoints.Location = new System.Drawing.Point(38, 12);
            this.btnLoadWayPoints.Name = "btnLoadWayPoints";
            this.btnLoadWayPoints.Size = new System.Drawing.Size(119, 39);
            this.btnLoadWayPoints.TabIndex = 0;
            this.btnLoadWayPoints.Text = "Load Control";
            this.btnLoadWayPoints.UseVisualStyleBackColor = true;
            this.btnLoadWayPoints.Click += new System.EventHandler(this.btnLoadWayPoints_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(188, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(119, 39);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Reset Drone CPU";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(333, 12);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(119, 39);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(1015, 75);
            this.tbLog.MaxLength = 65543;
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(260, 617);
            this.tbLog.TabIndex = 4;
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(1012, 57);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(25, 13);
            this.lblPath.TabIndex = 6;
            this.lblPath.Text = "Log";
            // 
            // PictureBox1
            // 
            this.PictureBox1.Location = new System.Drawing.Point(18, 57);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(963, 680);
            this.PictureBox1.TabIndex = 7;
            this.PictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 10;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 753);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Time:";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(54, 753);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(27, 13);
            this.lblTime.TabIndex = 9;
            this.lblTime.Text = "0.0s";
            // 
            // cpu_timer
            // 
            this.cpu_timer.Interval = 20;
            this.cpu_timer.Tick += new System.EventHandler(this.cpu_timer_Tick);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1156, 698);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(119, 39);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save Log File";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblDEBUG
            // 
            this.lblDEBUG.AutoSize = true;
            this.lblDEBUG.Location = new System.Drawing.Point(199, 753);
            this.lblDEBUG.Name = "lblDEBUG";
            this.lblDEBUG.Size = new System.Drawing.Size(45, 13);
            this.lblDEBUG.TabIndex = 11;
            this.lblDEBUG.Text = "DEBUG";
            this.lblDEBUG.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1287, 775);
            this.Controls.Add(this.lblDEBUG);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnLoadWayPoints);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Drone Simulator";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadWayPoints;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.PictureBox PictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer cpu_timer;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblDEBUG;
    }
}

