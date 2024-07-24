using Application.Features.Interfaces.IRepositries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Command.Delete
{
    public class DeleteDonation
    {
        public record DeleteDonationCommand(Guid Id) : IRequest;
        public class DeleteProductRequestHandler : IRequestHandler<DeleteDonationCommand>
        {
            private readonly IDonationRepository _donationRepo;
            public DeleteProductRequestHandler(IDonationRepository donationRepo) 
            {
                _donationRepo = donationRepo;
            }
            public async Task Handle(DeleteDonationCommand request, CancellationToken cancellationToken)
            {
                var getDonation = await _donationRepo.Get(d => d.Id == request.Id);
                if (getDonation != null)
                {
                    _donationRepo.Delete(getDonation);
                }
            }
        }
    }
}
