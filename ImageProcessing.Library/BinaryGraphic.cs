using System.IO;

namespace ImageProcessing.Library
{
    public class BinaryGraphic
    {
        public BinaryGraphic(Stream value) => Value = value;

        public Stream Value { get; }
    }
}