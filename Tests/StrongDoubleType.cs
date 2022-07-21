namespace Diabase.StrongTypes.Tests
{
    [TestClass]
    public class StrongDoubleType
    {
        [TestMethod]
        public void Assignments()
        {

            // arrange
            const double const1 = 1.1;
            const double const2 = 2.2;
            double? const3 = null;

            // act
            TestDoubleA sta1 = const1;
            TestDoubleA sta2 = const2;
            TestDoubleA? sta3 = const3;

#if false // when set to true, the following line should be an error demonstrating the strong type prevents the assignment
            TestIntB sib1 = sta1;
#endif

            double var1 = sta1;
            double var2 = sta2;

            // assert
            Assert.AreEqual(const1, var1);
            Assert.AreEqual(const2, var2);
            Assert.IsTrue(sta1 == (TestDoubleA)const1);
            Assert.IsTrue(sta1 == const1);
            Assert.IsFalse(sta1 == sta2);
            Assert.IsTrue(sta1 != sta2);
            Assert.IsNull(sta3);
            Assert.AreEqual(double.MaxValue, TestDoubleA.MaxValue);
            Assert.AreEqual(double.MinValue, TestDoubleA.MinValue);
        }


    }

    [StrongDoubleType]
    public readonly partial struct TestDoubleA
    {
    }

    [StrongDoubleType]
    public readonly partial struct TestDoubleB
    {
    }
}
