using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflexSmurf
{
    public class Score
    {
        public string UserName { get; set; }
        public int Value { get; set; }
        public DateTime Timestamp { get; set; }

        public Score(string userName, int value, DateTime timestamp)
        {
            UserName = userName;
            Value = value;
            Timestamp = timestamp;
        }
    }
}
