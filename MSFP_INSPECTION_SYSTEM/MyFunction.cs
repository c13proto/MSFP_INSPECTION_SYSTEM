using System;
using System.Collections.Generic;
using System.Linq;
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

        }
    }
}
