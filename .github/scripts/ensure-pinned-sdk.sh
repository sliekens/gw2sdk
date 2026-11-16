#!/usr/bin/env bash
# The devcontainer image is the normal SDK source. It is rebuilt on the same
# push that bumps the pin, so the first CI run can still be on the previous
# image. Install the exact global.json SDK for this job when the image does
# not have it. With no rollForward key, resolution uses the patch policy and
# will accept a later patch when the pinned version is missing. Compare the
# resolved version to sdk.version so that does not skip the install.
# This script does not set rollForward.
set -euo pipefail

pinned=$(
  sed -n '/"sdk"[[:space:]]*:/,/}/p' global.json \
    | sed -n 's/.*"version"[[:space:]]*:[[:space:]]*"\([^"]*\)".*/\1/p' \
    | head -n 1
)
if [[ -z "$pinned" ]]; then
  echo "Could not read sdk.version from global.json." >&2
  exit 1
fi

resolved=""
if resolved=$(dotnet --version 2>/dev/null); then
  if [[ "$resolved" == "$pinned" ]]; then
    echo "Pinned SDK is already installed: $resolved"
    exit 0
  fi
  echo "Resolved SDK $resolved does not match pin $pinned. Installing $pinned."
else
  echo "Pinned SDK $pinned is not installed. Installing it."
fi

install_dir="${HOME}/.dotnet"
installer="${RUNNER_TEMP:-/tmp}/dotnet-install.sh"
curl -fsSL https://dot.net/v1/dotnet-install.sh -o "$installer"
bash "$installer" --jsonfile global.json --install-dir "$install_dir"

# Prepend so this muxer wins over the image's dotnet.
if [[ -n "${GITHUB_PATH:-}" ]]; then
  echo "$install_dir" >> "$GITHUB_PATH"
fi
if [[ -n "${GITHUB_ENV:-}" ]]; then
  echo "DOTNET_ROOT=$install_dir" >> "$GITHUB_ENV"
fi

export DOTNET_ROOT="$install_dir"
export PATH="$install_dir:$PATH"
installed=$(dotnet --version)
if [[ "$installed" != "$pinned" ]]; then
  echo "Installed SDK $installed does not match pin $pinned." >&2
  exit 1
fi
echo "Installed pinned SDK: $installed"
