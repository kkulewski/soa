<template>
    <div class="home">
        <div class="home">
            <p>User: {{ username }}</p>
            <button @click="login" v-if="!isAuthenticated">Login</button>
            <button @click="logout" v-if="isAuthenticated">Logout</button>
        </div>
 
    </div>
</template>

<script>
import AuthService from '@/services/auth.service';

export default {
  name: 'Home',
  props: { },
  data () {
    return {
      auth: null,
      username: '',
      isAuthenticated: false
    }
  },
  async created() {
    this.auth = new AuthService();
    this.isAuthenticated = await this.auth.isAuthenticated();

    if (this.isAuthenticated) {
        this.auth.getUser().then((user) => {
            this.username = user.profile.sub;
        });
    }
  },
  methods: {
    login() {
        this.auth.login();
        this.isAuthenticated = true;
        this.auth.getUser().then((user) => {
            this.username = user.profile.sub;
        });
    },
    logout() {
        this.auth.logout();
        this.isAuthenticated = false;
        this.username = '';
    }
  }
}
</script>

<style>
</style>