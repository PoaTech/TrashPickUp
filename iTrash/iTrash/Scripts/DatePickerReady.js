if (!Modernizr.inputtypes.date) {
    $(function () {
        $(".datecontrol").datepicker();
    });
}
$(function () {
    $(".datefield").datepicker();
});