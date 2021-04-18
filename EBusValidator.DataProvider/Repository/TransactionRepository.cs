using EBusValidator.DataProvider.Repository.Interface;
using EBusValidator.DataProvider.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EBusValidator.DataProvider.Repository
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {

        public TransactionRepository(IUnitOfWork<EBusValidatorContext> unitOfWork)
        : base(unitOfWork)
        {

        }
        public TransactionRepository(EBusValidatorContext context)
        : base(context)
        {
        }

        public List<Transaction> GetTransactions(string fromDate, string toDate)
        {
            return this.Entities.Where(x => x.TransactionDate >= Convert.ToDateTime(fromDate) && x.TransactionDate <= Convert.ToDateTime(toDate)).ToList();
        }
    }
}
