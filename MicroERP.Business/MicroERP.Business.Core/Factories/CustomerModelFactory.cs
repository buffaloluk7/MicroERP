using System;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Models;

namespace MicroERP.Business.Core.Factories
{
    public static class CustomerModelFactory
    {
        public static CustomerModel FromType(CustomerType customerType)
        {
            switch (customerType)
            {
                case CustomerType.Company:
                    return new CompanyModel();

                case CustomerType.Person:
                    return new PersonModel();

                default:
                    throw new ArgumentOutOfRangeException("customerType");
            }
        }
    }
}