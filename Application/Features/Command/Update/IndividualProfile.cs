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
    public class IndividualProfile
    {
        //updating individual profile
        public class Handler : IRequestHandler<IndividualUpdateCommandModel, BaseResponse<string>>
        {
            private readonly IUserRepository _userRepository;
            private readonly ICurrentUser _currentUser;
            private readonly IIndividualRepository _individualRepo;
            public Handler(IUserRepository userRepository, ICurrentUser currentUser, IIndividualRepository individualRepo)
            {
                _userRepository = userRepository;
                _currentUser = currentUser;
                _individualRepo = individualRepo;
            }
            public async Task<BaseResponse<string>> Handle(IndividualUpdateCommandModel request, CancellationToken cancellationToken)
            {
                //retriving user
                var loggedinUser = await _currentUser.LoggedInUser();

                //getting user properties
                var getUser = await _userRepository.GetUserAsync(u => u.Id == loggedinUser.Id);

                if (getUser == null)
                {
                    return new BaseResponse<string>
                    {
                        IsSuccessfull = false,
                        Message = "Family Head not FOUND"
                    };
                }

                var singleUser = await _individualRepo.Get(i => i.UserId == loggedinUser.Id);

                //assigning new profile update
                getUser.ProfileImage = getUser.ProfileImage ?? request.ProfileImage;
                getUser.PhoneNumber = getUser.PhoneNumber ?? request.PhoneNumber;
                getUser.Name = getUser.Name;
                getUser.Email = getUser.Email;
                getUser.IsEmailConfirmed = getUser.IsEmailConfirmed ? true : false;
                getUser.Address = getUser.Address ?? request.Address;

                singleUser.Nin = singleUser.Nin;
                singleUser.LOcalGovernment = request.LOcalGovernment;
                singleUser.PostalCode = request.PostalCode;
                singleUser.City = request.City;

                _userRepository.Update(getUser);
                _individualRepo.Update(singleUser);
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
