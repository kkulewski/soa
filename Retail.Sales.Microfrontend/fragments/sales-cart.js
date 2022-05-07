class SalesCart extends HTMLElement
{
    products = [];

    connectedCallback() {
        window.addEventListener("sales:item_added_to_cart", e => {
            this.products.push(e.detail.product);
            this.render();
        });
        this.render();
    }

    render() {
        this.innerHTML = `${this.products.length} products in the cart. Total: $${this.products.reduce((sum, product) => sum + product.price, 0).toFixed(2)}`
    }
}

window.customElements.define("sales-cart", SalesCart);