using System.IO;

namespace ImageProcessingLibrary
{
    public class BinaryGraphic
    {
        public BinaryGraphic(Stream value) => Value = value;

        public Stream Value { get; }
    }
}