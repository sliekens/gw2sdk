#!/usr/bin/env bash
# Usage: ./merge_public_api.sh
# Ships all APIs in both modern and legacy folders, using their respective Unshipped files.

base_dir="$(dirname "$0")"
modern_dir="$base_dir/modern"
legacy_dir="$base_dir/legacy"

shipped_modern="$modern_dir/PublicAPI.Shipped.txt"
shipped_legacy="$legacy_dir/PublicAPI.Shipped.txt"
unshipped_modern="$modern_dir/PublicAPI.Unshipped.txt"
unshipped_legacy="$legacy_dir/PublicAPI.Unshipped.txt"

for pair in \
  "$shipped_modern|$unshipped_modern" \
  "$shipped_legacy|$unshipped_legacy"
  do
    IFS='|' read -r shipped unshipped <<< "$pair"
    # Get removed APIs (strip *REMOVED* prefix)
    removed=$(grep '^\*REMOVED\*' "$unshipped" | sed 's/^\*REMOVED\*//')
    # Get APIs to ship (not *REMOVED* and not comments/empty)
    toship=$(grep -v '^\*REMOVED\*' "$unshipped" | grep -v '^\s*$' | grep -v '^#')
    # Remove shipped APIs that are marked as removed
    grep -vxFf <(echo "$removed") "$shipped" > "$shipped.tmp"
    # Add newly shipped APIs (if not already present)
    printf "%s\n" "$toship" >> "$shipped.tmp"
    awk '!seen[$0]++' "$shipped.tmp" > "$shipped"
    rm "$shipped.tmp"
    # Clear Unshipped (except comments/header)
    grep '^\s*#' "$unshipped" > "$unshipped.tmp"
    mv "$unshipped.tmp" "$unshipped"
done
