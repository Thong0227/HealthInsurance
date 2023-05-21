$(document).ready(function () {
    var url = window.location.pathname;
    $('#mySidebar  a[href="' + url + '"]').addClass('w3-blue');
});