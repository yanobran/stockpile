using System;
using System.Collections.Generic;
using System.Text;

namespace StockPile.Data.MemoryDbImpl
{
    public class MemoryDbConfig
    {
        public MemoryDbConfig() { DataPath = ""; }
        public string DataPath { get; set; }
    }
}
