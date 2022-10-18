﻿using System.Collections.Concurrent;

namespace Segres.Internal.Cache;

internal sealed class HandlerCache<TValue> : Dictionary<Type, TValue>, IHandlerCache<TValue>
{
    public TValue FindHandler(Type key)
    {
        if (TryGetValue(key, out var value))
            return value;

        throw new InvalidOperationException($"No handler found for type: {key.Name}");
    }

    public TValue FindOrAddHandler(Type key, Func<Type, TValue> adding)
    {
        if (TryGetValue(key, out var value)) 
            return value;
        
        var newHandler = adding(key);
        Add(key, newHandler);

        return newHandler;
    }

    public void Add(KeyValuePair<Type, TValue> keyValuePair)
        => this.Add(keyValuePair.Key, keyValuePair.Value);
}