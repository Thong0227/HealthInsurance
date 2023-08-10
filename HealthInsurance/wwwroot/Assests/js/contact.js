function sendContact() {
    let formData = $('form#contact-form').serializeArray();
    $.ajax({
        type: 'POST',
        url: '/Contact/Create',
        data: formData,
        success: function (result) {
            if (result.success) {
                // Xử lý khi thành công
                $('.alert-success').removeClass('d-none');
                $('.alert-danger').addClass('d-none');
            } else {
                // Xử lý khi không thành công
                var errors = result.errors;
                var errorMessages = '';
                for (var i = 0; i < errors.length; i++) {
                    var errorMessage = errors[i].errorMessage;
                    errorMessages += '<p>' + errorMessage + '</p>';
                }
                $('.alert-success').addClass('d-none');
                $('.alert-danger').removeClass('d-none');
                $('.alert-danger p').html(errorMessages);

            }
        },
        error: function (error) {
            // Xử lý khi gặp lỗi request
            alert('Lỗi request: ' + error);
            $('.alert-success').hide();
            $('.alert-danger').show();
        }
    })
}
