class SalesBuy extends HTMLElement
{
    product = { productId: "0", price: 0, isAvailable: false };

    productId = null;

    async connectedCallback() {
        this.productId = this.getAttribute("productid");
        await this.render();
    }

    async render() {
        await this.fetchProductData();
        this.innerHTML = this.createBuyButton();
        this.querySelector("button").addEventListener("click", () => { this.placeOrder(); });
    }

    createBuyButton() {
        return this.product.isAvailable
        ? `<button type="button">Add to cart ($${this.product.price})</button>`
        : `<button type="button" disabled>Not available ($${this.product.price})</button>`;
    }

    async fetchProductData() {
        const response = await fetch(`http://localhost:5002/products/${this.productId}`, { method: 'GET' } )
        this.product = await response.json()
    }

    async placeOrder() {
        this.blockBuyButton();
        const response = await fetch('http://localhost:5002/orders', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ productId: this.productId })
        });
        
        if (response.status == 200) {
            this.indicateSuccess();
        }
        else {
            this.indicateFailure();
        }
    }

    blockBuyButton() {
        const buyButton = this.querySelector("button");
        buyButton.disabled = true;
        buyButton.innerText = "Processing...";
    }

    indicateSuccess() {
        const buyButton = this.querySelector("button");
        buyButton.style.backgroundColor = "#ADFBB0";
        buyButton.innerText = "Added to cart!";

        const itemAddedEvent = new CustomEvent("sales:item_added_to_cart", {
            bubbles: true,
            detail: { product: this.product }});
        this.dispatchEvent(itemAddedEvent);

        setTimeout(() => { this.render() }, 3000);
    }

    indicateFailure() {
        const buyButton = this.querySelector("button");
        buyButton.style.backgroundColor = "#FF9A9A";
        buyButton.innerText = "Cannot add to cart!";
        setTimeout(() => { this.render() }, 3000);
    }
}

window.customElements.define("sales-buy", SalesBuy);