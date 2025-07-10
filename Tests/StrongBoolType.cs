using Diabase.StrongTypes.Types;

namespace Diabase.StrongTypes.Tests
{
    [TestClass]
    public class StrongBoolType
    {
        [TestMethod]
        public void Assignments()
        {

            // arrange
            const bool const1 = false;
            const bool const2 = true;
            bool? const3 = null;

            // act
            TestBoolA sta1 = const1;
            TestBoolA sta2 = const2;
            TestBoolA? sta3 = const3;

#if false // when set to true, the following line should be an error demonstrating the strong type prevents the assignment
            TestBoolB stb1 = sta1;
#endif

            bool var1 = sta1;
            bool var2 = sta2;

            // assert
            Assert.AreEqual(const1, var1);
            Assert.AreEqual(const2, var2);
            Assert.IsTrue(sta1 == (TestBoolA)const1);
            Assert.IsTrue(sta1 == const1);
            Assert.IsFalse(sta1 == sta2);
            Assert.IsTrue(sta1 != sta2);
            Assert.IsNull(sta3);
        }
    }

    [StrongBoolType]
    public readonly partial struct TestBoolA
    {
    }

    [StrongBoolType]
    public readonly partial struct TestBoolB
    {
    }
}
