using System.Drawing;

namespace ImageProcessingLibrary
{
    public class MainColorsConverter : IMainColorsConverter
    {
        public Bitmap ToMainColorsConverter(Image bitmapToConvert)
        {
            var convertedBitmap = new Bitmap(bitmapToConvert);
            var pixel = new Color();
            for (var i = 0; i < convertedBitmap.Height; i++)
            {
                for (var j = 0; j < convertedBitmap.Width; j++)
                {
                    pixel = convertedBitmap.GetPixel(j, i);
                    if (pixel.R > pixel.G && pixel.R > pixel.B)
                    {
                        convertedBitmap.SetPixel(j, i, Color.Red);
                    }
                    else if (pixel.G > pixel.R && pixel.G > pixel.B)
                    {
                        convertedBitmap.SetPixel(j,i,Color.Green);
                    }
                    else if (pixel.B > pixel.R && pixel.B > pixel.G)
                    {
                        convertedBitmap.SetPixel(j,i, Color.Blue);
                    }
                }
            }
            return convertedBitmap;
        }
    }
}