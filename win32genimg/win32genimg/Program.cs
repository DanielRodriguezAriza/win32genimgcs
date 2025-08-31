using System.Drawing;
using System.Drawing.Imaging;
namespace win32genimg
{
    public class Program
    {
        public static ImageFormat GetFormatFromString(string s)
        {
            s = s.ToLower().Trim().TrimStart('.'); // pass to lower, trim whitespace and trim initial "." character if present.
            switch (s)
            {
                case "png":
                    return ImageFormat.Png;
                case "bmp":
                    return ImageFormat.Bmp;
                case "jpg":
                case "jpeg":
                    return ImageFormat.Jpeg;
                case "gif":
                    return ImageFormat.Gif;
                case "webp":
                    return ImageFormat.Webp;
                case "ico":
                case "icon":
                    return ImageFormat.Icon;
                default:
                    throw new Exception($"Unknwon format \"{s}\"");
            }
        }

        public static void GenerateImage(ImageFormat outFormat, string inFileName, string outFileName)
        {
            Bitmap bm = new Bitmap(inFileName);
            bm.Save(outFileName, outFormat);
            Console.WriteLine($"Generated image \"{outFileName}\" with format \"{outFormat.ToString()}\"");
        }

        public static void GenerateImage(string outFormat, string inFileName, string outFileName)
        {
            GenerateImage(GetFormatFromString(outFormat), inFileName, outFileName);
        }

        public static void GenerateImage(string inFileName, string outFileName)
        {
            ImageFormat outFormat = GetFormatFromString(Path.GetExtension(outFileName));
            GenerateImage(outFormat, inFileName, outFileName);
        }

        public static int Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.Error.WriteLine("Usage : win32genimg <input-file-name> <output-file-name> [output-format]");
                return 1;
            }
            try
            {
                if (args.Length < 3)
                {
                    GenerateImage(args[0], args[1]);
                }
                else
                {
                    GenerateImage(args[2], args[0], args[1]);
                }
            }
            catch(Exception e)
            {
                Console.Error.WriteLine($"Error: {e.Message}");
            }

            return 0;
        }
    }
}
