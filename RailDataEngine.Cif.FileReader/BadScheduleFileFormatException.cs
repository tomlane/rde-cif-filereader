using System;

namespace RailDataEngine.Cif.FileReader
{
    public class BadScheduleFileFormatException : Exception
    {
        public BadScheduleFileFormatException(string message)
            : base(message) { }
    }
}