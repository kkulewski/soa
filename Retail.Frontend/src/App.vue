<template>
  <div id="app">

    <header>
      <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
          <div class="container">
              <a class="navbar-brand" asp-area="" asp-controller="Home" asp-action="Index">Retail</a>
              <button class="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse">
                  <span class="navbar-toggler-icon"></span>
              </button>
              <div class="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
                  <ul class="navbar-nav flex-grow-1">
                      <li class="nav-item">
                          <a class="nav-link text-dark">Home</a>
                      </li>
                      <li class="nav-item">
                          <a class="nav-link text-dark">Privacy</a>
                      </li>
                  </ul>
              </div>
          </div>
      </nav>
    </header>

    <div class="container">
      <Products msg="Retail"/>
    </div>

    <footer class="border-top footer text-muted">
      <div class="container">
        &copy; 2021 - Retail
      </div>
    </footer>

  </div>
</template>

<script>
import Products from './components/Products.vue'

export default {
  name: 'App',
  components: {
    Products
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
