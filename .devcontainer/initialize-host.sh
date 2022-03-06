#!/bin/bash
#
# This script is executed on the host before the dev container is created.
# Useful for just-in-time config.
#

if [ -d /run/WSL ]; then
  git config --global credential.helper "/mnt/c/Program\ Files/Git/mingw64/libexec/git-core/git-credential-manager-core.exe"
fi