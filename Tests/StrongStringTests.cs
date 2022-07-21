using System.Text.RegularExpressions;

namespace Diabase.StrongTypes.Tests
{
    [TestClass]
    public class StrongStringTests
    {
        [TestMethod]
        public void NonNullable()
        {

            // arrange
            const string const1 = "a";
            const string const2 = "b";
            const string? const3 = null;

            // act
            TestStrongString ss1 = const1;
            TestStrongString ss2 = const2;
            TestStrongString ss3 = const3!; // without the null-forgiving operator, this should show a warning

            string string1 = ss1;
            string string2 = ss2;
            string string3 = ss3;

            // assert
            Assert.AreEqual(const1, string1);
            Assert.AreEqual(const2, string2);
            Assert.AreEqual(string.Empty, string3);
            Assert.IsTrue(ss1 == (TestStrongString)const1);
            Assert.IsTrue(ss1 == const1);
            Assert.IsTrue(const1 == ss1);
            Assert.IsFalse(ss1 == ss2);
            Assert.IsTrue(ss1 != ss2);
        }

        [TestMethod]
        public void NullAsEmpty()
        {
            const string const1 = "a";
            const string const2 = "b";
            const string? const3 = null;

            // arrange
            TestNullAsEmptyString ss1 = const1;
            TestNullAsEmptyString ss2 = const2;
            TestNullAsEmptyString ss3 = const3;

            // act
            string string1 = ss1;
            string string2 = ss2;
            string string3 = ss3;

            // assert
            Assert.AreEqual(const1, string1);
            Assert.AreEqual(const2, string2);
            Assert.AreEqual(string.Empty, string3);
            Assert.IsTrue(ss1 == (TestNullAsEmptyString)const1);
            Assert.IsTrue(ss1 == const1);
            Assert.IsTrue(const1 == ss1);
            Assert.IsFalse(ss1 == ss2);
            Assert.IsTrue(ss1 != ss2);
        }

        [TestMethod]
        public void Nullable()
        {

            // arrange
            const string const1 = "a";
            const string const2 = "b";
            const string? const3 = null;

            // act
            TestNullableString? ss1 = const1;
            TestNullableString? ss2 = const2;
            TestNullableString? ss3 = const3;
            string string1 = ss1!;
            string string2 = ss2!;

            // assert
            Assert.AreEqual(const1, string1);
            Assert.AreEqual(const2, string2);
            Assert.IsTrue(ss1! == (TestNullableString)const1!);
            Assert.IsTrue(ss1! == const1!);
            Assert.IsTrue(const1! == ss1!);
            Assert.IsFalse(ss1! == ss2!);
            Assert.IsTrue(ss1! != ss2!);
            Assert.IsNull(ss3);
        }

        [TestMethod]
        public void WithConstraint()
        {

            // arrange
            const string const1 = "1";
            const string const2 = "a";
            const string const3 = "";
            const string const4 = "2";


            // act
            TestConstraintString ss1 = const1;
            TestConstraintString ss2 = const2;
            TestConstraintString ss3 = const3;
            TestConstraintString ss4 = const4;

            // assert
            Assert.IsTrue(ss1.IsValid);
            Assert.IsFalse(ss2.IsValid);
            Assert.IsFalse(ss3.IsValid);
            Assert.IsFalse(ss4.IsValid);
            Assert.IsNotNull(ss4.ValidationException);
        }
    }

    [StrongStringType]
    public partial class TestStrongString
    {

    }

    [StrongStringType(ImplicitNullConversionMode = ImplicitNullConversionMode.ToEmptyString)]
    public partial class TestNullAsEmptyString
    {

    }

    [StrongStringType(ImplicitNullConversionMode = ImplicitNullConversionMode.ToNullValue)]
    public partial class TestNullableString
    {

    }

    [StrongStringType(Constraints = StringConstraint.Required | StringConstraint.Regex | StringConstraint.Custom)]
    public partial class TestConstraintString
    {
        static Regex ConstraintRegEx = new(@"\d");
        static bool CustomValidate(string? value, out ConstraintException? exception)
        {
            if (value != "2")
            {
                exception = null;
                return true;
            }
            else
            {
                exception = new ConstraintException("Value must not be 2.");
                return false;
            }
        }
    }
}
