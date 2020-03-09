using EBusValidator.DataProvider.Repository.Interface;
using EBusValidator.DataProvider.UnitOfWork;
using System.Linq;

namespace EBusValidator.DataProvider.Repository
{
    public class SmartcardRepository : GenericRepository<Smartcard>, ISmartcardRepository
    {
       
        public SmartcardRepository(IUnitOfWork<EBusValidatorContext> unitOfWork)
        : base(unitOfWork)
        {

        }
        public SmartcardRepository(EBusValidatorContext context)
        : base(context)
        {
        }

        public Smartcard GetByCardNumber(string smartcardNumber)
        {
            return this.Entities.Where(x => x.SmartcardNumber.Equals(smartcardNumber)).FirstOrDefault();
        }
    }
}
