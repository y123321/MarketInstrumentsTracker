
namespace MarketsTracker.Model
{
    public class Instrument
    {
        public int InstrumentId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        //in the future, turning InstrumentType to an enum with a lookup table could make sense
        public string InstrumentType { get; set; }
    }
}