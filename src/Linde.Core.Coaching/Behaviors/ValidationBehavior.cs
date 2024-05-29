using ErrorOr;
using FluentValidation;
using MediatR;

namespace Linde.Core.Coaching.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new FluentValidation.ValidationContext<TRequest>(request);
            var result = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(context, cancellationToken)));
            var failures = result.SelectMany(x => x.Errors)
                .Where(x => x != null)
                .Select(x => Error.Validation(
                    x.PropertyName,
                    x.ErrorMessage
                )).ToList();
            return (dynamic)failures;
        }
        return await next();
    }
}
