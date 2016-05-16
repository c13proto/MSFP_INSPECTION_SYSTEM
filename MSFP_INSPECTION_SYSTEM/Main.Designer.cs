namespace MSFP_INSPECTION_SYSTEM
{
    partial class Main
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_テンプレート = new System.Windows.Forms.Button();
            this.button_検査対象 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox_検査面数 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_正解リスト = new System.Windows.Forms.Button();
            this.pictureBoxIpl = new OpenCvSharp.UserInterface.PictureBoxIpl();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.button_評価開始 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton_評価開始 = new System.Windows.Forms.RadioButton();
            this.radioButton_検査対象 = new System.Windows.Forms.RadioButton();
            this.radioButton_テンプレート = new System.Windows.Forms.RadioButton();
            this.button_画像保存 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_FBA_min = new System.Windows.Forms.TextBox();
            this.textBox_FBA_max = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIpl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_テンプレート
            // 
            this.button_テンプレート.BackColor = System.Drawing.SystemColors.Control;
            this.button_テンプレート.Location = new System.Drawing.Point(36, 35);
            this.button_テンプレート.Name = "button_テンプレート";
            this.button_テンプレート.Size = new System.Drawing.Size(79, 23);
            this.button_テンプレート.TabIndex = 0;
            this.button_テンプレート.Text = "テンプレート";
            this.button_テンプレート.UseVisualStyleBackColor = false;
            this.button_テンプレート.Click += new System.EventHandler(this.Click_テンプレート);
            // 
            // button_検査対象
            // 
            this.button_検査対象.BackColor = System.Drawing.SystemColors.Control;
            this.button_検査対象.Location = new System.Drawing.Point(37, 64);
            this.button_検査対象.Name = "button_検査対象";
            this.button_検査対象.Size = new System.Drawing.Size(79, 23);
            this.button_検査対象.TabIndex = 1;
            this.button_検査対象.Text = "検査対象";
            this.button_検査対象.UseVisualStyleBackColor = false;
            this.button_検査対象.Click += new System.EventHandler(this.Click_検査対象);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(36, 238);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(79, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "探索";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Click_探索);
            // 
            // textBox_検査面数
            // 
            this.textBox_検査面数.Location = new System.Drawing.Point(92, 10);
            this.textBox_検査面数.Name = "textBox_検査面数";
            this.textBox_検査面数.Size = new System.Drawing.Size(22, 19);
            this.textBox_検査面数.TabIndex = 3;
            this.textBox_検査面数.Text = "5";
            this.textBox_検査面数.TextChanged += new System.EventHandler(this.TextChanged_検査面数);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "検査面数";
            // 
            // button_正解リスト
            // 
            this.button_正解リスト.BackColor = System.Drawing.SystemColors.Control;
            this.button_正解リスト.Location = new System.Drawing.Point(36, 93);
            this.button_正解リスト.Name = "button_正解リスト";
            this.button_正解リスト.Size = new System.Drawing.Size(79, 23);
            this.button_正解リスト.TabIndex = 5;
            this.button_正解リスト.Text = "正解リスト";
            this.button_正解リスト.UseVisualStyleBackColor = false;
            this.button_正解リスト.Click += new System.EventHandler(this.Click_正解リスト);
            // 
            // pictureBoxIpl
            // 
            this.pictureBoxIpl.Location = new System.Drawing.Point(123, 9);
            this.pictureBoxIpl.Name = "pictureBoxIpl";
            this.pictureBoxIpl.Size = new System.Drawing.Size(435, 307);
            this.pictureBoxIpl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxIpl.TabIndex = 6;
            this.pictureBoxIpl.TabStop = false;
            // 
            // trackBar
            // 
            this.trackBar.AutoSize = false;
            this.trackBar.Location = new System.Drawing.Point(36, 207);
            this.trackBar.Maximum = 4;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(80, 25);
            this.trackBar.TabIndex = 7;
            this.trackBar.ValueChanged += new System.EventHandler(this.ValueChanged_trackBar);
            // 
            // button_評価開始
            // 
            this.button_評価開始.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_評価開始.Location = new System.Drawing.Point(35, 158);
            this.button_評価開始.Name = "button_評価開始";
            this.button_評価開始.Size = new System.Drawing.Size(79, 23);
            this.button_評価開始.TabIndex = 8;
            this.button_評価開始.Text = "評価開始";
            this.button_評価開始.UseVisualStyleBackColor = true;
            this.button_評価開始.Click += new System.EventHandler(this.Click_評価開始);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton_評価開始);
            this.panel1.Controls.Add(this.radioButton_検査対象);
            this.panel1.Controls.Add(this.radioButton_テンプレート);
            this.panel1.Location = new System.Drawing.Point(6, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(25, 146);
            this.panel1.TabIndex = 9;
            // 
            // radioButton_評価開始
            // 
            this.radioButton_評価開始.AutoSize = true;
            this.radioButton_評価開始.Location = new System.Drawing.Point(6, 128);
            this.radioButton_評価開始.Name = "radioButton_評価開始";
            this.radioButton_評価開始.Size = new System.Drawing.Size(14, 13);
            this.radioButton_評価開始.TabIndex = 10;
            this.radioButton_評価開始.UseVisualStyleBackColor = true;
            this.radioButton_評価開始.CheckedChanged += new System.EventHandler(this.CheckedChanged_評価開始);
            // 
            // radioButton_検査対象
            // 
            this.radioButton_検査対象.AutoSize = true;
            this.radioButton_検査対象.Location = new System.Drawing.Point(6, 34);
            this.radioButton_検査対象.Name = "radioButton_検査対象";
            this.radioButton_検査対象.Size = new System.Drawing.Size(14, 13);
            this.radioButton_検査対象.TabIndex = 1;
            this.radioButton_検査対象.UseVisualStyleBackColor = true;
            this.radioButton_検査対象.CheckedChanged += new System.EventHandler(this.CheckedChanged_検査対象);
            // 
            // radioButton_テンプレート
            // 
            this.radioButton_テンプレート.AutoSize = true;
            this.radioButton_テンプレート.Checked = true;
            this.radioButton_テンプレート.Location = new System.Drawing.Point(6, 5);
            this.radioButton_テンプレート.Name = "radioButton_テンプレート";
            this.radioButton_テンプレート.Size = new System.Drawing.Size(14, 13);
            this.radioButton_テンプレート.TabIndex = 0;
            this.radioButton_テンプレート.TabStop = true;
            this.radioButton_テンプレート.UseVisualStyleBackColor = true;
            this.radioButton_テンプレート.CheckedChanged += new System.EventHandler(this.CheckedChanged_テンプレート);
            // 
            // button_画像保存
            // 
            this.button_画像保存.Location = new System.Drawing.Point(6, 267);
            this.button_画像保存.Name = "button_画像保存";
            this.button_画像保存.Size = new System.Drawing.Size(108, 23);
            this.button_画像保存.TabIndex = 10;
            this.button_画像保存.Text = "画像保存";
            this.button_画像保存.UseVisualStyleBackColor = true;
            this.button_画像保存.Click += new System.EventHandler(this.Click_画像保存);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "FilterByArea";
            // 
            // textBox_FBA_min
            // 
            this.textBox_FBA_min.Location = new System.Drawing.Point(58, 134);
            this.textBox_FBA_min.Name = "textBox_FBA_min";
            this.textBox_FBA_min.Size = new System.Drawing.Size(17, 19);
            this.textBox_FBA_min.TabIndex = 12;
            this.textBox_FBA_min.Text = "9";
            this.textBox_FBA_min.TextChanged += new System.EventHandler(this.TextChanged_FBA_min);
            // 
            // textBox_FBA_max
            // 
            this.textBox_FBA_max.Location = new System.Drawing.Point(81, 134);
            this.textBox_FBA_max.Name = "textBox_FBA_max";
            this.textBox_FBA_max.Size = new System.Drawing.Size(33, 19);
            this.textBox_FBA_max.TabIndex = 13;
            this.textBox_FBA_max.Text = "100";
            this.textBox_FBA_max.TextChanged += new System.EventHandler(this.TextChanged_FBA_max);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(579, 350);
            this.Controls.Add(this.textBox_FBA_max);
            this.Controls.Add(this.textBox_FBA_min);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_画像保存);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button_評価開始);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.pictureBoxIpl);
            this.Controls.Add(this.button_正解リスト);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_検査面数);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button_検査対象);
            this.Controls.Add(this.button_テンプレート);
            this.Name = "Main";
            this.Text = "Main";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIpl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_テンプレート;
        private System.Windows.Forms.Button button_検査対象;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox_検査面数;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_正解リスト;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Button button_評価開始;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton_評価開始;
        private System.Windows.Forms.RadioButton radioButton_検査対象;
        private System.Windows.Forms.RadioButton radioButton_テンプレート;
        private OpenCvSharp.UserInterface.PictureBoxIpl pictureBoxIpl;
        private System.Windows.Forms.Button button_画像保存;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_FBA_min;
        private System.Windows.Forms.TextBox textBox_FBA_max;
    }
}

