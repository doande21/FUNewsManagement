using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface ISystemAccountService
    {
        BusinessObjects.SystemAccount GetAccountByEmail(string email);
        IEnumerable<BusinessObjects.SystemAccount> GetAll();
        BusinessObjects.SystemAccount GetById(short id);
        void Insert(BusinessObjects.SystemAccount account);
        void Update(BusinessObjects.SystemAccount account);
        void Delete(short id);
    }
}
