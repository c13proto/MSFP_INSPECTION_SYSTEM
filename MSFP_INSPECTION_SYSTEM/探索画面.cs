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
    public partial class 探索画面 : Form
    {
        private static 探索画面 _instance;
        //init時に格納
        Mat[] テンプレート;
        Mat[] 合成用素材;
        Mat[] 合成画像;
        int[][,] 正解座標;

        //初期状態null
        public static ProgressBar プログレスバー;

        MyFunction MyFunc = new MyFunction();
        public 探索画面()
        {
            InitializeComponent();
            テンプレート = (Mat[])Main.テンプレート.Clone();
            if(検査対象画面.合成用素材!=null) 合成用素材 = (Mat[])検査対象画面.合成用素材.Clone();
            合成画像=(Mat[])検査対象画面.合成画像.Clone();
            正解座標 = (int[][,])Main.正解座標.Clone();
            プログレスバー = progressBar;
        }

        public static 探索画面 Instance
        {
            get
            {
                //_instanceがnullまたは破棄されているときは、
                //新しくインスタンスを作成する
                if (_instance == null || _instance.IsDisposed)
                    _instance = new 探索画面();
                return _instance;
            }
        }

        private void Click_Start(object sender, EventArgs e)
        {
            if (button_Start.Text == "Start")
            {
                int[,] パラメータ ={ //[i,0]=i番目のパラメータの最小値,[i,1]=最大値
                    {int.Parse(textBox_1s.Text),int.Parse(textBox_1e.Text)},
                    {int.Parse(textBox_2s.Text),int.Parse(textBox_2e.Text)},
                    {int.Parse(textBox_3s.Text),int.Parse(textBox_3e.Text)},
                    {int.Parse(textBox_4s.Text),int.Parse(textBox_4e.Text)},
                    {int.Parse(textBox_5s.Text),int.Parse(textBox_5e.Text)},
                };
                if (checkBox_合成skip.Checked)
                {
                    パラメータ[0, 0] = 0;
                    パラメータ[0, 1] = 0;
                    パラメータ[1, 0] = 0;
                    パラメータ[1, 1] = 0;
                }
                    System.Diagnostics.Debug.WriteLine("Start");
                if (checkBox_list.Checked)
                {
                    if (checkBox_合成skip.Checked)
                        MyFunc.リストの作成(テンプレート, null, 合成画像, 正解座標, textBox_点数範囲.Text, int.Parse(textBox_組数.Text), パラメータ);
                    else
                        MyFunc.リストの作成(テンプレート, 合成用素材, null, 正解座標, textBox_点数範囲.Text, int.Parse(textBox_組数.Text), パラメータ);
                }
                else
                {
                    if (checkBox_合成skip.Checked)
                        MyFunc.PfGA処理(テンプレート, null, 合成画像, 正解座標, パラメータ,int.Parse(textBox_最終世代.Text),int.Parse(textBox_世代毎.Text),checkBox_収束グラフ.Checked,int.Parse(textBox_pitch.Text));
                    else
                        MyFunc.PfGA処理(テンプレート, 合成用素材, null, 正解座標, パラメータ,int.Parse(textBox_最終世代.Text), int.Parse(textBox_世代毎.Text), checkBox_収束グラフ.Checked, int.Parse(textBox_pitch.Text));
                } 
                button_Start.Text = "Stop";
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Stop");

                button_Start.Text = "Start";
            }
        }
    }
}
