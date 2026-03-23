import js from "@eslint/js";
import svelte from "eslint-plugin-svelte";

const browserGlobals = {
    URLSearchParams: "readonly",
    console: "readonly",
    document: "readonly",
    globalThis: "readonly",
    localStorage: "readonly",
    location: "readonly",
    window: "readonly",
};

const sharedRules = {
    yoda: "error",
    eqeqeq: ["warn", "smart"],
    indent: ["error", 4, { SwitchCase: 1 }],
    quotes: ["error", "double"],
    semi: ["off", "never"],
    "no-multi-spaces": ["error", { ignoreEOLComments: true }],
    curly: "error",
    "comma-spacing": "error",
    "brace-style": ["error", "1tbs"],
    "no-var": "error",
    "key-spacing": "warn",
    "keyword-spacing": "warn",
    "space-infix-ops": "warn",
    "arrow-spacing": "warn",
    "no-trailing-spaces": "error",
    "space-before-blocks": "warn",
    "no-unused-vars": ["warn", { args: "none" }],
    "no-console": ["warn", { allow: ["error"] }],
    "no-extra-boolean-cast": "off",
    "no-multiple-empty-lines": ["warn", { max: 1, maxBOF: 0 }],
    "lines-between-class-members": ["warn", "always", { exceptAfterSingleLine: true }],
    "no-unneeded-ternary": "error",
    "no-else-return": ["error", { allowElseIf: false }],
    "array-bracket-newline": ["error", "consistent"],
    "eol-last": ["error", "always"],
    "prefer-template": "error",
    "template-curly-spacing": ["warn", "never"],
    "comma-dangle": ["warn", "always-multiline"],
};

export default [
    {
        ignores: [
            ".DS_Store",
            "node_modules/**",
            "build/**",
            ".svelte-kit/**",
            "package/**",
            ".env",
            ".env.*",
            "wwwroot/**",
            "pnpm-lock.yaml",
            "package-lock.json",
            "yarn.lock",
        ],
    },
    {
        ...js.configs.recommended,
        files: ["**/*.js"],
        languageOptions: {
            ...js.configs.recommended.languageOptions,
            ecmaVersion: "latest",
            sourceType: "module",
        },
        rules: {
            ...js.configs.recommended.rules,
            ...sharedRules,
        },
    },
    ...svelte.configs.recommended,
    {
        files: ["src/**/*.{js,svelte}"],
        languageOptions: {
            ecmaVersion: "latest",
            sourceType: "module",
            globals: browserGlobals,
        },
    },
];
