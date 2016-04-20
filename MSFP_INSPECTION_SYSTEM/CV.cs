using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace MSFP_INSPECTION_SYSTEM
{
    class CV
    {
        public void 自作反射光除去(Mat[] images, ref Mat DST)
        {
            int width = images[0].Width;
            int height = images[0].Height;
            //var indexer = DST.GetGenericIndexer<Vec3b>();

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {//medianX,Y SG完成
                    Vec3b color = DST.Get<Vec3b>(y, x);//indexer[y, x];
                    double[] vals = { 0, 0, 0, 0 };
                    for (int num = 0; num < 4; num++) vals[num] = images[num].At<Vec3b>(y, x).Item0;
                    Array.Sort(vals);//並び替えを行う．min=vals[0]
                    color.Item0 = (byte)((vals[0] + vals[1] + vals[2]) / 3.0);
                    DST.Set<Vec3b>(y, x, color);
                    //indexer[y, x] = color;

                }

        }

        public void コントラスト調整(ref Mat src, double 倍率)
        {
            int width = src.Width;
            int height = src.Height;
            //var indexer = src.GetGenericIndexer<Vec3b>();
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    Vec3b color = src.Get<Vec3b>(y, x);//indexer[y, x];
                    double val = color.Item0 * 倍率;
                    if (val > 255) color.Item0 = 255;
                    else if (val < 0) color.Item0 = 0;
                    else color.Item0 = (byte)val;
                    src.Set<Vec3b>(y, x, color);//indexer[y, x] = color;
                }

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

            vals[0] = img.At<Vec3b>(center_y - 10, center_x - 10).Item0; vals[3] = img.At<Vec3b>(center_y - 10, center_x).Item0; vals[6] = img.At<Vec3b>(center_y - 10, center_x + 10).Item0;
            vals[1] = img.At<Vec3b>(center_y, center_x - 10).Item0; vals[4] = img.At<Vec3b>(center_y, center_x).Item0; vals[7] = img.At<Vec3b>(center_y, center_x + 10).Item0;
            vals[2] = img.At<Vec3b>(center_y + 10, center_x - 10).Item0; vals[5] = img.At<Vec3b>(center_y + 10, center_x).Item0; vals[8] = img.At<Vec3b>(center_y + 10, center_x + 10).Item0;

            for (int num = 0; num < 9; num++) average += vals[num];
            average = average / 9.0;
            diff = 目標 - average;

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    Vec3b color = img.Get<Vec3b>(y, x);//indexer[y, x];
                    double val = color.Item0 + diff;
                    if (val > 255) color.Item0 = 255;
                    else if (val < 0) color.Item0 = 0;
                    else color.Item0 = (byte)val;
                    img.Set<Vec3b>(y, x, color);//indexer[y, x] = color;
                }
        }


    }
}
