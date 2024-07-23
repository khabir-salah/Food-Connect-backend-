using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DTOs
{
    public class CreateFamilyCommandModel
    {
        public record CreateFamilyCommand( string FirstName,
                                            string LastName,
                                            int FamilyCount,
                                            string PhoneNumber,
                                            string Email,
                                            string Password) : IRequest<BaseResponse<CreateFamilyResponseCommand>>;


        public record CreateFamilyResponseCommand( Guid UserId,
                                                    Guid FamilyId,
                                                    Guid RoleId,
                                                    string email,
                                                    string FirstName);
             
        
    }
}
