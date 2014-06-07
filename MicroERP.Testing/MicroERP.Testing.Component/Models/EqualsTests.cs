using System;
using MicroERP.Business.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroERP.Testing.Component.Models
{
    [TestClass]
    public class EqualsTests
    {
        [TestMethod]
        public void Test_PersonEquatable()
        {
            var p1 = new PersonModel(1, "A", "B", "C", "Herr", "First", "Last", "", new DateTime(1993, 6, 1));
            var p2Isp1 = new PersonModel(1, "A", "B", "C", "Herr", "First", "Last", "", new DateTime(1993, 6, 1));
            var p3 = new PersonModel(9, "Straße 1", "Straße 2", "Straße 3", "Herr", "First", "Last", "",
                new DateTime(1993, 6, 1));

            Assert.IsTrue(p1.Equals(p2Isp1));
            Assert.AreEqual(p1, p1);
            Assert.AreEqual(p1, p2Isp1);
            Assert.AreNotEqual(p1, p3);
        }

        [TestMethod]
        public void Test_CompanyEquatable()
        {
            var c1 = new CompanyModel(1, "A", "B", "C", "Firma #1", "UID #1");
            var c2Isc1 = new CompanyModel(1, "A", "B", "C", "Firma #1", "UID #1");
            var c3 = new CompanyModel(9, "Street X", "Street Y", "Street Z", "Firma #2", "UID #2");

            Assert.IsTrue(c1.Equals(c2Isc1));
            Assert.AreEqual(c1, c1);
            Assert.AreEqual(c1, c2Isc1);
            Assert.AreNotEqual(c1, c3);
        }

        [TestMethod]
        public void Test_InvoiceEquatable()
        {
            var i1 = new InvoiceModel(1, new DateTime(2014, 1, 1), new DateTime(2014, 1, 10), "Comment", "Message",
                new PersonModel(), null);
            var i2Isi1 = new InvoiceModel(1, new DateTime(2014, 1, 1), new DateTime(2014, 1, 10), "Comment", "Message",
                new PersonModel(), null);
            var i3 = new InvoiceModel(2, new DateTime(2010, 10, 10), new DateTime(2011, 10, 10), "Comment #2",
                "Message #2", new CompanyModel(), null);

            Assert.IsTrue(i1.Equals(i2Isi1));
            Assert.AreEqual(i1, i1);
            Assert.AreEqual(i1, i2Isi1);
            Assert.AreNotEqual(i1, i3);
        }

        [TestMethod]
        public void Test_InvoiceItemEquatable()
        {
            var ii1 = new InvoiceItemModel(1, "Artikel #1", 10, 10.0m, 0.2m);
            var ii2Isii1 = new InvoiceItemModel(1, "Artikel #1", 10, 10.0m, 0.2m);
            var ii3 = new InvoiceItemModel(2, "Artikel #2", 1, 13.2m, 0.19m);

            Assert.IsTrue(ii1.Equals(ii2Isii1));
            Assert.AreEqual(ii1, ii1);
            Assert.AreEqual(ii1, ii2Isii1);
            Assert.AreNotEqual(ii1, ii3);
        }
    }
}