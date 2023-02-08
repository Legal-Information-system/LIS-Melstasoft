﻿using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    [Serializable]
    public class Functions
    {
        [DBField("function_id")]
        public int FunctionId { get; set; }

        [DBField("function_name")]
        public string FunctionName { get; set; }

        public string Status { get; set; }
    }
}
