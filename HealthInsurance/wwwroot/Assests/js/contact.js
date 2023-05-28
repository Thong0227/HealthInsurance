function sendContact() {
    let formData = $('form#contact-form').serializeArray();
    $.ajax({
        type: 'POST',
        url: '/Contact/Create',
        data: formData,
        success: function (result) {
            alert('Successfully received Data ');
            console.log(result);
        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed ');
        }
    })
}