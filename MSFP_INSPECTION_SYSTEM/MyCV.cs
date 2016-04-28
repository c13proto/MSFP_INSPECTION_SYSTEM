using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace MSFP_INSPECTION_SYSTEM
{
    class MyCV
    {
        public void Zero(ref Mat src)
        {
            var indexer = new MatOfByte3(src).GetIndexer();
            for (int x = 0; x < src.Width; x++)
                for (int y = 0; y < src.Height; y++)
                {
                    Vec3b color = indexer[y, x];
                    color.Item0 = 0;
                    color.Item1 = 0;
                    color.Item2 = 0;
                    indexer[y, x] = color;
                }
            indexer = null;

        }
        public void 自作反射光除去(Mat[] images, ref Mat DST)
        {
            int width = images[0].Width;
            int height = images[0].Height;

            MatIndexer<Vec3b>[] indexers = new MatIndexer<Vec3b>[4];
            var indexer = new MatOfByte3(DST).GetIndexer();

            for (int i = 0; i < 4; i++) indexers[i] = new MatOfByte3(images[i]).GetIndexer();//images[i].GetGenericIndexer<Vec3b>();

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    Vec3b[] colors = new Vec3b[4];
                    Vec3b color = indexer[y, x];
                    for (int i = 0; i < 4; i++)colors[i] = indexers[i][y, x];
                    double[] vals = { 0, 0, 0, 0 };
                    for (int num = 0; num < 4; num++) vals[num] = colors[num].Item0;
                    Array.Sort(vals);//並び替えを行う．min=vals[0]
                    color.Item0 = (byte)((vals[0] + vals[1] + vals[2]) / 3.0);
                    indexer[y, x] = color;

                    colors = null;
                }
            indexers = null;
            indexer = null;
        }

        public void コントラスト調整(ref Mat src, double 倍率)
        {
            int width = src.Width;
            int height = src.Height;
            
            var indexer = new MatOfByte3(src).GetIndexer();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vec3b color = indexer[y, x];
                    double val = color.Item0 * 倍率;
                    if (val > 255) color.Item0 = 255;
                    else if (val < 0) color.Item0 = 0;
                    else color.Item0 = (byte)val;
                    indexer[y, x] = color;
                }
            }
            indexer = null;
        }

        public void 明るさ調整(ref Mat img, double 目標)
        {//中心近くの9ピクセルから輝度調整

            int width = img.Width;
            int height = img.Height;
            int center_x = width / 5;
            int center_y = height / 5;
            //var indexer = img.GetGenericIndexer<Vec3b>();

            double[] vals = new double[9];
            double average = 0;
            double diff = 0;
            var indexer = new MatOfByte3(img).GetIndexer();

            vals[0] = indexer[center_y - 10, center_x - 10].Item0;  vals[3] = indexer[center_y - 10, center_x].Item0;   vals[6] = indexer[center_y - 10, center_x + 10].Item0;
            vals[1] = indexer[center_y, center_x - 10].Item0;       vals[4] = indexer[center_y, center_x].Item0;        vals[7] = indexer[center_y, center_x + 10].Item0;
            vals[2] = indexer[center_y + 10, center_x - 10].Item0;  vals[5] = indexer[center_y + 10, center_x].Item0;   vals[8] = indexer[center_y + 10, center_x + 10].Item0;

            for (int num = 0; num < 9; num++) average += vals[num];
            average = average / 9.0;
            diff = 目標 - average;
            
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    Vec3b color = indexer[y, x];//indexer[y, x];
                    double val = color.Item0 + diff;
                    if (val > 255) color.Item0 = 255;
                    else if (val < 0) color.Item0 = 0;
                    else color.Item0 = (byte)val;
                    indexer[y, x] = color;//indexer[y, x] = color;
                }
            indexer = null;
        }

        public void TopHat(Mat src,ref Mat dst,int size,int num)
        {
            dst = src.Clone();
            size = 2 * size + 1;
            var kernel = Cv2.GetStructuringElement(MorphShapes.Rect,new Size(size,size));
            Cv2.MorphologyEx(src,dst,MorphTypes.TopHat, kernel, null,num);
        }
        public void 二値化(ref Mat src, int val)
        {
            src = src.Threshold(val,255,ThresholdTypes.Binary);
        }

        public void 評価用画像作成(Mat テンプレート, Mat 検査結果,ref Mat dst)
        {

            Mat mask = テンプレート.Clone();//この時点では輪郭が白
            Zero(ref dst);

            Cv2.BitwiseNot(テンプレート, mask);//正解画像の輪郭を黒にする
            Cv2.BitwiseAnd(検査結果, 検査結果, dst,mask);

            mask.Dispose();
        }
        

    }
}
