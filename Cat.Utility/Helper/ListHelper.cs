using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace Cat.Utility
{
    public class ListHelper
    {
        public static List<T> DataReaderToList<T>(DbDataReader SDR) where T : class
        {
            List<T> ListData = new List<T>();

            if (SDR.HasRows)
            {
                ListData.Clear();
                while (SDR.Read())
                {
                    object Obj = System.Activator.CreateInstance(typeof(T));
                    Type ObjType = Obj.GetType();
                    #region 一行数据赋值给一个对象
                    for (int i = 0; i < SDR.FieldCount; i++)
                    {
                        PropertyInfo PI = ObjType.GetProperty(SDR.GetName(i));
                        if (PI != null)
                        {
                            string PTName = PI.PropertyType.Name.ToString();
                            string FullName = PI.PropertyType.FullName;
                            string Name = PI.Name;

                            object Value = PI.GetValue(Obj, null);

                            switch (PI.PropertyType.ToString())
                            {
                                case "System.Int64":
                                    PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToInt64(SDR[Name]), null);
                                    break;
                                case "System.Byte[]":
                                    PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : (byte[])SDR[Name], null);
                                    break;
                                case "System.Boolean":
                                    PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToBoolean(SDR[Name]), null);
                                    break;
                                case "System.String":
                                    PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToString(SDR[Name]), null);
                                    break;
                                case "System.DateTime":
                                    PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToDateTime(SDR[Name]), null);
                                    break;
                                case "System.Decimal":
                                    PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToDecimal(SDR[Name]), null);
                                    break;
                                case "System.Double":
                                    PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToDouble(SDR[Name]), null);
                                    break;
                                case "System.Int32":
                                    PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToInt32(SDR[Name]), null);
                                    break;
                                case "System.Single":
                                    PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToSingle(SDR[Name]), null);
                                    break;
                                case "System.Byte":
                                    PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToByte(SDR[Name]), null);
                                    break;
                                default:
                                    int Chindex = PTName.IndexOf("Nullable");
                                    if (FullName.IndexOf("System.Int64") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToInt64(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.Boolean") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToBoolean(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.String") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToString(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.DateTime") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToDateTime(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.Decimal") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToDecimal(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.Double") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToDouble(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.Int32") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToInt32(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.Single") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToSingle(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.Byte") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToByte(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.Int16") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToInt16(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.UInt16") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToUInt16(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.UInt32") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToUInt32(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.UInt64") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToUInt64(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.SByte") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToSByte(SDR[Name]), null);
                                    }
                                    break;
                            }

                        }
                    }
                    #endregion
                    ListData.Add(Obj as T);
                }
            }
            if (!SDR.IsClosed)
                SDR.Close();

            return ListData;
        }
    }
}
