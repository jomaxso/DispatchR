﻿using System.Runtime.CompilerServices;
using Segres.UnitTest.Stream.Objects;

namespace Segres.UnitTest.Stream.Handlers;

public class IntegerStreamHandler : IStreamHandler<IntegerStreamRequest, int>
{
    public async IAsyncEnumerable<int> HandleAsync(IntegerStreamRequest streamRequest, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        for (var i = 0; i < 10; i++) yield return i;
    }
}