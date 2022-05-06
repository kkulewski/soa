import { createApp } from "vue";
import Vue from "vue";
import App from "./App.vue";

import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";

Vue.config.ignoredElements = ["sales-buy"];

createApp(App).mount("#app");
