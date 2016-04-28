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
    public partial class 検査対象画面 : Form
    {
        Mat[] 合成用素材;
        Mat[] 合成画像;
        Mat[] TopHat;
        Mat[] 二値化;
        int 検査面数;

        private static 検査対象画面 _instance;
        MyCV MyCV = new MyCV();


        public 検査対象画面()
        {
            InitializeComponent();
            検査面数 = Main.検査面数;
        }

        public static 検査対象画面 Instance
        {
            get
            {
                //_instanceがnullまたは破棄されているときは、
                //新しくインスタンスを作成する
                if (_instance == null || _instance.IsDisposed)
                    _instance = new 検査対象画面();
                return _instance;
            }
        }

        private void Click_合成用素材(object sender, EventArgs e)
        {
            合成用素材 = new Mat[検査面数*4];
            合成画像 = new Mat[検査面数];

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
                    合成用素材[index] = new Mat(file.value, ImreadModes.GrayScale);
                    if (index % 4 == 3)//画像合成
                    {

                        合成画像[index / 4] = new OpenCvSharp.Mat(合成用素材[index].Height, 合成用素材[index].Width, MatType.CV_8UC1);
                        Mat[] images = new Mat[4];
                        for (int i = 0; i < 4; i++) images[i] = 合成用素材[index - i];
                        MyCV.自作反射光除去(images, ref 合成画像[index / 4]);
                        MyCV.コントラスト調整(ref 合成画像[index / 4], double.Parse(textBox_傾き.Text));
                        MyCV.明るさ調整(ref 合成画像[index / 4], double.Parse(textBox_明るさ.Text));
                        this.Text = 100.0 * index / (4 * 検査面数) + "%";

                    }

                 }

                if(radioButton_合成.Checked)表示画像更新();
                else radioButton_合成.Checked = true;
            }
        }
        private void Click_合成済み(object sender, EventArgs e)
        {
            合成画像 = new Mat[検査面数];

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
                    合成画像[index] = new Mat(file.value, ImreadModes.GrayScale);

                }

                if (radioButton_合成.Checked)表示画像更新();
                else radioButton_合成.Checked = true;
            }
        }
        private void ValueChanged_trackBar(object sender, EventArgs e)
        {
            表示画像更新();
        }

        private void Click_合成開始(object sender, EventArgs e)
        {
            if(合成画像!=null)foreach (Mat mat in 合成画像) mat.Dispose();
            if(合成用素材!=null)自作合成処理();
        }

        void 自作合成処理()
        {
            if (合成用素材.Length == 検査面数 * 4)
                for (int num = 0; num < 検査面数; num++)
                {
                    合成画像[num] = new OpenCvSharp.Mat(合成用素材[0].Height, 合成用素材[0].Width, MatType.CV_8UC1);
                    Mat[] images = new Mat[4];
                    for (int i = 0; i < 4; i++) images[i] = 合成用素材[num*4 + i];
                    MyCV.自作反射光除去(images, ref 合成画像[num]);
                    MyCV.コントラスト調整(ref 合成画像[num], double.Parse(textBox_傾き.Text));
                    MyCV.明るさ調整(ref 合成画像[num], double.Parse(textBox_明るさ.Text));
                    this.Text = num*100.0/検査面数 + "%";
                }

            if (radioButton_合成.Checked) 表示画像更新();
            radioButton_合成.Checked = true;
        }

        private void Click_TopHat(object sender, EventArgs e)
        {
            TopHat = new Mat[検査面数];
            
            for (int i = 0; i < 検査面数; i++) MyCV.TopHat(合成画像[i],ref TopHat[i], int.Parse(textBox_サイズ.Text), int.Parse(textBox_回数.Text));
            radioButton_TopHat.Checked = true;
        }

        private void Click_二値(object sender, EventArgs e)
        {
            二値化 = new Mat[検査面数];
            if (TopHat != null)
            {
                foreach (var indexMat in TopHat.Select((mat, index) => new { mat,index }))
                {
                    int index = indexMat.index;
                    二値化[index] = indexMat.mat.Clone();
                    MyCV.二値化(ref 二値化[index],int.Parse(textBox_二値.Text));
                }
                Main.検査結果 = 二値化;

                if (radioButton_二値.Checked) 表示画像更新();
                radioButton_二値.Checked = true;

            }
        }
        void 表示画像更新()
        {
            var val = trackBar.Value;
            if (radioButton_合成.Checked)
            {
                if (合成画像 != null) pictureBoxIpl.ImageIpl = 合成画像[val];
            }
            else if (radioButton_TopHat.Checked)
            {
                if (TopHat != null) pictureBoxIpl.ImageIpl = TopHat[val];
            }
            else
            {
                if (二値化 != null) pictureBoxIpl.ImageIpl = 二値化[val];
            }
        }

        //ラジオボタンの変更(2回処理されるのを回避)
        private void CheckedChanged_二値(object sender, EventArgs e)
        {
            if(radioButton_二値.Checked) 表示画像更新();
        }
        private void CheckedChanged_TopHat(object sender, EventArgs e)
        {
            if (radioButton_TopHat.Checked) 表示画像更新();
        }
        private void CheckedChanged_合成(object sender, EventArgs e)
        {
            if (radioButton_合成.Checked) 表示画像更新();
        }
    }
}
