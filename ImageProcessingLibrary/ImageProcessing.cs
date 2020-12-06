using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessingLibrary
{
    public class ImageProcessing
    {
        public Bitmap ReadFile(string readPath)
        {
            return new Bitmap(readPath);
        }
        public Bitmap ToMainColors(Bitmap imageToConvert)
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