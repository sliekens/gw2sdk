import { defineConfig } from "vuepress/config";

export default defineConfig({
  title: 'GW2SDK',
  description: 'A .NET code library for interacting with the Guild Wars 2 API and game client.',
  base: '/gw2sdk/',
  themeConfig: {
    repo: 'sliekens/gw2sdk',
    docsDir: 'docs',
    docsBranch: 'main',
    editLinks: true,
    nav: [
      { link: '/guide/', text: 'Guide' },
    ],
    sidebar: [
      '/guide/',
      '/guide/install',
      '/guide/usage',
      '/guide/advanced-usage',
      '/guide/http-client-factory',
      '/guide/resiliency',
      '/guide/summary'
    ],
    lastUpdated: true
  }
});