

using SalonManagement.Data;
using SalonManagement.Models;
using SalonManagement.Repository.IRepository;
using SalonManagement.Repository.IRepostiory;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SalonManagement.Repository
{
    public class StateRepository : Repository<State>, IStateRepository
    {
        private readonly ApplicationDbContext _db;
        public StateRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public async Task<State> UpdateAsync(State entity)
        {
         
            _db.States.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
