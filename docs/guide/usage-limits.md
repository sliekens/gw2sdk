# Usage limits

However unfortunate, the API is not unlimited. There is a rate limit that applies to all API requests, which is designed to prevent abuse and ensure fair and stable performance for all users.

The rate limit for the Guild Wars 2 API is officially 600 requests per minute. However the community has discovered that the actual limit is only about 300 requests per minute. This means that if you make more than 300 requests in one minute from the same IP address, you will receive an error response with the status code 429 (Too Many Requests) and a message saying "too many requests"

The API rate limits are applied per IP address, not per account or application. This means that if you have multiple applications or devices using the same IP address, they will share the same rate limit.

## How to deal with the API rate limits?

The best way to deal with the API rate limits is to avoid hitting them in the first place. Here are some tips on how to optimize your API usage and reduce the number of requests:

- Use caching
- Use batching
- use throttling

You should cache any data that is static or rarely changes, such as items, recipes, maps, etc. and only redownload them from the API when your cache becomes stale. You can use various tools and libraries to implement caching in your application, such as a MemoryCache or a distributed cache like Redis.

The Guild Wars 2 API primarily uses the `Last-Modified` or `Expires` headers to indicate the age or freshness of the data. You can use these headers to determine when to refresh your cache. At the time of writing, there is no good caching library for .NET that supports these headers, so you will have to implement your own caching logic.

Batching is a technique that allows you to combine multiple requests into one single request, by using comma-separated values in the query parameters. This can reduce the number of requests. For example, instead of making 10 requests to get 10 different items by their IDs, you can make one request with all 10 IDs in the query parameter: /v2/items?ids=1,2,3,...,10

GW2SDK supports batching by allowing you to pass multiple IDs to the `GetItemsByIds` method. For example, instead of calling `GetItemById(1)` 10 times, you can call `GetItemsByIds(new[] { 1, 2, 3, ..., 10 })` once.

> [!WARNING]
> There is a hard limit of 200 IDs per request. If you pass more than 200 IDs, you will receive an `ArgumentException` with a message saying "too many ids".

> [!TIP]
> Methods for bulk expansion are provided which can be used with more than 200 IDs.
> 
> For example, you can use `Gw2Client.Items.GetItemsBulk(Enumerable.Range(1, 1000).ToList())` to get 1000 items by their ids. This will fan out into 5 requests, each with 200 IDs, which will be executed in parallel. The result is an `IAsyncEnumerable<Item>` which you can iterate with `await foreach`

Throttling is a technique that limits the rate at which you make requests to the API. For example, instead of making 10 requests per second, you can make 1 request per second. You can implement throttling using various methods, such as timers, queues, or libraries.

## What happens if you hit the API rate limits?

If you hit the API rate limits, you will receive a `TooManyRequestsException`

This means that you have exceeded your allowed number of requests. You should temporarily stop making any further requests. Unfortunately, there is no way to know exactly how many requests are available to you at any given time, so you will have to guess based on your previous usage patterns.

## How to handle the API rate limit errors?

GW2SDK does not by default handle API rate limit errors, because rate limit handling is a complex topic that depends on the specific application and use case.

If you encounter a Too Many Requests error, you should handle it gracefully in your application. Here are some suggestions on how to do that:

Non-interactive applications (background jobs, message queue handlers, etc.):

- Retry the request after a reasonable delay. You can use a backoff strategy to increase the delay between retries, to avoid hitting the rate limit again.
- Monitor and log the rate limit errors, and analyze them to identify the root cause and possible solutions.

Interactive applications (apps, websites, chat bots, command line tools etc.):

- Show an appropriate message to the user, informing them that the data is temporarily unavailable and asking them to try again later.
- You can also provide alternative options or fallback data, such as cached data or default values, to keep the user engaged and satisfied.
