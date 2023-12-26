using SalonManagement.Data;
using SalonManagement.Models;
using SalonManagement.Repository.IRepository;
using SalonManagement.Repository.IRepostiory;

namespace SalonManagement.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IApplicationRoleRepository ApplicationRole { get; private set; }
        public IApplicationUserRoleRepository ApplicationUserRole { get; private set; }
        public IUserRepository User { get; private set; }
        public IPaymentRepository Payment { get; private set; }
        public IGenderRepository Gender { get; private set; }
        public IFirstServiceRepository FirstService { get; private set; }
        public ISecondServiceRepository SecondService { get; private set; }
        public ICountryRepository Country { get; private set; } 
        public IStateRepository State { get; private set; } 
        public ICityRepository City { get; private set; }
     
     


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            //Category = new CategoryRepository(_db);
        
            ApplicationUser = new ApplicationUserRepository(_db);
            ApplicationRole = new ApplicationRoleRepository(_db);
            ApplicationUserRole = new ApplicationUserRoleRepository(_db);
            Payment = new PaymentRepository(_db);
            Gender = new GenderRepository(_db);
            FirstService = new FirstServiceRepository(_db);
            SecondService = new SecondServiceRepository(_db);
            Country = new CountryRepository(_db);
            State = new StateRepository(_db);
            City = new CityRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
