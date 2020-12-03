using System;
using System.IO;
using System.Linq;

namespace ImageProcessingLibrary
{
    public class ExtensionIdentifier
    {
        public static Enum Identify(byte[] image)
        {
            var pngMagicNumber = new byte[] {0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A};
            var bmpMagicNumber = new byte[] {0x42, 0x4D};
            var jpgMagicNumber = new byte[] {0xFF, 0xD8};
            var isPng = image.Take(8).ToArray().SequenceEqual(pngMagicNumber);
            var isBmp = image.Take(2).ToArray().SequenceEqual(bmpMagicNumber);
            var isJpg = image.Take(2).ToArray().SequenceEqual(jpgMagicNumber);

            if (isPng)
                return Extensions.Png;

            if (isBmp)
                return Extensions.Bmp;

            if (isJpg)
                return Extensions.Jpg;

            return Extensions.Unknown;
        }
    }
    public enum Extensions
    {
        Unknown,
        Png,
        Jpg,
        Bmp
    }
}