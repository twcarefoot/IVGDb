$(document).ready(function () {

    $('input#NewGame-title').change(function () {
        validateTitle();
    });

    function validateTitle() {
        var name = $('input#NewGame-title').val();
        if (name.length == 0) {
            $('#GameTitleErr').text('Title Is Required');
            return false;
        }
        if (name.length >= 1) {
            $('#GameTitleErr').text('OK!');
            return true;
        }
    }


    $('input#Register-username').change(function () {
        validateUsername();
    });
    $('input#Register-email').change(function () {
        validateEmail();
    });
    $('input#Register-password').change(function () {
        validatePassword();
    });
    $('input#Register-confirm-password').keyup(function () {
        validateConfirmPassword();
    });

    $('#Registration-form').submit(function (e) {
        if (!validateUsername())
            e.preventDefault();

        if (!validateEmail())
            e.preventDefault();

        if (!validatePassword())
            e.preventDefault();

        if (!validateConfirmPassword())
            e.preventDefault();
    });

    $('#Form-login').bootstrapValidator({
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glypchicon-refresh'
        },
        fields: {
            username: {
                validators: {
                    notEmpty: {
                        message: 'Username is required!'
                    }
                }
            },
            password: {
                validators: {
                    notEmpty: {
                        message: 'Password is required!'
                    }
                }
            }
        }
    });

    // Login button click handler
    $('#Btn-login').on('click', function () {
        bootbox
            .dialog({
                title: 'Login',
                message: $('#Form-login'),
                show: false // We will show it manually later
            })
            .on('shown.bs.modal', function () {
                $('#Form-login')
                    .show()                                 // Show the login form
                    .bootstrapValidator('resetForm', true); // Reset form
            })
            .on('hide.bs.modal', function (e) {
                // Bootbox will remove the modal (including the body which contains the login form)
                // after hiding the modal
                // Therefor, we need to backup the form
                $('#Form-login').hide().appendTo('body');
            })
            .modal('show');
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
        return false;
    }
    if (confpass === pass) {
        $('#ConfirmPassErr').text('');
        return false;
    }

}