using System;
using System.Linq;
using RailDataEngine.Cif.FileReader.Ext;
using Xunit;

namespace RailDataEngine.Cif.FileReader.Tests
{
    public class TReadCifRecords
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void given_a_null_or_empty_record_exception_is_thrown(string record)
        {
            var records = new[]
            {
                record
            };

            Assert.Throws<ArgumentNullException>(() => records.ReadCifRecords().ToArray());
        }

        [Fact]
        public void given_a_record_that_is_longer_than_80_characters_exception_is_thrown()
        {
            var records = new[]
            {
                "REC".PadRight(100)
            };

            Assert.Throws<ArgumentException>(() => records.ReadCifRecords().ToArray());
        }

        [Fact]
        public void given_a_record_that_is_shorter_than_80_characters_exception_is_thrown()
        {
            var records = new[]
            {
                "REC".PadRight(10)
            };

            Assert.Throws<ArgumentException>(() => records.ReadCifRecords().ToArray());
        }

        [Fact]
        public void given_a_single_record_single_cif_is_returned()
        {
            var records = new[]
            {
                CifRecordTypes.Header.PadRight(80),
                CifRecordTypes.TiplocInsert.PadRight(80),
                CifRecordTypes.TiplocAmend.PadRight(80),
                CifRecordTypes.TiplocDelete.PadRight(80),
                CifRecordTypes.Association.PadRight(80),
                CifRecordTypes.TrailerRecord.PadRight(80),
            };

            foreach (var cifRecord in records.ReadCifRecords())
            {
                Assert.IsType<CifRecord>(cifRecord);
            }
        }

        [Fact]
        public void given_a_basic_schedule_record_schedule_cif_is_returned()
        {
            var records = new[]
            {
                CifRecordTypes.BasicSchedule.PadRight(80),
                CifRecordTypes.BasicScheduleExtraDetails.PadRight(80),
                CifRecordTypes.OriginLocation.PadRight(80),
                CifRecordTypes.IntermediateLocation.PadRight(80),
                CifRecordTypes.ChangesEnRoute.PadRight(80),
                CifRecordTypes.IntermediateLocation.PadRight(80),
                CifRecordTypes.TerminatingLocation.PadRight(80),
            };

            var cifSchedule = Assert.IsType<BasicScheduleCifRecord>(records.ReadCifRecords().First());

            Assert.Contains(CifRecordTypes.BasicSchedule, cifSchedule.CifString);

            Assert.NotNull(cifSchedule.ExtraDetails);
            Assert.Contains(CifRecordTypes.BasicScheduleExtraDetails, cifSchedule.ExtraDetails.CifString);

            Assert.Equal(5, cifSchedule.SubRecords.Count);
        }

        [Fact]
        public void given_an_unknown_record_type_exception_is_thrown()
        {
            var badRecords = new[]
            {
                "XX".PadRight(80)
            };

            Assert.Throws<BadScheduleFileFormatException>(() => badRecords.ReadCifRecords().ToArray());
        }
    }
}
