using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Xml.Serialization;
using My.App.Business.Service.JD.Util;

namespace My.App.Business.Service.JD.Parser
{
    internal class JsonParser<T> : IParser<T> where T : ApiResponse
    {
        private static readonly Dictionary<string, Dictionary<string, Attribute>> attrs = new Dictionary<string, Dictionary<string, Attribute>>();
        
        public T Parse(string body) {
            T rsp = null;

            IDictionary json = Jayrock.Json.Conversion.JsonConvert.Import(body) as IDictionary;
            if (json != null) {
                IDictionary data = null;

                // 忽略根节点的名称
                foreach (object key in json.Keys) {
                    data = json[key] as IDictionary;
                    break;
                }

                if (data != null) {
                    IReader reader = new JsonReader(data);
                    rsp = (T)DJsonConvert(reader, typeof(T));
                }
            }

            if (rsp == null) {
                rsp = Activator.CreateInstance<T>();
            }

            if (rsp != null) {
                rsp.Body = body;
            }

            return rsp;
        }

        protected static readonly DConvert DJsonConvert = delegate(IReader reader, Type type) {
            object rsp = null;
            Dictionary<string, Attribute> pas = GetAttributes(type);

            Dictionary<string, Attribute>.Enumerator em = pas.GetEnumerator();
            while (em.MoveNext()) {
                KeyValuePair<string, Attribute> kvp = em.Current;
                Attribute ta = kvp.Value;
                string itemName = ta.ItemName;
                string listName = ta.ListName;
              
                if (!reader.HasReturnField(itemName) && (string.IsNullOrEmpty(listName) || !reader.HasReturnField(listName))) {
                    continue;
                }

                object value = null; 
                if (ta.ListType != null) {
                    value = reader.GetListObjects(listName, itemName, ta.ListType, DJsonConvert);
                } else {
                    if (typeof(string) == ta.ItemType) {
                        object tmp = null;
                        if (!string.IsNullOrEmpty(listName)) {
                            tmp = reader.GetPrimitiveObject(itemName, listName);
                        } else { 
                            tmp = reader.GetPrimitiveObject(itemName); 
                        }
                        if (tmp != null) {
                            value = tmp.ToString();
                        }
                    } else if (typeof(long) == ta.ItemType) {
                        object tmp = null;
                        if (!string.IsNullOrEmpty(listName)) {
                            tmp = reader.GetPrimitiveObject(itemName, listName);
                        } else { 
                            tmp = reader.GetPrimitiveObject(itemName); 
                        }
                        if (tmp != null) {
                            value = ((IConvertible)tmp).ToInt64(null);
                        }
                    } else if (typeof(bool) == ta.ItemType) {
                        object tmp = null;
                        if (!string.IsNullOrEmpty(listName)) {
                            value = reader.GetPrimitiveObject(itemName, listName);
                        } else {
                            value = reader.GetPrimitiveObject(itemName); 
                        }
                    } else {
                        value = reader.GetReferenceObject(itemName, ta.ItemType, DJsonConvert);
                    }
                }

                if (value != null) {
                    if (rsp == null) {
                        rsp = Activator.CreateInstance(type);
                    }
                    ta.Method.Invoke(rsp, new object[] { value });
                }
            }

            return rsp;
        };

        private static Dictionary<string, Attribute> GetAttributes(Type type) {
            Dictionary<string, Attribute> tas = null;
            bool inc = attrs.TryGetValue(type.FullName, out tas);

            if (inc && tas != null) {  // 从缓存中获取类属性元数据
                return tas;
            } else { // 创建新的类属性元数据缓存
                tas = new Dictionary<string, Attribute>();
            }

            PropertyInfo[] pis = type.GetProperties();
            foreach (PropertyInfo pi in pis) {
                Attribute ta = new Attribute();
                ta.Method = pi.GetSetMethod();

                // 获取对象属性名称
                XmlElementAttribute[] xeas = pi.GetCustomAttributes(typeof(XmlElementAttribute), true) as XmlElementAttribute[];
                if (xeas != null && xeas.Length > 0) {
                    ta.ItemName = xeas[0].ElementName;
                }

                // 获取列表属性名称
                if (ta.ItemName == null) {
                    XmlArrayItemAttribute[] xaias = pi.GetCustomAttributes(typeof(XmlArrayItemAttribute), true) as XmlArrayItemAttribute[];
                    if (xaias != null && xaias.Length > 0) {
                        ta.ItemName = xaias[0].ElementName;
                    }

                    XmlArrayAttribute[] xaas = pi.GetCustomAttributes(typeof(XmlArrayAttribute), true) as XmlArrayAttribute[];
                    if (xaas != null && xaas.Length > 0) {
                        ta.ListName = xaas[0].ElementName;
                    }

                    if (ta.ListName == null) {
                        continue;
                    }
                }

                // 获取属性类型
                if (pi.PropertyType.IsGenericType) {
                    Type[] types = pi.PropertyType.GetGenericArguments();
                    ta.ListType = types[0];
                } else {
                    ta.ItemType = pi.PropertyType;
                }

                tas.Add(pi.Name, ta);
            }

            attrs[type.FullName] = tas;
            return tas;
        }
    }
}
