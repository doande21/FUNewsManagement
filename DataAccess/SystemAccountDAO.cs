// Thư mục: DataAccess/SystemAccountDAO.cs
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class SystemAccountDAO
    {
        private static SystemAccountDAO instance = null;
        private static readonly object instanceLock = new object();
        private SystemAccountDAO() { }
        public static SystemAccountDAO Instance
        {
            get { lock (instanceLock) { if (instance == null) instance = new SystemAccountDAO(); return instance; } }
        }

        public SystemAccount GetAccountByEmail(string email)
        {
            using var db = new FUNewsManagementContext();
            return db.SystemAccounts.SingleOrDefault(a => a.AccountEmail == email);
        }

        public IEnumerable<SystemAccount> GetAll()
        {
            using var db = new FUNewsManagementContext();
            return db.SystemAccounts.ToList();
        }

        public SystemAccount GetById(short id)
        {
            using var db = new FUNewsManagementContext();
            return db.SystemAccounts.Find(id);
        }

        public void Insert(SystemAccount account)
        {
            using var db = new FUNewsManagementContext();
            // Tự động generate AccountID nếu cần (vì DB thiết kế None Identity cho AccountID)
            if (account.AccountID == 0)
            {
                var maxId = db.SystemAccounts.Any() ? db.SystemAccounts.Max(a => a.AccountID) : 0;
                account.AccountID = (short)(maxId + 1);
            }
            db.SystemAccounts.Add(account);
            db.SaveChanges();
        }

        public void Update(SystemAccount account)
        {
            using var db = new FUNewsManagementContext();
            db.SystemAccounts.Update(account);
            db.SaveChanges();
        }

        public void Delete(short id)
        {
            using var db = new FUNewsManagementContext();
            var acc = db.SystemAccounts.Find(id);
            if (acc != null)
            {
                db.SystemAccounts.Remove(acc);
                db.SaveChanges();
            }
        }
    }
}