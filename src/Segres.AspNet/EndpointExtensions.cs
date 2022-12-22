﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Segres.AspNet;

public static class EndpointExtensions
{
    public static IApplicationBuilder UseSegres(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.MapEndpoints();
    }

    private static IApplicationBuilder MapEndpoints(this IApplicationBuilder applicationBuilder)
    {
        var options = applicationBuilder.ApplicationServices.GetRequiredService<SegresConfiguration>();

        var types = options.Assemblies.SelectMany(y => y.DefinedTypes
                .Where(x => x is {IsAbstract: false, IsInterface: false})
                .Where(x => x.GetInterfaces().Any(i => i == typeof(IEndpointConfiguration))))
            .SelectMany(x => x.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IAsyncRequestHandler<,>)))
            .ToArray();

        foreach (var type in types)
        {
            var definitionType = GetDefinitionType(type);
            var definition = (EndpointDefinition) Activator.CreateInstance(definitionType, applicationBuilder)!;

            var serviceProvider = applicationBuilder.ApplicationServices.CreateScope().ServiceProvider;
            if (serviceProvider.GetRequiredService(type) is not IEndpointConfiguration endpointConfiguration)
                continue;

            endpointConfiguration.Configure(definition);
        }

        return applicationBuilder;
    }

    private static Type GetDefinitionType(Type type)
    {
        var arguments = type.GetGenericArguments();
        
        var firstArgument = arguments[0];
        var secondArgument = arguments[1];

        return secondArgument == typeof(IHttpResult) 
            ? typeof(RequestEndpointDefinition<>).MakeGenericType(firstArgument) 
            : typeof(RequestEndpointDefinition<,>).MakeGenericType(firstArgument, secondArgument.GetGenericArguments()[0]);
    }
    
    internal static object? ToResult<TResult>(this IHttpResult<TResult> result)
    {
        if (result is IAspNetResult aspNetResult)
            return aspNetResult.AspNetResult;

        if (result.IsSuccess)
            return result.Result;

        return result.Error;
    }
    
    internal static object? ToResult(this IHttpResult result)
    {
        if (result is IAspNetResult aspNetResult)
            return aspNetResult.AspNetResult;

        if (result.IsSuccess)
            return Results.Ok();

        return result.Error;
    }
}