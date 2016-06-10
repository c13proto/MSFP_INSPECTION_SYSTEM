using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MSFP_INSPECTION_SYSTEM
{
    class MyFunction
    {
        public void read_csv(ref int[,] 正解座標,string path)
        {
            var csvRecords = new System.Collections.ArrayList();

            try
            {
                // csvファイルを開く
                using (var sr = new System.IO.StreamReader(path))
                {
                    // ストリームの末尾まで繰り返す
                    while (!sr.EndOfStream)csvRecords.Add(sr.ReadLine());
                }
                System.Diagnostics.Debug.WriteLine(path);
                正解座標 = new int[csvRecords.Count, 2];
                //正解座標リスト = new List<int[]>();
                for (int i = 0; i < csvRecords.Count; i++)
                {
                    var values = csvRecords[i].ToString().Split(',');
                    正解座標[i, 0] = int.Parse(values[0]);
                    正解座標[i, 1] = int.Parse(values[1]);
                }
                for (int i = 0; i < 正解座標.Length / 2; i++) System.Diagnostics.Debug.WriteLine("{0}\t{1}", 正解座標[i, 0], 正解座標[i, 1]);
            }
            catch (System.Exception e)// ファイルを開くのに失敗したとき
            {                
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            csvRecords = null;

        }
        public void PfGA処理(Mat[] テンプレート,Mat[] 合成用素材,Mat[] 合成画像,int[][,] 正解座標,int[,] パラメータ, int 最終世代,int 世代毎,bool graph,int pitch)
        {
            int 今の世代 = 0;
            List<int[]> グラフデータ = new List<int[]>();
            int[,] 局所集団 = new int[4, パラメータ.Length / 2 + 1];

            局所集団= 局所集団の初期化(パラメータ);

            while (今の世代 <= 最終世代)
            {
                int max_score = 0;//グラフデータ作成のため
                局所集団 = 次の局所集団を作成(テンプレート,合成用素材,合成画像,正解座標,局所集団, パラメータ);
                for (int i = 0; i < 4; i++)
                {
                    局所集団[i, パラメータ.Length / 2] = 評価結果(テンプレート,合成用素材,合成画像,正解座標,局所集団[i, 0], 局所集団[i, 1], 局所集団[i, 2], 局所集団[i, 3], 局所集団[i, 4]);
                    if (局所集団[i, パラメータ.Length / 2] > max_score) max_score = 局所集団[i, パラメータ.Length / 2];

                }
                System.Diagnostics.Debug.WriteLine("世代:"+今の世代+" スコア:"+max_score);

                if (今の世代 % 世代毎 == 0 || 今の世代 == 1 || max_score==10000)//途中と最終結果を出力 
                {
                    遺伝子情報をCSV出力(局所集団, 4, パラメータ.Length / 2,
                       DateTime.Now.ToString("yy-MM-dd_") + "Pf_" + 今の世代 + "_");
                }
                if (graph && (今の世代 % pitch == 0))
                    グラフデータ.Add(new int[] { 今の世代, max_score });
                if (max_score == 10000) break;
                
                探索画面.プログレスバー.Value = 100 * 今の世代 / 最終世代;

                今の世代++;
            }

            局所集団 = 遺伝子を成績順にソート(局所集団, 4, パラメータ.Length / 2);
            //遺伝子情報を画面に出力(局所集団[0, 0], 局所集団[0, 1], 局所集団[0, 2], 局所集団[0, 3], 局所集団[0, 4], 局所集団[0, 5]);
            if(graph)グラフデータを出力(グラフデータ, DateTime.Now.ToString("yy-MM-dd_") + "グラフデータ");
            System.Diagnostics.Debug.WriteLine("PfGA終了");
            探索画面.プログレスバー.Value = 100;

            グラフデータ = null;
            局所集団 = null;

        }
        public void リストの作成(Mat[] テンプレート,Mat[] 合成用素材,Mat[] 合成画像,int[][,] 正解座標,string スコア範囲,int 目標累計,int[,] パラメータ)
        {
            int 累計達成者 = 0;
            int 総世代数 = 0;
            string[] 目標スコア = スコア範囲.Split(',');

            //親は[0~1,],子は[2~3,]に戻す
            int[,] 局所集団 = new int[4, パラメータ.Length / 2 + 1];
            List<int[]> リスト = new List<int[]>();


            while (累計達成者 < 目標累計)
            {
                bool ノルマ達成 = false;
                局所集団 = 局所集団の初期化(パラメータ);

                while (!ノルマ達成)
                {
                    局所集団 = 次の局所集団を作成(テンプレート, 合成用素材, 合成画像,正解座標,局所集団, パラメータ);
                    for (int i = 0; i < 4; i++) 局所集団[i, パラメータ.Length / 2] = 評価結果(テンプレート,合成用素材,合成画像,正解座標,局所集団[i, 0], 局所集団[i, 1], 局所集団[i, 2], 局所集団[i, 3], 局所集団[i, 4]);
                    for (int i = 0; i < 4; i++)
                    {
                        if (局所集団[i, パラメータ.Length / 2] >= int.Parse(目標スコア[0]) && 局所集団[i, パラメータ.Length / 2] <= int.Parse(目標スコア[1]))
                        {
                            int[] 達成者 = new int[パラメータ.Length/2+1];
                            for (int j = 0; j < 達成者.Length; j++) 達成者[j]=局所集団[i, j];
                            リスト.Add(達成者);
                            達成者 = null;
                            ノルマ達成 = true;
                        }
                    }
                    System.Diagnostics.Debug.WriteLine("総世代数:" + 総世代数 + ",累計達成者:" + 累計達成者);
                    総世代数++;
                }
                
                累計達成者++;
                局所集団 = null;

                探索画面.プログレスバー.Value = 100*累計達成者/目標累計;
                グラフデータを出力(リスト, DateTime.Now.ToString("yy-MM-dd_") + "List" + "_" + 目標スコア[0] + "_" + 目標スコア[1]);
            }
            探索画面.プログレスバー.Value = 100;
            System.Diagnostics.Debug.WriteLine("総世代数:" + 総世代数 + ",累計達成者:" + 累計達成者);
            System.Diagnostics.Debug.WriteLine("リスト出力完了");
            
            リスト = null;
        }
        private int[,] 局所集団の初期化(int[,] パラメータ)
        {
            var group = new int[4, パラメータ.Length / 2 + 1];

            int[] 初期親1 = ランダムに遺伝子1つ作成(パラメータ);
            int[] 初期親2 = ランダムに遺伝子1つ作成(パラメータ);
            int[] 初期子1 = 親からランダムな交叉(初期親1, 初期親2, パラメータ.Length / 2);
            int[] 初期子2 = 親からランダムな交叉(初期親1, 初期親2, パラメータ.Length / 2);
            初期子2 = ランダムな突然変異(初期子2, パラメータ);

            for (int i = 0; i < パラメータ.Length / 2; i++)
            {
                group[0, i] = 初期親1[i];
                group[1, i] = 初期親2[i];
                group[2, i] = 初期子1[i];
                group[3, i] = 初期子2[i];
            }

            return group;
        }
        private int[,] 遺伝子を成績順にソート(int[,] 遺伝子, int 遺伝子の個数, int パラメータ数)
        {
            // System.Diagnostics.Debug.WriteLine("遺伝子ソート中");
            bool NotSort = true;
            while (NotSort)
            {
                for (int i = 0; i < 遺伝子の個数 - 1; i++)//ソート完了まで継続
                {
                    NotSort = false;
                    if (遺伝子[i, パラメータ数] < 遺伝子[i + 1, パラメータ数])//[i,パラメータ数]に評価が入っている
                    {
                        //次の遺伝子のほうが成績が良かったら
                        NotSort = true;
                        int[] 遺伝子temp = new int[パラメータ数 + 1];//一時的にi番目の遺伝子を保存
                        for (int j = 0; j < パラメータ数 + 1; j++) 遺伝子temp[j] = 遺伝子[i, j];
                        //i番目の遺伝子とi+1番目の遺伝子を交換
                        for (int j = 0; j < パラメータ数 + 1; j++)
                        {
                            遺伝子[i, j] = 遺伝子[i + 1, j];
                            遺伝子[i + 1, j] = 遺伝子temp[j];
                        }
                        break;
                    }
                }
            }
            return 遺伝子;
        }
        private void 遺伝子情報をCSV出力(int[,] 遺伝子, int 実験体数, int パラメータ数, String ファイル名)
        {
            System.IO.Directory.CreateDirectory(@"result");
            String 結果 = "";
            for (int i = 0; i < 実験体数; i++)
                for (int j = 0; j < パラメータ数 + 1; j++)
                {
                    結果 += 遺伝子[i, j] + ",";
                    if (j == パラメータ数) 結果 += "\n";
                }
            using (StreamWriter w = new StreamWriter(@"result\" + ファイル名 + ".csv"))
            {
                w.Write(結果);
                w.Dispose();
            }
            
        }
        private int[] ランダムに遺伝子1つ作成(int[,] パラメータ)
        {
            //Int32と同じサイズのバイト配列にランダムな値を設定する
            //byte[] bs = new byte[sizeof(int)];
            byte[] bs = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng =new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bs);
            //Int32に変換する
            int seed = System.BitConverter.ToInt32(bs, 0);
            // そのseedを基にRandomを作る
            var r = new Random(seed++);

            int[] gene = new int[パラメータ.Length / 2 + 1];

            for (int i = 0; i < パラメータ.Length / 2; i++)
            {
                gene[i] = r.Next(パラメータ[i, 0], パラメータ[i, 1] + 1);
            }
            bs = null;
            rng.Dispose();
            r = null;
            return gene;
        }
        private int[] 親からランダムな交叉(int[] geneA, int[] geneB, int パラメータ数)
        {
            //Int32と同じサイズのバイト配列にランダムな値を設定する
            //byte[] bs = new byte[sizeof(int)];
            byte[] bs = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bs);
            //Int32に変換する
            int seed = System.BitConverter.ToInt32(bs, 0);
            // そのseedを基にRandomを作る
            var r = new Random(seed++);

            int[] gene = new int[パラメータ数 + 1];

            for (int i = 0; i < パラメータ数; i++)
            {
                if (r.Next(2) == 1) gene[i] = geneA[i];
                else gene[i] = geneB[i];
            }
            bs = null;
            rng.Dispose();
            r = null;
            return gene;
        }

        private int[] ランダムな突然変異(int[] original, int[,] パラメータ)
        {
            //Int32と同じサイズのバイト配列にランダムな値を設定する
            //byte[] bs = new byte[sizeof(int)];
            byte[] bs = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bs);
            //Int32に変換する
            int seed = System.BitConverter.ToInt32(bs, 0);
            // そのseedを基にRandomを作る
            var r = new Random(seed++);

            int 個数 = r.Next(1,パラメータ.Length / 2 + 1);
            int[] gene = new int[パラメータ.Length / 2 + 1];
            //重複しないように変更するパラメータを選ぶ
            HashSet<int> 変更するパラメータ = new HashSet<int>();
            while (変更するパラメータ.Count < 個数) 変更するパラメータ.Add(r.Next(パラメータ.Length / 2));

            if (パラメータ[0, 0] == 0)//スキップするとき
            {
                個数 = r.Next(1, パラメータ.Length / 2 + 1 - 2);
                変更するパラメータ.Clear();
                while (変更するパラメータ.Count < 個数) 変更するパラメータ.Add(r.Next(2,パラメータ.Length / 2));
            }

            for (int j = 0; j < パラメータ.Length / 2; j++)
            {
                if (変更するパラメータ.Contains(j))
                {
                    //System.Diagnostics.Debug.Write(j + ",");
                    gene[j] = r.Next(パラメータ[j, 0], パラメータ[j, 1] + 1);
                }
                else gene[j] = original[j];
                
            }
            変更するパラメータ = null;
            bs = null;
            rng.Dispose();
            r = null;
            return gene;
        }
        private int[,] 次の局所集団を作成(Mat[] テンプレート,Mat[] 合成用素材,Mat[] 合成画像,int[][,] 正解座標,int[,] group, int[,] パラメータ)
        {

            int[] 親の大きい方 = new int[パラメータ.Length / 2 + 1];
            int[] 親の小さい方 = new int[パラメータ.Length / 2 + 1];
            int[] 子の大きい方 = new int[パラメータ.Length / 2 + 1];
            int[] 子の小さい方 = new int[パラメータ.Length / 2 + 1];
            int[] 新子供1 = new int[パラメータ.Length / 2 + 1];
            int[] 新子供2 = new int[パラメータ.Length / 2 + 1];
            int[,] next_group = new int[4, パラメータ.Length / 2 + 1];

            //パラメータから点数を評価
            for (int i = 0; i < 4; i++)
            {
                group[i, パラメータ.Length / 2] = 評価結果(テンプレート,合成用素材,合成画像,正解座標,group[i, 0], group[i, 1], group[i, 2], group[i, 3], group[i, 4]);
                //System.Diagnostics.Debug.WriteLine("" + group[i, 0] + "," + group[i, 1] + "," + group[i, 2] + "," + group[i, 3] + "," + group[i, 4] + "," + group[i, 5]);
            }
            if (group[0, パラメータ.Length / 2] > group[1, パラメータ.Length / 2])
                for (int i = 0; i < パラメータ.Length / 2; i++)
                {
                    親の大きい方[i] = group[0, i];
                    親の小さい方[i] = group[1, i];
                }
            else
                for (int i = 0; i < パラメータ.Length / 2; i++)
                {
                    親の大きい方[i] = group[1, i];
                    親の小さい方[i] = group[0, i];
                }
            if (group[2, パラメータ.Length / 2] > group[3, パラメータ.Length / 2])
                for (int i = 0; i < パラメータ.Length / 2; i++)
                {
                    子の大きい方[i] = group[2, i];
                    子の小さい方[i] = group[3, i];
                }
            else
                for (int i = 0; i < パラメータ.Length / 2; i++)
                {
                    子の大きい方[i] = group[3, i];
                    子の小さい方[i] = group[2, i];
                }

            //子2個体がともに親の2個体より良かった場合（ケースA）は、子2個体及び適応度の良かった方の親個体計3個体が局所集団に戻し，そのうち二体を親として選ぶ。
            if ((group[2, パラメータ.Length / 2] >= 親の大きい方[パラメータ.Length / 2]) && (group[3, パラメータ.Length / 2] >= 親の大きい方[パラメータ.Length / 2]))
            {
                //Int32と同じサイズのバイト配列にランダムな値を設定する
                //byte[] bs = new byte[sizeof(int)];
                byte[] bs = new byte[4];
                System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
                rng.GetBytes(bs);
                //Int32に変換する
                int seed = System.BitConverter.ToInt32(bs, 0);
                // そのseedを基にRandomを作る
                var r = new Random(seed++);
                int number = r.Next(3);


                //局所集団から2個体（親）をランダムに取り出し、ランダムな多点交叉を行なう
                if (number == 2)
                {
                    新子供1 = 親からランダムな交叉(子の大きい方, 子の小さい方, パラメータ.Length / 2);
                    新子供2 = 親からランダムな交叉(子の大きい方, 子の小さい方, パラメータ.Length / 2);
                    for (int i = 0; i < パラメータ.Length / 2; i++)
                    {
                        next_group[0, i] = 子の大きい方[i];
                        next_group[1, i] = 子の小さい方[i];
                        next_group[2, i] = 新子供1[i];
                    }
                }
                if (number == 1)
                {
                    新子供1 = 親からランダムな交叉(子の大きい方, 親の大きい方, パラメータ.Length / 2);
                    新子供2 = 親からランダムな交叉(子の大きい方, 親の大きい方, パラメータ.Length / 2);
                    for (int i = 0; i < パラメータ.Length / 2; i++)
                    {
                        next_group[0, i] = 子の大きい方[i];
                        next_group[1, i] = 親の大きい方[i];
                        next_group[2, i] = 新子供1[i];
                    }
                }
                else
                {
                    新子供1 = 親からランダムな交叉(子の小さい方, 親の大きい方, パラメータ.Length / 2);
                    新子供2 = 親からランダムな交叉(子の小さい方, 親の大きい方, パラメータ.Length / 2);
                    for (int i = 0; i < パラメータ.Length / 2; i++)
                    {
                        next_group[0, i] = 子の小さい方[i];
                        next_group[1, i] = 親の大きい方[i];
                        next_group[2, i] = 新子供1[i];
                    }

                }
                //交叉によって生成された2個体（子）のうち1個体をランダムに選択し、ランダムな数と位置において突然変異を行なう。
                新子供2 = ランダムな突然変異(新子供2, パラメータ);
                for (int i = 0; i < パラメータ.Length / 2; i++)next_group[3, i] = 新子供2[i];

                bs = null;
                rng.Dispose();
                r = null;

            }
            //子2個体がともに親の2個体より悪かった場合（ケースB）は、親2個体のうち良かった方のみが局所集団に戻り、局所集団数は1減少する。
            else if (子の大きい方[パラメータ.Length / 2] <= 親の小さい方[パラメータ.Length / 2])
            {
                //全探索空間から1個体をランダムに取り出し、これを局所集団に追加する。
                int[] 新親 = ランダムに遺伝子1つ作成(パラメータ);

                新子供1 = 親からランダムな交叉(親の大きい方, 新親, パラメータ.Length / 2);
                新子供2 = 親からランダムな交叉(親の大きい方, 新親, パラメータ.Length / 2);
                新子供2 = ランダムな突然変異(新子供2, パラメータ);
                for (int i = 0; i < パラメータ.Length / 2; i++)
                {
                    next_group[0, i] = 親の大きい方[i];
                    next_group[1, i] = 新親[i];
                    next_group[2, i] = 新子供1[i];
                    next_group[3, i] = 新子供2[i];
                }
            }
            //親2個体のうちどちらか一方のみが子2個体より良かった場合（ケースC）は、親2個体のうち良かった方と子2個体のうち良かった方が局所集団に戻り、局所集団数は変化しない。
            else if ((親の大きい方[パラメータ.Length / 2] >= 子の大きい方[パラメータ.Length / 2])
                && (親の小さい方[パラメータ.Length / 2] <= 子の大きい方[パラメータ.Length / 2]))
            {
                新子供1 = 親からランダムな交叉(親の大きい方, 子の大きい方, パラメータ.Length / 2);
                新子供2 = 親からランダムな交叉(親の大きい方, 子の大きい方, パラメータ.Length / 2);
                新子供2 = ランダムな突然変異(新子供2, パラメータ);
                for (int i = 0; i < パラメータ.Length / 2; i++)
                {
                    next_group[0, i] = 親の大きい方[i];
                    next_group[1, i] = 子の大きい方[i];
                    next_group[2, i] = 新子供1[i];
                    next_group[3, i] = 新子供2[i];
                }
            }
            //子2個体のうちどちらか一方のみが親2個体より良かった場合（ケースD）は、子2個体のうち良かった方のみが局所集団に戻り、全探索空間からランダムに1個体選んで局所集団に追加
            else if ((子の大きい方[パラメータ.Length / 2] >= 親の大きい方[パラメータ.Length / 2])
                && (子の小さい方[パラメータ.Length / 2] <= 親の大きい方[パラメータ.Length / 2]))
            {
                //全探索空間から1個体をランダムに取り出し、これを局所集団に追加する。
                int[] 新親 = ランダムに遺伝子1つ作成(パラメータ);

                新子供1 = 親からランダムな交叉(子の大きい方, 新親, パラメータ.Length / 2);
                新子供2 = 親からランダムな交叉(子の大きい方, 新親, パラメータ.Length / 2);
                新子供2 = ランダムな突然変異(新子供2, パラメータ);
                for (int i = 0; i < パラメータ.Length / 2; i++)
                {
                    next_group[0, i] = 親の大きい方[i];
                    next_group[1, i] = 子の大きい方[i];
                    next_group[2, i] = 新子供1[i];
                    next_group[3, i] = 新子供2[i];
                }
            }
            else System.Diagnostics.Debug.WriteLine("newPattern?");
            return next_group;
        }
        private int 評価結果(Mat[] テンプレート,Mat[] 合成用素材,Mat[] 合成画像,int[][,] 正解座標,int p1, int p2, int p3, int p4, int p5)
        {
            MyCV MyCv = new MyCV();

            int 検査面数 = テンプレート.Length;
            Mat[] dst = new Mat[検査面数];
            int[] scores = new int[検査面数];
            int width = テンプレート[0].Width;
            int height = テンプレート[0].Height;

            if (合成用素材 != null)
            {
                for (int num = 0; num < 合成用素材.Length; num++)
                {
                    if (num % 4 == 3)
                    {
                        dst[num / 4] = new Mat(height, width, MatType.CV_8UC1);
                        Mat[] images = new Mat[4];
                        for (int i = 0; i < 4; i++) images[i] = 合成用素材[num - i].Clone();
                        MyCv.自作反射光除去(images, ref dst[num / 4]);
                        MyCv.コントラスト調整(ref dst[num / 4], (double)(p1 / 10.0));
                        MyCv.明るさ調整(ref dst[num / 4], (double)p2);
                        for (int i = 0; i < images.Length; i++) images[i].Dispose();
                    }
                }
            }
            else dst = (Mat[])合成画像.Clone();
           

            for (int i = 0; i < 検査面数; i++)
            {
                MyCv.TopHat(dst[i].Clone(), ref dst[i], p3, p4);
                MyCv.二値化(ref dst[i], p5);
                MyCv.評価用画像作成(テンプレート[i], dst[i].Clone(), ref dst[i]);
                scores[i] = MyCv.点数計算(dst[i], 正解座標[i]);
            }
            
            for(int i=0;i<dst.Length;i++)dst[i].Dispose();
            MyCv = null;
            return (int)scores.Average();
        }
        private void グラフデータを出力(List<int[]> data, string ファイル名)
        {
            System.IO.Directory.CreateDirectory(@"result");
            String 結果 = "";
            foreach (int[] score in data)
            {
                for (int i = 0; i < score.Length; i++) 結果 += score[i] + ",";
                結果 +="\n";
            }
            using (StreamWriter w = new StreamWriter(@"result\" + ファイル名 + ".csv"))
            {
                w.Write(結果);
                w.Dispose();
            }
        }
    }

}
