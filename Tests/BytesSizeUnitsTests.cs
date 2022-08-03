using Diabase.StrongTypes.Units;

namespace Diabase.StrongTypes.Tests
{
    [TestClass]
    public class BytesSizeUnitsTests
    {
        [TestMethod]
        public void Bytes_Convert()
        {
            // arrange
            Bytes bytes = 1_500M;

            // act
            Kilobytes kilobytes = bytes.ConvertTo<Kilobytes>();
            Megabytes megabytes = bytes.ConvertTo<Megabytes>();
            Gigabytes gigabytes = bytes.ConvertTo<Gigabytes>();
            Terabytes terabytes = bytes.ConvertTo<Terabytes>();
            Petabytes petabytes = bytes.ConvertTo<Petabytes>();

            // assert
            Assert.AreEqual(1_500M, (decimal)bytes);
            Assert.AreEqual(1.5M, (decimal)kilobytes);
            Assert.AreEqual(0.001_5M, (decimal)megabytes);
            Assert.AreEqual(0.000_001_5M, (decimal)gigabytes);
            Assert.AreEqual(0.000_000_001_5M, (decimal)terabytes);
            Assert.AreEqual(0.000_000_000_001_5M, (decimal)petabytes);
        }

        [TestMethod]
        public void Bytes_Convert_Chained()
        {
            // arrange
            Bytes bytes = 1_500M;

            // act
            Kilobytes kilobytes = bytes.ConvertTo<Kilobytes>();
            Megabytes megabytes = kilobytes.ConvertTo<Megabytes>();
            Gigabytes gigabytes = megabytes.ConvertTo<Gigabytes>();
            Terabytes terabytes = gigabytes.ConvertTo<Terabytes>();
            Petabytes petabytes = terabytes.ConvertTo<Petabytes>();

            // assert
            Assert.AreEqual(1_500M, (decimal)bytes);
            Assert.AreEqual(1.5M, (decimal)kilobytes);
            Assert.AreEqual(0.001_5M, (decimal)megabytes);
            Assert.AreEqual(0.000_001_5M, (decimal)gigabytes);
            Assert.AreEqual(0.000_000_001_5M, (decimal)terabytes);
            Assert.AreEqual(0.000_000_000_001_5M, (decimal)petabytes);
        }

        [TestMethod]
        public void Gigabytes_Convert()
        {
            // arrange
            Gigabytes gigabytes = 1.5M;

            // act
            Bytes bytes = gigabytes.ConvertTo<Bytes>();
            Kilobytes kilobytes = gigabytes.ConvertTo<Kilobytes>();
            Megabytes megabytes = gigabytes.ConvertTo<Megabytes>();
            Terabytes terabytes = gigabytes.ConvertTo<Terabytes>();
            Petabytes petabytes = gigabytes.ConvertTo<Petabytes>();

            // assert
            Assert.AreEqual(1_500_000_000M, (decimal)bytes);
            Assert.AreEqual(1_500_000M, (decimal)kilobytes);
            Assert.AreEqual(1_500M, (decimal)megabytes);
            Assert.AreEqual(1.5M, (decimal)gigabytes);
            Assert.AreEqual(0.001_5M, (decimal)terabytes);
            Assert.AreEqual(0.000_001_5M, (decimal)petabytes);
        }
    }
}
