using EBusValidator.DataProvider;
using EBusValidator.DataProvider.Repository;
using EBusValidator.DataProvider.UnitOfWork;
using EBusValidator.Logger;
using EBusValidator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBusValidator.Core
{
    public class SmartcardService
    {
        private UnitOfWork<EBusValidatorContext> unitOfWork = new UnitOfWork<EBusValidatorContext>();
        private SmartcardRepository smartCardRepo;
        private GuardianRepository guardianRepo;
        public SmartcardService(LoggerManager logger)
        {
            smartCardRepo = new SmartcardRepository(unitOfWork);
            guardianRepo = new GuardianRepository(unitOfWork);
        }

        public SmartcardService()
        {
            smartCardRepo = new SmartcardRepository(unitOfWork);
            guardianRepo = new GuardianRepository(unitOfWork);
        }

        public List<SmartcardModel> GetSmartCards()
        {
            try
            {
                return smartCardRepo.GetAll().Select(x => new SmartcardModel()
                {
                    CellPhone = x.Cellphone ?? string.Empty,
                    EMail = x.EmailAddress ?? string.Empty,
                    Gender = x.Gender ?? string.Empty,
                    ID = x.ID,
                    LastUpdatedBy = x.LastUpdatedBy ?? string.Empty,
                    LastUpdatedDate = x.LastUpdatedDate ?? DateTime.Now,
                    Name = x.Name ?? string.Empty,
                    SmartcardNumber = x.SmartcardNumber ?? string.Empty,
                    Status = x.Status ?? true,
                    Surname = x.Surname ?? string.Empty,
                    CardType = x.CardType ?? string.Empty,
                    Number = x.Number ?? string.Empty
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SmartcardModel GetSmartCard(int smartcardID)
        {
            SmartcardModel smartCardModel = null;
            try
            {
                var smartCard = smartCardRepo.GetById(smartcardID);
                if (smartCard != null)
                {
                    var guardians = guardianRepo.GetGuardianForSmartCard(smartcardID);
                    smartCardModel = MapSmartCardModel(smartCard);
                    smartCardModel.Guardians = MapGuardianModel(guardians);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return smartCardModel;
        }

        public string InsertOrUpdateSmartcard(SmartcardModel smartcard)
        {
            var status = "";
            try
            {
                unitOfWork.CreateTransaction();
                if (smartcard.ID <= 0)
                {
                    if (smartCardRepo.GetByCardNumber(smartcard.SmartcardNumber) == null)
                    {
                        Smartcard entity = MapSmartCardDBModel(smartcard);
                        smartCardRepo.Insert(entity);
                        unitOfWork.Save();
                        var guardians = MapGuardianDBModel(smartcard.Guardians, entity.ID);
                        guardianRepo.BulkInsert(guardians);
                        unitOfWork.Save();
                    }
                    else
                    {
                        status = "Smartcard number is already regestired.!!";
                    }
                }
                else
                {
                    var smartCardRec = smartCardRepo.GetById(smartcard.ID);
                    MapSmartCardDBModel(smartcard, smartCardRec);
                    smartCardRepo.Update(smartCardRec);

                    guardianRepo.DeleteGuardiansForSmartCard(smartcard.ID);
                    var guardians = MapGuardianDBModel(smartcard.Guardians, smartcard.ID);
                    guardianRepo.BulkInsert(guardians);
                    unitOfWork.Save();
                }
                unitOfWork.Commit();
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
                throw;
            }
            return status;
        }

        private List<SmartcardGuardian> MapGuardianDBModel(List<GuardianModel> guardians, int smartCardID)
        {
            var result = new List<SmartcardGuardian>();
            guardians.ForEach(x =>
            {
                result.Add(new SmartcardGuardian()
                {
                    Name = x.Name,
                    Surname = x.Surname,
                    RelationShip = x.RelationShip,
                    Cellphone = x.CellPhone,
                    Telephone = x.TelePhone,
                    Email = x.EMail,
                    SmartCardID = smartCardID,
                    LastUpdateBy = "System",
                    LastUpdateOn = DateTime.Now
                });
            });
            return result;
        }

        public Smartcard MapSmartCardDBModel(SmartcardModel x)
        {
            return new Smartcard()
            {
                Cellphone = x.CellPhone,
                EmailAddress = x.EMail,
                Gender = x.Gender,
                ID = x.ID,
                LastUpdatedBy = "System",
                LastUpdatedDate = DateTime.Now,
                Name = x.Name,
                SmartcardNumber = x.SmartcardNumber,
                Status = x.Status,
                Surname = x.Surname,
                CardType = x.CardType,
                Number = x.Number
            };

        }

        public void MapSmartCardDBModel(SmartcardModel x, Smartcard y)
        {
            y.Cellphone = x.CellPhone;
            y.EmailAddress = x.EMail;
            y.Gender = x.Gender;
            y.LastUpdatedBy = "System";
            y.LastUpdatedDate = DateTime.Now;
            y.Name = x.Name;
            y.SmartcardNumber = x.SmartcardNumber;
            y.Status = x.Status;
            y.Surname = x.Surname;
            y.CardType = x.CardType;
            y.Number = x.Number;
        }

        public SmartcardModel MapSmartCardModel(Smartcard x)
        {
            return new SmartcardModel()
            {
                CellPhone = x.Cellphone,
                EMail = x.EmailAddress,
                Gender = x.Gender,
                ID = x.ID,
                LastUpdatedBy = x.LastUpdatedBy,
                LastUpdatedDate = x.LastUpdatedDate ?? DateTime.Now,
                Name = x.Name,
                SmartcardNumber = x.SmartcardNumber,
                Status = x.Status ?? true,
                Surname = x.Surname,
                CardType = x.CardType,
                Number = x.Number
            };
        }

        public List<GuardianModel> MapGuardianModel(List<SmartcardGuardian> guardians)
        {
            var result = new List<GuardianModel>();
            if (guardians.Any())
            {
                guardians.ForEach(x =>
                {
                    result.Add(new GuardianModel()
                    {
                        ID = x.ID,
                        Name = x.Name,
                        Surname = x.Surname,
                        RelationShip = x.RelationShip,
                        CellPhone = x.Cellphone,
                        TelePhone = x.Telephone,
                        EMail = x.Email
                    });
                });
            }
            return result;
        }
    }
}
