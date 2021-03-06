using System;
using System.Runtime.InteropServices;

namespace HPT.Led.YBKJ
{
    public class BX_5EDynamicAreaSDK
    {
        private const string dllPath = @"lib/LedDynamicArea.dll";

        #region 通用dll
        //定义回调
        public delegate void CallBack(string szMessagge, int nProgress);
        //声明回调
        /*-------------------------------------------------------------------------------
        过程名:    AddScreen
        向动态库中添加显示屏信息；该函数不与显示屏通讯，只用于动态库中的指定显示屏参数信息配置。
        参数:
          nControlType    :显示屏的控制器型号；详见宏定义“控制器型号定义”
              Controller_BX_5AT = 0x0051;
              Controller_BX_5A0 = 0x0151;
              Controller_BX_5A1 = 0x0251;
              Controller_BX_5A2 = 0x0351;
              Controller_BX_5A3 = 0x0451;
              Controller_BX_5A4 = 0x0551;
              Controller_BX_5A1_WIFI = 0x0651;
              Controller_BX_5A2_WIFI = 0x0751;
              Controller_BX_5A4_WIFI = 0x0851;
              Controller_BX_5A  = 0x0951;
              Controller_BX_5A2_RF = 0x1351;
              Controller_BX_5A4_RF = 0x1551;
              Controller_BX_5AT_WIFI = 0x1651;
              Controller_BX_5AL = 0x1851;

              Controller_AX_AT  = 0x2051;
              Controller_AX_A0  = 0x2151;

              Controller_BX_5MT = 0x0552;
              Controller_BX_5M1 = 0x0052;
              Controller_BX_5M1X = 0x0152;
              Controller_BX_5M2 = 0x0252;
              Controller_BX_5M3 = 0x0352;
              Controller_BX_5M4 = 0x0452;

              Controller_BX_5E1 = 0x0154;
              Controller_BX_5E2 = 0x0254;
              Controller_BX_5E3 = 0x0354;

              Controller_BX_5UT = 0x0055;
              Controller_BX_5U0 = 0x0155;
              Controller_BX_5U1 = 0x0255;
              Controller_BX_5U2 = 0x0355;
              Controller_BX_5U3 = 0x0455;
              Controller_BX_5U4 = 0x0555;
              Controller_BX_5U5 = 0x0655;
              Controller_BX_5U  = 0x0755;
              Controller_BX_5UL = 0x0855;

              Controller_AX_UL  = 0x2055;
              Controller_AX_UT  = 0x2155;
              Controller_AX_U0  = 0x2255;
              Controller_AX_U1  = 0x2355;
              Controller_AX_U2  = 0x2455;

              Controller_BX_5Q0 = 0x0056;
              Controller_BX_5Q1 = 0x0156;
              Controller_BX_5Q2 = 0x0256;
              Controller_BX_5Q0P = 0x1056;
              Controller_BX_5Q1P = 0x1156;
              Controller_BX_5Q2P = 0x1256;
              Controller_BX_5QL = 0x1356;

              Controller_BX_5QS1 = 0x0157;
              Controller_BX_5QS2 = 0x0257;
              Controller_BX_5QS = 0x0357;
              Controller_BX_5QS1P = 0x1157;
              Controller_BX_5QS2P = 0x1257;
              Controller_BX_5QSP = 0x1357;
          nScreenNo       :显示屏屏号；该参数与LedshowTW 2013软件中"设置屏参"模块的"屏号"参数一致。
          nSendMode       :与显示屏的通讯模式；
              0:串口模式、BX-5A2&RF、BX-5A4&RF等控制器为RF串口无线模式;
              1:GPRS模式
              2:网络模式
              4:WiFi模式
              5:ONBON服务器-GPRS
              6:ONBON服务器-3G
          nWidth          :显示屏宽度 16的整数倍；最小64；BX-5E系列最小为80
          nHeight         :显示屏高度 16的整数倍；最小16；
          nScreenType     :显示屏类型；1：单基色；2：双基色；
            3：双基色；注意：该显示屏类型只有BX-4MC支持；同时该型号控制器不支持其它显示屏类型。
            4：全彩色；注意：该显示屏类型只有BX-5Q系列支持；同时该型号控制器不支持其它显示屏类型。
            5：双基色灰度；注意：该显示屏类型只有BX-5QS支持；同时该型号控制器不支持其它显示屏类型。
          nPixelMode      :点阵类型；1：R+G；2：G+R；该参数只对双基色屏有效 ；默认为2；
          nDataDA         :数据极性；，0x00：数据低有效，0x01：数据高有效；默认为0；
          nDataOE         :OE极性；  0x00：OE 低有效；0x01：OE 高有效；默认为0；
          nRowOrder       :行序模式；0：正常；1：加1行；2：减1行；默认为0；
          nFreqPar        :扫描点频；0~6；默认为0；
          pCom            :串口名称；串口通讯模式时有效；例:COM1
          nBaud           :串口波特率：目前支持9600、57600；默认为57600；
          pSocketIP       :控制卡IP地址，网络通讯模式时有效；例:192.168.0.199；
            本动态库网络通讯模式时只支持固定IP模式，单机直连和网络服务器模式不支持。
          nSocketPort     :控制卡网络端口；网络通讯模式时有效；例：5005
          nStaticIPMode	固定IP通讯模式  0：TCP模式  ；1：UDP模式  
          nServerMode     :0:服务器模式未启动；1：服务器模式启动。
          pBarcode        :设备条形码
          pNetworkID      :服务器模式时的网络ID编号
          pServerIP       :中转服务器IP地址
          nServerPort     :中转服务器网络端口
          pServerAccessUser:中转服务器访问用户名
          pServerAccessPassword:中转服务器访问密码
          pWiFiIP         :控制器WiFi模式的IP地址信息；WiFi通讯模式时有效；例:192.168.100.1
          nWiFiPort       :控制卡WiFi端口；WiFi通讯模式时有效；例：5005
          pGprsIP         :GPRS服务器IP地址
          nGprsPort       :GPRS服务器端口
          pGprsID         :GPRS编号
          pScreenStatusFile:用于保存查询到的显示屏状态参数保存的INI文件名；
            只有执行查询显示屏状态GetScreenStatus时，该参数才有效
          pCallBack :返回发送的消息和进度
                     类型为 TCallBackFunc = procedure(szMessagge:string;nProgress:integer); stdcall;
        返回值            :详见返回状态代码定义。
      -------------------------------------------------------------------------------*/
        [DllImport("BX_IV.dll")]
        public static extern int AddScreen(int nControlType, int nScreenNo, int nSendMode, int nWidth, int nHeight,
        int nScreenType, int nPixelMode, int nDataDA, int nDataOE, int nRowOrder, int DataFlow, int nFreqPar,
        string pCom, int nBaud, string pSocketIP, int nSocketPort, int nStaticIPMode, int nServerMode, string pBarcode,
        string pNetworkID, string pServerIP, int nServerPort, string pServerAccessUser, string pServerAccessPassword, string pWiFiIP,
         int nWiFiPort, string pGprsIP, int nGprsPort, string pGprsID, string pScreenStatusFile, CallBack pCallBack); //添加屏显
        #endregion


        ///<----------------------------dll start-------------------------------------->
        /*-------------------------------------------------------------------------------
        过程名:    Initialize
        初始化动态库；该函数不与显示屏通讯。
        参数:
        返回值            :详见返回状态代码定义。
        -------------------------------------------------------------------------------*/
        [DllImport(dllPath)]
        public static extern int Initialize(); //初始化动态库    

        /*-------------------------------------------------------------------------------
        过程名:    Uninitialize
        释放动态库；该函数不与显示屏通讯。
        参数:
        返回值            :详见返回状态代码定义。
        -------------------------------------------------------------------------------*/
        [DllImport(dllPath)]
        public static extern int Uninitialize(); //释放动态库    

        /*-------------------------------------------------------------------------------
            过程名:    AddScreen_Dynamic
            向动态库中添加显示屏信息；该函数不与显示屏通讯。
            参数:
            nControlType:显示屏的控制器型号，目前该动态区域动态库只支持BX-5E1、BX-5E2、BX-5E3等BX-5E系列控制器。
            nScreenNo：显示屏屏号；该参数与LedshowTW 2013软件中"设置屏参"模块的"屏号"参数一致。
            nSendMode：通讯模式；目前动态库中支持0:串口通讯；2：网络通讯(只支持固定IP模式)；5：保存到文件等三种通讯模式。
            nWidth：显示屏宽度；单位：像素。
            nHeight：显示屏高度；单位：像素。
            nScreenType：显示屏类型；1：单基色；2：双基色。
            nPixelMode：点阵类型，只有显示屏类型为双基色时有效；1：R+G；2：G+R。
            pCom：通讯串口，只有在串口通讯时该参数有效；例："COM1"
            nBaud：通讯串口波特率，只有在串口通讯时该参数有效；目前只支持9600、57600两种波特率。
            pSocketIP：控制器的IP地址；只有在网络通讯(固定IP模式)模式下该参数有效，例："192.168.0.199"
            nSocketPort：控制器的端口地址；只有在网络通讯(固定IP模式)模式下该参数有效，例：5005
            nServerMode     :0:服务器模式未启动；1：服务器模式启动。
            pBarcode        :设备条形码，用于服务器模式和中转服务器
            pNetworkID      :网络ID编号，用于服务器模式和中转服务器
            pServerIP       :中转服务器IP地址
            nServerPort     :中转服务器网络端口
            pServerAccessUser:中转服务器访问用户名
            pServerAccessPassword:中转服务器访问密码
            pCommandDataFile：保存到文件方式时，命令保存命令文件名称。只有在保存到文件模式下该参数有效，例："curCommandData.dat"
            返回值:    详见返回状态代码定义。
        -------------------------------------------------------------------------------*/
        [DllImport(dllPath)]
        public static extern int AddScreen_Dynamic(int nControlType, int nScreenNo, int nSendMode, int nWidth, int nHeight,
              int nScreenType, int nPixelMode, string pCom, int nBaud, string pSocketIP, int nSocketPort, int nStaticIpMode, int nServerMode,
              string pBarcode, string pNetworkID, string pServerIP, int nServerPort, string pServerAccessUser, string pServerAccessPassword,
              string pCommandDataFile);

        /*-------------------------------------------------------------------------------
          过程名:    QuerryServerDeviceList
          查询中转服务器设备的列表信息。
          该函数与显示屏进行通讯
          参数:
            pTransitDeviceType :中转设备类型 BX-3GPRS，BX-3G
            pServerIP       :中转服务器IP地址
            nServerPort     :中转服务器网络端口
            pServerAccessUser:中转服务器访问用户名
            pServerAccessPassword:中转服务器访问密码
            pDeviceList       : 保存查询的设备列表信息
              将设备的信息用组成字符串, 比如：
              设备1：名称 条形码 状态 类型 网络ID
              设备2：名称 条形码 状态 类型 网络ID
              组成字符串为：'设备1名称,设备1条形码,设备1状态,设备1类型,设备1网络ID;设备2名称,设备2条形码,设备2状态,设备2类型,设备2网络ID'
              设备状态(Byte):  $10:设备未知
                               $11:设备在线
                               $12:设备不在线

              设备类型(Byte): $16:设备为2G
                              $17:设备为3G
            pDeviceCount      : 查询的设备个数

          返回值            :详见返回状态代码定义。
        -------------------------------------------------------------------------------*/
        [DllImport(dllPath)]
        public static extern int QuerryServerDeviceList(string pTransitDeviceType, string pServerIP, int nServerPort, string pServerAccessUser, string pServerAccessPassword,
                                                        byte[] pDeviceList, ref int nDeviceCount);

        /*-------------------------------------------------------------------------------
          过程名:    AddScreenDynamicArea
          向动态库中指定显示屏添加动态区域；该函数不与显示屏通讯。
          参数:
            nScreenNo：显示屏屏号；该参数与AddScreen函数中的nScreenNo参数对应。
            nDYAreaID：动态区域编号；目前系统中最多5个动态区域；该值取值范围为0~4;
            nRunMode：动态区域运行模式：
                      0:动态区数据循环显示；
                      1:动态区数据显示完成后静止显示最后一页数据；
                      2:动态区数据循环显示，超过设定时间后数据仍未更新时不再显示；
                      3:动态区数据循环显示，超过设定时间后数据仍未更新时显示Logo信息,Logo 信息即为动态区域的最后一页信息
                      4:动态区数据顺序显示，显示完最后一页后就不再显示
            nTimeOut：动态区数据超时时间；单位：秒 
            nAllProRelate：节目关联标志；
                      1：所有节目都显示该动态区域；
                      0：在指定节目中显示该动态区域，如果动态区域要独立于节目显示，则下一个参数为空。
            pProRelateList：节目关联列表，用节目编号表示；节目编号间用","隔开,节目编号定义为LedshowTW 2013软件中"P***"中的"***"
            nPlayImmediately：动态区域是否立即播放0：该动态区域与异步节目一起播放；1：异步节目停止播放，仅播放该动态区域
            nAreaX：动态区域起始横坐标；单位：像素
            nAreaY：动态区域起始纵坐标；单位：像素
            nAreaWidth：动态区域宽度；单位：像素
            nAreaHeight：动态区域高度；单位：像素
            nAreaFMode：动态区域边框显示标志；0：纯色；1：花色；255：无边框
            nAreaFLine：动态区域边框类型, 纯色最大取值为FRAME_SINGLE_COLOR_COUNT；花色最大取值为：FRAME_MULI_COLOR_COUNT
            nAreaFColor：边框显示颜色；选择为纯色边框类型时该参数有效；
            nAreaFStunt：边框运行特技；
                      0：闪烁；1：顺时针转动；2：逆时针转动；3：闪烁加顺时针转动；
                      4:闪烁加逆时针转动；5：红绿交替闪烁；6：红绿交替转动；7：静止打出
            nAreaFRunSpeed：边框运行速度；
            nAreaFMoveStep：边框移动步长；该值取值范围：1~8；
          返回值:    详见返回状态代码定义。
        -------------------------------------------------------------------------------*/
        [DllImport(dllPath)]
        public static extern int AddScreenDynamicArea(int nScreenNo, int nDYAreaID, int nRunMode,
            int nTimeOut, int nAllProRelate, string pProRelateList, int nPlayImmediately,
            int nAreaX, int nAreaY, int nAreaWidth, int nAreaHeight, int nAreaFMode, int nAreaFLine, int nAreaFColor,
            int nAreaFStunt, int nAreaFRunSpeed, int nAreaFMoveStep);

        /*-------------------------------------------------------------------------------
          过程名:    AddScreenDynamicAreaFile
          向动态库中指定显示屏的指定动态区域添加信息文件；该函数不与显示屏通讯。
          参数:
            nScreenNo：显示屏屏号；该参数与AddScreen函数中的nScreenNo参数对应。
            nDYAreaID：动态区域编号；该参数与AddScreenDynamicArea函数中的nDYAreaID参数对应
            pFileName：添加的信息文件名称；目前只支持txt(支持ANSI、UTF-8、Unicode等格式编码)、bmp的文件格式
            nShowSingle：文字信息是否单行显示；0：多行显示；1：单行显示；显示该参数只有szFileName为txt格式文档时才有效；
            pFontName：文字信息显示字体；该参数只有szFileName为txt格式文档时才有效；
            nFontSize：文字信息显示字体的字号；该参数只有szFileName为txt格式文档时才有效；
            nBold：文字信息是否粗体显示；0：正常；1：粗体显示；该参数只有szFileName为txt格式文档时才有效；
            nFontColor：文字信息显示颜色；该参数只有szFileName为txt格式文档时才有效；
            nStunt：动态区域信息运行特技；
                      00：随机显示 
                      01：静止显示
                      02：快速打出 
                      03：向左移动 
                      04：向左连移 
                      05：向上移动 
                      06：向上连移 
                      07：闪烁 
                      08：飘雪 
                      09：冒泡 
                      10：中间移出 
                      11：左右移入 
                      12：左右交叉移入 
                      13：上下交叉移入 
                      14：画卷闭合 
                      15：画卷打开 
                      16：向左拉伸 
                      17：向右拉伸 
                      18：向上拉伸 
                      19：向下拉伸 
                      20：向左镭射 
                      21：向右镭射 
                      22：向上镭射 
                      23：向下镭射 
                      24：左右交叉拉幕 
                      25：上下交叉拉幕 
                      26：分散左拉 
                      27：水平百页 
                      28：垂直百页 
                      29：向左拉幕 
                      30：向右拉幕 
                      31：向上拉幕 
                      32：向下拉幕 
                      33：左右闭合 
                      34：左右对开 
                      35：上下闭合 
                      36：上下对开 
                      37：向右移动 
                      38：向右连移 
                      39：向下移动 
                      40：向下连移
            nRunSpeed：动态区域信息运行速度
            nShowTime：动态区域信息显示时间；单位：10ms
          返回值:    详见返回状态代码定义。
        -------------------------------------------------------------------------------*/
        [DllImport(dllPath)]
        public static extern int AddScreenDynamicAreaFile(int nScreenNo, int nDYAreaID,
            string pFileName, int nShowSingle, string pFontName, int nFontSize, int nBold, int nFontColor,
            int nStunt, int nRunSpeed, int nShowTime);

        /*-------------------------------------------------------------------------------
          过程名:    AddScreenDynamicAreaText
          向动态库中指定显示屏的指定动态区域添加信息文件；该函数不与显示屏通讯。
          参数:
            nScreenNo：显示屏屏号；该参数与AddScreen函数中的nScreenNo参数对应。
            nDYAreaID：动态区域编号；该参数与AddScreenDynamicArea函数中的nDYAreaID参数对应
            pText：添加的信息文件名称；目前只支持txt(支持ANSI、UTF-8、Unicode等格式编码)、bmp的文件格式
            nShowSingle：文字信息是否单行显示；0：多行显示；1：单行显示；显示该参数只有szFileName为txt格式文档时才有效；
            pFontName：文字信息显示字体；该参数只有szFileName为txt格式文档时才有效；
            nFontSize：文字信息显示字体的字号；该参数只有szFileName为txt格式文档时才有效；
            nBold：文字信息是否粗体显示；0：正常；1：粗体显示；该参数只有szFileName为txt格式文档时才有效；
            nFontColor：文字信息显示颜色；该参数只有szFileName为txt格式文档时才有效；
            nStunt：动态区域信息运行特技；
                      00：随机显示 
                      01：静止显示
                      02：快速打出 
                      03：向左移动 
                      04：向左连移 
                      05：向上移动 
                      06：向上连移 
                      07：闪烁 
                      08：飘雪 
                      09：冒泡 
                      10：中间移出 
                      11：左右移入 
                      12：左右交叉移入 
                      13：上下交叉移入 
                      14：画卷闭合 
                      15：画卷打开 
                      16：向左拉伸 
                      17：向右拉伸 
                      18：向上拉伸 
                      19：向下拉伸 
                      20：向左镭射 
                      21：向右镭射 
                      22：向上镭射 
                      23：向下镭射 
                      24：左右交叉拉幕 
                      25：上下交叉拉幕 
                      26：分散左拉 
                      27：水平百页 
                      28：垂直百页 
                      29：向左拉幕 
                      30：向右拉幕 
                      31：向上拉幕 
                      32：向下拉幕 
                      33：左右闭合 
                      34：左右对开 
                      35：上下闭合 
                      36：上下对开 
                      37：向右移动 
                      38：向右连移 
                      39：向下移动 
                      40：向下连移
            nRunSpeed：动态区域信息运行速度
            nShowTime：动态区域信息显示时间；单位：10ms
          返回值:    详见返回状态代码定义。
        -------------------------------------------------------------------------------*/
        [DllImport(dllPath)]
        public static extern int AddScreenDynamicAreaText(int nScreenNo, int nDYAreaID,
            string pText, int nShowSingle, string pFontName, int nFontSize, int nBold, int nFontColor,
            int nStunt, int nRunSpeed, int nShowTime);

        /*-------------------------------------------------------------------------------
          过程名:    DeleteScreen
          删除动态库中指定显示屏的所有信息；该函数不与显示屏通讯。
          参数:
            nScreenNo：显示屏屏号；该参数与AddScreen函数中的nScreenNo参数对应。
          返回值:    详见返回状态代码定义
        -------------------------------------------------------------------------------*/
        [DllImport(dllPath)]
        public static extern int DeleteScreen_Dynamic(int nScreenNo);

        /*-------------------------------------------------------------------------------
          过程名:    DeleteScreenDynamicAreaFile
          删除动态库中指定显示屏指定的动态区域指定文件信息；该函数不与显示屏通讯。
          参数:
            nScreenNo：显示屏屏号；该参数与AddScreen函数中的nScreenNo参数对应。
            nDYAreaID：动态区域编号；该参数与AddScreenDynamicArea函数中的nDYAreaID参数对应
            nFileOrd：动态区域的指定文件的文件序号；该序号按照文件添加顺序，从0顺序递增，如删除中间的文件，后面的文件序号自动填充。
          返回值:    详见返回状态代码定义
        -------------------------------------------------------------------------------*/
        [DllImport(dllPath)]
        public static extern int DeleteScreenDynamicAreaFile(int nScreenNo, int nDYAreaID, int nFileOrd);

        /*-------------------------------------------------------------------------------
         过程名:    SendDynamicAreaInfoCommand
         发送动态库中指定显示屏指定的动态区域信息到显示屏；该函数与显示屏通讯。
         参数:
           nScreenNo：显示屏屏号；该参数与AddScreen函数中的nScreenNo参数对应。
           nDYAreaID：动态区域编号；该参数与AddScreenDynamicArea函数中的nDYAreaID参数对应
         返回值:    详见返回状态代码定义
       -------------------------------------------------------------------------------*/
        [DllImport(dllPath)]
        public static extern int SendDynamicAreaInfoCommand(int nScreenNo, int nDYAreaID);

        /*-------------------------------------------------------------------------------
          过程名:    SendDeleteDynamicAreasCommand
          删除动态库中指定显示屏指定的动态区域信息；同时向显示屏通讯删除指定的动态区域信息。该函数与显示屏通讯
          参数:
            nScreenNo：显示屏屏号；该参数与AddScreen函数中的nScreenNo参数对应。
            nDelAllDYArea	动态区域编号列表；1：同时删除多个动态区域，0：删除单个动态区域
            pDYAreaIDList	动态区域编号；当nDelAllDYArea为1时，其值为” ”；当nDelAllDYArea为0时，该参数与AddScreenDynamicArea函数中的nDYAreaID参数对应，删除相应动态区域
          返回值:    详见返回状态代码定义
        -------------------------------------------------------------------------------*/
        [DllImport(dllPath)]
        public static extern int SendDeleteDynamicAreasCommand(int nScreenNo, int nDelAllDYArea, string pDYAreaIDList); //删除指定动态区域的信息

        /*-------------------------------------------------------------------------------
          过程名:    StartServer
          启动服务器,用于网络模式下的服务器模式和GPRS通讯模式。
          参数:
            nSendMode       :与显示屏的通讯模式；
              0:串口模式、BX-5A2&RF、BX-5A4&RF等控制器为RF串口无线模式;
              1:GPRS模式
              2:网络模式
              4:WiFi模式
              5:ONBON服务器-GPRS
              6:ONBON服务器-3G
            pServerIP       :中转服务器IP地址
            nServerPort     :中转服务器网络端口
          返回值            :详见返回状态代码定义。
        -------------------------------------------------------------------------------*/
        [DllImport(dllPath)]
        public static extern int StartServer(int nSendMode, string pServerIP, int nServerPort);

        /*-------------------------------------------------------------------------------
          过程名:    StopServer
          关闭服务器,用于网络模式下的服务器模式和GPRS通讯模式。
          参数:
            nSendMode       :与显示屏的通讯模式；
              0:串口模式、BX-5A2&RF、BX-5A4&RF等控制器为RF串口无线模式;
              1:GPRS模式
              2:网络模式
              4:WiFi模式
              5:ONBON服务器-GPRS
              6:ONBON服务器-3G
          返回值            :详见返回状态代码定义。
        -------------------------------------------------------------------------------*/
        [DllImport(dllPath)]
        public static extern int StopServer(int nSendMode);

        #region
        public const int CONTROLLER_BX_5E1 = 0x0154;
        public const int CONTROLLER_BX_5E2 = 0x0254;
        public const int CONTROLLER_BX_5E3 = 0x0354;
        public const int CONTROLLER_BX_5Q0P = 0x1056;
        public const int CONTROLLER_BX_5Q1P = 0x1156;
        public const int CONTROLLER_BX_5Q2P = 0x1256;

        public const int CONTROLLER_BX_5E1_INDEX = 0;
        public const int CONTROLLER_BX_5E2_INDEX = 1;
        public const int CONTROLLER_BX_5E3_INDEX = 2;
        public const int CONTROLLER_BX_5Q0P_INDEX = 3;
        public const int CONTROLLER_BX_5Q1P_INDEX = 4;
        public const int CONTROLLER_BX_5Q2P_INDEX = 5;

        public const int FRAME_SINGLE_COLOR_COUNT = 23; //纯色边框图片个数
        public const int FRAME_MULI_COLOR_COUNT = 47; //花色边框图片个数

        //------------------------------------------------------------------------------
        // 通讯模式
        public const int SEND_MODE_SERIALPORT = 0;
        public const int SEND_MODE_NETWORK = 2;
        public const int SEND_MODE_Server_2G = 5;
        public const int SEND_MODE_Server_3G = 6;
        public const int SEND_MODE_SAVEFILE = 7;
        //------------------------------------------------------------------------------
        //------------------------------------------------------------------------------
        // 动态区域运行模式
        public const int RUN_MODE_CYCLE_SHOW = 0; //动态区数据循环显示；
        public const int RUN_MODE_SHOW_LAST_PAGE = 1; //动态区数据显示完成后静止显示最后一页数据；
        public const int RUN_MODE_SHOW_CYCLE_WAITOUT_NOSHOW = 2; //动态区数据循环显示，超过设定时间后数据仍未更新时不再显示；
        public const int RUN_MODE_SHOW_ORDERPLAYED_NOSHOW = 4; //动态区数据顺序显示，显示完最后一页后就不再显示
        //------------------------------------------------------------------------------
        //==============================================================================
        // 返回状态代码定义
        public const int RETURN_ERROR_NOFIND_DYNAMIC_AREA = 0xE1; //没有找到有效的动态区域。
        public const int RETURN_ERROR_NOFIND_DYNAMIC_AREA_FILE_ORD = 0xE2; //在指定的动态区域没有找到指定的文件序号。
        public const int RETURN_ERROR_NOFIND_DYNAMIC_AREA_PAGE_ORD = 0xE3; //在指定的动态区域没有找到指定的页序号。
        public const int RETURN_ERROR_NOSUPPORT_FILETYPE = 0xE4; //不支持该文件类型。
        public const int RETURN_ERROR_RA_SCREENNO = 0xF8; //已经有该显示屏信息。如要重新设定请先DeleteScreen删除该显示屏再添加；
        public const int RETURN_ERROR_NOFIND_AREA = 0xFA; //没有找到有效的显示区域；可以使用AddScreenProgramBmpTextArea添加区域信息。
        public const int RETURN_ERROR_NOFIND_SCREENNO = 0xFC; //系统内没有查找到该显示屏；可以使用AddScreen函数添加显示屏
        public const int RETURN_ERROR_NOW_SENDING = 0xFD; //系统内正在向该显示屏通讯，请稍后再通讯；
        public const int RETURN_ERROR_OTHER = 0xFF; //其它错误；
        public const int RETURN_NOERROR = 0; //没有错误
        //==============================================================================

        public const int SCREEN_NO = 1;
        public const int SCREEN_WIDTH = 128;
        public const int SCREEN_HEIGHT = 64;
        public const int SCREEN_TYPE = 2;
        public const int SCREEN_DATADA = 0;
        public const int SCREEN_DATAOE = 0;
        public const string SCREEN_COMM = "COM1";
        public const int SCREEN_BAUD = 57600;
        public const int SCREEN_ROWORDER = 0;
        public const int SCREEN_FREQPAR = 0;
        public const string SCREEN_SOCKETIP = "192.168.1.159";
        public const int SCREEN_SOCKETPORT = 5005;
        //private bool m_bSendBusy = false;此变量在数据更新中非常重要，请务必保留。
        ///<----------------------------end dll------------------------------------->
        #endregion

        #region 方法

        public static string GetErrorMessage(string szfunctionName, int nResult)
        {
            string szResult;
            DateTime dt = DateTime.Now;
            szResult = dt.ToString() + "---执行函数：" + szfunctionName + "---返回结果：";
            switch (nResult)
            {
                case RETURN_ERROR_NOFIND_DYNAMIC_AREA:
                    szResult += szResult + "没有找到有效的动态区域。\r\n";
                    break;
                case RETURN_ERROR_NOFIND_DYNAMIC_AREA_FILE_ORD:
                    szResult += szResult + "在指定的动态区域没有找到指定的文件序号。\r\n";
                    break;
                case RETURN_ERROR_NOFIND_DYNAMIC_AREA_PAGE_ORD:
                    szResult += szResult + "在指定的动态区域没有找到指定的页序号。\r\n";
                    break;
                case RETURN_ERROR_NOSUPPORT_FILETYPE:
                    szResult += szResult + "动态库不支持该文件类型。\r\n";
                    break;
                case RETURN_ERROR_RA_SCREENNO:
                    szResult += szResult + "系统中已经有该显示屏信息。如要重新设定请先执行DeleteScreen函数删除该显示屏后再添加。\r\n";
                    break;
                case RETURN_ERROR_NOFIND_AREA:
                    szResult += szResult + "系统中没有找到有效的动态区域；可以先执行AddScreenDynamicArea函数添加动态区域信息后再添加。\r\n";
                    break;
                case RETURN_ERROR_NOFIND_SCREENNO:
                    szResult += szResult + "系统内没有查找到该显示屏；可以使用AddScreen函数添加该显示屏。\r\n";
                    break;
                case RETURN_ERROR_NOW_SENDING:
                    szResult += szResult + "系统内正在向该显示屏通讯，请稍后再通讯。\r\n";
                    break;
                case RETURN_ERROR_OTHER:
                    szResult += szResult + "其它错误。\r\n";
                    break;
                case RETURN_NOERROR:
                    szResult += szResult + "函数执行成功。\r\n";
                    break;
            }
            return szResult;
        }

        /// <summary>
        /// 添加屏幕
        /// </summary>
        /// <param name="controller"></param>
        public static bool AddScreen_Dynamic(Controller controller)
        {
            int nControlType;
            switch (controller.ControlType)
            {
                case 0:
                    nControlType = CONTROLLER_BX_5E1;
                    break;
                case 1:
                    nControlType = CONTROLLER_BX_5E2;
                    break;
                case 2:
                    nControlType = CONTROLLER_BX_5E3;
                    break;
                default:
                    nControlType = CONTROLLER_BX_5E1;
                    break;
            }
            int nScreenNo = controller.Lid;
            int nSendMode = SEND_MODE_NETWORK;
            int nWidth = controller.Width;
            int nHeight = controller.Heigth;
            int nScreenType = 1;
            int nPixelMode = 1;
            string pCom = "COM1";
            int nBaud = 57600;
            string pSocketIP = controller.IPAddress;
            int nSocketPort = controller.Port;
            int nStaticIpMode = controller.Protocol;
            int nServerMode = 0;
            string pBarcode = string.Empty;
            string pNetworkID = string.Empty;
            string pServerIP = string.Empty;
            int pServerPort = 0;
            string pServerAccessUser = string.Empty;
            string pServerAccessPassword = string.Empty;
            string pCommandDataFile = Environment.CurrentDirectory + @"/Led/DebugcurCommandData.dat";
            int result = AddScreen_Dynamic(nControlType, nScreenNo, nSendMode, nWidth, nHeight, nScreenType, nPixelMode, pCom, nBaud,
               pSocketIP, nSocketPort, nStaticIpMode, nServerMode, pBarcode, pNetworkID, pServerIP, pServerPort, pServerAccessUser, pServerAccessPassword, pCommandDataFile);
            return result == RETURN_NOERROR;
        }

        /// <summary>
        /// 添加动态区域
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static bool AddScreenDynamicArea(AreaInfo area)
        {
            int nScreenNo = area.Lid;
            int nDYAreaID = area.AreaId;
            int nRunMode = RUN_MODE_CYCLE_SHOW;
            int nTimeOut = 10;
            int nAllProRelate = 1;
            string pProRelateList = string.Empty;
            int nPlayImmediately = 0;
            int nAreaX = area.Point_X;
            int nAreaY = area.Point_Y;
            int nAreaWidth = area.Width;
            int nAreaHeight = area.Height;
            int nAreaFMode = area.BordreType;
            int nAreaFLine = area.BorderNo;
            int nAreaFColor = 255;
            int nAreaFStunt = area.BorderEffect;
            int nAreaFRunSpeed = area.BorderSpeed;
            int nAreaFMoveStep = area.BorderLength;
            int result = AddScreenDynamicArea(nScreenNo, nDYAreaID, nRunMode, nTimeOut, nAllProRelate, pProRelateList, nPlayImmediately,
            nAreaX, nAreaY, nAreaWidth, nAreaHeight, nAreaFMode, nAreaFLine, nAreaFColor, nAreaFStunt, nAreaFRunSpeed, nAreaFMoveStep);
            if (result == RETURN_NOERROR)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 添加文本到动态区域
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static bool AddScreenDynamicAreaText(AreaInfo area, string pText)
        {
            int nScreenNo = area.Lid;
            int nDYAreaID = area.AreaId;
            //string pText = string.Empty;
            //string content = ReadTxtFile(area.Text);
            //pText = DBService.GetContent(content, area.LID);
            int nShowSingle = area.SingleLine;
            string pFontName = area.TextFont;
            int nFontSize = area.TextFontSize;
            int nBold = area.TextBold;
            int nFontColor = 65535;
            int nStunt = area.DisplayEffect;
            int nRunSpeed = area.Speed;
            int nShowTime = area.Stay;
            int result = AddScreenDynamicAreaText(nScreenNo, nDYAreaID, pText, nShowSingle, pFontName, nFontSize, nBold, nFontColor, nStunt, nRunSpeed, nShowTime);
            if (result == RETURN_NOERROR)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion



        /// <summary>
        /// 添加文件到动态区域
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static bool AddScreenDynamicTxtFile(AreaInfo area, string fileName = "")
        {
            int nScreenNo = area.Lid;
            int nDYAreaID = area.AreaId;
            ///<---------从数据库读取文本信息------------>
            ///
            string pText = fileName;
            ///pText = DBService.GetContent(pText, area.LID);
            ///<--------------------------------------------->

            int nShowSingle = area.SingleLine;
            string pFontName = area.TextFont;
            int nFontSize = area.TextFontSize;
            int nBold = area.TextBold;
            int nFontColor = 65535;
            int nStunt = area.DisplayEffect;
            int nRunSpeed = area.Speed;
            int nShowTime = area.Stay;
            int result = AddScreenDynamicAreaFile(nScreenNo, nDYAreaID, pText, nShowSingle, pFontName, nFontSize, nBold, nFontColor, nStunt, nRunSpeed, nShowTime);
            if (result == RETURN_NOERROR)
            {
                //Logs.WriteLog("添加文本[" + pText + "]到动态区域【" + area.LID + area.AreaId + "】成功!");
                return true;
            }
            else
            {
                //Logs.WriteLog("添加文本[" + pText + "]到动态区域【" + area.LID + area.AreaId + "】失败!");
                return false;
            }
        }



        /// <summary>
        /// 添加文件到动态区域
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static bool AddScreenDynamicBmpFile(AreaInfo area, string fileName)
        {
            int nScreenNo = area.Lid;
            int nDYAreaID = area.AreaId;
            ///<---------从数据库读取文本信息------------>
            ///
            string pText = fileName;
            ///DBService.GetContent(pText, area.LID);
            ///<--------------------------------------------->
            int nShowSingle = area.SingleLine;
            string pFontName = area.TextFont;
            int nFontSize = area.TextFontSize;
            int nBold = area.TextBold;
            int nFontColor = 65535;
            int nStunt = area.DisplayEffect;
            int nRunSpeed = area.Speed;
            int nShowTime = area.Stay;
            int result = AddScreenDynamicAreaFile(nScreenNo, nDYAreaID, pText, nShowSingle, pFontName, nFontSize, nBold, nFontColor, nStunt, nRunSpeed, nShowTime);
            if (result == RETURN_NOERROR)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
