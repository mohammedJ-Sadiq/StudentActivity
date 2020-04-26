$(function () {
    $(".datepicker").datepicker({
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        changeYear: true,
        yearRange: "-01:+10",
        //minDate: new Date(2017, 0, 1),
        //maxDate: new Date(2018, 0, 1),
    });
});

$(document).ready(function () {
    $("#Time").datetimepicker({
        timeFormat: 'hh:mm TT',
        timeOnly: true,
    });
});