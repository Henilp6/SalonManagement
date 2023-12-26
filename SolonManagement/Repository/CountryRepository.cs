
using SalonManagement.Data;
using SalonManagement.Models;
using SalonManagement.Repository.IRepository;
using SalonManagement.Repository.IRepostiory;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SalonManagement.Repository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private readonly ApplicationDbContext _db;
        public CountryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public async Task<Country> UpdateAsync(Country entity)
        {
         
            _db.Countries.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
