using System.Collections.Generic;

namespace RailDataEngine.Cif.FileReader
{
    public class BasicScheduleCifRecord : CifRecord
    {
        public CifRecord ExtraDetails { get; set; }
        public ICollection<CifRecord> SubRecords { get; set; } = new List<CifRecord>();
    }
}