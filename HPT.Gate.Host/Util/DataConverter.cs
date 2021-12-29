using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.Device.Data;
using HPT.Gate.Device.Dev;
using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HPT.Gate.Host.Util
{
    public class DataConverter
    {
        #region 获取Tcp设备
        public static TcpDevice GetTcpDevice(DeviceInfo device)
        {
            if (device == null) return null;
            TcpDevice dev = new TcpDevice();
            dev._MachineId = device.DeviceId;
            dev._DeviceName = device.DeviceName;
            dev._Mac = device.Mac;
            dev._IPAddress = device.IPAddress;
            dev._SubnetMark = device.SubNet;
            dev._Gateway = device.GateWay;
            dev._Port = device.Port;
            dev._HardVersion = device.HardVersion;
            dev._SoftVersion = device.SoftVersion;
            return dev;
        }
        #endregion

        #region 获取节假日
        internal static DataVacation GetDataVacation()
        {
            DataVacation dVacation = new DataVacation();
            List<Vacation> vacationList = VacationService.GetEffectList();
            foreach (Vacation vacation in vacationList)
            {
                DateGroup dateGroup = new DateGroup(vacation.VBeginDate, vacation.VEndDate);
                dVacation.VacationList[vacation.Vid] = dateGroup;
            }
            return dVacation;
        }

        internal static DataTimeGroupOfVacation GetDataTimegroupOfVacation()
        {
            List<DategroupOfVacation> dateGroupList = DategroupOfVacationService.ToList();
            List<TimeGroup> tgList = new List<TimeGroup>();
            DataTimeGroupOfVacation dTimegroupOfVacation = new DataTimeGroupOfVacation();
            for (int i = 0; i < 3; i++)
            {
                List<DataTimeGroup> timeGroup = new List<DataTimeGroup>();
                timeGroup.Add(new DataTimeGroup(dateGroupList[i].BeginTime1, dateGroupList[i].EndTime1));
                timeGroup.Add(new DataTimeGroup(dateGroupList[i].BeginTime2, dateGroupList[i].EndTime2));
                timeGroup.Add(new DataTimeGroup(dateGroupList[i].BeginTime3, dateGroupList[i].EndTime3));
                timeGroup.Add(new DataTimeGroup(dateGroupList[i].BeginTime4, dateGroupList[i].EndTime4));
                timeGroup.Add(new DataTimeGroup(dateGroupList[i].BeginTime5, dateGroupList[i].EndTime5));
                dTimegroupOfVacation.TimeGroupList[i] = timeGroup;
            }
            return dTimegroupOfVacation;
        }

        internal static List<DataTimeGroupOfDoor> GetDataTimegroupOfWeek()
        {
            List<DataTimeGroupOfDoor> timeGroupList = new List<DataTimeGroupOfDoor>();
            List<TimegroupOfDoor> doorList = TimegroupOfDoorService.GetEffectList();
            foreach (TimegroupOfDoor timeGroupOfDoor in doorList)
            {
                DataTimeGroupOfDoor dataDoor = new DataTimeGroupOfDoor();
                dataDoor.GroupNo = new byte[] { (byte)timeGroupOfDoor.Id };
                List<TimeOfGroup> timeList = TimeOfGroupService.GetById(timeGroupOfDoor.Id);
                for (int i = 0; i < 7; i++)
                {
                    List<TimeOfGroup> list = timeList.Where(p => p.WeekNo == i + 1).ToList();
                    foreach (TimeOfGroup timeOfGroup in list)
                    {
                        dataDoor.TimeGroupList[i].Add(new DataTimeGroup(timeOfGroup.BeginTime, timeOfGroup.EndTime));
                    }
                }
                timeGroupList.Add(dataDoor);
            }
            return timeGroupList;
        }

        #endregion

        #region 获取时间组内所有时间段
        internal static List<DataTimeGroup> GetTimeGroupList(int groupNo, int weekNo)
        {
            List<DataTimeGroup> timeGroupList = new List<DataTimeGroup>();
            List<TimeOfGroup> timeList = TimeOfGroupService.GetById(groupNo).Where(p => p.WeekNo == weekNo).ToList();
            foreach (TimeOfGroup group in timeList)
            {
                timeGroupList.Add(new DataTimeGroup(group.BeginTime, group.EndTime, true));
            }
            return timeGroupList;
        }

        internal static Record GetRecord(DataRecord record)
        {
            if (record == null)
                return null;
            Record dbRecord = new Record();
            dbRecord.Type = record.Type;
            dbRecord.CardNo = record.CardNo;
            dbRecord.DeviceId = record.MachineId;
            dbRecord.IOFlag = record.SIOFlag;
            dbRecord.RecDatetime = record.RecDatetime;
            dbRecord.RecordType = record.SRecordType;
            dbRecord.Name = record.Name;
            dbRecord.Sex = record.Sex;
            dbRecord.Nation = record.Nation;
            dbRecord.Address = record.Address;
            return dbRecord;
        }

        internal static DataCard GetDataCard(CardUpdate card)
        {
            DataCard dCard = new DataCard();
            dCard.Type = card.Type;
            string cardNo = card.CardNo;
            if (string.IsNullOrEmpty(card.CardNo))
                card.CardNo = "FFFFFFFF";
            else
            {
                int length = card.CardNo.Length;
                switch (dCard.Type)
                {
                    default:
                        cardNo = card.CardNo;
                        dCard.CardNo = ArrayHelper.HexToArray(cardNo, 4);
                        break;
                    case 2:
                        if (cardNo.Length == 16)
                            cardNo = card.CardNo.Substring(8, 8);
                        dCard.CardNo = ArrayHelper.HexToArray(cardNo, 4);
                        break;
                    case 3:
                        if (cardNo.Length == 18)
                            cardNo = card.CardNo.Substring(10, 8).ToUpper().Replace("X", "A");
                        dCard.CardNo = ArrayHelper.HexToArray(cardNo, 4);
                        break;
                    case 5:
                        if (card.FingerPrintData == null || card.FingerPrintData.Length < 100)
                        {
                            dCard.CardNo = new byte[] { 0xff, 0xff, 0xff, 0xff };
                            dCard.FingerData = new byte[2048];
                        }
                        else
                        {
                            cardNo = card.CardNo;
                            dCard.CardNo = ArrayHelper.HexToArray(cardNo, 4);
                            dCard.FingerData = card.FingerPrintData;
                        }

                        break;
                    case 6:

                        break;
                }
            }
            dCard.CurCardSerial = ArrayHelper.IntToBytes(card.CardId, 2);
            dCard.TatolCardSerial = ArrayHelper.IntToBytes(card.TotalNum, 2);
            dCard.CardStatus = ArrayHelper.IntToBytes(card.BlackName, 1);
            dCard.CardType = ArrayHelper.IntToBytes(card.CardType, 1);
            dCard.Row1 = ArrayHelper.GB2312ToArray(card.Row1, 16);
            dCard.Row2 = ArrayHelper.GB2312ToArray(card.Row2, 16);
            dCard.Row3 = ArrayHelper.GB2312ToArray(card.Row3, 16);
            dCard.RightOfDoorIn = ArrayHelper.IntToBytes(card.InRight, 1);
            dCard.RightOfDoorOut = ArrayHelper.IntToBytes(card.OutRight, 1);
            dCard.VoiceNo = ArrayHelper.IntToBytes(card.VoiceNo, 1);
            dCard.PhotoName = ArrayHelper.IntToBytes(card.EmpId, 2);
            dCard.VacationGrop = ArrayHelper.IntToBytes(card.VacationId, 1);
            dCard.TimeGroupOfNormalDoorIn = ArrayHelper.IntToBytes(card.InTimeGroupNo, 1);
            dCard.TimeGroupOfNormalDoorOut = ArrayHelper.IntToBytes(card.OutTimeGroupNo, 1);
            dCard.BeginDate = ArrayHelper.DateToArray1(Convert.ToDateTime(card.BeginDate));
            dCard.EndDate = ArrayHelper.DateToArray1(Convert.ToDateTime(card.EndDate));
            return dCard;
        }

        internal static DataCard GetDataCard(int max, int empId, EmpInfo emp, int type, TicketType ticket, int blackName, string[] displayContents)
        {
            DataCard dCard = new DataCard();
            dCard.Type = type;
            int current = 0;
            switch (type)
            {
                case 1:
                    current = (empId - 1) * 5;
                    if (emp == null)
                        dCard.CardNo = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
                    else
                    {
                        if (string.IsNullOrEmpty(emp.ICCardNo))
                            dCard.CardNo = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
                        else
                            dCard.CardNo = ArrayHelper.HexToArray(emp.ICCardNo, 4);
                    }
                    break;
                case 2:
                    current = (empId - 1) * 5 + 1;
                    if (emp == null)
                        dCard.CardNo = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
                    else
                    {
                        if (string.IsNullOrEmpty(emp.IDSerial))
                            dCard.CardNo = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
                        else
                            dCard.CardNo = ArrayHelper.HexToArray(emp.IDSerial.Substring(8, 8), 4);
                    }
                    break;
                case 3:
                    current = (empId - 1) * 5 + 2;
                    if (emp == null)
                        dCard.CardNo = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
                    else
                    {
                        if (string.IsNullOrEmpty(emp.IDCardNo))
                            dCard.CardNo = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
                        else
                            dCard.CardNo = ArrayHelper.HexToArray(emp.IDCardNo.Substring(10, 8).ToUpper().Replace("X", "A"), 4);
                    }
                    break;
                case 5:
                    current = (empId - 1) * 5 + 3;
                    if (emp == null)
                        dCard.CardNo = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
                    else
                    {
                        if (emp.FingerData1 == null || emp.FingerData1.Length < 100)
                        {
                            dCard.CardNo = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
                            dCard.FingerData = new byte[1024];
                        }
                        else
                        {
                            dCard.CardNo = ArrayHelper.IntToBytes((uint)current);
                            dCard.FingerData = emp.FingerData1;
                        }
                    }
                    break;
                case 6:
                    current = (empId - 1) * 5 + 4;
                    if (emp == null)
                        dCard.CardNo = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
                    else
                        dCard.CardNo = BitConverter.GetBytes((uint)current);
                    break;
            }

            dCard.CurCardSerial = ArrayHelper.IntToBytes(current, 2);
            dCard.TatolCardSerial = ArrayHelper.IntToBytes(max, 2);
            dCard.CardStatus = ArrayHelper.IntToBytes(blackName, 1);
            dCard.CardType = ArrayHelper.IntToBytes(ticket==null?0:ticket.CardType, 1);
            dCard.Row1 = ArrayHelper.GB2312ToArray(displayContents == null ? "" : displayContents[0], 16);
            dCard.Row2 = ArrayHelper.GB2312ToArray(displayContents == null ? "" : displayContents[1], 16);
            dCard.Row3 = ArrayHelper.GB2312ToArray(displayContents == null ? "" : displayContents[2], 16);
            dCard.RightOfDoorIn = ArrayHelper.IntToBytes(ticket == null ? 0 : ticket.InRight, 1);
            dCard.RightOfDoorOut = ArrayHelper.IntToBytes(ticket == null ? 0 : ticket.OutRight, 1);
            dCard.VoiceNo = ArrayHelper.IntToBytes(ticket == null ? 0 : ticket.VoiceNo, 1);
            dCard.PhotoName = ArrayHelper.IntToBytes(emp == null ? 0 : emp.EmpId, 2);
            dCard.VacationGrop = ArrayHelper.IntToBytes(ticket == null ? 0 : ticket.VacationId, 1);
            dCard.TimeGroupOfNormalDoorIn = ArrayHelper.IntToBytes(ticket == null ? 0 : ticket.IntimeGroupNo, 1);
            dCard.TimeGroupOfNormalDoorOut = ArrayHelper.IntToBytes(ticket == null ? 0 : ticket.OutTimeGroupNo, 1);
            dCard.BeginDate = ArrayHelper.DateToArray1(Convert.ToDateTime(emp == null ? "2000-01-01" : emp.BeginDate));
            dCard.EndDate = ArrayHelper.DateToArray1(Convert.ToDateTime(emp == null ? "2000-01001" : emp.EndDate));
            return dCard;
        }


        #endregion


    }
}
