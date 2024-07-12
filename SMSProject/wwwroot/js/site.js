//handle signout

$(document).ready(function () {

    $('.js-signout').on('click', function () {

        $('#SignOut').submit();
        $(this).parent().submit();
    });

    $('body').on('click', '.js-confirm', function () {
        var btn = $(this);

        bootbox.confirm({
            message: btn.data('message'),
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-secondary'
                }
            },
            callback: function (result) {
                if (result) {
                    $.post({
                        url: btn.data('url'),
                        data: {
                            '__RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
                        },
                        success: function () {
                            showSuccessMessage();
                        },
                        error: function () {
                            showErrorMessage();
                        }
                    });
                }
            }
        });
    });

});
//Select2
function applySelect2() {
    $('.js-select2').select2();
    $('.js-select2').on('select2:select', function (e) {
        $('form').not('#SignOut').validate().element('#' + $(this).attr('id'));
    });
}
applySelect2();
/*function showSuccessMessage(message = 'Saved successfully!') {
    Swal.fire({
        icon: 'success',
        title: 'Good Job',
        text: message,
        customClass: {
            confirmButton: "btn btn-primary"
        }
    });
}

function showErrorMessage(message = 'Something went wrong!') {
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: message.responseText !== undefined ? message.responseText : message,
        customClass: {
            confirmButton: "btn btn-primary"
        }
    });
}*/

function showSuccessMessage(callback, message = 'Saved successfully!') {
    Swal.fire({
        icon: 'success',
        title: 'Good Job',
        text: message,
        customClass: {
            confirmButton: "btn btn-primary"
        }
    }).then((result) => {
        if (result.isConfirmed) {
            callback();
        }
    });
}

function showErrorMessage(message = 'Something went wrong!') {
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: message,
        customClass: {
            confirmButton: "btn btn-primary"
        }
    });
}

$(document).ready(function () {
    $('#userForm').on('submit', function (event) {
        event.preventDefault();

        var formData = new FormData(this);

        $.ajax({
            type: 'POST',
            url: '/Users/Create', // Change to your actual controller and action
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.success) {
                   /* showSuccessMessage();
                    window.location.href = '/Users/Index'; // Redirect to index page
*/
                    showSuccessMessage(function () {
                        window.location.href = '/Users/Index'; // Redirect to index page
                    });
                    // Optionally, you can redirect or clear the form here
                } else {
                    showErrorMessage(response.errors.join(', '));
                }
            },
            error: function (xhr, status, error) {
                showErrorMessage('Something went wrong!');
            }
        });
    });
});


////
//Handle Toggle Status
$('body').delegate('.js-toggle-status', 'click', function () {
    var btn = $(this);

    bootbox.confirm({
        message: "Are you sure that you need to toggle this item status?",
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-danger'
            },
            cancel: {
                label: 'No',
                className: 'btn-secondary'
            }
        },
        callback: function (result) {
            if (result) {
                $.post({
                    url: btn.data('url'),
                    data: {
                        '__RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
                    },
                    success: function (lastUpdatedOn) {
                        var row = btn.closest('tr');
                        row.remove();

                        showSuccessMessage();

                        showSuccessMessage();
                    },
                    error: function () {
                        showErrorMessage();
                    }
                });
            }
        }
    });
});