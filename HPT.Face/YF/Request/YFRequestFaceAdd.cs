using HPT.Face.Utils;
using System.Drawing;
using System.Text;

namespace HPT.Face.YF.Request
{
    public class RequestFace
    {
        public string Pass { get; set; }

        public string PersonId { get; set; }

        public string FaceId { get; set; }

        public Image FaceImage { get; set; }

        public string Serialize()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append($"pass={Pass}")
                .Append("&")
                .Append($"personId={PersonId}")
                .Append("&")
                .Append($"faceId={FaceId}")
                .Append("&");
            if (FaceImage != null)
                buffer.Append($"imgBase64={ConvertHelper.ImageToBase64String(FaceImage)}");
            else
                buffer.Append($"imgBase64=");
            return buffer.ToString();
        }
    }
}
