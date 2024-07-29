using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DTOs
{
    public class CreateFamilyHeadCommandModel
    {
        public record CreateFamilyHeadCommand( string FirstName,
                                            string LastName,
                                            int FamilyCount,
                                            string PhoneNumber,
                                            string Email,
                                            string NIN,
                                            string Password) : IRequest<BaseResponse<CreateFamilyHeadResponseCommand>>;


        public record CreateFamilyHeadResponseCommand( Guid UserId,
                                                    Guid FamilyId,
                                                    Guid RoleId,
                                                    string email,
                                                    string NIN,
                                                    string FirstName);
             
        
    }
}
