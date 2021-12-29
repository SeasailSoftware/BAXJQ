using HPT.Face.Utils;
using System.Drawing;
using System.Text;

namespace HPT.Face.YF.Request
{
    public class YFRequestChangeLogo
    {
        public string Pass { get; set; }

        public Image Logo { get; set; }

        public string Serialize()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append($"pass={Pass}");
            buffer.Append("&");
            buffer.Append("imgBase64=");
            if (Logo == null)
                buffer.Append("-1");
            else
            {
                using (Bitmap image = new Bitmap(Logo))
                {
                    buffer.Append(ConvertHelper.ImageToBase64String(image));
                }
            }
            return buffer.ToString();

        }
    }
}
