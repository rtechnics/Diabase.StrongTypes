using Diabase.StrongTypes.Types;
using Diabase.StrongTypes.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabase.StrongTypes.Tests
{
    [TestClass]
    public class StrongBytesSizeUnitTests
    {
        [TestMethod]
        public void Test()
        {
            // arrange
            const decimal bytesValue = 1_500M;

            // act
            TestByteSize bytes = 1_500M;
            TestByteSize kilobytes = (Kilobytes)bytesValue;
            TestByteSize megabytes = (Megabytes)bytesValue;
            TestByteSize gigabytes = (Gigabytes)bytesValue;
            TestByteSize terabytes = (Terabytes)bytesValue;
            TestByteSize petabytes = (Petabytes)bytesValue;

            // assert
            Assert.AreEqual(1_500M, (decimal)bytes);
            Assert.AreEqual(1_500_000M, (decimal)kilobytes);
            Assert.AreEqual(1_500_000_000M, (decimal)megabytes);
            Assert.AreEqual(1_500_000_000_000M, (decimal)gigabytes);
            Assert.AreEqual(1_500_000_000_000_000M, (decimal)terabytes);
            Assert.AreEqual(1_500_000_000_000_000_000M, (decimal)petabytes);
        }
    }

    [StrongBytesSizeUnit]
    public partial struct TestByteSize
    {

    }
}
