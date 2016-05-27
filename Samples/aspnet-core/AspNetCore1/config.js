var path = require("path");

// Folder names for client code (sources, distribution)
exports.APP_SRC = "src";
exports.APP_DIST = "wwwroot";

// TypeScript sources
exports.TS_SOURCES =
    ["typings/browser.d.ts", path.join(exports.APP_SRC, "**/*.ts")]

// External script dependencies (combined into single file)
exports.SCRIPT_DEPENDENCIES = [
    "node_modules/systemjs/dist/system.src.js",
    "node_modules/angular2/bundles/angular2-polyfills.js",
    "node_modules/rxjs/bundles/Rx.js",
    "node_modules/angular2/bundles/angular2.dev.js",
    "node_modules/angular2/bundles/http.dev.js"
];
exports.SCRIPT_COMBINED = "scripts.js";

exports.STYLES_DEPENDENCIES = [
    "node_modules/bootstrap/dist/css/bootstrap.css"
];
exports.STYLES_COMBINED = "styles.css";
