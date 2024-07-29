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
    public class OrganizationHeadProfile
    {
        //updating profile for head of an organization
        public class Handler : IRequestHandler<OrganizationHeadModel, BaseResponse<string>>
        {
            private readonly IUserRepository _userRepo;
            private readonly ICurrentUser _currentUser;
            private readonly IOragnisationRepository _organizationRepo;
            public Handler(IUserRepository userRepo, ICurrentUser currentUser, IOragnisationRepository organizationRepo)
            {
                _userRepo = userRepo;
                _currentUser = currentUser;
                _organizationRepo = organizationRepo;
            }
            public async Task<BaseResponse<string>> Handle(OrganizationHeadModel request, CancellationToken cancellationToken)
            {
                var loggedinUser = await _currentUser.LoggedInUser();

                //  retreiving user
                var getUser = await _userRepo.GetUserAsync(u => u.Id == loggedinUser.Id);
                if (getUser == null)
                {
                    return new BaseResponse<string>
                    {
                        IsSuccessfull = false,
                        Message = "Family Head not FOUND"
                    };
                }

                var organization = await _organizationRepo.Get(i => i.Id == loggedinUser.Id);

                //updating properties
                getUser.ProfileImage = getUser.ProfileImage ?? request.ProfileImage;
                getUser.PhoneNumber = getUser.PhoneNumber ?? request.PhoneNumber;
                getUser.Name = getUser.Name;
                getUser.Email = getUser.Email;
                getUser.IsEmailConfirmed = getUser.IsEmailConfirmed ? true : false;
                getUser.Address = getUser.Address ?? request.Address;

                organization.LOcalGovernment = request.LOcalGovernment;
                organization.PostalCode = request.PostalCode;
                organization.City = request.City;
                organization.NumberOfPeopleInOrganization = request.NumberOfPeopleInOrganization;

                _userRepo.Update(getUser);
                _organizationRepo.Update(organization);
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
