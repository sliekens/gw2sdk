#!/bin/bash
#
# This script reconfigures devcontainer.json to build a new image.
#
# Syntax: ./use-dockerfile
devcontainer=$(dirname $(realpath $0))
jq 'del(.image) + { build: { dockerfile: "Dockerfile" } }' $devcontainer/devcontainer.json > $devcontainer/devcontainer.json.tmp
mv $devcontainer/devcontainer.json.tmp $devcontainer/devcontainer.json