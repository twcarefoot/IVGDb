﻿$(document).ready(function () {

    $('button[name="Register"]').attr('disabled', 'disabled');

    $('input#Register-username').change(function () {
        validateUsername();
    });
    $('input#Register-email').change(function () {
        validateEmail();
    });
    $('input#Register-pic-url').change(function () {
        validateURL();
    });
    $('input#Register-password').change(function () {
        validatePassword();
    });
    $('input#Register-confirm-password').keyup(function () {
        validateConfirmPassword();
    });
});

function validateUsername() {

    var name = $('input#Register-username').val();
    if (name.length < 6) {
        $('#UsernameErr').text('Username must be 6 or more characters long');
        return false;
    }
    if (name.length > 5) {
        $('#UsernameErr').text('');
        return true;
    }

}

function validateEmail() {

    var email = $('input#Register-email').val();
    var pattern = new RegExp(/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$/i);
    var match = pattern.test(email);

    if (!match) {
        $('#EmailErr').text('The mail you entered is in an invalid format.');
        return false;
    }
    if (match) {
        $('#EmailErr').text('');
        return true;
    }

}

function validatePassword() {

    var pass = $('input#Register-password').val();
    if (pass.length < 6) {
        $('#PasswordErr').text('Your password must be at least 6 characters long.');
        return false;
    }
    if (pass.length > 5) {
        $('#PasswordErr').text('');
        return true;
    }

}

function validateConfirmPassword() {

    var pass = $('input#Register-password').val();
    var confpass = $('input#Register-confirm-password').val();
    if (confpass !== pass) {
        $('#ConfirmPassErr').text('Your passwords don\'t match');
        $('button[name="Register"]').attr('disabled', 'disabled');
    }
    if (confpass === pass) {
        $('#ConfirmPassErr').text('');
        if (validateUsername() && validateEmail() && validateURL())
            $('button[name="Register"]').removeAttr('disabled');
    }

}
jQuery.validator.setDefaults({
    debug: true,
    success: "valid"
});
function validateURL() {
    
    var exists = $('input#Register-pic-url').val().validate({
        rules: {
            field: {
                required: true,
                url: true
            }
        }
    });

    alert(exists);

}
