using Diabase.StrongTypes.Types;

namespace Diabase.StrongTypes.Tests
{
    [TestClass]
    public class StrongIntType
    {
        [TestMethod]
        public void Assignments()
        {

            // arrange
            const int const1 = 1;
            const int const2 = 2;
            int? const3 = null;

            // act
            TestIntA sta1 = const1;
            TestIntA sta2 = const2;
            TestIntA? sta3 = const3;

#if false // when set to true, the following line should be an error demonstrating the strong type prevents the assignment
            TestIntB stb1 = sta1;
#endif

            int var1 = sta1;
            int var2 = sta2;

            // assert
            Assert.AreEqual(const1, var1);
            Assert.AreEqual(const2, var2);
            Assert.IsTrue(sta1 == (TestIntA)const1);
            Assert.IsTrue(sta1 == const1);
            Assert.IsFalse(sta1 == sta2);
            Assert.IsTrue(sta1 != sta2);
            Assert.IsNull(sta3);
            Assert.AreEqual(int.MaxValue, TestIntA.MaxValue);
            Assert.AreEqual(int.MinValue, TestIntA.MinValue);
        }


    }

    [StrongIntType]
    public readonly partial struct TestIntA
    {
    }

    [StrongIntType]
    public readonly partial struct TestIntB
    {
    }
}
