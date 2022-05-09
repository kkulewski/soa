import { createApp } from "vue";
import App from "./App.vue";

import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";

const app = createApp(App);
app.config.compilerOptions.isCustomElement = (tag) => tag.startsWith("sales-");
app.mount("#app");
