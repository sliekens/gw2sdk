#!/bin/bash
#
# Run this script to fetch the latest library scripts from the VS Code and Codespaces teams
#
# Syntax: ./update-scripts.sh

scripts="common-debian,docker-debian,dotnet-debian,git-from-src-debian,git-lfs-debian,github-debian,powershell-debian,node-debian"
devcontainer=$(dirname $(realpath $0))
mkdir -p $devcontainer/library-scripts && cd $_
curl --fail-early --remote-name-all\
  --fail --silent --show-error "https://raw.githubusercontent.com/microsoft/vscode-dev-containers/main/script-library/{$scripts}.sh"