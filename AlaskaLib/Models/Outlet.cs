﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlaskaLib.Models
{
    public class Outlet
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public int Waiter { get; set; }
    }
}
