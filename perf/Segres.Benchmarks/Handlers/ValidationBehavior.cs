﻿using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Segres;
using Segres.Abstractions;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace DispatchR.Benchmarks.Handlers;

// public class ValidationBehavior<TRequest, TResponse> : IAsyncRequestBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
// {
//     private readonly IValidator<TRequest>? _requestValidator;
//
//     public ValidationBehavior(IValidator<TRequest>? requestValidator = null)
//     {
//         _requestValidator = requestValidator;
//     }
//
//     public async ValueTask<TResponse> HandleAsync(AsyncRequestHandlerDelegate<TResponse> next, TRequest request, CancellationToken cancellationToken)
//     {
//         try
//         {
//             ValidationResult? validationResult = null;
//
//             if (_requestValidator is not null)
//                 validationResult = await _requestValidator.ValidateAsync(request, cancellationToken);
//
//             if (validationResult?.IsValid is null or true)
//                 return await next(request, cancellationToken);
//
//             throw new ValidationException(validationResult.Errors);
//         }
//         catch (ValidationException e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
// }
//
// public class FakeValidationBehaviorS<TRequest, TResponse> : IAsyncRequestBehavior<TRequest, TResponse> where TRequest : Segres.Abstractions.IRequest<TResponse>
// {
//     public ValueTask<TResponse> HandleAsync(AsyncRequestHandlerDelegate<TResponse> next, TRequest request, CancellationToken cancellationToken)
//     {
//         return next(request, cancellationToken);
//     }
// }
//
// public class FakeValidationBehaviorM<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : global::MediatR.IRequest<TResponse>
// {
//     public Task<TResponse> Handle(TRequest request, global::MediatR.RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
//     {
//         return next();
//     }
// }