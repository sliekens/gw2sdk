#!/bin/bash
npm ci
npm run build
git stash push --all -- docs/.vuepress/dist
git clean -xdf
git fetch origin gh-pages
git checkout gh-pages
git rm -rf .
git stash pop
git add .
git mv docs/.vuepress/dist/* .
git commit -m "Update docs"
git push