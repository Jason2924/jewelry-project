const boxes = document.querySelectorAll(".cart__quantity-box");
const leftButtons = document.querySelectorAll(".cart__quantity-button--left");
const rightButtons = document.querySelectorAll(".cart__quantity-button--right");

for (let i = 0; i < boxes.length; i++) {
    const leftButton = boxes[i].querySelector(".cart__quantity-button--left");
    const rightButton = boxes[i].querySelector(".cart__quantity-button--right");
    leftButton.addEventListener("click", function () { changeQuantity(this, -1); })
    rightButton.addEventListener("click", function () { changeQuantity(this, 1); })
}

function changeQuantity(button, operator) {
    const box = button.parentElement.parentElement;
    const input = box.querySelector(".cart__quantity-input");
    const id = input.dataset.id;
    let quantity = parseInt(box.querySelector(".cart__quantity-input").value);
    if (operator < 0 && quantity > 1 || operator > 0) {
        quantity += operator;
        //Send request to controller
        const xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                document.location.reload();
            }
        };
        xhttp.open("GET", "Cart/Update?id=" + id + "&quantity=" + quantity, true);
        xhttp.send();
    }
}