import { defaultTheme } from 'vuepress';

export default {
  title: 'GW2SDK',
  description: 'A .NET code library for interacting with the Guild Wars 2 API and game client.',
  base: '/gw2sdk/',
  theme: defaultTheme({
    repo: 'sliekens/gw2sdk',
    docsDir: 'docs',
    docsBranch: 'main',
    editLink: true,
    navbar: [
      { link: '/guide/', text: 'Guide' },
    ],
    sidebar: [
      '/guide/',
      '/guide/install',
      '/guide/usage',
      '/guide/translations',
      '/guide/game-link',
      '/guide/http-client-factory',
      '/guide/resiliency',
      '/guide/feedback'
    ],
    lastUpdated: true
  })
};