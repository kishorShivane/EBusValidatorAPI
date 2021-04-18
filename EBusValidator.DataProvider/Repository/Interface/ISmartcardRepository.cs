namespace EBusValidator.DataProvider.Repository.Interface
{
    public interface ISmartcardRepository : IGenericRepository<Smartcard>
    {
        Smartcard GetByCardNumber(string smartcardid);
    }
}
