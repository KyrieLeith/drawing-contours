using System;
using System.Drawing;

namespace BodySegmentationApp
{
    public class ColorFactory
    {
        public string[] MyColors { get; set; }

        public ColorFactory()
        {
            MyColors = Enum.GetNames(typeof(KnownColor));
        }

        public Color GetColorByIndex(int index)
        {
            return Color.FromName(MyColors[index]);
        }
    }
}
