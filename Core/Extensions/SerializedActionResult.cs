using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class SerializedActionResult : IActionResult
    {
        public SerializedActionResult(object data)
        {
            this.Data = data;
        }
        public object Data { get; private set; }
        public async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(memoryStream))
                {
                    using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
                    {
                        serializer.Serialize(jsonWriter, Data);
                    }
                }
                byte[] byteArray = memoryStream.ToArray();
                await context.HttpContext.Response.Body.WriteAsync(byteArray);
            }
        }















    }
}
