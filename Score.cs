using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflexSmurf
{
    public class Score
    {
        public int Value { get; set; }
        public DateTime Timestamp { get; set; }

        public Score(int value, DateTime timestamp)
        {
            Value = value;
            Timestamp = timestamp;
        }
    }
}
