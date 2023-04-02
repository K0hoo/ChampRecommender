using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using static ChampRecommender.Models.GameManager;


namespace ChampRecommender.Models
{
    static class ImageSeachClass
    {
        [DllImport("ImageSearchDLL.dll")]
        private static extern IntPtr ImageSearch(int x, int y, int right, int bottom, [MarshalAs(UnmanagedType.LPTStr)] string imagePath);

        private static readonly int screenWindowX = 1920;
        private static readonly int screenWindowY = 1080;

        public static async Task<int[]> WaitCatchGame()
        {
            while (true)
            {
                int[] result = ImgSearch.UseImageSearch(0, 0, screenWindowX, screenWindowY, @"Image/SelectChampion.png");
            }
        }
    }

    public class API
    {
        [DllImport("ImageSearchDLL.dll")]
        public static extern IntPtr ImageSearch(int x, int y, int right, int bottom, [MarshalAs(UnmanagedType.LPStr)] string imagePath);

        [DllImport("ImageSearchDLL.dll")]
        public static extern IntPtr ImageSearch_img(int x, int y, int right, int bottom, [MarshalAs(UnmanagedType.LPStr)] string bimagePath, [MarshalAs(UnmanagedType.LPStr)] string imagePath);
    }

    public class ImgSearch
    {
        public static int[] UseImageSearch(int VecX, int VecY, int VecX2, int VecY2, string imgPath)
        {
            IntPtr result = API.ImageSearch(VecX, VecY, VecX2, VecY2, imgPath);
            string res = Marshal.PtrToStringAnsi(result);

            if (res[0] == '0')
            {
                return null;
            }
            //찾지 못함

            string[] data = res.Split('|'); //0->찾음, 1->x, 2->y, 3->이미지 넓이, 4->이미지 높이;        

            int[] parse = new int[2];

            int.TryParse(data[1], out parse[0]);
            int.TryParse(data[2], out parse[1]);

            return parse; //x, y 좌표 반환
        }

        public static int[] UseImageSearch_img(int VecX, int VecY, int VecX2, int VecY2, string aImage, string imgPath)
        {
            IntPtr result = API.ImageSearch_img(VecX, VecY, VecX2, VecY2, aImage, imgPath);
            string res = Marshal.PtrToStringAnsi(result);

            //res에 이미지서치 결과 반환
            //실패할시 0, 찾았을 시엔 1|x|y|넓이|높이 반환

            if (res[0] == '0') return null;//찾지 못함


            string[] data = res.Split('|'); //찾은 결과값에서 x, y 좌표 추출을 위해 스피릿
                                            //0->찾음, 1->x, 2->y, 3->이미지 넓이, 4->이미지 높이;        


            int[] parse = new int[2];

            int.TryParse(data[1], out parse[0]);
            int.TryParse(data[2], out parse[1]);

            return parse; //x, y 좌표 반환
        }
    }
}
