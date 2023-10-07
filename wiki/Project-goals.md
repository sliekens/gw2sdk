## Problem statement

The Guild Wars 2 API is a network dependency. There are many things to consider when working with out-of-process dependencies: availability and interoperability just to name a few. Abstracting away the network is difficult and unmarshalling objects from JSON text is [not a well-defined engineering problem][complexity-of-json].

Writing code to fetch data from an HTTP API is easy to do but hard to do right. Many programmers quickly settle for something that works on their machine, without consideration for technical aspects that can lead to failures.

## Project goals

The goal of GW2SDK is to solve at least the interoperability problem:

1. By providing methods to fetch the data
2. By returning data structures that are easy to consume
3. By converting HTTP errors to typed exceptions
4. By always tracking schema changes and providing new packages accordingly

In other words, it allows you to access the Guild Wars 2 API with less code and more confidence.

## Project scope

My personal definition of Done includes:

1. Support the latest schema version of all active API endpoints
2. Support reading data from the Mumble Link protocol on Windows

Currently not in scope (subject to change)

1. Pulling data from the Wiki API or other third-party APIs (help needed)
2. Reading data from the [Arcdps bridge][arcdps-bridge] (help needed)
3. Reading bitmaps from the Logitech LCD API (help needed)

Forever out of scope: any code that violates the ToS like reverse engineering game files or process memory.

### Caveats

GW2SDK attempts to hide the transport protocol without pretending there is no transport layer. For example: if the API is unreachable, you will get an exception to indicate that there is a network problem.

[complexity-of-json]:https://web.archive.org/web/20210323201150/https://einarwh.wordpress.com/2020/05/08/on-the-complexity-of-json-serialization/
[arcdps-bridge]:https://github.com/knobin/arcdps_bridge