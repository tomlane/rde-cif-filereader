using System;
using System.Collections.Generic;

namespace RailDataEngine.Cif.FileReader.Ext
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<CifRecord> ReadCifRecords(this IEnumerable<string> lines)
        {
            BasicScheduleCifRecord basicScheduleRecord = null;

            foreach (var line in lines)
            {
                var type = line.Substring(0, 2);

                switch (type)
                {
                    case CifRecordTypes.Header:
                    case CifRecordTypes.TiplocInsert:
                    case CifRecordTypes.TiplocAmend:
                    case CifRecordTypes.TiplocDelete:
                    case CifRecordTypes.Association:
                    case CifRecordTypes.TrailerRecord:
                        yield return new CifRecord
                        {
                            CifString = line
                        };
                        break;
                    case CifRecordTypes.BasicSchedule:
                        basicScheduleRecord = new BasicScheduleCifRecord
                        {
                            CifString = line
                        }; 
                        break;
                    case CifRecordTypes.BasicScheduleExtraDetails:
                        basicScheduleRecord.ExtraDetails = new CifRecord
                        {
                            CifString = line
                        };
                        break;
                    case CifRecordTypes.OriginLocation:
                    case CifRecordTypes.IntermediateLocation:
                    case CifRecordTypes.ChangesEnRoute:
                        basicScheduleRecord.SubRecords.Add(new CifRecord
                        {
                            CifString = line
                        });
                        break;
                    case CifRecordTypes.TerminatingLocation:
                        basicScheduleRecord.SubRecords.Add(new CifRecord
                        {
                            CifString = line
                        });
                        yield return basicScheduleRecord;
                        break;
                    default:
                        throw new ArgumentException("Unknown CIF record");
                }
            }
        }
    }
}