#!/bin/bash
#
# This script is executed inside the dev container when it is first created.
# Useful for just-in-time config.
#

# Generate a dev cert for web samples
dotnet dev-certs https

# Ensure dotnet tools are available from terminal
dotnet tool restore
echo "alias jb='dotnet jb'" >> ~/.bashrc
