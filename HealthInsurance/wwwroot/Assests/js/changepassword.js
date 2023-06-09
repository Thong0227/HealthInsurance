$(function () {
    $('#confirmPassword').on('change', function () {
        if ($(this).val() != $('#newPassword').val()) {
            $(this).addClass('border-danger');
            $('#newPassword').addClass('border-danger');
            $('.alertForm').html('Password incorrect');

        } else {
            $(this).removeClass('border-danger');
            $('#newPassword').removeClass('border-danger');
            $('.alertForm').html('');
        }
    });
});
function saveNewPassword() {
    let formData = {
        password: $('#password').val(),
        newpassword: $('#newPassword').val()
    };
    if ($('#confirmPassword').val() == $('#newPassword').val()) {
        $.ajax({
            type: 'POST',
            url: '/Account/ChangePassword',
            data: formData,
            success: function (result) {
                $('.alert.alert-success').show().delay(3000).fadeOut();
            },
            error: function () {
                $('.alert.alert-danger').show().delay(3000).fadeOut();
            }
        })
    }
}