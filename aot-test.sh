#!/usr/bin/env bash
set -euo pipefail

# Publishes tests/GuildWars2.AotTests with Native AOT (reflection-based serialization disabled) and runs the native
# binary. It is a consumer of the public SDK surface; see https://github.com/sliekens/gw2sdk/issues/521.
# Requires the Native AOT prerequisites: https://aka.ms/nativeaot-prerequisites (clang and zlib1g-dev).

root="$(cd "$(dirname "$0")" && pwd)"
project="$root/tests/GuildWars2.AotTests/GuildWars2.AotTests.csproj"
case "$(uname -m)" in
    x86_64) rid=linux-x64 ;;
    aarch64 | arm64) rid=linux-arm64 ;;
    *) echo "Unsupported architecture: $(uname -m)" >&2; exit 1 ;;
esac
output="$root/artifacts/aot/$rid"

# The same checks with the JIT, as a baseline
dotnet run --project "$project" --configuration Release -- "$@"

dotnet publish "$project" --configuration Release --runtime "$rid" --output "$output"
"$output/GuildWars2.AotTests" --native "$@"
