import * as fs from 'fs';
import * as yaml from 'js-yaml';
import { SidebarConfigArray, SidebarGroup, SidebarItem } from 'vuepress';

type Entry = { name: string, href?: string, items?: Entry[] };

function toItem(entry: Entry, depth: number): string | SidebarItem | SidebarGroup {
    return {
        text: entry.name,
        link: entry.href
    };
}

export function generateApiSidebar(): SidebarConfigArray {
    const yamlData = fs.readFileSync('api/toc.yml', 'utf8');
    const jsObject = yaml.load(yamlData) as Entry[];
    return jsObject.map(toItem);
}
