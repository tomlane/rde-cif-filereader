using System.Collections.Generic;

namespace BasicCifParser
{
    public class BasicScheduleCifRecord : CifRecord
    {
        public CifRecord ExtraDetails { get; set; }
        public ICollection<CifRecord> SubRecords { get; set; } = new List<CifRecord>();
    }
}