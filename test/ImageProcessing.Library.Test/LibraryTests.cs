using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ImageProcessingLibraryTest
{
    public class LibraryTests
    {
        private ImageProcessing.Library.ImageProcessing _imageProcessor;
        private MemoryStream _memoryStream;
        [SetUp]
        public void Setup()
        {
            _imageProcessor = new ImageProcessing.Library.ImageProcessing();
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
            var testImage = CreateBitmap(r,g,b,1,1);
            testImage.Save(_memoryStream,ImageFormat.Bmp);
            var bingraph = new ImageProcessing.Library.Image(_memoryStream);
            var resultImage = _imageProcessor.ToMainColors(bingraph);
            var resultBitmap = new Bitmap(resultImage.Value);
            var expectedBitmap = CreateBitmap(rExp, gExp, bExp,1,1);
            Assert.AreEqual(expectedBitmap.GetPixel(0,0),resultBitmap.GetPixel(0,0));
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
        public async Task TestBoostColorsAsync(int r, int g, int b, int rExp, int gExp, int bExp)
        {
            var testImage = CreateBitmap(r,g,b,1,1);
            testImage.Save(_memoryStream,ImageFormat.Bmp);
            var bingraph = new ImageProcessing.Library.Image(_memoryStream);
            var resultImage = await _imageProcessor.ToMainColorsAsync(bingraph);
            var resultBitmap = new Bitmap(resultImage.Value);
            var expectedBitmap = CreateBitmap(rExp, gExp, bExp,1,1);
            Assert.AreEqual(expectedBitmap.GetPixel(0,0),resultBitmap.GetPixel(0,0));
        }
        
        [Test]
        [TestCase (200,200)]
        [TestCase (10,111120)]
        [TestCase (1123123,1)]
        [TestCase (12313,12331)]
        public void BoostMainColorsSmokeTest(int height, int width)
        {
            
            var testBitmap = CreateBitmap(1, 1, 1, height, width);
            testBitmap.Save(_memoryStream,ImageFormat.Bmp);
            var binGraphic = new ImageProcessing.Library.Image(_memoryStream);
            Assert.DoesNotThrow(()=>_imageProcessor.ToMainColors(binGraphic));
        }
        
        [Test]
        [TestCase (200,200)]
        [TestCase (10,111120)]
        [TestCase (1123123,1)]
        [TestCase (12313,12331)]
        public async Task BoostMainColorsSmokeTestAsync(int height, int width)
        {
            var testBitmap = CreateBitmap(1, 1, 1, height, width);
            testBitmap.Save(_memoryStream,ImageFormat.Bmp);
            var binGraphic = new ImageProcessing.Library.Image(_memoryStream);
            Assert.DoesNotThrowAsync(async ()=> await _imageProcessor.ToMainColorsAsync(binGraphic));
        }
        private Bitmap CreateBitmap(int red, int green, int blue, int height, int width)
        {
            Bitmap flag = new Bitmap(height, width);
            flag.SetPixel(0,0,Color.FromArgb(red, green, blue));
            return flag;
        }
    }
}