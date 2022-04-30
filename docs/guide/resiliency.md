# Resiliency with Polly

Networks are inherently unreliable. The API might not be reachable, be offline for maintenance or simply not respond. There is also a sliding request limit of 600 requests per minute after which the API returns errors for otherwise valid requests. 

To combat these issues, make your HttpClient more resilient with automatic retries, timeout and delay policies. See the full guide here: <https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests>

I recommend at least adding some timeouts and automatic retries. Tweak as needed.

<<< @/samples/PollyUsage/Program.cs

