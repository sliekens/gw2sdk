import { defineConfig } from "vuepress/config";

export default defineConfig({
  title: 'GW2SDK',
  description: 'A .NET code library for interacting with the Guild Wars 2 API and game client.',
  base: '/gw2sdk/',
  themeConfig: {
    nav: [
      { link: '/', text: 'Home' },
      { link: '/guide/', text: 'Guide' },
      { link: 'https://github.com/sliekens/gw2sdk/', text: 'GitHub' }
    ],
    sidebar: 'auto',
    lastUpdated: true
  }
});