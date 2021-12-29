using System.Text;

namespace HPT.Face.SYD
{
    public class SYDDeviceInfo
    {
        public int cameraDetectType { get; set; }
        public int doorType { get; set; }
        public string APKVersion { get; set; }
        public string sn { get; set; }
        public string tipsPairFail { get; set; }
        public float idCardFaceFeaturePairNumber { get; set; }
        public string deviceDefendTime { get; set; }
        public double openDoorContinueTime { get; set; }
        public float faceFeaturePairNumber { get; set; }
        public int deviceSoundSize { get; set; }
        public double faceFeaturePairSuccessOrFailWaitTime { get; set; }
        public string version { get; set; }
        public string name { get; set; }
        public int openDoorType { get; set; }
        public string appWelcomeMsg { get; set; }


        public string Serialize()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append($"cameraDetectType={cameraDetectType}")
                .Append("&")
                .Append($"faceFeaturePairNumber={faceFeaturePairNumber}")
                .Append("&")
                .Append($"faceFeaturePairSuccessOrFailWaitTime={faceFeaturePairSuccessOrFailWaitTime}")
                .Append("&")
                .Append($"openDoorType={openDoorType}")
                .Append("&")
                .Append($"openDoorContinueTime={openDoorContinueTime}")
                .Append("&")
                .Append($"doorType={doorType}")
                .Append("&")
                .Append($"idCardFaceFeaturePairNumber={idCardFaceFeaturePairNumber}")
                .Append("&")
                .Append($"appWelcomeMsg={appWelcomeMsg}")
                .Append("&")
                .Append($"deviceSoundSize={deviceSoundSize}")
                .Append("&")
                .Append($"deviceDefendTime={deviceDefendTime}")
                .Append("&")
                .Append($"deviceName")
                .Append("&")
                .Append($"appFailMsg={tipsPairFail}");
            return buffer.ToString();
        }
    }
}
