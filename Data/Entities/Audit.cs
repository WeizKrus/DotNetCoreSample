using System;

namespace AgEntities.CustomEntities
{
    public class Audit
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        // public System.DateTime DateTime { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
    }
}