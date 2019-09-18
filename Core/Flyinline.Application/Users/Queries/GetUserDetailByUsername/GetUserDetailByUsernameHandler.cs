using Flyinline.Application.Interfaces;
using Flyinline.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flyinline.Application.Users.Queries.GetUserDetailByUsername
{
    public class GetUserDetailByUsernameHandler : IRequestHandler<GetUserDetailByUsernameRequest, GetUserDetailByUsernameViewModel>
    {
        private readonly IFlyinlineDbContext _context;

        public GetUserDetailByUsernameHandler(IFlyinlineDbContext context)
        {
            _context = context;
        }

        public async Task<GetUserDetailByUsernameViewModel> Handle(GetUserDetailByUsernameRequest request, CancellationToken cancellationToken)
        {
            var ret = new GetUserDetailByUsernameViewModel();

            List<UserDetail> ls = await _context.UserDetail.AsNoTracking().Where(x => x.Username == request.Username).ToListAsync(cancellationToken);

            ret.Data.AddRange(ls);

            return ret;
        }
    }
}
