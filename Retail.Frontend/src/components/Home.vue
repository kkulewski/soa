<template>
  <div class="home">
    <h1 class="display-4">{{ msg }}</h1>
    <p>User: {{ username }}</p>
    <button @click="login" v-if="!isAuthenticated">Login</button>
    <button @click="logout" v-if="isAuthenticated">Logout</button>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import AuthService from "@/services/auth.service";

export default defineComponent({
  name: "HomeComponent",
  props: {
    msg: String,
  },
  data() {
    return {
      auth: new AuthService(),
      username: "",
      isAuthenticated: false,
    };
  },
  async created() {
    this.isAuthenticated = await this.auth.isAuthenticated();

    if (this.isAuthenticated) {
      this.auth.getUser().then((user) => {
        if (user) {
          this.username = user.profile["retail_user_email"];
        }
      });
    }
  },
  methods: {
    login() {
      this.auth.login();
      this.isAuthenticated = true;
      this.auth.getUser().then((user) => {
        if (user) {
          this.username = user.profile["retail_user_email"];
        }
      });
    },
    logout() {
      this.auth.logout();
      this.isAuthenticated = false;
      this.username = "";
    },
  },
});
</script>

<style></style>
