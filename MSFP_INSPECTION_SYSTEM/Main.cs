using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;

namespace MSFP_INSPECTION_SYSTEM
{
    public partial class Main : Form
    {
        public static Mat[] テンプレート;
        public static Mat[] 検査結果;
        public static Mat[] 評価結果;

        public static int[][,] 正解座標;
        public static int 検査面数= 5;
        

        MyFunction MyFunc = new MyFunction();
        MyCV mycv = new MyCV();

        public Main()
        {
            InitializeComponent();
            検査面数 = int.Parse(textBox_検査面数.Text);


            テンプレート = new Mat[検査面数];
        }

        private void Click_テンプレート(object sender, EventArgs e)
        {
            テンプレート = new Mat[検査面数];
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Multiselect = true,  // 複数選択の可否
                Filter =  // フィルタ
                "画像ファイル|*.bmp;*.gif;*.jpg;*.png|全てのファイル|*.*",
            };
            //ダイアログを表示
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                //OKボタンがクリックされたとき
                //選択されたファイル名をすべて表示する
                foreach (var file in dialog.FileNames.Select((value, index) => new { value, index }))
                {
                    var index = file.index;
                    テンプレート[index] = new Mat(file.value, ImreadModes.GrayScale);
                    mycv.二値化(ref テンプレート[index],254);
                }

                if (radioButton_テンプレート.Checked) 表示画像更新();
                radioButton_テンプレート.Checked = true;
            }
        }

        private void Click_検査対象(object sender, EventArgs e)
        {
            検査結果 = new Mat[検査面数];
            検査対象画面.Instance.Show();
        }

        private void Click_正解リスト(object sender, EventArgs e)
        {
            正解座標 = new int[検査面数][,];
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Multiselect = true,  // 複数選択の可否
                Filter =  "csvファイル|*.csv|全てのファイル|*.*",
            };
            //ダイアログを表示
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                foreach (var csv in dialog.FileNames.Select((value, index) => new { value, index }))
                {
                    var index = csv.index;
                    var value = csv.value;
                    MyFunc.read_csv(ref 正解座標[index], value);

                }
            }
        }
        private void Click_評価開始(object sender, EventArgs e)
        {
            if (評価結果 != null) foreach (Mat mat in 評価結果) mat.Dispose();
            評価結果 = new Mat[検査面数];
            var gray = new Mat[検査面数];

            for (int i = 0; i < 検査面数; i++)
            {
                if (テンプレート != null && 検査結果 != null)
                {
                    評価結果[i] = new Mat(new OpenCvSharp.Size(テンプレート[i].Width, テンプレート[i].Height), MatType.CV_8UC3, Scalar.All(0));
                    gray[i] = 検査結果[i].Clone();//グレースケール
                    mycv.評価用画像作成(テンプレート[i], 検査結果[i], ref gray[i]);
                    if (正解座標 != null) mycv.評価結果画像作成_debug(gray[i], テンプレート[i], 正解座標[i], ref 評価結果[i]);
                    else mycv.評価結果画像作成_debug(gray[i], テンプレート[i], null, ref 評価結果[i]);
                }
            }
            gray = null;
            if (radioButton_評価開始.Checked) 表示画像更新();
            radioButton_評価開始.Checked = true;
        }
        private void Click_探索(object sender, EventArgs e)
        {
            if (検査対象画面.合成画像 != null && テンプレート != null&&正解座標!=null)
                探索画面.Instance.Show();
            else MessageBox.Show("ファイルを選択してください");
        }

        private void ValueChanged_trackBar(object sender, EventArgs e)
        {
            表示画像更新();
        }

        private void TextChanged_検査面数(object sender, EventArgs e)
        {
            var num = 0;
            if (int.TryParse(textBox_検査面数.Text, out num)) 検査面数 = num;
        }



        void 表示画像更新()
        {
            var val = trackBar.Value;

            if (radioButton_テンプレート.Checked)
            {
                if (テンプレート != null) pictureBoxIpl.ImageIpl = テンプレート[val];
            }
            else if (radioButton_検査対象.Checked)
            {
                if (検査結果 != null) pictureBoxIpl.ImageIpl = 検査結果[val];
            }
            else
            {
                if (評価結果 != null) pictureBoxIpl.ImageIpl = 評価結果[val];
            }
        }

        //ラジオボタンの変更(2回処理されるのを回避)
        private void CheckedChanged_評価開始(object sender, EventArgs e)
        {
            if (radioButton_評価開始.Checked) 表示画像更新();
        }
        private void CheckedChanged_検査対象(object sender, EventArgs e)
        {
            if (radioButton_検査対象.Checked) 表示画像更新();
        }
        private void CheckedChanged_テンプレート(object sender, EventArgs e)
        {
            if (radioButton_テンプレート.Checked) 表示画像更新();
        }

        private void Click_画像保存(object sender, EventArgs e)
        {
            if (pictureBoxIpl.ImageIpl != null)
            {
                //System.IO.Directory.CreateDirectory(@"result");//resultフォルダの作成
                SaveFileDialog sfd = new SaveFileDialog();//SaveFileDialogクラスのインスタンスを作成
                                                          //sfd.FileName = textBox_Gaus.Text + "_" + textBox_Bright.Text + "_" + textBox_Cont.Text;//はじめのファイル名を指定する
                                                          //sfd.InitialDirectory = @"result\";//はじめに表示されるフォルダを指定する
                sfd.Filter = "画像ファイル|*.bmp;*.gif;*.jpg;*.png|全てのファイル|*.*";//[ファイルの種類]に表示される選択肢を指定する
                sfd.FilterIndex = 1;//[ファイルの種類]ではじめに「画像ファイル」が選択されているようにする
                sfd.Title = "保存先のファイルを選択してください";//タイトルを設定する
                sfd.RestoreDirectory = true;//ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
                sfd.OverwritePrompt = true;//既に存在するファイル名を指定したとき警告する．デフォルトでTrueなので指定する必要はない
                sfd.CheckPathExists = true;//存在しないパスが指定されたとき警告を表示する．デフォルトでTrueなので指定する必要はない

                //ダイアログを表示する
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //OKボタンがクリックされたとき
                    //選択されたファイル名を表示する
                    System.Diagnostics.Debug.WriteLine(sfd.FileName);
                    pictureBoxIpl.ImageIpl.SaveImage(sfd.FileName);
                }
            }
        }
    }
}
