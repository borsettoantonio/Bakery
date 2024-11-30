"use strict";
const unitPriceElement = document.querySelector('#UnitPrice');
const quantityElement = document.querySelector('#Quantity');
const orderTotalElement = document.querySelector('#orderTotal');
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
function myUnFormatPrice(formated) {
    return parseFloat(formated.replaceAll(',', '.'));
}
