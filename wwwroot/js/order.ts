const unitPriceElement = document.querySelector('#UnitPrice') as HTMLInputElement;
const quantityElement = document.querySelector('#Quantity') as HTMLInputElement;
const orderTotalElement = document.querySelector('#orderTotal') as HTMLSpanElement;
if (quantityElement && unitPriceElement) {
    quantityElement.addEventListener('change', _ => {
        //const unitPrice = Number(unitPriceElement.value);
        const unitPrice = Number(myUnFormatPrice(unitPriceElement.value));
        const quantity = Number(quantityElement.value);
        const orderTotal = unitPrice * quantity;
        orderTotalElement.textContent = orderTotal.toLocaleString('it-IT', {
            style: 'currency',
            currency: 'EUR',
        });
    });
}

function myUnFormatPrice(formated: string) {
    return parseFloat(formated.replaceAll(',', '.'));
}