﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilasFitnessUI.Models
{
    public class FoodInDay
    {
        public List<FoodEntry> WhatIAte { get; set; }
        public DateTime Day { get; set; }
    }
}