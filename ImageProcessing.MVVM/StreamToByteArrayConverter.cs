using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Markup;

namespace ImageProcessing.MVVM
{
    public class StreamToByteArrayConverter : MarkupExtension, IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Stream valueStream)) return null;
            using var memoryStream = new MemoryStream();
            if (valueStream.CanSeek)
                valueStream.Seek(0, SeekOrigin.Begin);
            valueStream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value is byte[] byteArray ? new MemoryStream(byteArray) : null;

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}