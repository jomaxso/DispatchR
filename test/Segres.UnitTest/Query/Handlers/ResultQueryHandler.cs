﻿using Xunit.Sdk;

namespace Segres.UnitTest.Query;

public class ResultQueryHandler : IRequestHandler<ResultQuery, string>
{
    public async ValueTask<string> HandleAsync(ResultQuery query, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        return query.Number switch
        {
            0 => "Zero",
            > 0 => throw new IndexOutOfRangeException(),
            < 0 => throw new NotEmptyException()
        };
    }
}