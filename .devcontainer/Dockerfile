# Create a dev environment from scratch because we need .NET, .NET Core, Mono etc.
# Microsoft publishes only images that contain just a single SDK version.
# We'll start from a simple Debian image with just Curl installed, then add our dependencies 
FROM buildpack-deps:bullseye-curl

# [Option] Install zsh
ARG INSTALL_ZSH="true"
# [Option] Upgrade OS packages to their latest versions
ARG UPGRADE_PACKAGES="true"

# Install needed packages and setup non-root user. Use a separate RUN statement to add your own dependencies.
ARG USERNAME=vscode
ARG USER_UID=1000
ARG USER_GID=$USER_UID
COPY library-scripts/*.sh library-scripts/*.env /tmp/library-scripts/
WORKDIR /tmp
RUN export DEBIAN_FRONTEND=noninteractive\
  && bash /tmp/library-scripts/common-debian.sh "${INSTALL_ZSH}" "${USERNAME}" "${USER_UID}" "${USER_GID}" "${UPGRADE_PACKAGES}" "true" "true"\
  && apt-get update\
  && apt-get upgrade -y\
  && apt-get install -y apt-transport-https dirmngr gnupg ca-certificates\
  && curl -sSLO https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb\
  && dpkg -i packages-microsoft-prod.deb\
  && apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF\
  && echo "deb https://download.mono-project.com/repo/debian stable-buster main" | tee /etc/apt/sources.list.d/mono-official-stable.list\
  && apt-get update\
  && apt-get install -y dotnet-sdk-3.1\
  dotnet-sdk-5.0\
  dotnet-sdk-6.0\
  mono-complete\
  && apt-get clean -y\
  && rm -rf /var/lib/apt/lists/*\
  && rm -rf /tmp/*;

# Configure various dotnet-cli things the way Microsoft does it for their images
# Additionally disable telemetry
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_GENERATE_ASPNET_CERTIFICATE=false
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
ENV DOTNET_NOLOGO=true
ENV DOTNET_CLI_TELEMETRY_OPTOUT=true