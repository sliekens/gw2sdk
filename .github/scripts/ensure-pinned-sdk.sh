#!/usr/bin/env bash
# The devcontainer image is the normal SDK source. It is rebuilt on the same
# push that bumps the pin, so the first CI run can still be on the previous
# image. Install the exact global.json SDK for this job when that happens.
# The pin stays exact: this script does not set rollForward.
set -euo pipefail

if dotnet --version >/dev/null 2>&1; then
  echo "Pinned SDK is already installed: $(dotnet --version)"
  exit 0
fi

install_dir="${HOME}/.dotnet"
installer="${RUNNER_TEMP:-/tmp}/dotnet-install.sh"
curl -fsSL https://dot.net/v1/dotnet-install.sh -o "$installer"
bash "$installer" --jsonfile global.json --install-dir "$install_dir"

# Prepend so this muxer wins over the image's older dotnet.
if [[ -n "${GITHUB_PATH:-}" ]]; then
  echo "$install_dir" >> "$GITHUB_PATH"
fi
if [[ -n "${GITHUB_ENV:-}" ]]; then
  echo "DOTNET_ROOT=$install_dir" >> "$GITHUB_ENV"
fi

export DOTNET_ROOT="$install_dir"
export PATH="$install_dir:$PATH"
echo "Installed pinned SDK: $(dotnet --version)"
