using System;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Models;

namespace MicroERP.Business.Core.Factories
{
    public static class CustomerModelFactory
    {
        /// <summary>
        /// Retrieve either a company model or a person model - dependent on the given customer type.
        /// </summary>
        /// <param name="customerType">A customer type unequal to "None".</param>
        /// <returns>An empty customer model.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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