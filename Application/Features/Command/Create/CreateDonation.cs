using Application.Features.DTOs;
using Domain.Entities;
using Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.DTOs.CreateDonationCommandModel;

namespace Application.Features.Command.Create
{
    public class CreateDonation
    {
        public class DonationRequestModel : IRequest<BaseResponse<DonationResponseModel>>
        {
            
        }

        public class DonationResponseModel
        {
            public class Handler : IRequestHandler<CreateDonationCommand, BaseResponse<CreateDonationResponseCommand>>
            {
                public Task<BaseResponse<CreateDonationResponseCommand>> Handle(CreateDonationCommand request, CancellationToken cancellationToken)
                {
                    
                }
            }
        }
    }
}
