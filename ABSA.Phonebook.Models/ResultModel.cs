using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ABSA.PB.Models
{
    public class ResultModel<T>
    {
        public T Data { get; set; }
        public bool Success => string.IsNullOrEmpty(ErrorMessage) && Exception == null;
        public string ErrorMessage { get; set; }

        [JsonIgnore]
        public Exception Exception { get; set; } = null;
    }
}