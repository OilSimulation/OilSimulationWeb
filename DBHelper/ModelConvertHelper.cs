using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Management;

namespace DBHelper
{
    public class ModelConvertHelper<T> where T : new()
    {

        #region *****************  1

        private enum ModelType
        {
            //值类型
            Struct, 
            Enum,
            //引用类型
            String, 
            Object, 
            Else
        }
        private static ModelType GetModelType(Type modelType)
        {
            //值类型
            if (modelType.IsEnum) { return ModelType.Enum; }
            //值类型
            if (modelType.IsValueType) { return ModelType.Struct; }
            //引用类型 特殊类型处理
            if (modelType == typeof(string)) { return ModelType.String; }
            //引用类型 特殊类型处理
            return modelType == typeof(object) ? ModelType.Object : ModelType.Else;
        }
        public static List<T> DataTableToList<T>(DataTable table){
            var list = new List<T>();
            foreach (DataRow item in table.Rows){
                list.Add(DataRowToModel<T>(item));
            }return list;
        }


        public static T DataRowToModel<T>(DataRow row)
        {
            T model; 
            var type = typeof(T);
            var modelType = GetModelType(type);
            switch (modelType)
            {
                //值类型
                case ModelType.Struct:
                    {
                        model = default(T);
                        if (row[0] != null) model = (T)row[0];
                    } break;
                //值类型
                case ModelType.Enum:
                    {
                        model = default(T);
                        if (row[0] != null)
                        {
                            var fiType = row[0].GetType();
                            if (fiType == typeof(int))
                            {
                                model = (T)row[0];
                            }
                            else if (fiType == typeof(string))
                            {
                                model = (T)Enum.Parse(typeof(T), row[0].ToString());
                            }
                        }
                    } break;
                //引用类型 c#对string也当做值类型处理
                case ModelType.String:
                    {
                        model = default(T);
                        if (row[0] != null) model = (T)row[0];
                    } break;
                //引用类型 直接返回第一行第一列的值
                case ModelType.Object:
                    {
                        model = default(T);
                        if (row[0] != null) model = (T)row[0];
                    } break;
                //引用类型
                case ModelType.Else:
                    {
                        //引用类型 必须对泛型实例化
                        model = Activator.CreateInstance<T>();
                        //获取model中的属性
                        var modelPropertyInfos = type.GetProperties();
                        //遍历model每一个属性并赋值DataRow对应的列
                        foreach (var pi in modelPropertyInfos)
                        {
                            //获取属性名称
                            var name = pi.Name;
                            if (!row.Table.Columns.Contains(name) || row[name] == null)
                                continue;
                            var piType = GetModelType(pi.PropertyType);
                            switch (piType) { case ModelType.Struct: { var value = Convert.ChangeType(row[name], pi.PropertyType); pi.SetValue(model, value, null); } break; case ModelType.Enum: { var fiType = row[0].GetType(); if (fiType == typeof(int)) { pi.SetValue(model, row[name], null); } else if (fiType == typeof(string)) { var value = (T)Enum.Parse(typeof(T), row[name].ToString()); if (value != null) pi.SetValue(model, value, null); } } break; case ModelType.String: { var value = Convert.ChangeType(row[name], pi.PropertyType); pi.SetValue(model, value, null); } break; case ModelType.Object: { pi.SetValue(model, row[name], null); } break; case ModelType.Else: throw new Exception("不支持该类型转换"); default: throw new Exception("未知类型"); }
                        }
                    } break;
                default: model = default(T); break;
            } return model;
        }

        #endregion

        #region *****************  2

        public static IList<T> ConvertTo<T>(DataTable table)
        {
            if (table == null)
            {
                return null;
            }

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return ConvertTo<T>(rows);
        }

        public static IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;

            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }

            return list;
        }

        public static T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    try
                    {
                        object value = row[column.ColumnName];
                        if (prop.PropertyType == typeof(string))
                        {
                            prop.SetValue(obj, value.ToString(), null);
                        }
                        else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
                        {
                            prop.SetValue(obj, int.Parse(value.ToString()), null);
                        }
                        else if (prop.PropertyType == typeof(DateTime?) || prop.PropertyType == typeof(DateTime))
                        {
                            prop.SetValue(obj, DateTime.Parse(value.ToString()), null);
                        }
                        else if (prop.PropertyType == typeof(float))
                        {
                            prop.SetValue(obj, float.Parse(value.ToString()), null);
                        }
                        else if (prop.PropertyType == typeof(double))
                        {
                            prop.SetValue(obj, double.Parse(value.ToString()), null);
                        }
                        else if (prop.PropertyType == typeof(bool))
                        {
                            bool x = false;
                            string strValue = value.ToString();
                            if (strValue == "0")
                            {
                                x = false;
                            }
                            else if (strValue == "1")
                            {
                                x = true;
                            }
                            else if (strValue.ToLower() == "true")
                            {
                                x = true;
                            }
                            else if (strValue.ToLower() == "false")
                            {
                                x = false;
                            }
                            //Boolean x = Boolean.Equals("true");
                            prop.SetValue(obj, x, null);
                        }
                        else
                        {
                            prop.SetValue(obj, value, null);
                        }

                        //prop.SetValue(obj, value, null);
                    }
                    catch
                    {  //You can log something here     
                        //throw;    
                    }
                }
            }

            return obj;
        }
        #endregion
        #region ***************** 3

        public static IList<T> ConvertToModel(DataTable dt)
        {
            // 定义集合    
            IList<T> ts = new List<T>();

            // 获得此模型的类型   
            Type type = typeof(T);
            string tempName = "";

            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性      
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;  // 检查DataTable是否包含此列    

                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter      
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                        {

                            if (pi.PropertyType == typeof(string))
                            {
                                pi.SetValue(t, value.ToString(), null);
                            }
                            else if (pi.PropertyType == typeof(int) || pi.PropertyType == typeof(int?))
                            {
                                pi.SetValue(t, int.Parse(value.ToString()), null);
                            }
                            else if (pi.PropertyType == typeof(DateTime?) || pi.PropertyType == typeof(DateTime))
                            {
                                pi.SetValue(t, DateTime.Parse(value.ToString()), null);
                            }
                            else if (pi.PropertyType == typeof(float))
                            {
                                pi.SetValue(t, float.Parse(value.ToString()), null);
                            }
                            else if (pi.PropertyType == typeof(double))
                            {
                                pi.SetValue(t, double.Parse(value.ToString()), null);
                            }
                            else if (pi.PropertyType == typeof(bool))
                            {
                                bool x = false;
                                string strValue = value.ToString();
                                if (strValue == "0")
                                {
                                    x = false;
                                }
                                else if (strValue == "1")
                                {
                                    x = true;
                                }
                                else if (strValue.ToLower() == "true")
                                {
                                    x = true;
                                }
                                else if (strValue.ToLower() == "false")
                                {
                                    x = false;
                                }
                                //Boolean x = Boolean.Equals("true");
                                pi.SetValue(t, x, null);
                            }
                            else
                            {
                                pi.SetValue(t, value, null);
                            }
                        }
                    }
                }
                ts.Add(t);
            }
            return ts;
        }
        #endregion

    }
}
