<script setup lang="ts">
import Home from "./components/Home.vue";
import Products from "./components/Products.vue";
</script>

<template>
  <div id="app">
    <br />

    <div class="border-top"></div>

    <br />

    <div class="container">
      <Home msg="Retail" />
    </div>

    <br />

    <div class="border-top"></div>

    <br />

    <div class="container">
      <Products msg="Product catalog" />
    </div>

    <footer class="border-top footer text-muted">
      <br />
      <div class="container">&copy; 2022 - Retail</div>
    </footer>
  </div>
</template>

<style>
@import "./assets/base.css";
</style>
