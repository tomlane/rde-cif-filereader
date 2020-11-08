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
                if (string.IsNullOrWhiteSpace(line))
                    throw new ArgumentNullException(nameof(line));

                if (line.Length != 80)
                    throw new ArgumentException("CIF record must have a length of 80.");

                var type = line.AsSpan().Slice(0, 2).ToString();

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
                        throw new BadScheduleFileFormatException($"Unexpected CIF record - {type}");
                }
            }
        }
    }
}