using Application.Features.Command.Update;
using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Application.Features.Interfaces.IServices;
using Domain.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Get
{
    public class ViewProfile : IViewProfile 
    {
        private readonly IUserRepository _userRepo;
        private readonly IFamilyHeadRepository _familyHeadRepo;
        private readonly IFamilyRepository _familyRepo;
        private readonly IManagerRepository _managerRepo;
        private readonly IOragnisationRepository _ragnisationRepo;
        private readonly IIndividualRepository _individualRepo;
        public ViewProfile(IUserRepository userRepository, IFamilyHeadRepository familyHeadRepository, IManagerRepository managerRepository, IOragnisationRepository ragnisationRepository, IIndividualRepository individualRepository, IFamilyRepository familyRepo)
        {
            _userRepo = userRepository;
            _familyHeadRepo = familyHeadRepository;
            _managerRepo = managerRepository;
            _ragnisationRepo = ragnisationRepository;
            _individualRepo = individualRepository;
            _familyRepo = familyRepo;
        }


        public async Task<BaseResponse<ViewProfileCommandModel>> ViewProfileAsync(Guid Id)
        {
            var user = await _userRepo.GetUserAsync(u => u.Id == Id);
            if (user.Role.Name == RoleConst.Individual)
            {
                var individualProfile = await _individualRepo.Get(i => i.UserId == Id);
                return new BaseResponse<ViewProfileCommandModel>
                {
                    IsSuccessfull = true,
                    Data = new ViewProfileCommandModel
                    {
                           Address = user.Address,
                           Email = user.Email,
                           Name = user.Name,
                           Password = user.Password,
                           PhoneNumber = user.PhoneNumber,
                           ProfileImage = user.ProfileImage,
                           Role = RoleConst.Individual,
                           City = individualProfile.City,
                           LOcalGovernment = individualProfile.LOcalGovernment,
                           Nin = individualProfile.Nin,
                           PostalCode = individualProfile.PostalCode,
                    },
                };
            }
            else if (user.Role.Name == RoleConst.OrganizationHead)
            {
                var organizationHead = await _ragnisationRepo.Get(i => i.UserId == Id);
                return new BaseResponse<ViewProfileCommandModel>
                {
                    IsSuccessfull = true,
                    Data = new ViewProfileCommandModel
                    {
                        Address = user.Address,
                        Email = user.Email,
                        Name = user.Name,
                        Password = user.Password,
                        PhoneNumber = user.PhoneNumber,
                        ProfileImage = user.ProfileImage,
                        Role = RoleConst.OrganizationHead,
                        City = organizationHead.City,
                        LOcalGovernment = organizationHead.LOcalGovernment,
                        CacNumber = organizationHead.CacNumber,
                        PostalCode = organizationHead.PostalCode,
                        NumberOfPeopleInOrganization = organizationHead.NumberOfPeopleInOrganization,
                    },
                };
            }
            else if(user.Role.Name == RoleConst.Manager)
            {
                var manager = await _managerRepo.Get(i => i.UserId == Id);
                return new BaseResponse<ViewProfileCommandModel>
                {
                    IsSuccessfull = true,
                    Data = new ViewProfileCommandModel
                    {
                        Address = user.Address,
                        Email = user.Email,
                        Name = user.Name,
                        Password = user.Password,
                        PhoneNumber = user.PhoneNumber,
                        ProfileImage = user.ProfileImage,
                        Role = RoleConst.Manager,
                        City = manager.City,
                        LOcalGovernment = manager.LOcalGovernment,
                        Nin = manager.Nin,
                        PostalCode = manager.PostalCode,
                    },
                };
            }
            else if (user.Role.Name == RoleConst.FamilyHead)
            {
                var familyHead = await _familyHeadRepo.Get(i => i.UserId == Id);
                var allMembers = await _familyRepo.GetAll();
                var familyHeadMember = allMembers.Where(m => m.FamilyHeadId == familyHead.Id);
                return new BaseResponse<ViewProfileCommandModel>
                {
                    IsSuccessfull = true,
                    Data = new ViewProfileCommandModel
                    {
                        Address = user.Address,
                        Email = user.Email,
                        Name = user.Name,
                        Password = user.Password,
                        PhoneNumber = user.PhoneNumber,
                        ProfileImage = user?.ProfileImage,
                        Role = RoleConst.FamilyHead,
                        City = familyHead?.City,
                        LOcalGovernment = familyHead.LOcalGovernment,
                        Nin = familyHead.NIN,
                        PostalCode = familyHead?.PostalCode,
                        FamilySize = familyHead?.FamilySize ?? 0,
                        Families = familyHeadMember?.Select( m => new FamilyHeadMemebers
                        {
                            FirstName = m.FirstName,
                            LastName = m.LastName,
                            Nin = m.Nin,
                        }).ToList() ?? new List<FamilyHeadMemebers>(),
                    },
                };
            }
            return new BaseResponse<ViewProfileCommandModel>
            {
                IsSuccessfull = false,
                Message = "User not found."
            };
        }
    }
}
