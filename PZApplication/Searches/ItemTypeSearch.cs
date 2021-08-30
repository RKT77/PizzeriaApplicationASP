﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzeriaApplication.Searches
{
    public class ItemTypeSearch
    {
        [StringLength(40, ErrorMessage = "Text too long")]
        public string Name { get; set; }
    }
}
