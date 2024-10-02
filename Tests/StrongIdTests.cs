namespace Diabase.StrongTypes.Tests
{
    [TestClass]
    public class StrongIdTests
    {
        [TestMethod]
        public void StrongIntId()
        {

            // arrange
            const int id1 = 1;
            const int id2 = 2;
            int? id3 = null;

            // act
            IntId sid1 = (IntId)id1;
            IntId sid2 = (IntId)id2;
            IntId? sid3 = id3;
            int rid1 = sid1;
            int rid2 = sid2;

            // assert
            Assert.AreEqual(id1, rid1);
            Assert.AreEqual(id2, rid2);
            Assert.IsTrue(sid1 == (IntId)id1);
            Assert.IsTrue(sid1 == id1);
            Assert.IsFalse(sid1 == sid2);
            Assert.IsTrue(sid1 != sid2);
            Assert.IsNull(sid3);
        }

        [TestMethod]
        public void StrongIntIdUsingPublic()
        {
            // arrange
            const int id1 = 101;

            // act
            IntIdPA pida1 = (IntIdPA)id1;
            string sida = pida1.Public;
            IntIdPA pida2 = IntIdPA.FromPublic(sida);
            IntIdPB pidb1 = (IntIdPB)id1;
            string sidb = pidb1.Public;

            int rida = pida2;
            int ridb = pidb1;

            // assert
            Assert.AreEqual(id1, rida);
            Assert.AreEqual(id1, ridb);
            Assert.AreNotEqual(sida, sidb);
        }

        [TestMethod]
        public void StrongIntIdImplicitStringConversion()
        {
            // arrange
            const int id1 = 101;

            // act
            IntIdSA pida1 = id1;
            string sida = pida1;
            IntIdSA pida2 = sida;

            int rida = pida1;
            int ridb = pida2;

            // assert
            Assert.AreEqual(id1, rida);
            Assert.AreEqual(id1, ridb);
        }

        [TestMethod]
        public void StrongIntIdImplicitStringConversionAsPublicId()
        {
            // arrange
            const int id1 = 101;

            // act
            IntIdPA pida1 = id1;
            string sida = pida1;
            IntIdPA pida2 = sida;

            int rida = pida1;
            int ridb = pida2;

            // assert
            Assert.AreEqual(id1, rida);
            Assert.AreEqual(id1, ridb);
            Assert.AreNotEqual("101", sida);
        }


        [TestMethod]
        public void StrongStringId()
        {
            const string id1 = "123";
            const string id2 = "456";

            // arrange
            StringId sid1 = (StringId)id1;
            StringId sid2 = (StringId)id2;
            StringId? sid3 = null;

            // act
            string rid1 = sid1;
            string rid2 = sid2;

            // assert
            Assert.AreEqual(id1, rid1);
            Assert.AreEqual(id2, rid2);
            Assert.IsTrue(sid1 == (StringId)id1);
            Assert.IsTrue(sid1 == id1);
            Assert.IsFalse(sid1 == sid2);
            Assert.IsTrue(sid1 != sid2);
            Assert.IsNull(sid3);
        }

        [TestMethod]
        public void StrongGuidId()
        {

            // arrange
            Guid id1 = Guid.Parse("{F564F5D7-66A8-45A4-A140-297424494558}");
            Guid id2 = Guid.Parse("{F564F5D7-66A8-45A4-A140-297424494559}");
            Guid? id3 = null;


            // act
            GuidId sid1 = (GuidId)id1;
            GuidId sid2 = (GuidId)id2;
            GuidId? sid3 = id3;
            Guid rid1 = sid1;
            Guid rid2 = sid2;

            // assert
            Assert.AreEqual(id1, rid1);
            Assert.AreEqual(id2, rid2);
            Assert.IsTrue(sid1 == (GuidId)id1);
            //Assert.IsTrue(sid1 == id1);
            Assert.IsFalse(sid1 == sid2);
            Assert.IsTrue(sid1 != sid2);
            Assert.IsNull(sid3);
        }

        [TestMethod]
        public void StrongGuidIdImplicitStringConversion()
        {
            // arrange
            Guid id1 = Guid.Parse("{F564F5D7-66A8-45A4-A140-297424494558}");

            // act
            GuidIdS pida1 = id1;
            string sida = pida1;
            GuidIdS pida2 = sida;

            Guid rida = pida1;
            Guid ridb = pida2;

            // assert
            Assert.AreEqual(id1, rida);
            Assert.AreEqual(id1, ridb);
        }

    }

    [StrongIntId]
    public readonly partial struct IntId
    {
    }

    [StrongIntId]
    public readonly partial struct IntIdA
    {
    }

    [StrongIntId]
    public readonly partial struct IntIdB
    {
    }

    [StrongStringId]
    public readonly partial struct StringId
    {
    }

    [StrongGuidId]
    public readonly partial struct GuidId
    {
    }

    [StrongGuidId(IncludeImplicitStringConversion = true)]
    public readonly partial struct GuidIdS
    {
    }

    [StrongIntId(IncludePublicIdSupport = true, PublicIdAesKey = "0123456789abcdef0123456789abcdef", PublicIdAesIv = "0123456789abcdef", IncludeImplicitStringConversion = true)]
    public readonly partial struct IntIdPA
    {
    }

    [StrongIntId(IncludePublicIdSupport = true, PublicIdAesKey = "abcdef0123456789abcdef0123456789", PublicIdAesIv = "abcdef0123456789")]
    public readonly partial struct IntIdPB
    {
    }

    [StrongIntId(IncludeImplicitStringConversion = true)]
    public readonly partial struct IntIdSA
    {
    }
}