//handle signout

$(document).ready(function () {

    $('.js-signout').on('click', function () {

        $('#SignOut').submit();
        $(this).parent().submit();
    });

});
