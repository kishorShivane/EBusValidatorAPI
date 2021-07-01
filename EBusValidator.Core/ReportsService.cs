using EBusValidator.DataProvider;
using EBusValidator.DataProvider.Repository;
using EBusValidator.DataProvider.UnitOfWork;
using EBusValidator.Logger;
using EBusValidator.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace EBusValidator.Core
{
    public class ReportsService
    {
        private UnitOfWork<EBusValidatorContext> unitOfWork = new UnitOfWork<EBusValidatorContext>();
        private TransactionRepository transRepo;
        private SmartcardRepository smartcardRepo;

        public ReportsService(LoggerManager logger)
        {
            transRepo = new TransactionRepository(unitOfWork);
            smartcardRepo = new SmartcardRepository(unitOfWork);
        }

        public ReportsService()
        {
            transRepo = new TransactionRepository(unitOfWork);
            smartcardRepo = new SmartcardRepository(unitOfWork);
        }

        public List<UsageSummaryModel> GetAllUsageSummary()
        {
            try
            {
                List<UsageSummaryModel> usageSummaryList = new List<UsageSummaryModel>();
                List<UsageSummaryModel> usageSummary = (from t in transRepo.Table
                                                        join s in smartcardRepo.Table on t.CardEsn equals s.ESN into summary
                                                        from sum in summary.DefaultIfEmpty()
                                                        select new UsageSummaryModel
                                                        {
                                                            FirstName = sum.Name,
                                                            SurName = sum.Surname,
                                                            Kilometers = 0,
                                                            Smartcard = t.CardEsn,
                                                            TotalTagIns = t.Action
                                                        }).ToList();

                usageSummary.GroupBy(x => x.Smartcard).ToList().ForEach(x =>
                {
                    UsageSummaryModel firstItem = x.FirstOrDefault();
                    usageSummaryList.Add(new UsageSummaryModel()
                    {
                        FirstName = firstItem.FirstName,
                        Smartcard = firstItem.Smartcard,
                        SurName = firstItem.SurName,
                        Kilometers = x.Sum(c => c.Kilometers),
                        TotalTagIns = x.Sum(c => c.TotalTagIns)
                    });
                });

                return usageSummaryList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<UsageSummaryModel> GetUsageSummaries(SearchParams searchParams)
        {
            try
            {
                List<int> actions = new List<int> { 1, 11, 12, 253 };
                if (searchParams != null && !string.IsNullOrEmpty(searchParams.DriverNumber))
                {
                    searchParams.DriverNumber = string.Format("{0:x}", searchParams.DriverNumber);
                }

                List<UsageSummaryModel> usageSummaryList = new List<UsageSummaryModel>();
                List<UsageSummaryModel> usageSummary = (from t in transRepo.Table
                                                        join s in smartcardRepo.Table on t.CardEsn equals s.ESN into summary
                                                        from sum in summary.DefaultIfEmpty()
                                                        where DbFunctions.TruncateTime(t.TransactionDate) >= DbFunctions.TruncateTime(searchParams.FromDate) && DbFunctions.TruncateTime(t.TransactionDate) <= DbFunctions.TruncateTime(searchParams.ToDate) && //actions.Contains(t.Action) &&
                                                        (searchParams.BusNumber == 0 || t.BusNumber == searchParams.BusNumber) &&
                                                        (string.IsNullOrEmpty(searchParams.DriverNumber) || (sum.CardType == "Driver" && t.CardEsn == searchParams.DriverNumber))
                                                        select new UsageSummaryModel
                                                        {
                                                            FirstName = sum.Name,
                                                            SurName = sum.Surname,
                                                            AccountNumber = sum.AccountNumber,
                                                            Location = sum.Location,
                                                            Kilometers = 0,
                                                            Smartcard = t.CardEsn
                                                        }).ToList();

                usageSummary.GroupBy(x => x.Smartcard).ToList().ForEach(x =>
                {
                    UsageSummaryModel firstItem = x.FirstOrDefault();
                    usageSummaryList.Add(new UsageSummaryModel()
                    {
                        FirstName = firstItem.FirstName,
                        Smartcard = !string.IsNullOrEmpty(firstItem.Smartcard)?Convert.ToInt64(firstItem.Smartcard, 16).ToString(): string.Empty,
                        SurName = firstItem.SurName,
                        Location = firstItem.Location,
                        AccountNumber = firstItem.AccountNumber,
                        Kilometers = x.Sum(c => c.Kilometers),
                        TotalTagIns = x.Count()
                    });
                });

                return usageSummaryList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<UsageHistoryModel> GetUsageHistory(DateTime fromDate, DateTime toDate, string smartcard)
        {
            try
            {
                if (!string.IsNullOrEmpty(smartcard)) smartcard = Convert.ToInt64(smartcard).ToString("X");

                List<UsageHistoryModel> usageHistory = (from t in transRepo.Table
                                                        join s in smartcardRepo.Table on t.CardEsn equals s.ESN into summary
                                                        from sum in summary.DefaultIfEmpty()
                                                        where DbFunctions.TruncateTime(t.TransactionDate) >= DbFunctions.TruncateTime(fromDate) && DbFunctions.TruncateTime(t.TransactionDate) <= DbFunctions.TruncateTime(toDate) && (t.CardEsn == smartcard || string.IsNullOrEmpty(smartcard))
                                                        select new UsageHistoryModel
                                                        {
                                                            SurName = sum.Surname,
                                                            FirstName = sum.Name,
                                                            AccountNumber = sum.AccountNumber,
                                                            Location = sum.Location,
                                                            Smartcard = t.CardEsn,
                                                            Action = t.Action,
                                                            Bus = t.BusNumber,
                                                            TransactionDate = t.TransactionDate,
                                                            Driver = ""
                                                        }).ToList();

                usageHistory.ForEach(x => { if(!string.IsNullOrEmpty(x.Smartcard)) x.Smartcard = Convert.ToInt64(x.Smartcard, 16).ToString(); });

                usageHistory.ForEach(x => { x.ActivityType = MapActionToActivityType(x.Action); x.Date = x.TransactionDate.ToShortDateString(); x.Time = x.TransactionDate.ToShortTimeString(); });

                return usageHistory.OrderByDescending(x=>x.Date).ThenBy(x=>x.Time).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string MapActionToActivityType(int action)
        {
            string activityType = "";

            switch (action)
            {
                case 1:
                    activityType = "Tag-In";
                    break;
                case 2:
                    activityType = "Tag-Out ";
                    break;
                case 10:
                    activityType = "DEVICE_STARTUP";
                    break;
                case 11:
                    activityType = "DRIVER_SIGN_ON";
                    break;
                case 12:
                    activityType = "DRIVER_SIGN_OFF";
                    break;
                case -1:
                    activityType = "ERROR_HOTLISTED";
                    break;
                case -2:
                    activityType = "ERROR_INVALID_CUSTCODE";
                    break;
                case -3:
                    activityType = "ERROR_UNKNOWN_CARD";
                    break;
                case -4:
                    activityType = "ERROR_DAMAGED_CARD";
                    break;
                case -5:
                    activityType = "ERROR_EXPIRED_CARD ";
                    break;
                case -6:
                    activityType = "ERROR_PASSBACK";
                    break;
                default:
                    break;
            }

            return activityType;
        }
    }
}
