#!/bin/bash
set -euo pipefail
export DEBIAN_FRONTEND=noninteractive

# https://learn.microsoft.com/dotnet/core/deploying/native-aot/#prerequisites
echo "(*) Installing Native AOT prerequisites"

apt-get update -y
apt-get install -y --no-install-recommends clang zlib1g-dev

echo "- Cleaning up"
apt-get clean -y
rm -rf /var/lib/apt/lists/*

echo "Done!"
