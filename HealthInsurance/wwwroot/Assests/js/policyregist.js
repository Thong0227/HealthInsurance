
    function registPolicy() {
        let formData = $('form#registPolicy').serializeArray();
        formData.push({ name: "EndDate", value: '1900-05-27' });
        formData.push({ name: "PolicyStatus", value: false });
        $.ajax({
            type: 'POST',
            url: '/RegistPolicy/Create',
            data: formData,
            success: function (result) {
                $('.alert.alert-success').show().delay(2000).fadeOut(function () {
                    $('#overlay').show();
                    $('.loading').show().delay(3000).fadeOut(function () {
                        $('.payModal').show().fadeIn();
                    });
                });
            },
            error: function () {
                alert('Failed to receive the Data');
            }
        })
    }

$(document).ready(function () {
    // Ẩn modal và overlay khi nhấp vào nút close
    $('.payModal .close').click(function () {
        $('.payModal').fadeOut();
        $('#overlay').fadeOut();
    });

    // Ẩn modal và overlay khi nhấp vào overlay
    $('#overlay').click(function () {
        $('.payModal').fadeOut();
        $('#overlay').fadeOut();
    });
});