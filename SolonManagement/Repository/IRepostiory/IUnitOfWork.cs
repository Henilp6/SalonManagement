﻿using SalonManagement.Repository.IRepostiory;

namespace SalonManagement.Repository.IRepository
{
    public interface IUnitOfWork
    {
        //ICategoryRepository Category { get; }
       

        IApplicationUserRepository ApplicationUser { get; }
        IApplicationRoleRepository ApplicationRole { get; }
        IApplicationUserRoleRepository ApplicationUserRole { get; }
        IUserRepository User { get; }
        IPaymentRepository Payment { get; }
        IGenderRepository Gender { get; }
        IFirstServiceRepository FirstService { get; }
        ISecondServiceRepository SecondService { get; }
        ICountryRepository Country { get; }
        IStateRepository State { get; }
        ICityRepository City{ get; } 
        ISalonBranchXGenderRepository SalonBranchXGender { get; }
        ISalonBranchXPaymentRepository SalonBranchXPayment { get; }
        ISalonBranchXServiceRepository SalonBranchXService { get; }
        IBookingRepository Booking { get; }
        ISalonBranchRepository SalonBranch { get; }
        void Save();
    }
}
