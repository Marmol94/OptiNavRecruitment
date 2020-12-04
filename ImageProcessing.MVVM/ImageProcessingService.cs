using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageProcessing.MVVM
{
    public class ImageProcessingService
    {
        public System.Drawing.Image ImageWpfToGDI(System.Windows.Media.ImageSource image) {
            MemoryStream ms = new MemoryStream();
            var encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();
            encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(image as System.Windows.Media.Imaging.BitmapSource));
            encoder.Save(ms);
            ms.Flush();      
            return System.Drawing.Image.FromStream(ms);
        }
        public ImageSource ConvertToMainColors(ImageSource imageC)
        {
            var processor = new ImageProcessingLibrary.ImageProcessing();
            var imageToMainColors = processor.ToMainColors(ImageWpfToGDI(imageC));
            imageC = ConvertImage(imageToMainColors);
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
        
        public  System.Windows.Media.ImageSource ConvertImage(System.Drawing.Image image)
        {
            var bitmap = new System.Windows.Media.Imaging.BitmapImage();
            bitmap.BeginInit();
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
            bitmap.StreamSource = memoryStream;
            bitmap.EndInit();
            return bitmap;
        }
    }
}