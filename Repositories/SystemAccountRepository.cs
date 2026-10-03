using Repositories;

public class SystemAccountRepository : ISystemAccountRepository
{
    public BusinessObjects.SystemAccount GetAccountByEmail(string email) => DataAccess.SystemAccountDAO.Instance.GetAccountByEmail(email);
    public IEnumerable<BusinessObjects.SystemAccount> GetAll() => DataAccess.SystemAccountDAO.Instance.GetAll();
    public BusinessObjects.SystemAccount GetById(short id) => DataAccess.SystemAccountDAO.Instance.GetById(id);
    public void Insert(BusinessObjects.SystemAccount account) => DataAccess.SystemAccountDAO.Instance.Insert(account);
    public void Update(BusinessObjects.SystemAccount account) => DataAccess.SystemAccountDAO.Instance.Update(account);
    public void Delete(short id) => DataAccess.SystemAccountDAO.Instance.Delete(id);
}
