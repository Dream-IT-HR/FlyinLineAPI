using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Flyinline.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Flyinline.Application.Pipeline
{
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public RequestPerformanceBehaviour(ILogger<TRequest> logger, ICurrentUserAccessor currentUserAccessor)
        {
            _timer = new Stopwatch();
            _logger = logger;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 500)
            {
                var name = typeof(TRequest).Name;
                string userName = _currentUserAccessor.GetUsername();
                string userText = "";

                if (!string.IsNullOrWhiteSpace(userName))
                {
                    userText = $"Username: {userName}";
                }

                _logger.LogWarning($"Long Running Request: {name} ({_timer.ElapsedMilliseconds} milliseconds) {userText} { request.ToString() }");
            }

            return response;
        }
    }
}
