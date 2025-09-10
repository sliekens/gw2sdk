#!/bin/bash
set -euo pipefail
export DEBIAN_FRONTEND=noninteractive

# https://www.mono-project.com/download/stable/#download-lin-debian
echo "(*) Installing MONO"

apt-get update -y
apt-get install -y --no-install-recommends ca-certificates curl gnupg dirmngr

KEYRING_PATH=/usr/share/keyrings/mono-official-archive-keyring.gpg
SOURCES_PATH=/etc/apt/sources.list.d/mono-official-stable.sources
MONO_KEY=3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF

echo "- Fetching GPG key"
if ! curl -fsSL https://download.mono-project.com/repo/xamarin.gpg | gpg --dearmor > "${KEYRING_PATH}.tmp"; then
    echo "  Primary download failed; trying keyserver" >&2
    gpg --batch --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys "${MONO_KEY}" || gpg --batch --keyserver hkp://keyserver.pgp.com --recv-keys "${MONO_KEY}"
    # Export and dearmor
    gpg --export "${MONO_KEY}" | gpg --dearmor > "${KEYRING_PATH}.tmp"
fi
mv "${KEYRING_PATH}.tmp" "${KEYRING_PATH}"
chmod 644 "${KEYRING_PATH}"

echo "- Configuring apt source (deb822)"
# Debian 13 (Trixie) note: Mono repo key uses SHA1 and will trigger a policy warning.
# Upstream must re-sign with a stronger digest; this script only sets up sources.
cat > "${SOURCES_PATH}" <<EOF
Types: deb
URIs: https://download.mono-project.com/repo/debian
Suites: stable
Components: main
Signed-By: ${KEYRING_PATH}
EOF

echo "- Extending Sequoia SHA1 acceptance (Mono repo workaround)"
# Workaround: extend sha1.second_preimage_resistance to keep accepting Mono's SHA1 signatures
# until upstream re-signs with a stronger hash. Security trade-off: prolongs SHA1 acceptance.
SEQUOIA_OVERRIDE_DIR=/etc/crypto-policies/back-ends
SEQUOIA_OVERRIDE_FILE="${SEQUOIA_OVERRIDE_DIR}/apt-sequoia.config"
TARGET_SHA1_DATE=2028-02-01
mkdir -p "${SEQUOIA_OVERRIDE_DIR}"
if [ ! -f "${SEQUOIA_OVERRIDE_FILE}" ]; then
    cp /usr/share/apt/default-sequoia.config "${SEQUOIA_OVERRIDE_FILE}" || echo "  (info) default sequoia config not found; creating minimal file" >&2
    if grep -q '^sha1.second_preimage_resistance' "${SEQUOIA_OVERRIDE_FILE}"; then
        sed -i "s/^sha1.second_preimage_resistance = .*/sha1.second_preimage_resistance = ${TARGET_SHA1_DATE}    # Extended for Mono repository (SHA1 workaround)/" "${SEQUOIA_OVERRIDE_FILE}"
    else
        printf '\n[hash_algorithms]\nsha1.second_preimage_resistance = %s    # Extended for Mono repository (SHA1 workaround)\n' "${TARGET_SHA1_DATE}" >> "${SEQUOIA_OVERRIDE_FILE}"
    fi
else
    echo "  (info) Existing Sequoia override detected; leaving unchanged" >&2
fi


echo "- Updating package lists"
apt-get update -y

echo "- Installing packages"
apt-get install -y --no-install-recommends mono-complete gdb

echo "- Cleaning up"
apt-get clean -y
rm -rf /var/lib/apt/lists/*

echo "Done!"
