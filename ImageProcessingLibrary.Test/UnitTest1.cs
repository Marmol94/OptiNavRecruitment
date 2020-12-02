using System;
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
        public void IsPng()
        {
            var png = File.ReadAllBytes("Szreki/Shrek.png");
            var result = ExtensionIdentifier.Identify(png);
            Assert.AreEqual(result, Extensions.Png);
        }
        [Test]
        public void IsJpg()
        {
            var jpg = File.ReadAllBytes("Szreki/Shrek.jpg");
            var result = ExtensionIdentifier.Identify(jpg);
            Assert.AreEqual(result, Extensions.Jpg);
        }
        [Test]
        public void IsBmp()
        {
            var bmp = File.ReadAllBytes("Szreki/Shrek.bmp");
            var result = ExtensionIdentifier.Identify(bmp);
            Assert.AreEqual(result, Extensions.Bmp);
        }
        [Test]
        public void IsUnknown()
        {
            var unknown = new byte[] {0x00, 0x00, 0x00, 0x00};
            var result = ExtensionIdentifier.Identify(unknown);
            Assert.AreEqual(result, Extensions.Unknown);
        }
    }
}