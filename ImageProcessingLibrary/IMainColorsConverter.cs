using System;
using System.Drawing;
using System.Xml.Schema;

namespace ImageProcessingLibrary
{
    public interface IMainColorsConverter
    {
        Bitmap ToMainColorsConverter(Bitmap source);
    }
}