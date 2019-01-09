using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ron.JsonTest
{
    [JsonObject(MemberSerialization.OptIn)]
    public partial class JsonReturn : ContentResult
    {
        [JsonProperty] public int Code { get; protected set; }
        [JsonProperty] public string Message { get; protected set; }
        [JsonProperty] public Hashtable Data { get; protected set; } = new Hashtable();

        public JsonReturn(int code, string message) { this.Code = code; this.SetMessage(message); }

        public JsonReturn SetMessage(string value) { this.Message = value; return this; }

        public JsonReturn SetData(params object[] value)
        {
            this.Data.Clear();
            return this.AppendData(value);
        }

        public JsonReturn AppendData(params object[] value)
        {
            if (value?.Length < 2)
                return this;

            for (int a = 0; a < value.Length; a += 2)
            {
                if (value[a] == null) continue;
                this.Data[value[a]] = a + 1 < value.Length ? value[a + 1] : null;
            }
            return this;
        }

        private void ToJson(ActionContext context)
        {
            this.ContentType = "text/json;charset=utf-8;";
            this.Content = JsonConvert.SerializeObject(this);
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            ToJson(context);
            return base.ExecuteResultAsync(context);
        }

        public override void ExecuteResult(ActionContext context)
        {
            ToJson(context);
            base.ExecuteResult(context);
        }

        /// <summary>
        /// 成功 0
        /// </summary>
        public static JsonReturn 成功 { get { return new JsonReturn(0, "成功"); } }

        /// <summary>
        ///  失败 500
        /// </summary>
        public static JsonReturn 失败 { get { return new JsonReturn(500, "失败"); } }
    }
}
