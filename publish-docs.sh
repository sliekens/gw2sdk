#!/usr/bin/env bash

# Build the documentation
npm ci
npm run build

# Create a stash with the created documentation files
git stash push --all -- docs/.vuepress/dist

# Switch to the gh-pages branch
git clean -xdf
git fetch origin gh-pages
git checkout gh-pages

# Overwrite all files with the ones from the stash
git rm -rf .
git stash pop
git add .
git mv docs/.vuepress/dist/* .
git commit -m "Update docs"
git push