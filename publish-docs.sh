#!/bin/bash
npm ci
npm run build
git stash push -u -- docs/.vuepress/dist
git fetch origin gh-pages
git checkout gh-pages
git rm -rf .
git stash pop
git add .
git mv docs/.vuepress/dist/* .
git commit -m "Update docs"
git push