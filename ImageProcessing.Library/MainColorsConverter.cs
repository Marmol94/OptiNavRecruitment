using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImageProcessing.Library
{
    public class MainColorsConverter : IMainColorsConverter
    {
        public Bitmap ToMainColorsConverter(Bitmap source)
        {
            var result = new Bitmap(source.Width, source.Height);

            var sourceData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var resultData = result.LockBits(new Rectangle(0, 0, result.Width, result.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            var buffer = new byte[resultData.Stride * result.Height];
            Marshal.Copy(sourceData.Scan0, buffer, 0, buffer.Length);
            source.UnlockBits(sourceData);
            for (var i = 0; i < buffer.Length; i += 4)
            {
                var indexOfDominant = Array.IndexOf(buffer,
                    Math.Max(buffer[i], Math.Max(buffer[i + 1], buffer[i + 2])), i, 3);
                Array.Copy(buffer, i, buffer, i, 4);
                buffer[i] = 0;
                buffer[i + 1] = 0;
                buffer[i + 2] = 0;
                buffer[indexOfDominant] = byte.MaxValue;
            }

            Marshal.Copy(buffer, 0, resultData.Scan0, buffer.Length);
            result.UnlockBits(resultData);
            return result;
        }
    }
}