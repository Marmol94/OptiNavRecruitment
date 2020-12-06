using System;
using System.Drawing;
using System.Xml.Schema;

namespace ImageProcessing.Library
{
    public interface IMainColorsConverter
    {
        Bitmap ToMainColorsConverter(Bitmap source);
    }
}