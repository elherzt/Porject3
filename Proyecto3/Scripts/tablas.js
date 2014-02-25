$(document).ready(function(){
    $(".tabla1").dataTable();
    //$("#F_START").datepicker();
    //$("#F_END").datepicker();

    $("#F_START").datepicker({
        dateFormat: "yy/mm/dd",
        defaultDate: "+1w",
        changeMonth: true,
        numberOfMonths: 3,
        onClose: function (selectedDate) {
            $("#F_END").datepicker("option", "minDate", selectedDate);
        }
    });
    $("#F_END").datepicker({
        dateFormat: "yy/mm/dd",
        defaultDate: "+1w",
        changeMonth: true,
        numberOfMonths: 3,
        onClose: function (selectedDate) {
            $("#F_START").datepicker("option", "maxDate", selectedDate);
        }
    });
    
    //$("#showcalendar").fullCalendar({
    //    header: {
    //        left: 'prev,next today',
    //        center: 'title',
    //        right: 'month,agendaWeek,agendaDay'
    //    },
    //    defaultView: 'AgendaDay',
    //    editable: true,
    //    allDaySlot: false,
    //    selectable: true,
    //    slotMinutes: 15,
    //    events: [{
    //        id: 1,
    //        title: 'Event 1',
    //        start: '2014-02-02T14:30:00',
    //        end: '2014-02-02T22:30:00',
    //        color: 'red'
    //    }]
    //});
});