using MicroERP.Business.Domain.Models;
using System;
using System.Collections.Generic;

namespace MicroERP.Data.Mock
{
    internal class MockData
    {
        #region Fields

        private static readonly MockData instance = new MockData();

        #endregion

        #region Properties

        internal static MockData Instance
        {
            get { return MockData.instance; }
        }

        internal List<CustomerModel> Customers
        {
            get;
            private set;
        }

        internal List<InvoiceModel> Invoices
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public MockData()
        {
            this.createMockData();
        }

        #endregion

        #region CreateMockData

        private void createMockData()
        {
            var viktorCompany = new CompanyModel(3, "Viktorweg", "Viktorweg 1", "Viktorweg 2", "Viktor AG", "98765432");
            var simonCompany = new CompanyModel(4, "Simonweg", "Simonweg 1", "Simonweg 2", "Simon GmbH", "0123456789");
            var thomasCompany = new CompanyModel(5, "Thomasweg", "Thomasweg 1", "Thomasweg 2", "Thomas GmbH", "6543217890");

            this.Customers = new List<CustomerModel>(new CustomerModel[]
            {
                viktorCompany,
                simonCompany,
                thomasCompany,
                new PersonModel(1, "Wehlistraße", "Wehlistraße 1", "Wehlistraße 2", "Dr", "Thomas", "Eizinger", "Bsc", DateTime.Now, viktorCompany),
                new PersonModel(2, "Lukasweg", "Lukasweg 1", "Lukasweg 2", "Dr", "Lukas", "Streiter", "Msc", DateTime.Now),
                new PersonModel(6, "Wehlistraße", "Wehlistraße 1", "Wehlistraße 2", "Dr.", "Viktor", "Hofinger", "Bsc", DateTime.Now, simonCompany),
                new PersonModel(7, "Anotherstreet", "Anotherstreet 1", "Anotherstreet 2", "Dr.", "Another", "Person", "Bsc", DateTime.Now, simonCompany),
                new PersonModel(8, "Copy ninja street", "Copy ninja street 1", "Copy ninja street 2", "DDr.", "Copy", "Ninja", "Msc", DateTime.Now, viktorCompany),
            });

            this.Invoices = new List<InvoiceModel>(new InvoiceModel[]
            {
                new InvoiceModel(1, DateTime.Now, DateTime.Now, "Test-KommentarKommentarKommentarKommentarKommentarKommentarKommentarKommentarKommentarKommentarKommentarKommentarKommentar", "Test-Message", this.Customers[0], new InvoiceItemModel[]
                {
                    new InvoiceItemModel(1, "Artikel 1", 100, 10.2m, 0.1m),
                    new InvoiceItemModel(2, "Artikel 2", 120, 5.4m, 0.2m)
                }),
                new InvoiceModel(2, DateTime.Now, DateTime.Now, "Test-Kommentar", "Test-MesKommentarKommentarKommentarKommentarKommentarsage", this.Customers[0], new InvoiceItemModel[]
                {
                    new InvoiceItemModel(3, "Artikel 3", 70, 14.2m, 0.2m),
                    new InvoiceItemModel(4, "Artikel 4", 132, 1.4m, 0.19m)
                }),
                new InvoiceModel(3, DateTime.Now, DateTime.Now, "Test-KomKommentarKommentarKommentarmentar", "TesKommentarKommentarKommentart-Message", this.Customers[1], new InvoiceItemModel[]
                {
                    new InvoiceItemModel(5, "Artikel 3", 70, 14.2m, 0.2m),
                    new InvoiceItemModel(6, "Artikel 4", 132, 1.4m, 0.19m)
                }),
                new InvoiceModel(4, DateTime.Now, DateTime.Now, "Test-Kommentar", "Test-Message", this.Customers[2], new InvoiceItemModel[]
                {
                    new InvoiceItemModel(7, "Artikel 3", 70, 14.2m, 0.2m),
                    new InvoiceItemModel(8, "Artikel 4", 132, 1.4m, 0.19m)
                })
            });
        }

        #endregion
    }
}
