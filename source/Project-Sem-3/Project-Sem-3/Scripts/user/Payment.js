const down = document.querySelectorAll(".payment__item-down");
const checkbox = document.querySelector(".payment__checkbox-input");
let user = null;

for (let i = 0; i < down.length; i++) {
    down[i].addEventListener("click", function () { toggleInfo(this); });
}

if (checkbox != null) {
    checkbox.addEventListener("click", function () { showInfo(); });
    const xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            user = JSON.parse(this.responseText);
        }
    };
    xhttp.open("GET", "/Payment/UserInfo", true);
    xhttp.send();
}

function toggleInfo(down) {
    const box = down.parentElement.parentElement;
    const details = box.querySelector(".payment__item-details");
    if (details.style.display === "" || details.style.display === "none") {
        details.style.display = "block";
    } else {
        details.style.display = "none";
    }
}

// Get User Info

function showInfo() {
    const fullName = document.querySelector("#FullName");
    const email = document.querySelector("#Email");
    const phone = document.querySelector("#Phone");
    const address = document.querySelector("#Address");
    const city = document.querySelector("#City");
    const country = document.querySelector("#Country");
    if (checkbox.checked) {

        fullName.value = user.FullName;
        email.value = user.Email;
        phone.value = user.Phone;
        address.value = user.Address;
        city.value = user.City;
        country.value = user.Country;

        fullName.readOnly = true;
        email.readOnly = true;
        phone.readOnly = true;
        address.readOnly = true;
        city.readOnly = true;
        country.readOnly = true;
    } else {
        fullName.readOnly = false;
        email.readOnly = false;
        phone.readOnly = false;
        address.readOnly = false;
        city.readOnly = false;
        country.readOnly = false;
    }
}

// For reload page
const reloadPage = document.querySelector(".reload-page");

if (reloadPage != null) {
    setTimeout(function () { window.location.href = "/" }, 3000);
}