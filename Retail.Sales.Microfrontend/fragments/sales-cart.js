class SalesCart extends HTMLElement
{
    products = [];

    connectedCallback() {
        window.addEventListener("sales:item_added_to_cart", e => {
            this.products.push(e.detail.productId);
            this.render();
        });
        this.render();
    }

    render() {
        this.innerHTML = `${this.products.length} products in the cart`
    }
}

window.customElements.define("sales-cart", SalesCart);