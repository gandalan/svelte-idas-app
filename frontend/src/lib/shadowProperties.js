// Chromium ignores @property rules in stylesheets of a Shadow Root (NeherApp3 mounts with useShadowDom).
// If the shell does not register Tailwind's properties itself, borders, shadows and gradients would be missing.

/**
 * @property rules from the CSS whose property is not registered yet
 * @param {string} css
 * @param {Set<string>} existingNames
 */
export function missingPropertyRules(css, existingNames) {
    const rules = css.match(/@property\s+--[\w-]+\s*\{[^}]*\}/g) ?? [];
    return rules.filter(rule => !existingNames.has(rule.match(/--[\w-]+/)?.[0] ?? ""));
}

/** @param {Document} doc */
function registeredNames(doc) {
    const names = new Set();
    /** @param {CSSRuleList} rules */
    const walk = (rules) => {
        for (const rule of rules) {
            if (typeof CSSPropertyRule !== "undefined" && rule instanceof CSSPropertyRule) {
                names.add(rule.name);
            } else if ("cssRules" in rule) {
                walk(/** @type {CSSGroupingRule} */ (rule).cssRules);
            }
        }
    };
    for (const sheet of doc.styleSheets) {
        try {
            walk(sheet.cssRules);
        } catch {
            // foreign stylesheets without CORS cannot be read
        }
    }
    return names;
}

// Load and check only once per id, even if nothing was missing (then there is no <style> as marker)
/** @type {Map<string, Promise<void>>} */
const done = new Map();

/**
 * Adds once to the shell's document what our CSS needs and the shell did not register
 * @param {string} cssAddress
 * @param {string} id
 * @param {Document} [doc]
 */
export function registerMissingProperties(cssAddress, id, doc = document) {
    let promise = done.get(id);
    if (!promise) {
        // retry on the next mount after an error
        promise = register(cssAddress, id, doc).catch((e) => {
            done.delete(id);
            throw e;
        });
        done.set(id, promise);
    }
    return promise;
}

/**
 * @param {string} cssAddress
 * @param {string} id
 * @param {Document} doc
 */
async function register(cssAddress, id, doc) {
    if (doc.getElementById(id)) {
        return;
    }
    const response = await fetch(cssAddress);
    if (!response.ok) {
        // an error page would look like CSS without properties and never be retried
        throw new Error(`${cssAddress}: HTTP ${response.status}`);
    }
    const css = await response.text();
    const rules = missingPropertyRules(css, registeredNames(doc));
    if (!rules.length) {
        return;
    }
    const style = doc.createElement("style");
    style.id = id;
    style.textContent = rules.join("\n");
    doc.head.appendChild(style);
}
