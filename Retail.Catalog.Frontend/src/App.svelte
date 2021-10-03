<script>
	export let products;

	async function getProducts () {
		const res = await fetch('http://localhost:5001/product', { method: 'GET'} )
		products = await res.json()
	}

</script>

<button type="button" on:click={getProducts}>Fetch products</button>
<ul>
	{#each products as product}
		<li>[{product.productId}] {product.name} - {product.description}</li>
	{/each}
</ul>

<style>
</style>