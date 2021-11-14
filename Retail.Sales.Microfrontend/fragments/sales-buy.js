class SalesBuy extends HTMLElement
{
    connectedCallback() {
        this.innerHTML = "<button>Buy now</button>"
    }
}

window.customElements.define("sales-buy", SalesBuy);