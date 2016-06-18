using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Blob;

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
        public void 評価結果画像作成_debug(Mat 検査結果,Mat テンプレート,int[,] 正解座標,ref Mat color_debug)
        {
            
            Mat res_color  = new Mat(new Size(検査結果.Width, 検査結果.Height), MatType.CV_8UC3, Scalar.All(0));
            var temp_color = res_color.Clone();

            CvBlobs blobs = new CvBlobs(検査結果);
            int score=0;

            blobs.FilterByArea(Main.FilterByArea[0], Main.FilterByArea[1]);
            blobs.RenderBlobs(検査結果, res_color);

            
            Cv2.CvtColor(テンプレート, temp_color, ColorConversionCodes.GRAY2BGR);
            Cv2.Add(temp_color, res_color, color_debug);

            点数計算_debug(blobs, 正解座標, ref color_debug, ref score);
            
            res_color.Dispose();
            temp_color.Dispose();
            blobs = null;

        }

        public void 点数計算_debug(CvBlobs blobs, int[,] 正解座標, ref Mat color, ref int score)
        {
            if (正解座標 != null)
            {
                
                int[,] 正解座標2 = (int[,])正解座標.Clone();
                int 正解数 = 0;
                int 不正回数 = 0;

                foreach (CvBlob item in blobs.Values)
                {
                    CvContourPolygon polygon = item.Contour.ConvertToPolygon();
                    Point2f circleCenter;
                    float circleRadius;

                    GetEnclosingCircle(polygon, out circleCenter, out circleRadius);
                    for (int j = 0; j < 正解座標2.Length / 2; j++)
                    {
                        if (正解座標2[j, 0] != 0 && (Math.Pow(circleCenter.X - 正解座標2[j, 0], 2) + Math.Pow(circleCenter.Y - 正解座標2[j, 1], 2) < circleRadius * circleRadius))
                        {//外接円内にあったら
                            Cv2.Circle(color, item.Centroid, (int)circleRadius, new Scalar(0, 0, 255), 2);
                            正解数++;
                            正解座標2[j, 0] = 正解座標2[j, 1] = 0;
                            j = 正解座標2.Length;//ひとつ照合確定したら，このfor文を抜けて次のラベルの検査に移動
                        }
                    }
                }

                System.Diagnostics.Debug.WriteLine("未検出座標");
                for (int i = 0; i < 正解座標2.Length / 2; i++)
                {//検出されなかった座標が残る
                    if (正解座標2[i, 0] != 0) System.Diagnostics.Debug.WriteLine(i + ":" + 正解座標2[i, 0] + "," + 正解座標2[i, 1]);
                }

                不正回数 = blobs.Count - 正解数;

                score = (int)((float)(正解数 - 不正回数) * (10000.0f / (正解座標.Length / 2)));

                Cv2.PutText(color, "score= " + score.ToString(), new Point(10, 120), HersheyFonts.HersheySimplex,1, new Scalar(0, 0, 0));
            }
        }

        public int 点数計算(Mat 検査結果, int[,] 正解座標)
        {
            
            int score = 0;
            CvBlobs blobs = new CvBlobs(検査結果);
            blobs.FilterByArea(Main.FilterByArea[0], Main.FilterByArea[1]);

            int[,] 正解座標2 = (int[,])正解座標.Clone();
            int 正解数 = 0;
            int 不正回数 = 0;

            foreach (CvBlob item in blobs.Values)
            {
                CvContourPolygon polygon = item.Contour.ConvertToPolygon();
                Point2f circleCenter;
                float circleRadius;

                GetEnclosingCircle(polygon, out circleCenter, out circleRadius);
                for (int j = 0; j < 正解座標2.Length / 2; j++)
                {
                    if (正解座標2[j, 0] != 0 && (Math.Pow(circleCenter.X - 正解座標2[j, 0], 2) + Math.Pow(circleCenter.Y - 正解座標2[j, 1], 2) < circleRadius * circleRadius))
                    {//外接円内にあったら
                        正解数++;
                        正解座標2[j, 0] = 正解座標2[j, 1] = 0;
                        j = 正解座標2.Length;//ひとつ照合確定したら，このfor文を抜けて次のラベルの検査に移動
                    }
                }
            }

            不正回数 = blobs.Count - 正解数;

            score = (int)((float)(正解数 - 不正回数) * (10000.0f / (正解座標.Length / 2)));
            blobs = null;
            return score;
        }
        public void GetEnclosingCircle(IEnumerable<Point> points, out Point2f center, out float radius)
        {
            var pointsArray = points.ToArray();
            using (var pointsMat = new Mat(pointsArray.Length, 1, MatType.CV_32SC2, pointsArray))
            {
                Cv2.MinEnclosingCircle(pointsMat, out center, out radius);
            }
        }
    }

}
