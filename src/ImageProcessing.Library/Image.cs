using System.IO;

namespace ImageProcessing.Library
{
    public class Image
    {
        public Image(Stream value) => Value = value;

        public Stream Value { get; }
    }
}