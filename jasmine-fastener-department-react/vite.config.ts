import react from '@vitejs/plugin-react';
import path from 'node:path';
import {defineConfig} from 'vite';
import svgr from 'vite-plugin-svgr';
import tailwindcss from '@tailwindcss/vite'

export default defineConfig({
    server: {
        host: '0.0.0.0', // Слушаем все интерфейсы
        port: 5173,      // Явно указываем порт (опционально)
    },
    plugins: [
        tailwindcss(),
        svgr(),
        react({
            babel: {
                plugins: [
                    [
                        '@emotion/babel-plugin',
                        {
                            importMap: {
                                '@mui/material/styles': {
                                    styled: {
                                        canonicalImport: ['@emotion/styled', 'default'],
                                        styledBaseImport: ['@mui/material/styles', 'styled'],
                                    },
                                },
                                '@mui/system': {
                                    styled: {
                                        canonicalImport: ['@emotion/styled', 'default'],
                                        styledBaseImport: ['@mui/system', 'styled'],
                                    },
                                },
                            },
                        },
                    ],
                ],
            },
            jsxImportSource: '@emotion/react',
        }),
    ],
    resolve: {
        alias: {
            '~': path.resolve(__dirname, 'src'),
        },
    },
});
