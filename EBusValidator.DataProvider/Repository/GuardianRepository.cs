using EBusValidator.DataProvider.Repository.Interface;
using EBusValidator.DataProvider.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace EBusValidator.DataProvider.Repository
{
    public class GuardianRepository : GenericRepository<SmartcardGuardian>, IGuardianRepository
    {
       
        public GuardianRepository(IUnitOfWork<EBusValidatorContext> unitOfWork)
        : base(unitOfWork)
        {

        }
        public GuardianRepository(EBusValidatorContext context)
        : base(context)
        {
        }

        public List<SmartcardGuardian> GetGuardianForSmartCard(int smartcardID)
        {
            return this.Entities.Where(x => x.SmartCardID== smartcardID).ToList();
        }

        public void DeleteGuardiansForSmartCard(int smartcardID)
        {
            var guardians = this.Entities.Where(x => x.SmartCardID == smartcardID).ToList();
            guardians.ForEach(x => {
                this.Entities.Remove(x);
            });
            this.Context.SaveChanges();
        }
    }
}
