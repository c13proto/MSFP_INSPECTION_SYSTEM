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

        public static int[][] 正解座標;
        public static int 検査面数= 5;

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
                    テンプレート[index] = テンプレート[index].Threshold(254, 255, ThresholdTypes.Binary);
                }
                pictureBoxIpl.ImageIpl = テンプレート[0];
            }
        }

        private void Click_検査対象(object sender, EventArgs e)
        {
            検査結果 = new Mat[検査面数];
            検査対象画面.Instance.Show();
        }

        private void Click_正解リスト(object sender, EventArgs e)
        {

        }


        private void ValueChanged_trackBar(object sender, EventArgs e)
        {
            var val = trackBar.Value;

            if (radioButton_テンプレート.Checked)
                if (テンプレート != null)pictureBoxIpl.ImageIpl = テンプレート[val];
            else if (radioButton_検査対象.Checked)
                if (検査結果!=null) pictureBoxIpl.ImageIpl = 検査結果[val];
            else 
                if(評価結果!=null) pictureBoxIpl.ImageIpl = 評価結果[val];
        }

        private void TextChanged_検査面数(object sender, EventArgs e)
        {
            var num = 0;
            if (int.TryParse(textBox_検査面数.Text, out num)) 検査面数 = num;
        }

        private void Click_評価開始(object sender, EventArgs e)
        {

        }
    }
}
