const minusButton = document.querySelector(".product-detail__quantity-button--left");
const plusButton = document.querySelector(".product-detail__quantity-button--right");
const inquiryButton = document.querySelector(".inquiry-button");
const addStone = document.querySelector(".form__add-stone");

var type = {}, quality = {};
// Stons & Details row function
const down = document.querySelectorAll(".product-detail__details-down");

for (let i = 0; i < down.length; i++) {
    down[i].addEventListener("click", function () { toggleList(this) })
}

function toggleList(down) {
    const list = down.querySelector(".product-detail__details-down-list");
    if (list.style.display === "" || list.style.display === "none") {
        list.style.display = "block";
    } else {
        list.style.display = "none";
    }
}

if (minusButton != null && plusButton != null) {
    minusButton.addEventListener("click", function () { changeQuantity(-1); });
    plusButton.addEventListener("click", function () { changeQuantity(+1); });
}

if (inquiryButton != null && addStone != null) {
    inquiryButton.addEventListener("click", function () { toggleInquiry(); });
    addStone.addEventListener("click", function () { addStoneField(); });
    //Send request to controller
    const xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var stone = JSON.parse(this.responseText);
            type = stone["Type"];
            quality = stone["Quality"];
        }
    };
    xhttp.open("GET", "/ProductDetail/StoneInfo", true);
    xhttp.send();
}

function changeQuantity(n) {
    let quantity = parseInt(document.querySelector(".product-detail__quantity-input").value);
    if (n < 0 && quantity > 1 || n > 0) {
        quantity += n;
        document.querySelector(".product-detail__quantity-input").value = quantity;
    }
}

function toggleInquiry() {
    const form = document.querySelector(".form--inquiry");
    if (form.style.display === "" || form.style.display === "none") {
        if (form.style.display === "") {
            addStoneField();
        }
        form.style.display = "block";
    } else {
        form.style.display = "none";
    }
}

function addStoneField() {
    const stoneZone = document.querySelector(".form__stone-zone");
    stoneZone.appendChild(getStoneField());
    const cancelButton = stoneZone.lastChild.querySelector(".form__cancel-button");
    cancelButton.addEventListener("click", function () { removeStoneField(this); });
}

function getStoneField() {
    let stoneBox = document.createElement("DIV");
    stoneBox.classList.add("form__box");
    stoneBox.classList.add("form__box--four");
    let row = '<div class="form__row">' +
        '<span class="text">Stone :</span>' +
        '<select class="text" name="stone[]">';
    for (let key of Object.values(type)) {
        let option = '<option value="' + key.Name + '">' + key.Name + '</option>';
        row += option;
    }
    row += '</select>' +
        '</div>' + '<div class="form__row">' +
        '<span class="text">Quality :</span>' +
        '<select class="text" name="quality[]">';
    for (let key of Object.values(quality)) {
        let option = '<option value="' + key.Quality + '">' + key.Quality + '</option>';
        row += option;
    }
    row += '</select>' +
        '</div>' +
        '<div class="form__row">' +
        '<span class="text">Carat :</span>' +
        '<input type="text" name="carat[]" required>' +
        '</div>' +
        '<div class="form__row">' +
        '<span class="text">Amount :</span>' +
        '<input type="text" name="amount[]" required>' +
        '</div>' +
        '<div class="form__cancel">' +
        '<a href="javascript:;" class="text form__cancel-button">' +
        '<svg width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-trash" fill="currentColor" xmlns="http://www.w3.org/2000/svg">' +
        '<path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z" />' +
        '<path fill-rule="evenodd" d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4L4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z" />' +
        '</svg>' +
        '</a>' +
        '</div>';
    stoneBox.innerHTML = row;
    return stoneBox;
}

function removeStoneField(button) {
    const stoneZone = document.querySelector(".form__stone-zone");
    if (stoneZone.childElementCount > 1) {
        let stoneField = button.parentElement.parentElement;
        stoneZone.removeChild(stoneField);
    } else {
        alert("Can not DELETE cause this row is latest!")
    }
}