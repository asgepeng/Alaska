﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alaska.Models
{
    public class CommonResult
    {
        [JsonPropertyName("success")] public bool Success { get; set; } = false;
        [JsonPropertyName("message")] public string Message { get; set; } = "";
    }
}
