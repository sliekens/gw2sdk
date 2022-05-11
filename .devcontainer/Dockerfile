# Create a dev environment from scratch because we need .NET, .NET Core, Mono etc.
# Microsoft publishes only images that contain just a single SDK version.
# We'll start from a simple Debian image with just Curl installed, then add our dependencies 
FROM buildpack-deps:bullseye-curl

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

# Install needed packages and setup non-root user. Use a separate RUN statement to add your own dependencies.
# [Option] Install zsh
ARG INSTALL_ZSH="true"
# [Option] Upgrade OS packages to their latest versions
ARG UPGRADE_PACKAGES="true"
ARG USERNAME=vscode
ARG USER_UID=1000
ARG USER_GID=$USER_UID
COPY library-scripts/*.sh /tmp/library-scripts/
RUN export DEBIAN_FRONTEND=noninteractive\
  && apt-get update\
  && bash /tmp/library-scripts/common-debian.sh "${INSTALL_ZSH}" "${USERNAME}" "${USER_UID}" "${USER_GID}" "${UPGRADE_PACKAGES}" "true" "true"\
  && bash /tmp/library-scripts/docker-debian.sh\
  && bash /tmp/library-scripts/dotnet-debian.sh "6.0" "false" "automatic" "true"\
  && bash /tmp/library-scripts/dotnet-debian.sh "5.0" "true" "automatic" "false"\
  && bash /tmp/library-scripts/dotnet-debian.sh "3.1" "true" "automatic" "false"\
  && bash /tmp/library-scripts/node-debian.sh\
  && bash /tmp/library-scripts/git-from-src-debian.sh "os-provided"\
  && bash /tmp/library-scripts/github-debian.sh\
  && bash /tmp/library-scripts/powershell-debian.sh\
  # https://www.mono-project.com/download/stable/#download-lin-debian
  && apt-get install -y apt-transport-https dirmngr gnupg ca-certificates\
  && apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF\
  && echo "deb https://download.mono-project.com/repo/debian stable-buster main" | tee /etc/apt/sources.list.d/mono-official-stable.list\
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

 ENTRYPOINT ["/usr/local/share/docker-init.sh"]
 CMD ["sleep", "infinity"]