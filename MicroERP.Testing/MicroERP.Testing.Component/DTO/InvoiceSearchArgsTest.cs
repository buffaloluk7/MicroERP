using System;
using MicroERP.Business.Domain.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroERP.Testing.Component.DTO
{
    [TestClass]
    public class InvoiceSearchArgsTest
    {
        [TestMethod]
        public void Test_IsEmpty()
        {
            var args = new InvoiceSearchArgs();

            Assert.IsTrue(args.IsEmpty());
        }

        public void Test_IsNotEmpty()
        {
            var args = new InvoiceSearchArgs
            {
                CustomerID = 1,
                MinDate = DateTime.Now,
                MaxDate = DateTime.Now,
                MinTotal = 0.0m,
                MaxTotal = 100.0m
            };

            Assert.IsFalse(args.IsEmpty());
        }
    }
}