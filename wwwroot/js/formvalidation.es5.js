
// Form validation to validate the contact, register and login-form
'use strict';

function displayErrorMessage(inputElement, errorMessage) {
    var errorContainer = inputElement.nextElementSibling;
    errorContainer.textContent = errorMessage;
    errorContainer.style.display = 'block';
}

function clearErrorMessage(inputElement) {
    var errorContainer = inputElement.nextElementSibling;
    errorContainer.textContent = '';
    errorContainer.style.display = 'none';
}

// Custom error messages
var errorMessages = {
    FirstName: "Your first name must contain at least three characters",
    LastName: "Your last name must contain at least three characters",
    Email: "You must use a valid email address",
    Message: "Your message must be 8 characters or longer",
    Password: "Please use a strong password, with special chars and so on",
    ConfirmPassword: "Password is not matching"
};

function validateTextInputField(event) {
    var htmlElement = event.srcElement;

    if (htmlElement.value.trim().length < 3) {
        htmlElement.classList.add("is-invalid");
        displayErrorMessage(htmlElement, errorMessages.FirstName);
        isValid = false;
    } else {
        htmlElement.classList.remove("is-invalid");
        clearErrorMessage(htmlElement);
        isValid = true;
    }
}

function validateLongTextInputField(event) {
    var htmlElement = event.srcElement;

    if (htmlElement.value.trim().length < 8) {
        htmlElement.classList.add("is-invalid");
        displayErrorMessage(htmlElement, errorMessages.Message);
        isValid = false;
    } else {
        htmlElement.classList.remove("is-invalid");
        clearErrorMessage(htmlElement);
        isValid = true;
    }
}

function validateEmailInputField(event) {
    var htmlElement = event.srcElement;
    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!emailRegex.test(htmlElement.value.trim())) {
        htmlElement.classList.add("is-invalid");
        displayErrorMessage(htmlElement, errorMessages.Email);
        isValid = false;
    } else {
        htmlElement.classList.remove("is-invalid");
        clearErrorMessage(htmlElement);
        isValid = true;
    }
}

function validatePasswordInputField(event) {
    var htmlElement = event.srcElement;
    var passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d|\W).{6,}$/;

    if (!passwordRegex.test(htmlElement.value.trim())) {
        htmlElement.classList.add("is-invalid");
        displayErrorMessage(htmlElement, errorMessages.Password);
        isValid = false;
    } else {
        htmlElement.classList.remove("is-invalid");
        clearErrorMessage(htmlElement);
        isValid = true;
    }
}

function validatePasswordConfirmInputField(event) {
    var htmlElement = event.srcElement;
    var passwordInput = document.getElementById("Password");

    if (htmlElement.value.trim() !== passwordInput.value.trim()) {
        htmlElement.classList.add("is-invalid");
        displayErrorMessage(htmlElement, errorMessages.ConfirmPassword);
        isValid = false;
    } else {
        htmlElement.classList.remove("is-invalid");
        clearErrorMessage(htmlElement);
        isValid = true;
    }
}

