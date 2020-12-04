using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessingLibrary
{
    public class ImageProcessing
    {
        public Image ReadFile(string readPath)
        {
            return Bitmap.FromFile(readPath);
        }
        public Bitmap ToMainColors(Image imageToConvert)
        {
            var imageConverter = new MainColorsConverter();
            return  imageConverter.ToMainColorsConverter(imageToConvert);
        }

        public void SaveFile(string savePath, Image image)
        {
            image.Save(savePath, ImageFormat.Png);
        }
    }
}