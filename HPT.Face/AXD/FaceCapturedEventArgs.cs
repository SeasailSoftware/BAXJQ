using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HPT.Face.AXD
{
    public class FaceCapturedEventArgs : EventArgs
    {
        /// <summary>
        /// 抓拍序号，从1开始，每产生一组抓拍数据增加1。
        /// </summary>
        public uint SequenceID { internal set; get; }
        /// <summary>
        /// 相机编号
        /// </summary>
        public string CameraID { internal set; get; }
        /// <summary>
        /// 点位编号
        /// </summary>
        public string AddrID { internal set; get; }
        /// <summary>
        /// 点位名称
        /// </summary>
        public string AddrName { internal set; get; }
        /// <summary>
        /// 抓拍时间
        /// </summary>
        public DateTime CaptureTime { internal set; get; }
        /// <summary>
        /// <para>实时抓拍标志</para>
        /// <para>true表示是实时数据，false表示历史数据</para>
        /// </summary>
        public bool IsRealTimeData { internal set; get; }
        /// <summary>
        /// <para>是否对比到库中的模板</para>
        /// <para>true表示对比到库中的人 false表示当前抓拍未比中库中模板</para>
        /// </summary>
        public bool IsPersonMatched { internal set; get; }
        /// <summary>
        /// 相似度
        /// </summary>
        public int MatchScore { internal set; get; }
        /// <summary>
        /// <para>人物编号</para>
        /// <para>只有对比到人脸才有值</para>
        /// </summary>
        public string PersonID { internal set; get; }
        /// <summary>
        /// <para>人物姓名</para>
        /// <para>只有对比到人脸才有值</para>
        /// </summary>
        public string PersonName { internal set; get; }
        /// <summary>
        /// 角色
        /// </summary>
        public int PersonRole { internal set; get; }
        /// <summary>
        /// 大图数据（JPEG格式图片的全部数据，包括文件头），可能为空表示没有传输大图
        /// </summary>
        public byte[] EnvironmentImageData { internal set; get; }
        /// <summary>
        /// 人脸在大图中的区域（没有大图时这个值可能是空值或者默认值）
        /// </summary>
        public Rectangle FaceInEnvironment { internal set; get; }
        /// <summary>
        /// <para>特写图数据（JPEG格式图片的全部数据，包括文件头），可能为空表示没有传输特写图</para>
        /// <para>一般来说特写图就是人脸图，不过不排除是头肩部的截取</para>
        /// </summary>
        public byte[] FeatureImageData { internal set; get; }
        /// <summary>
        /// 人脸在特写图中的区域（没有特写图时这个值可能是空值或者默认值）
        /// </summary>
        public Rectangle FaceInFeature { internal set; get; }
        /// <summary>
        /// 视频数据（可能为空表示没有视频数据）
        /// </summary>
        public byte[] VideoData { internal set; get; }
        /// <summary>
        /// 视频开始时间
        /// </summary>
        public DateTime VideoStartTime { internal set; get; }
        /// <summary>
        /// 视频结束时间
        /// </summary>
        public DateTime VideoEndTime { internal set; get; }
        /// <summary>
        /// 性别 0：未知 1：男 2：女
        /// </summary>
        public byte Sex { internal set; get; }
        /// <summary>
        /// 年龄 0：未知
        /// </summary>
        public byte Age { internal set; get; }
        /// <summary>
        /// 人脸标准度 0：未计算 1~100：一个表示人脸角度和光线/噪点等质量因素的值，越大越好（即越大角度越正，光线越均匀，像素越平坦）
        /// </summary>
        public byte QValue { internal set; get; }
        /// <summary>
        /// 抓拍图特征值数据
        /// </summary>
        public float[] FeatureData { internal set; get; }
        /// <summary>
        /// 比中的模板图（JPEG格式图片的全部数据，包括文件头）
        /// </summary>
        public byte[] ModelFaceImageData { internal set; get; }
    }
}
