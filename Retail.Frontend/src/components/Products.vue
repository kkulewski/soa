<template>

  <div class="text-center">
      <p>
        <sales-cart></sales-cart>
      </p>
      <h1 class="display-4">Welcome</h1>
      <br/>
      <div class="row justify-content-md-center">

        <div v-if="!isAuthenticated">
          <p>Log in to browse product catalog</p>
        </div>
        <div v-else-if="products.length == 0">
          <p>Product catalog is empty</p>
        </div>

        <div v-for="product in products" :key="product.productId" class="col-sm-4">
            <div class="card border-success mb-3">
                    <h5 class="card-header">{{ product.name}}</h5>
                    <div class="card-body">
                        <p class="card-text">
                            {{ product.description}}
                        </p>
                        <p class="card-text">
                            <small class="text-muted">{{ product.productId }}</small>
                        </p>
                    </div>
                    <div class="card-footer text-muted">
                      <sales-buy :productId="product.productId"></sales-buy>
                    </div>
            </div>
        </div>

      </div>
  </div>

</template>

<script>
import AuthService from '@/services/auth.service';

export default {
  name: 'Products',
  props: {
    msg: String
  },
  data () {
    return {
      auth: null,
      isAuthenticated: false,
      products: []
    }
  },
  async created () {
    this.auth = new AuthService();
    this.isAuthenticated = await this.auth.isAuthenticated();
    if (this.isAuthenticated) {
      this.auth = new AuthService();
      const userToken = await this.auth.getAccessToken();
      const response = await fetch("http://localhost:5001/product", { headers: { 'Authorization': `Bearer ${userToken}`} });
      this.products = await response.json();
    }
  },
  async mounted() {
    let salesCartWebComponent = document.createElement('script')
    salesCartWebComponent.setAttribute('src', 'http://localhost:6002/sales-cart.js')
    document.head.appendChild(salesCartWebComponent)

    let salesBuyWebComponent = document.createElement('script')
    salesBuyWebComponent.setAttribute('src', 'http://localhost:6002/sales-buy.js')
    document.head.appendChild(salesBuyWebComponent)
  }
}
</script>

<style scoped>
</style>
