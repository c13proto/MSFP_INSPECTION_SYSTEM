namespace MSFP_INSPECTION_SYSTEM
{
    partial class 検査対象画面
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
            this.button_合成用素材 = new System.Windows.Forms.Button();
            this.button_合成済み = new System.Windows.Forms.Button();
            this.button_合成開始 = new System.Windows.Forms.Button();
            this.textBox_傾き = new System.Windows.Forms.TextBox();
            this.textBox_明るさ = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBoxIpl = new OpenCvSharp.UserInterface.PictureBoxIpl();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_回数 = new System.Windows.Forms.TextBox();
            this.textBox_サイズ = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button_TopHat = new System.Windows.Forms.Button();
            this.textBox_二値 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton_二値 = new System.Windows.Forms.RadioButton();
            this.radioButton_TopHat = new System.Windows.Forms.RadioButton();
            this.radioButton_合成 = new System.Windows.Forms.RadioButton();
            this.button_二値 = new System.Windows.Forms.Button();
            this.trackBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIpl)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // button_合成用素材
            // 
            this.button_合成用素材.Location = new System.Drawing.Point(42, 12);
            this.button_合成用素材.Name = "button_合成用素材";
            this.button_合成用素材.Size = new System.Drawing.Size(75, 23);
            this.button_合成用素材.TabIndex = 0;
            this.button_合成用素材.Text = "合成用素材";
            this.button_合成用素材.UseVisualStyleBackColor = true;
            this.button_合成用素材.Click += new System.EventHandler(this.Click_合成用素材);
            // 
            // button_合成済み
            // 
            this.button_合成済み.Location = new System.Drawing.Point(41, 119);
            this.button_合成済み.Name = "button_合成済み";
            this.button_合成済み.Size = new System.Drawing.Size(75, 23);
            this.button_合成済み.TabIndex = 1;
            this.button_合成済み.Text = "合成済み";
            this.button_合成済み.UseVisualStyleBackColor = true;
            this.button_合成済み.Click += new System.EventHandler(this.Click_合成済み);
            // 
            // button_合成開始
            // 
            this.button_合成開始.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_合成開始.Location = new System.Drawing.Point(41, 90);
            this.button_合成開始.Name = "button_合成開始";
            this.button_合成開始.Size = new System.Drawing.Size(75, 23);
            this.button_合成開始.TabIndex = 2;
            this.button_合成開始.Text = "合成開始";
            this.button_合成開始.UseVisualStyleBackColor = true;
            this.button_合成開始.Click += new System.EventHandler(this.Click_合成開始);
            // 
            // textBox_傾き
            // 
            this.textBox_傾き.Location = new System.Drawing.Point(80, 41);
            this.textBox_傾き.Name = "textBox_傾き";
            this.textBox_傾き.Size = new System.Drawing.Size(36, 19);
            this.textBox_傾き.TabIndex = 3;
            this.textBox_傾き.Text = "3.0";
            // 
            // textBox_明るさ
            // 
            this.textBox_明るさ.Location = new System.Drawing.Point(80, 64);
            this.textBox_明るさ.Name = "textBox_明るさ";
            this.textBox_明るさ.Size = new System.Drawing.Size(36, 19);
            this.textBox_明るさ.TabIndex = 4;
            this.textBox_明るさ.Text = "100";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "傾き";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "明るさ";
            // 
            // pictureBoxIpl
            // 
            this.pictureBoxIpl.Location = new System.Drawing.Point(123, 12);
            this.pictureBoxIpl.Name = "pictureBoxIpl";
            this.pictureBoxIpl.Size = new System.Drawing.Size(440, 351);
            this.pictureBoxIpl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxIpl.TabIndex = 8;
            this.pictureBoxIpl.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "回数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "サイズ";
            // 
            // textBox_回数
            // 
            this.textBox_回数.Location = new System.Drawing.Point(80, 191);
            this.textBox_回数.Name = "textBox_回数";
            this.textBox_回数.Size = new System.Drawing.Size(36, 19);
            this.textBox_回数.TabIndex = 10;
            this.textBox_回数.Text = "3";
            // 
            // textBox_サイズ
            // 
            this.textBox_サイズ.Location = new System.Drawing.Point(80, 168);
            this.textBox_サイズ.Name = "textBox_サイズ";
            this.textBox_サイズ.Size = new System.Drawing.Size(36, 19);
            this.textBox_サイズ.TabIndex = 9;
            this.textBox_サイズ.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "<Open処理>";
            // 
            // button_TopHat
            // 
            this.button_TopHat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_TopHat.Location = new System.Drawing.Point(41, 216);
            this.button_TopHat.Name = "button_TopHat";
            this.button_TopHat.Size = new System.Drawing.Size(75, 23);
            this.button_TopHat.TabIndex = 14;
            this.button_TopHat.Text = "TopHat";
            this.button_TopHat.UseVisualStyleBackColor = true;
            this.button_TopHat.Click += new System.EventHandler(this.Click_TopHat);
            // 
            // textBox_二値
            // 
            this.textBox_二値.Location = new System.Drawing.Point(88, 250);
            this.textBox_二値.Name = "textBox_二値";
            this.textBox_二値.Size = new System.Drawing.Size(28, 19);
            this.textBox_二値.TabIndex = 16;
            this.textBox_二値.Text = "5";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton_二値);
            this.panel1.Controls.Add(this.radioButton_TopHat);
            this.panel1.Controls.Add(this.radioButton_合成);
            this.panel1.Location = new System.Drawing.Point(7, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(29, 281);
            this.panel1.TabIndex = 17;
            // 
            // radioButton_二値
            // 
            this.radioButton_二値.AutoSize = true;
            this.radioButton_二値.Location = new System.Drawing.Point(6, 240);
            this.radioButton_二値.Name = "radioButton_二値";
            this.radioButton_二値.Size = new System.Drawing.Size(14, 13);
            this.radioButton_二値.TabIndex = 2;
            this.radioButton_二値.UseVisualStyleBackColor = true;
            this.radioButton_二値.CheckedChanged += new System.EventHandler(this.CheckedChanged_二値);
            // 
            // radioButton_TopHat
            // 
            this.radioButton_TopHat.AutoSize = true;
            this.radioButton_TopHat.Location = new System.Drawing.Point(6, 208);
            this.radioButton_TopHat.Name = "radioButton_TopHat";
            this.radioButton_TopHat.Size = new System.Drawing.Size(14, 13);
            this.radioButton_TopHat.TabIndex = 1;
            this.radioButton_TopHat.UseVisualStyleBackColor = true;
            this.radioButton_TopHat.CheckedChanged += new System.EventHandler(this.CheckedChanged_TopHat);
            // 
            // radioButton_合成
            // 
            this.radioButton_合成.AutoSize = true;
            this.radioButton_合成.Checked = true;
            this.radioButton_合成.Location = new System.Drawing.Point(6, 83);
            this.radioButton_合成.Name = "radioButton_合成";
            this.radioButton_合成.Size = new System.Drawing.Size(14, 13);
            this.radioButton_合成.TabIndex = 0;
            this.radioButton_合成.TabStop = true;
            this.radioButton_合成.UseVisualStyleBackColor = true;
            this.radioButton_合成.CheckedChanged += new System.EventHandler(this.CheckedChanged_合成);
            // 
            // button_二値
            // 
            this.button_二値.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_二値.Location = new System.Drawing.Point(41, 248);
            this.button_二値.Name = "button_二値";
            this.button_二値.Size = new System.Drawing.Size(41, 23);
            this.button_二値.TabIndex = 18;
            this.button_二値.Text = "二値";
            this.button_二値.UseVisualStyleBackColor = true;
            this.button_二値.Click += new System.EventHandler(this.Click_二値);
            // 
            // trackBar
            // 
            this.trackBar.AutoSize = false;
            this.trackBar.Location = new System.Drawing.Point(43, 278);
            this.trackBar.Maximum = 4;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(73, 27);
            this.trackBar.TabIndex = 19;
            this.trackBar.ValueChanged += new System.EventHandler(this.ValueChanged_trackBar);
            // 
            // 検査対象画面
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(577, 376);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.button_二値);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox_二値);
            this.Controls.Add(this.button_TopHat);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_回数);
            this.Controls.Add(this.textBox_サイズ);
            this.Controls.Add(this.pictureBoxIpl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_明るさ);
            this.Controls.Add(this.textBox_傾き);
            this.Controls.Add(this.button_合成開始);
            this.Controls.Add(this.button_合成済み);
            this.Controls.Add(this.button_合成用素材);
            this.Name = "検査対象画面";
            this.Text = "検査対象画面";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIpl)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_合成用素材;
        private System.Windows.Forms.Button button_合成済み;
        private System.Windows.Forms.Button button_合成開始;
        private System.Windows.Forms.TextBox textBox_傾き;
        private System.Windows.Forms.TextBox textBox_明るさ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private OpenCvSharp.UserInterface.PictureBoxIpl pictureBoxIpl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_回数;
        private System.Windows.Forms.TextBox textBox_サイズ;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_TopHat;
        private System.Windows.Forms.TextBox textBox_二値;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton_二値;
        private System.Windows.Forms.RadioButton radioButton_TopHat;
        private System.Windows.Forms.RadioButton radioButton_合成;
        private System.Windows.Forms.Button button_二値;
        private System.Windows.Forms.TrackBar trackBar;
    }
}