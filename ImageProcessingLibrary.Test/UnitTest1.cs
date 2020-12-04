using System.IO;
using NUnit.Framework;

namespace ImageProcessingLibrary.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestToMainColors()
        {
            var imageProcessor = new ImageProcessing();
            var image=imageProcessor.ReadFile("Szreki/shrek.jpg");
            imageProcessor.SaveFile("Szreki/PrzetworzonySzrek.jpg", imageProcessor.ToMainColors(image));
            Assert.Pass();
        }
        [Test]
        public void TestMeasure()
        {
            var imageProcessor = new ImageProcessing();
            var image=imageProcessor.ReadFile("Szreki/shrek.bmp");
            var converted = MethodTimer.Measure(() => imageProcessor.ToMainColors(image));
            Assert.Pass();
        }
    }
}