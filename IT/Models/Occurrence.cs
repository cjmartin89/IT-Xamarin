using System;

namespace IT.Models
{
    public class Occurrence
    {
        public string ID { get; set; }
        public int pk { get; set; }
        public int TimeWrong { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public DateTime dateTime { get; set; }
    }
}
