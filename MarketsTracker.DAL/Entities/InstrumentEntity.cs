using System;
using System.Collections.Generic;
using System.Text;

namespace MarketsTracker.DAL.Entities
{
    public class InstrumentEntity
    {
        public int InstrumentId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string InstrumentType { get; set; }

    }
}
