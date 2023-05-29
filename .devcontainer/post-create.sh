#!/bin/bash
#
# This script is executed inside the dev container when it is first created.
# Useful for just-in-time config.
#
echo "alias ll='ls -lah'" >> ~/.bashrc
echo "alias jb='dotnet jb'" >> ~/.bashrc
echo "alias vuepress='npx vuepress'" >> ~/.bashrc
echo "alias httprepl='dotnet httprepl https://api.guildwars2.com'" >> ~/.bashrc

# Fix ownership of the home folder
# because the default owner is 'root' on some host operating systems
sudo chown -R vscode:vscode /home/vscode

# Ensure dotnet tools are available from terminal
dotnet tool restore