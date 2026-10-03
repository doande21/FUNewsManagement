using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class SystemAccountService : ISystemAccountService
    {
        private readonly Repositories.ISystemAccountRepository _repo;
        public SystemAccountService(Repositories.ISystemAccountRepository repo) { _repo = repo; }
        public BusinessObjects.SystemAccount GetAccountByEmail(string email) => _repo.GetAccountByEmail(email);
        public IEnumerable<BusinessObjects.SystemAccount> GetAll() => _repo.GetAll();
        public BusinessObjects.SystemAccount GetById(short id) => _repo.GetById(id);
        public void Insert(BusinessObjects.SystemAccount account) => _repo.Insert(account);
        public void Update(BusinessObjects.SystemAccount account) => _repo.Update(account);
        public void Delete(short id) => _repo.Delete(id);
    }
}
