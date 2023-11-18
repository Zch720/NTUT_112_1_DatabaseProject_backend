using Elecookies.Database;
using Elecookies.Entities;

namespace Elecookies.Repositories {
    public class AccountRepository : Repository<Account, Guid> {
        private ElecookiesDbContext dbContext;

        public AccountRepository(ElecookiesDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public void Save(Account value) {
            if (FindById(value.Id) == null) {
                dbContext.Accounts.Add(value);
            } else {
                dbContext.Accounts.Update(value);
            }
            dbContext.SaveChanges();
        }

        public void Delete(Guid id) {
            Account? account = dbContext.Accounts.Find(id);
            if (account != null) {
                dbContext.Accounts.Remove(account);
                dbContext.SaveChanges();
            }
        }

        public Account? FindById(Guid id) {
            return dbContext.Accounts.Find(id);
        }

        public List<Account> All() {
            return dbContext.Accounts.ToList();
        }
    }
}
