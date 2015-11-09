using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Data;
using System.Net;
using System.Net.Sockets;
namespace Thunder
{
    public class ThunderUpdAna
    {

       

        /// <summary>
        /// 使用探测仪	UsedIDS	Char(32)
        /// </summary>
        public string UsedIDS;
        /// <summary>
        /// 云地_云间标识	CG_IC	Char（4）
        /// </summary>
        public string CG_IC;
        /// <summary>
        /// 高度	Height	Float
        /// </summary>
        public float Height;
       

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Timemark;

        /// <summary>
        /// 时间的微秒。
        /// </summary>
        public double MicroSecond
        {
            get
            {
                return Timemark.Ticks % 10000000;
            }
        }

        /// <summary>
        /// 雷电经度
        /// </summary>
        public float fLongitude;

        /// <summary>
        /// 雷电纬度
        /// </summary>
        public float fLatitude;

        /// <summary>
        /// 回击峰值强度
        /// </summary>
        public float fIntensity;

        /// <summary>
        /// 回击最大陡度
        /// </summary>
        public float fSlope;

        /// <summary>
        /// 定位误差
        /// </summary>
        public float fError;

        /// <summary>
        /// 定位方式
        /// </summary>
        public byte cLocatemeth;

        /// <summary>
        /// 处理标志
        /// </summary>
        public byte ProcessFlag;

        public string sProvince, sDistrict, sCounty;

        public void ParseSD(BinaryReader br)
        {
            string DSyn = new string(br.ReadChars(3));

            string DType = new string(br.ReadChars(4));
            if (DType != "@@SD")
            {
                int DLen = br.ReadByte() * 0x100 + br.ReadByte();// Utils.ReadUShort(br, true);
                Timemark = ReadDate(br, 101);
                fLongitude = ReadFloat(br);
                fLatitude = ReadFloat(br);
                fIntensity = ReadFloat(br);
                fSlope = ReadFloat(br);
                fError = (float)Math.Round(ReadFloat(br), 1);
                cLocatemeth = ReadByte(br);
                ProcessFlag = ReadByte(br);
               
                UsedIDS = ReadString(br, 32);
                CG_IC = ReadString(br, 4);
                Height = (float)Math.Round(ReadFloat(br), 3);
               
               
                //string DFrom = ReadString(br, 4);
                //string DTo = ReadString(br, 4);
                //string DTime = ReadDate(br, 304).ToString();
                //string Sat = ReadString(br, 4);
                //string DLevel = ReadString(br, 4);

                ////状态数据个数N
                //string Num = ReadString(br, 4);
                ////探测仪编号 DecNum
                //int DecNum2 = (int)br.ReadInt16();
                ////探测仪名称 DecNum
                //string DecName = ReadString(br, 6);
                ////FrameStarte ID1
                //string FrameStarteID1 = ReadString(br, 3);
                ////FrameStarte ID2
                //string FrameStarteID2 = ReadString(br, 3);
                ////Frametag
                //byte FrameTag = ReadByte(br);
                ////导出时间参数
                //DateTime Timemark = ReadDate(br, 103);
                ////ResultOFSelfTest 最近一次自检通过标志
                //short ResultofSelfTest = ReadShort(br);
                ////Threshold 当前的阈值
                //short Threshold = ReadShort(br);
                ////TCR当前的阈值平均通过率
                //float TCR = ReadFloat(br);
                //float f = ReadFloat(br);
                ////经度
                //float Longitude = f;
                //f = ReadFloat(br);
                ////纬度
                //float Latitude = f;
                //#region V2.0 Change this, from dop float to dop short and SendorID short,but still use dop as float
                ////DOP
                //float DOP = ((float)ReadShort(br)) / 10f;
                ////闪电定位仪编号
                //short SensorID = ReadShort(br);
                //#endregion
                ////晶振偏差
                //short FrequencyError = ReadShort(br);
                ////帧校验和
                //string CheckSum = ReadByte(br).ToString();
                ////帧结束标志
                //byte FrameENDID1 = ReadByte(br);
                ////帧结束标识
                //byte FrameENDID2 = ReadByte(br);
                //Console.Write("DecNum=" + DecNum2.ToString() + "\n\r");
                //Console.Write("DecName=" + DecName + "\n\r");
                //Console.Write("Timemark=" + Timemark.ToString() + "\n\r");
                //Console.Write("ResultofSelfTest=" + ResultofSelfTest.ToString() + "\n\r");
                //Console.Write("Threshold=" + Threshold.ToString() + "\n\r");
                //Console.Write("TCR=" + TCR.ToString() + "\n\r");
                //Console.Write("logitude=" + Longitude.ToString() + "\n\r");
                //Console.Write("latitude=" + Latitude.ToString() + "\n\r");
                //Console.Write("DOP=" + DOP.ToString() + "\n\r");
                //Console.Write("FrequencyError=" + FrequencyError + "\n\r");
            }
            else
                return;

        }



        public static string ReadString(BinaryReader br, int len)
        {
            byte[] bs = br.ReadBytes(len);
            return System.Text.Encoding.Default.GetString(bs).Replace("\0", string.Empty);
        }
        public static short ReadShort(BinaryReader br)
        {
            return br.ReadInt16();
        }
        public static float ReadFloat(BinaryReader br)
        {
            return br.ReadSingle();
        }
        public static byte ReadByte(BinaryReader br)
        {
            return br.ReadByte();
        }
        public static DateTime ReadDate(BinaryReader br, int flag)
        {
            try
            {
                switch (flag)
                {
                    case 0:
                        return new DateTime(br.ReadInt64());
                    case 101:
                        //return new DateTime((int)br.ReadInt16(), (int)br.ReadByte(), (int)br.ReadByte(), (int)br.ReadByte(), (int)br.ReadByte(), (int)br.ReadByte()).AddMilliseconds(br.ReadInt32() * 0.0001);
                        return new DateTime((int)br.ReadInt16(), (int)br.ReadByte(), (int)br.ReadByte(), (int)br.ReadByte(), (int)br.ReadByte(), (int)br.ReadByte()).AddTicks(br.ReadInt32());
                    case 102:
                        return new DateTime((int)br.ReadInt16(), (int)br.ReadByte(), (int)br.ReadByte(), (int)br.ReadByte(), (int)br.ReadByte(), (int)br.ReadByte());
                    case 201:
                        return new DateTime((int)br.ReadInt16(), (int)br.ReadByte(), (int)br.ReadByte());
                    case 301:
                        return DateTime.ParseExact(new string(br.ReadChars(24)), "yyyy-MM-dd HH:mm:ss.ffff", System.Globalization.DateTimeFormatInfo.CurrentInfo);
                    case 302:
                        return DateTime.ParseExact(new string(br.ReadChars(19)), "yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.CurrentInfo);
                    case 303:
                        return DateTime.ParseExact(new string(br.ReadChars(18)), "yyyyMMddHHmmssffff", System.Globalization.DateTimeFormatInfo.CurrentInfo);
                    case 304:
                        string s304 = new string(br.ReadChars(17)).Replace("\0", "0");
                        return DateTime.ParseExact(s304, "yyyyMMddHHmmssfff", System.Globalization.DateTimeFormatInfo.CurrentInfo);
                    case 305:
                        return DateTime.ParseExact(new string(br.ReadChars(14)), "yyyyMMddHHmmss", System.Globalization.DateTimeFormatInfo.CurrentInfo);
                    case 306:
                        return DateTime.ParseExact(new string(br.ReadChars(8)), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.CurrentInfo);
                    case 307:
                        return DateTime.ParseExact(new string(br.ReadChars(23)), "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.DateTimeFormatInfo.CurrentInfo);
                    case 103:
                        int tmp103i1 = br.ReadByte(), tmp103i2 = br.ReadByte(), tmp103i3 = br.ReadByte();
                        return new DateTime((int)br.ReadInt16(), (int)br.ReadByte(), (int)br.ReadByte(), tmp103i1, tmp103i2, tmp103i3);
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return DateTime.MinValue;
        }

        public static int ReadUShort(BinaryReader br)
        {
            return ReadUShort(br, false);
        }
        public static int ReadUShort(BinaryReader br, bool UseBigEndian)
        {
            return UseBigEndian ? br.ReadByte() * 0x100 + br.ReadByte() : br.ReadUInt16();
        }
       
    }
}