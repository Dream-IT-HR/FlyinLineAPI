using System.Threading;
using System.Threading.Tasks;
using Flyinline.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Northwind.Application.Exceptions;

namespace Flyinline.Application.Pipeline
{
    public class RequestAuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICurrentUserAccessor _currentUserAccessor;
        public RequestAuthorizationBehaviour(ICurrentUserAccessor currentUserAccessor)
        {
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is IAuthorizedRequest)
            {
                string username = _currentUserAccessor.GetUsername();

                if (string.IsNullOrWhiteSpace(username))
                {
                    throw new NotAuthorizedException();
                }
            }

            var response = await next();
            
            return response;
        }
    }
}
