using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageProcessing.MVVM
{
    public class ImageProcessingService
    {
        public System.Drawing.Image ImageWpfToGDI(ImageSource image) {
            MemoryStream ms = new MemoryStream();
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image as BitmapSource));
            encoder.Save(ms);
            ms.Flush();      
            return System.Drawing.Image.FromStream(ms);
        }
        public ImageSource ConvertToMainColors(ImageSource imageC)
        {
            var processor = new Library.ImageProcessing();
            // var imageToMainColors = processor.ToMainColors(ImageWpfToGDI(imageC));
            // imageC = ConvertGdiToWpf(imageToMainColors);
            return imageC;
        }

        public MemoryStream ConvertToStream(ImageSource imageC)
        {
            var memStream = new MemoryStream();
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageC as BitmapSource));
            encoder.Save(memStream);
            return memStream;
        }
        
        public  ImageSource ConvertGdiToWpf(System.Drawing.Image image)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            memoryStream.Seek(0, SeekOrigin.Begin);
            bitmap.StreamSource = memoryStream;
            bitmap.EndInit();
            return bitmap;
        }
    }
}