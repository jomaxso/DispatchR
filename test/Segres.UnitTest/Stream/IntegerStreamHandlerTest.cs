﻿using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Segres.Extensions.DependencyInjection.Microsoft;
using Segres.UnitTest.Stream.Objects;
using Xunit;

namespace Segres.UnitTest.Stream;

public class IntegerStreamHandlerTest
{
    private readonly IServiceProvider _serviceProvider = new ServiceCollection()
        .AddSegres(typeof(IntegerStream))
        .BuildServiceProvider();

    
    [Fact]
    public async Task CreateStreamAsync_ShouldReturnIntegersFrom0To10_WhenIterated()
    {
        // Arrange
        var streamOption = new IntegerStream();
        var dispatcher = _serviceProvider.GetRequiredService<IServiceBroker>();

        var last = 0;

        // Act
        var stream = dispatcher.CreateStreamAsync(streamOption);

        //Assert
        await foreach (var item in stream)
        {
            item.Should().Be(last);
            last++;
        }
    }
}