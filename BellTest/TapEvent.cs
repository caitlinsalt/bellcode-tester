using System;

namespace BellTest
{
    public class TapEvent
    {
        public TapEventType EventType { get; private set; }
        public DateTime Timestamp { get; private set; }
        public bool Absorbed { get; set; }

        public TapEvent(TapEventType type)
        {
            EventType = type;
            Timestamp = DateTime.Now;
        }
    }
}
