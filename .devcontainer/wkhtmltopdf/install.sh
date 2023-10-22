#!/usr/bin/env bash
PACKAGE_URL="https://github.com/wkhtmltopdf/packaging/releases/download/$VERSION/wkhtmltox_$VERSION.bullseye_amd64.deb"

echo "(*) Installing wkhtmltopdf"
echo "$PACKAGE_URL"

wget -q $PACKAGE_URL -O wkhtmltopdf.deb
apt-get update
apt-get install -y ./wkhtmltopdf.deb
apt-get clean -y
rm -rf /var/lib/apt/lists/*;
rm wkhtmltopdf.deb
