using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace My.App.Business.Service.JD.Parser
{
    /// <summary>
    /// JSON响应通用读取器。
    /// </summary>
    internal class JsonReader : IReader
    {
        private IDictionary json;

        public JsonReader(IDictionary json) {
            this.json = json;
        }

        public bool HasReturnField(object name) {
            return json.Contains(name);
        }

        public object GetPrimitiveObject(object name) {
            return json[name];
        }

        public object GetPrimitiveObject(object name, object listName) {
            return (json[listName] as IDictionary)[name];
        }

        public object GetReferenceObject(object name, Type type, DConvert convert) {
            IDictionary dict = json[name] as IDictionary;
            if (dict != null && dict.Count > 0) {
                return convert(new JsonReader(dict), type);
            } else {
                return null;
            }
        }

        public IList GetListObjects(string listName, string itemName, Type type, DConvert convert) {
            IList listObjs = null;
            IDictionary jsonMap = null;
            if (String.IsNullOrEmpty(listName)) {
                jsonMap = json;
            } else { jsonMap = json[listName] as IDictionary; }
            if (jsonMap != null && jsonMap.Count > 0) {
                IList jsonList = jsonMap[itemName] as IList;
                if (jsonList != null && jsonList.Count > 0) {
                    Type listType = typeof(List<>).MakeGenericType(new Type[] { type });
                    listObjs = Activator.CreateInstance(listType) as IList;
                    foreach (object item in jsonList) {
                        if (typeof(IDictionary).IsAssignableFrom(item.GetType())) {
                            IDictionary subMap = item as IDictionary;
                            object subObj = convert(new JsonReader(subMap), type);
                            if (subObj != null) {
                                listObjs.Add(subObj);
                            }
                        } else if (typeof(IList).IsAssignableFrom(item.GetType())) {
                           
                        } else {
                            listObjs.Add(item);
                        }
                    }
                }
            }

            return listObjs;
        }
    }
}
