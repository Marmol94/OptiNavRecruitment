using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using ImageProcessing.WPF;
using NUnit.Framework;

namespace ImageProcessingLibraryTest
{
    public class WPFTests
    {
        private MainWindowViewModel _mainWindowVM;
        private ImageProcessing.Library.ImageProcessing _imageProcessingService;
        private MemoryStream _memoryStream;
        [SetUp]
        public void Setup()
        {
            _imageProcessingService = new ImageProcessing.Library.ImageProcessing();
            _mainWindowVM = new MainWindowViewModel(_imageProcessingService);
            _memoryStream = new MemoryStream();
            
        }
        [Test]
        [TestCase(200,100,12, 255,0,0, TestName = "Max Red")]
        [TestCase(150,100,200,0,0,255, TestName = "Max Blue")]
        [TestCase(14,33, 23, 0,255,0, TestName = "Max Green")]
        [TestCase(0,0, 0, 0,0,255, TestName = "All 0")]
        [TestCase(255,255, 255, 0,0,255, TestName = "All Max")]
        [TestCase(20,20, 0, 0,255,0, TestName = "Red and Green even")]
        [TestCase(0,20, 20, 0,0,255, TestName = "Blue and Green even")]
        [TestCase(20,0, 20, 0,0,255, TestName = "Red and Blue even")]
        public void TestBoostColors(int r, int g, int b, int rExp, int gExp, int bExp)
        {
            var bitmap = CreateBitmap(r, g, b, 1, 1);
            bitmap.Save(_memoryStream, ImageFormat.Bmp);
            _mainWindowVM.LoadedImage = _memoryStream;
            _mainWindowVM.ConvertCommand.Execute(null);
            var result = _mainWindowVM.ConvertedImage;
            var resultBitmap = new Bitmap(result);
            var expected = CreateBitmap(rExp, gExp, bExp,1,1);
            Assert.AreEqual(expected.GetPixel(0, 0), resultBitmap.GetPixel(0, 0));
        }
        
        private Bitmap CreateBitmap(int red, int green, int blue, int height, int width)
        {
            Bitmap flag = new Bitmap(height, width);
            flag.SetPixel(0,0,Color.FromArgb(red, green, blue));
            return flag;
        }

        
    }
}