using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Application.Features.Interfaces.IServices;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Command.Update
{
    public class FamilyProfile
    {
        //updating a family head profile, and can also register other family members
        public class Handler : IRequestHandler<UpdateFamilyHeadModel, BaseResponse<string>>
        {
            private readonly IUserRepository _userRepository;
            private readonly ICurrentUser _currentUser;
            private readonly IFamilyRepository _familyRepo;
            private readonly IFamilyHeadRepository _familyHeadRepo;
            private readonly IRoleRepository _roleRepo;
            public Handler(IUserRepository userRepository, ICurrentUser currentUser, IFamilyRepository familyRepo, IFamilyHeadRepository familyHeadRepo, IRoleRepository roleRepo)
            {
                _userRepository = userRepository;
                _currentUser = currentUser;
                _familyRepo = familyRepo;
                _familyHeadRepo = familyHeadRepo;
                _roleRepo = roleRepo;
            }
            public async Task<BaseResponse<string>> Handle(UpdateFamilyHeadModel request, CancellationToken cancellationToken)
            {
                var loggedinUser = await _currentUser.LoggedInUser();
                //geting user details
                var getUser = await _userRepository.GetUserAsync(u => u.Id == loggedinUser.Id);

                if (getUser == null)
                {
                    return new BaseResponse<string>
                    {
                        IsSuccessfull = false,
                        Message = "Family Head not FOUND"
                    };
                }

                var familyHead = await _familyHeadRepo.Get(i => i.Id == loggedinUser.Id);

                //assigning new update
                getUser.ProfileImage = getUser.ProfileImage ?? request.ProfileImage;
                getUser.Name = getUser.Name;
                getUser.Email = getUser.Email;
                getUser.IsEmailConfirmed = getUser.IsEmailConfirmed ? true : false;
                getUser.Address = getUser.Address ?? request.Address;

                familyHead.LOcalGovernment = request.LOcalGovernment;
                familyHead.PostalCode = request.PostalCode;
                familyHead.City = request.City;

                // registering other members of the famly
                var getRole = await _roleRepo.Get(r => r.Name == "Family");
                var familyMembers = request.FamilyMembers.Select(fm => new Family
                {
                    FirstName = fm.FirstName,
                    LastName = fm.LastName,
                    Nin = fm.Nin,
                    FamilyHeadId = familyHead.Id,
                    RoleId = getRole.RoleId,
                }).ToList();
                familyHead.Families = familyMembers;


            _userRepository.Update(getUser);
                _familyHeadRepo.Update(familyHead);
                _currentUser.SaveUserAsync();
                return new BaseResponse<string>
                {
                    IsSuccessfull = true,
                    Message = "Profile updated Successfully"
                };
            }
        }
    }
}
