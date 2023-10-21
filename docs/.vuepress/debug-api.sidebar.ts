import * as util from 'util';

// Usage: npx ts-node .vuepress/debug-api.sidebar.ts
import { generateApiSidebar } from './generate-api-sidebar';

console.log(util.inspect(generateApiSidebar(), { depth: null, colors: true, showHidden: false }));
