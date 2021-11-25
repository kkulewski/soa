<script>
	export let products;

	async function getProducts () {
		const res = await fetch('http://localhost:5001/productadmin', { method: 'GET'} )
		products = await res.json()
	}

	let newProduct = {
		productId: null,
		name: null,
		description: null
	};

	let addProductResult = {
		status: null,
		statusText: null
	}

	async function addProduct() {
		const res = await fetch('http://localhost:5001/productadmin', {
			method: 'POST',
			headers: { 'Content-Type': 'application/json' },
			body: JSON.stringify(newProduct)
		})

		addProductResult = await res;
	}

</script>

<h1>Product list</h1>

<button type="button" on:click={getProducts}>Fetch</button>
<ul>
	{#each products as product}
		<li>[{product.productId}] {product.name} - {product.description}</li>
	{/each}
</ul>

<hr/>

<h1>Add product</h1>

<label for="productId">ID:</label><input bind:value={newProduct.productId} name="productId" /><br/>
<label for="productName">Name:</label><input bind:value={newProduct.name} name="productName" /><br/>
<label for="productDescription">Description:</label><input bind:value={newProduct.description} name="productDescription" /><br/>
<button type="button" on:click={addProduct}>Add</button><br/>
<p>Result: {addProductResult.status} - {addProductResult.statusText}</p>

<style>
</style>