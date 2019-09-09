using System.Threading;
using System.Threading.Tasks;
using Flyinline.Application.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Flyinline.Application.Pipeline
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public RequestLogger(ILogger<TRequest> logger, ICurrentUserAccessor currentUserAccessor)
        {
            _logger = logger;
            _currentUserAccessor = currentUserAccessor;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            string username = _currentUserAccessor.GetUsername();
            username = string.IsNullOrEmpty(username) ? string.Empty : username;

            _logger.LogInformation($"\nRequest: {name} {request} User: {username}");

            return Task.CompletedTask;
        }
    }
}
