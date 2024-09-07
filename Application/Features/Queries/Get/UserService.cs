using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Application.Features.Interfaces.IServices;
using Application.Features.Queries.GeneralServices;
using Domain.Constant;
using Domain.Entities;
using Domain.Enum;
using MediatR;


namespace Application.Features.Queries.Get
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IUriService _uriService;
        private readonly IIndividualRepository _individualRepo;
        private readonly IOragnisationRepository _oragnisationRepo;
        private readonly IDonationRepository _donationRepo;
        private readonly IFamilyHeadRepository _familyHeadRepo;

        public UserService(IUserRepository userRepo, IUriService uriService, IIndividualRepository individualRepo, IDonationRepository donationRepo, IFamilyHeadRepository familyHeadRepo) 
        { 
            _userRepo = userRepo; 
            _uriService = uriService;
            _individualRepo = individualRepo;
            _donationRepo = donationRepo;
            _familyHeadRepo = familyHeadRepo;
        }

        public async Task<PagedResponse<ICollection<UserDetailResponseModel>>> PageResponse(string route, PaginationFilter filter)
        {
            var filterPage = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await ViewAllUsers();
            var pagedResponse = PaginationHelper.CreatePagedReponse(pagedData.Data, filterPage, _uriService, route);
            return pagedResponse;
        }

        public async Task<PagedResponse<ICollection<UserDetailResponseModel>>> ViewAllUsers()
        {
            PaginationFilter Filter = new PaginationFilter();

            var users = await _userRepo.GetAllUserDetailsAsync(Filter);
            if (users == null || !users.Any())
            {
                return new PagedResponse<ICollection<UserDetailResponseModel>>(null, 0, 0, 0)
                {

                    IsSuccessfull = false,
                    Data = null,
                };
            }

            var userDetails = users.Select(u => new UserDetailResponseModel
            {
                Role = u.Role.Name,
                Name = u.Name,
                Email = u.Email,
                IsActivated = u.IsActivated,
                PhoneNumber = u.PhoneNumber,
                UserId = u.Id,
            }).ToList();

            var totalRecords = await _userRepo.CountAsync();

            return new PagedResponse<ICollection<UserDetailResponseModel>>(userDetails, Filter.PageSize, Filter.PageNumber, totalRecords)
            {
                IsSuccessfull = true
                ,
                Data = userDetails,
            };
        }


        public async Task<BaseResponse<UserDetailModel>> GetUserByIdAsync(Guid UserId)
        {
            var user = await _userRepo.GetUserAsync(u => u.Id == UserId);
            var donations = await _donationRepo.GetAll();
            var donationMade = donations.Where(d => d.UserId == UserId ).ToList().Count();
            var donationReceived = donations.Where(d => d.Recipient == UserId ).ToList().Count();
           if(user.Role.Name == RoleConst.Individual)
            {
                var individualUser = await _individualRepo.Get(u => u.UserId == UserId);
                return new BaseResponse<UserDetailModel>
                {
                    IsSuccessfull = true,
                    Data = new UserDetailModel
                    {
                        FirstName = individualUser.FirstName,
                        LastName = individualUser.LastName,
                        Nin = individualUser.Nin,
                        PostalCode = individualUser.PostalCode,
                        PhoneNumber = user.PhoneNumber,
                        City = individualUser.City,
                        Email = user.Email,
                        Address = user.Address,
                        IsActivated = user.IsActivated? true : false,
                        LOcalGovernment = individualUser.LOcalGovernment,
                        NumberOfPersons = individualUser.NumberOfPersons,
                        DonationsDonated = donationMade,
                        DonationsReceived = donationReceived,
                        Role = user.Role.Name,
                        UserId = individualUser.UserId,
                    }
                };
            }
           else if(user.Role.Name == RoleConst.OrganizationHead)
            {
                var organization = await _oragnisationRepo.Get(O => O.UserId == UserId);
                return new BaseResponse<UserDetailModel>
                {
                    IsSuccessfull = true,
                    Data = new UserDetailModel
                    {
                        OganisationName = organization.OganisationName,
                        CacNumber = organization.CacNumber,
                        PostalCode = organization.PostalCode,
                        PhoneNumber = user.PhoneNumber,
                        City = organization.City,
                        Email = user.Email,
                        Address = user.Address,
                        IsActivated = user.IsActivated ? true : false,
                        LOcalGovernment = organization.LOcalGovernment,
                        NumberOfPersons = (int)organization.NumberOfPeopleInOrganization,
                        DonationsDonated = donationMade,
                        DonationsReceived = donationReceived,
                        Role = user.Role.Name,
                        UserId = organization.UserId
                    }
                };
            }
            else if (user.Role.Name == RoleConst.FamilyHead)
            {
                var family = await _familyHeadRepo.Get(F => F.UserId == UserId);
                return new BaseResponse<UserDetailModel>
                {
                    IsSuccessfull = true,
                    Data = new UserDetailModel
                    {
                        FirstName = family.FirstName,
                        LastName = family.LastName,
                        Nin = family.NIN,
                        PostalCode = family.PostalCode,
                        PhoneNumber = user.PhoneNumber,
                        City = family.City,
                        Email = user.Email,
                        Address = user.Address,
                        IsActivated = user.IsActivated ? true : false,
                        LOcalGovernment = family.LOcalGovernment,
                        NumberOfPersons = (int)family.FamilySize,
                        DonationsDonated = donationMade,
                        DonationsReceived = donationReceived,
                        Role = user.Role.Name,
                        UserId = family.UserId
                    }
                };
            }
            return new BaseResponse<UserDetailModel>
            {
                IsSuccessfull = false,
            };
        }

       
    }
}
