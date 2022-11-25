﻿namespace Segres;

/// <summary>
/// 
/// </summary>
public interface IStreamer
{
    /// <summary>
    /// Asynchronously receive a streamRequest from a single handler.
    /// </summary>
    /// <param name="streamRequest"></param>
    /// <param name="cancellationToken">An optional cancellation token to observe while waiting for the task to complete.</param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns>A streamRequest as <see cref="IAsyncEnumerable{T}"/>.</returns>
    /// <seealso cref="IStreamHandler{TStream,TResult}"/>
    IAsyncEnumerable<TResult> CreateStreamAsync<TResult>(IStreamRequest<TResult> streamRequest, CancellationToken cancellationToken = default);
}