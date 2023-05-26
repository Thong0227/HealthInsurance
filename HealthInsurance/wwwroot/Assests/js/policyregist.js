
    function registPolicy() {
        let formData = $('form#registPolicy').serializeArray();
        formData.push({ name: "EndDate", value: '1900-05-27' });
        formData.push({ name: "PolicyStatus", value: false });
        $.ajax({
            type: 'POST',
            url: '/RegistPolicy/Create',
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

