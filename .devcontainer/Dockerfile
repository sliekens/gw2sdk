# Create a dev environment from scratch because we need .NET, .NET Core, Mono etc.
# Microsoft publishes only images that contain just a single SDK version.
# We'll start from a simple Debian image with just Curl installed, then add our dependencies 
FROM mcr.microsoft.com/devcontainers/base:debian-11

# Configure various dotnet-cli things the way Microsoft does it for their images
# Additionally disable telemetry
ENV DOCKER_BUILDKIT=1
ENV DOTNET_ROOT="/usr/local/dotnet"
ENV PATH="${PATH}:${DOTNET_ROOT}"
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_GENERATE_ASPNET_CERTIFICATE=false
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
ENV DOTNET_NOLOGO=true
ENV DOTNET_CLI_TELEMETRY_OPTOUT=true

RUN export DEBIAN_FRONTEND=noninteractive\
  && apt-get update\
  # https://www.mono-project.com/download/stable/#download-lin-debian
  && gpg --homedir /tmp --no-default-keyring --keyring /usr/share/keyrings/mono-official-archive-keyring.gpg --keyserver hkp://keyserver.ubuntu.com --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF\
  && echo "deb [signed-by=/usr/share/keyrings/mono-official-archive-keyring.gpg] https://download.mono-project.com/repo/debian stable-buster main" | sudo tee /etc/apt/sources.list.d/mono-official-stable.list\
  && apt-get update\
  && apt-get install -y mono-complete\
  && apt-get clean -y\
  && rm -rf /var/lib/apt/lists/* /tmp/library-scripts;

# Create empty folders for named volumes and change their owner to the non-root user
RUN mkdir -p /home/vscode/.vscode-server/extensions\
 && chown -R vscode /home/vscode/.vscode-server\
 && mkdir -p /home/vscode/.microsoft/usersecrets\
 && chown -R vscode /home/vscode/.microsoft\
 && mkdir -p /home/vscode/.nuget/packages\
 && chown -R vscode /home/vscode/.nuget
 
 CMD ["sleep", "infinity"]