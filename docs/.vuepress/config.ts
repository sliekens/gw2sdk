import { defaultTheme, viteBundler } from 'vuepress';
import { generateApiSidebar } from './generate-api-sidebar';

const apiSidebar = generateApiSidebar();

export default {
    title: 'GW2SDK',
    description: 'A .NET code library for interacting with the Guild Wars 2 API and game client.',
    dest: '../artifacts/',
    base: '/gw2sdk/',
    shouldPreload: false,
    shouldPrefetch: false,
    theme: defaultTheme({
        repo: 'sliekens/gw2sdk',
        docsDir: 'docs',
        docsBranch: 'main',
        editLink: true,
        navbar: [
            { link: '/guide/', text: 'Guide' },
            { link: '/api/', text: 'API reference' },
        ],
        sidebar: {
            "/guide/": [
                '/guide/',
                '/guide/install',
                '/guide/usage',
                '/guide/translations',
                '/guide/game-link',
                '/guide/http-client-factory',
                '/guide/resiliency',
                '/guide/feedback',
            ],
            "/api/": apiSidebar
        },
        lastUpdated: true
    }),
    bundler: viteBundler({
        vuePluginOptions: {
            template: {
                compilerOptions: {
                    isCustomElement: tag => tag === 'xref'
                }
            }
        }
    })
};