$(document).ready(function () {
    var path = window.location.pathname.substring(1);
    var pieces = path.split('/');
    path = pieces[pieces.length - 1];
    $('#main-menu>ul>li>a[href$="' + path + '"]').parent().addClass('active');
});