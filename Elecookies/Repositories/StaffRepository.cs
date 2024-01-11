using Elecookies.Database;
using Elecookies.Entities;

namespace Elecookies.Repositories {
    public class StaffRepository : Repository<Staff, Guid> {

        private ElecookiesDbContext dbContext;

        public StaffRepository(ElecookiesDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public void Save(Staff value) {
            if (FindById(value.Id) == null) {
                dbContext.Staffs.Add(value);
            }
            else {
                dbContext.Staffs.Update(value);
            }
            dbContext.SaveChanges();
        }

        public void Delete(Guid id) {
            Staff? staff = dbContext.Staffs.Find(id);
            if (staff != null) {
                dbContext.Staffs.Remove(staff);
                dbContext.SaveChanges();
            }
        }

        public Staff? FindById(Guid id) {
            return dbContext.Staffs.Find(id);
        }

        public List<Staff> All() {
            return dbContext.Staffs.ToList();
        }
    }
}
