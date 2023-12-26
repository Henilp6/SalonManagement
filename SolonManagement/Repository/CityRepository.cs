
using SalonManagement.Data;
using SalonManagement.Models;
using SalonManagement.Repository.IRepository;
using SalonManagement.Repository.IRepostiory;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SalonManagement.Repository
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        private readonly ApplicationDbContext _db;
        public CityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public async Task<City> UpdateAsync(City entity)
        {
         
            _db.Cities.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
