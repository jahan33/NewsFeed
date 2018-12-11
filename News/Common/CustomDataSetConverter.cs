using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace News.Common
{
    public class CustomDataSetConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(DataSet));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DataSet x = (DataSet)value;
            JObject jObject = new JObject();
            // DataTable a = x.Tables["A"];
            //foreach (DataColumn col in a.Columns)
            //{
            //    jObject.Add(col.Caption.ToLower(), a.Rows[0][col].ToString());
            //}
            dynamic val = null;
            foreach (DataTable b in x.Tables)
            {
                JArray jArray = new JArray();

                foreach (DataRow row in b.Rows)
                {
                    JObject jo = new JObject();


                    foreach (DataColumn col in b.Columns)
                    {
                        val = row[col];
                        if (val == null || DBNull.Value.Equals(val))
                        {
                            val = "";
                        }
                        jo.Add(col.Caption, val);
                    }
                    jArray.Add(jo);
                }
                jObject.Add(b.TableName, jArray);
            }
            jObject.WriteTo(writer);
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

}
