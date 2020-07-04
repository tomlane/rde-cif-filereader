namespace RailDataEngine.Cif.FileReader
{
    public class CifRecordTypes
    {
        public const string Header = "HD";
        public const string TiplocInsert = "TI";
        public const string TiplocAmend = "TA";
        public const string TiplocDelete = "TD";
        public const string Association = "AA";
        public const string BasicSchedule = "BS";
        public const string BasicScheduleExtraDetails = "BX";
        public const string OriginLocation = "LO";
        public const string IntermediateLocation = "LI";
        public const string ChangesEnRoute = "CR";
        public const string TerminatingLocation = "LT";
        public const string TrailerRecord = "ZZ";
    }
}