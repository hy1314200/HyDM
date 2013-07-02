//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using ESRI.ArcGIS.Controls;
//using System.IO;
//using ESRI.ArcGIS.Geometry;
//using VCTEXCHANGELib;

//namespace Check.Task.CustomClass
//{
//    /// <summary>
//    /// VCT文件整体检查类
//    /// </summary>
//   public  class VctIntegerityCheck
//    {
//        /// <summary>
//        /// 根据比例尺代码获取比例尺
//        /// </summary>
//        /// <param name="strCode">比例尺代码，比如：G</param>
//        /// <returns>默认缺省值返回1:10000</returns>
//        public static string GetScale(string strCode)
//        {
//            string strScale = null;
//            switch (strCode)
//            {
//                case "I":
//                    strScale = "1:2000";
//                    break;
//                case "H":
//                    strScale = "1:5000";
//                    break;
//                case "G":
//                    strScale = "1:10000";
//                    break;
//                case "F":
//                    strScale = "1:25000";
//                    break;
//                case "E":
//                    strScale = "1:50000";
//                    break;
//                case "D":
//                    strScale = "1:100000";
//                    break;
//                case "C":
//                    strScale = "1:250000";
//                    break;
//                case "B":
//                    strScale = "1:500000";
//                    break;
//                default:
//                    strScale = "1:10000";
//                    break;
//            }
//            return strScale;
//        }

//        /// <summary>
//        /// 从VCT数据获取空间参考
//        /// </summary>
//        /// <param name="VCTReadClass"></param>
//        /// <returns></returns>
//        public static ISpatialReference GetVCTSpatialRef(VctReaderClass VCTReadClass)
//        {

//            //得到空间参考
//            ISpatialReference pSpatialRef = VCTReadClass.SpatialReference as ISpatialReference;
//            return pSpatialRef;
//        }

//        /// <summary>
//        /// 读取vct数据的头文件信息，并判断头文件是否和标准符合
//        /// </summary>
//        /// <param name="strFilePath">vct数据路径</param>
//        /// <param name="headInfo">vct头文件类</param>
//        public static void ReadVCTFile(string strFilePath, ref VCTHeadInfo headInfo, out  List<string> errors)
//        {
//            FileStream fs = File.OpenRead(strFilePath);
//            StreamReader streamReader = new StreamReader(fs);

//            ///读取HeadBegin和HeadEnd之间的头文件信息
//            string strHeaderBegin = streamReader.ReadLine();
//            string[] lstHead = new string[17];
//            lstHead[0] = streamReader.ReadLine();
//            lstHead[1] = streamReader.ReadLine();
//            lstHead[2] = streamReader.ReadLine();
//            lstHead[3] = streamReader.ReadLine();
//            lstHead[4] = streamReader.ReadLine();
//            lstHead[5] = streamReader.ReadLine();
//            lstHead[6] = streamReader.ReadLine();
//            lstHead[7] = streamReader.ReadLine();
//            lstHead[8] = streamReader.ReadLine();
//            lstHead[9] = streamReader.ReadLine();
//            lstHead[10] = streamReader.ReadLine();
//            lstHead[11] = streamReader.ReadLine();
//            lstHead[12] = streamReader.ReadLine();
//            lstHead[13] = streamReader.ReadLine();
//            lstHead[14] = streamReader.ReadLine();
//            lstHead[15] = streamReader.ReadLine();
//            lstHead[16] = streamReader.ReadLine();
//            string strHeaderEnd = streamReader.ReadLine();

//            errors = new List<string>();
//            ///获取vct头文件类
//            GetVCTHeadInfo(strFilePath, lstHead, ref headInfo,ref errors);

//            ///关闭文件流
//            fs.Close();
//            fs.Dispose();
//        }

//        /// <summary>
//        /// 根据vct头文件，获取vct头文件的结构体
//        /// </summary>
//        /// <param name="strFilePath">vct文件路径</param>
//        /// <param name="lstHead">vct头文件信息</param>
//        /// <param name="headInfo">vct头文件类</param>
//        /// <param name="errors"></param>
//        public static void GetVCTHeadInfo(string strFilePath, string[] lstHead, ref VCTHeadInfo headInfo,ref List<string> errors)
//        {
//            ///检查HeadBegin和HeadEnd之间的头文件信息是否和标准相符
//            for (int i = 0; i < lstHead.Length; i++)
//            {
//                string strTemp = lstHead[i];
//                if (strTemp == null)
//                {
//                    continue;
//                }
//                string[] subStr = strTemp.Split(':');

//                switch (i)
//                {
//                    case 0:
//                        {
//                            if (subStr[0].ToLower().Trim() != "datamark")
//                            {
//                                string strLog = strFilePath + "头文件中无数据标识记录";
//                                errors.Add(strLog);
//                            }
//                            else if (subStr[1].ToUpper().Trim() != "LANDUSE.VCT")
//                            {
//                                string strLog = strFilePath + "不是土地利用数据";
//                                errors.Add(strLog);
//                            }
//                            else
//                            {
//                                headInfo.strDataMark = subStr[1].Trim();
//                            }
//                            break;
//                        }
//                    case 1:
//                        {
//                            if (subStr[0].ToLower().Trim() != "version")
//                            {
//                                string strLog = strFilePath + "头文件中无版本记录";
//                                errors.Add(strLog);
//                            }
//                            else if (subStr[1].Trim() != "2.0")
//                            {
//                                string strLog = strFilePath + "头文件中版本号不为2.0";
//                                errors.Add(strLog);
//                            }
//                            else
//                            {
//                                headInfo.strVersion = subStr[1].Trim();
//                            }
//                            break;
//                        }
//                    case 2:
//                        {
//                            if (subStr[0].ToLower().Trim() != "unit")
//                            {
//                                string strLog = strFilePath + "头文件中无坐标单位记录";
//                                errors.Add(strLog);
//                            }
//                            else
//                            {
//                                headInfo.strUnit = subStr[1].Trim();
//                            }
//                            break;
//                        }
//                    case 3:
//                        {
//                            if (subStr[0].ToLower().Trim() != "dim")
//                            {
//                                string strLog = strFilePath + "头文件中无坐标维数记录";
//                                errors.Add(strLog);
//                            }
//                            else if (subStr[1].Trim() != "2" && subStr[1].Trim() != "3")
//                            {
//                                string strLog = strFilePath + "头文件中坐标维数为" + subStr[1] + "，与标准不符";
//                                errors.Add(strLog);
//                            }
//                            else
//                            {
//                                headInfo.nDim = Convert.ToInt32(subStr[1].Trim());
//                            }
//                            break;
//                        }
//                    case 4:
//                        {
//                            if (subStr[0].ToLower().Trim() != "topo")
//                            {
//                                string strLog = strFilePath + "头文件中无拓扑关系记录";
//                                errors.Add(strLog);
//                            }
//                            else if (subStr[1].Trim() != "1")
//                            {
//                                string strLog = strFilePath + "的矢量数据交换格式为" + strTemp + "，与标准不符";
//                                errors.Add(strLog);
//                            }
//                            else
//                            {
//                                headInfo.nTopo = Convert.ToInt32(subStr[1].Trim());
//                            }
//                            break;
//                        }
//                    case 5:
//                        {
//                            if (subStr[0].ToLower().Trim() != "coordinate")
//                            {
//                                string strLog = strFilePath + "头文件中无坐标系记录";
//                                errors.Add(strLog);
//                            }
//                            else if (subStr[1].ToUpper().Trim() != "G" && subStr[1].ToUpper().Trim() != "M")
//                            {
//                                string strLog = strFilePath + "的坐标系为" + strTemp + "，与标准不符";
//                                errors.Add(strLog);
//                            }
//                            else
//                            {
//                                headInfo.strCoordinate = subStr[1].Trim();
//                            }
//                            break;
//                        }
//                    case 6:
//                        {
//                            if (subStr[0].ToLower().Trim() != "projection")
//                            {
//                                string strLog = strFilePath + "头文件中无投影类型记录";
//                                errors.Add(strLog);
//                            }
//                            else
//                            {
//                                headInfo.strProjection = subStr[1].Trim();
//                            }
//                            break;
//                        }
//                    case 7:
//                        {
//                            if (subStr[0].ToLower().Trim() != "spheroid")
//                            {
//                                string strLog = strFilePath + "头文件中无参考椭球体记录";
//                                errors.Add(strLog);
//                            }
//                            else
//                            {
//                                headInfo.strSpheroid = subStr[1].Trim();
//                            }
//                            break;
//                        }
//                    case 8:
//                        {
//                            if (subStr[0].ToLower().Trim() != "parameters")
//                            {
//                                string strLog = strFilePath + "头文件中无投影参数记录";
//                                errors.Add(strLog);
//                            }
//                            else
//                            {
//                                string[] strPara = subStr[1].Split(',');
//                                headInfo.nParameters = new List<int>();
//                                headInfo.nParameters.Add(Convert.ToInt32(strPara[0]));
//                                headInfo.nParameters.Add(Convert.ToInt32(strPara[1]));
//                            }
//                            break;
//                        }
//                    case 9:
//                        {
//                            if (subStr[0].ToLower().Trim() != "meridian")
//                            {
//                                string strLog = strFilePath + "头文件中无中央子午线经度记录";
//                                errors.Add(strLog);
//                            }
//                            else
//                            {
//                                headInfo.dMeridian = Math.Round(Convert.ToDouble(subStr[1].Trim()), 6);
//                            }
//                            break;
//                        }
//                    case 10:
//                        {
//                            if (subStr[0].ToLower().Trim() != "minx")
//                            {
//                                string strLog = strFilePath + "头文件中无最小X坐标记录";
//                                errors.Add(strLog);
//                            }
//                            else
//                            {
//                                headInfo.dMinX = Math.Round(Convert.ToDouble(subStr[1].Trim()), 6);
//                            }
//                            break;
//                        }
//                    case 11:
//                        {
//                            if (subStr[0].ToLower().Trim() != "miny")
//                            {
//                                string strLog = strFilePath + "头文件中无最小y坐标记录";
//                                errors.Add(strLog);
//                            }
//                            else
//                            {
//                                headInfo.dMinY = Math.Round(Convert.ToDouble(subStr[1].Trim()), 6);
//                            }
//                            break;
//                        }
//                    case 12:
//                        {
//                            if (subStr[0].ToLower().Trim() != "maxx")
//                            {
//                                string strLog = strFilePath + "头文件中无最大X坐标记录";
//                                errors.Add(strLog);
//                            }
//                            else
//                            {
//                                headInfo.dMaxX = Math.Round(Convert.ToDouble(subStr[1].Trim()), 6);
//                            }
//                            break;
//                        }
//                    case 13:
//                        {
//                            if (subStr[0].ToLower().Trim() != "maxy")
//                            {
//                                string strLog = strFilePath + "头文件中无最大y坐标记录";
//                                errors.Add(strLog);
//                            }
//                            else
//                            {
//                                headInfo.dMaxY = Math.Round(Convert.ToDouble(subStr[1].Trim()), 6);
//                            }
//                            break;
//                        }
//                    case 14:
//                        {
//                            if (subStr[0].ToLower().Trim() != "scale")
//                            {
//                                string strLog = strFilePath + "头文件中无比例尺记录";
//                                errors.Add(strLog);
//                            }
//                            else
//                            {
//                                headInfo.strScale = subStr[1].Trim();
//                            }
//                            break;
//                        }
//                    case 15:
//                        {
//                            if (subStr[0].ToLower().Trim() != "date")
//                            {
//                                string strLog = strFilePath + "头文件中无调查日期记录";
//                                errors.Add(strLog);
//                            }
//                            else
//                            {
//                                headInfo.date = Convert.ToDateTime(subStr[1].Trim());
//                            }
//                            break;
//                        }
//                    case 16:
//                        {
//                            if (subStr[0].ToLower().Trim() != "separator")
//                            {
//                                string strLog = strFilePath + "头文件中无分隔符记录";
//                                errors.Add(strLog);
//                            }
//                            else
//                            {
//                                headInfo.cSeparator = Convert.ToChar(subStr[1].Trim());
//                            }
//                            break;
//                        }
//                }
//            }
//        }
//    }
//}
