namespace HPT.Face.HPT.Request
{
    public class HPTConfig
    {

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 识别距离
        /// </summary>
        public int IdentifyDistance { get; set; }

        /// <summary>
        /// 识别分数
        /// </summary>
        public int IdentifyScores { get; set; }

        /// <summary>
        /// 识别间隔
        /// </summary>
        public int SaveIdentifyTime { get; set; }

        /// <summary>
        /// 语音模式类型
        /// </summary>
        public int TtsModType { get; set; }

        /// <summary>
        ///自定义语音内容
        /// </summary>
        public string TtsModContent { get; set; }

        /// <summary>
        /// 串口模式类型
        /// </summary>
        public int ComModType { get; set; }

        /// <summary>
        /// 自定义串口输出内容
        /// </summary>
        public string ComModContent { get; set; }

        /// <summary>
        /// 显示模式类型
        /// </summary>
        public int DisplayModType { get; set; }

        /// <summary>
        /// 自定义显示内容
        /// </summary>
        public string DisplayModContent { get; set; }

        /// <summary>
        /// 标语
        /// </summary>
        public string Slogan { get; set; }

        /// <summary>
        /// 公司简介
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// 陌生人时间判定登记
        /// </summary>
        public int RecStrangerTimesThreshold { get; set; }

        /// <summary>
        /// 陌生人开关
        /// </summary>
        public int RecStrangerType { get; set; }

        /// <summary>
        /// 陌生人语音模式
        /// </summary>
        public int TtsModStrangerType { get; set; }

        /// <summary>
        /// 自定义陌生人语音内容
        /// </summary>
        public string TtsModStrangerContent { get; set; }

        /// <summary>
        /// 识别模式
        /// </summary>
        public int MultiplayerDetection { get; set; }

        /// <summary>
        /// 维根输出信号类型
        /// </summary>
        public string Wg { get; set; }

        /// <summary>
        /// 识别等级
        /// </summary>
        public int RecRank { get; set; }

        /// <summary>
        /// 维根类型
        /// </summary>
        public int WgType { get; set; }

        /// <summary>
        /// 继电器执行时间
        /// </summary>
        public int RelayRate { get; set; }

        /// <summary>
        /// 灯光模式
        /// </summary>
        public int LightPattern { get; set; }
    }
}
