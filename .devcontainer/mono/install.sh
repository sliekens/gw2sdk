#!/bin/bash
export DEBIAN_FRONTEND=noninteractive

# https://www.mono-project.com/download/stable/#download-lin-debian
echo "(*) Installing MONO"
gpg --homedir /tmp --no-default-keyring --keyring /usr/share/keyrings/mono-official-archive-keyring.gpg --keyserver hkp://keyserver.ubuntu.com --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
echo "deb [signed-by=/usr/share/keyrings/mono-official-archive-keyring.gpg] https://download.mono-project.com/repo/debian stable-buster main" | sudo tee /etc/apt/sources.list.d/mono-official-stable.list
apt-get update
apt-get install -y mono-complete
apt-get clean -y
rm -rf /var/lib/apt/lists/*;

echo "Done!"