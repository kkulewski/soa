<template>

  <div class="text-center">
      <h1 class="display-4">Welcome</h1>
      <br/>
      <div class="row justify-content-md-center">

        <div v-if="products.length == 0">
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
                        <p>buy button</p>
                    </div>
            </div>
        </div>

      </div>
  </div>

</template>

<script>
export default {
  name: 'Products',
  props: {
    msg: String
  },
  data () {
    return {
      products: []
    }
  },
  async created () {
    const response = await fetch("http://localhost:5001/product");
    this.products = await response.json();
  },

}
</script>

<style scoped>
</style>
