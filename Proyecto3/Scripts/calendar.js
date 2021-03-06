$(document).ready(function () {
    //metod para actualizar tareas
    function UpdateEvent(EventID, EventStart, EventEnd) {
        var dataRow = {
            'id': EventID,
            'start': EventStart,
            'end': EventEnd
        }
        $.ajax({

            type: 'POST',
            url: "@Url.Content("~/Tareas/EditarTareaAjax")",
            contentType: "application/json; charset=utf-8",
        dataType: 'json',

        data: JSON.stringify(dataRow),
        async: false,
        cache: false,
        success: function (data) {
            if (data == 1) {
                alert("Tarea Editada");
            } else {
                alert("Tarea no editada")
                revertFunc();
            }

        },

        error: function (XMLHttpRequest, textStatus, errorThrown) {
            revertFunc();
            console.log("Tarea no pudo ser editada")
        }
    });
}
    //


    $.ajax({
type: "GET",
url: "@Url.Content("~/Home/GetEvents")",
//data: formulario,
dataType: 'json',
success: function (data) {
    console.log(data)
    $("#showcalendar").fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        defaultView: 'month',
        editable: true,
        allDaySlot: true,
        selectable: true,
        slotMinutes: 15,
        eventClick: function (event) {
            alert("Tarea: " + event.title + " \nDescripcion: "
                + event.descripcion);
        },
        eventResize: function (event, dayDelta, minuteDelta, revertFunc) {
            alert(
                "La fecha de termino de " + event.title + " ha sido movida \n" +
                dayDelta + " dias y " +
                minuteDelta + " minutos."
            );

            if (confirm("is this okay?")) {
                //llama metodo actualizar
                UpdateEvent(event.id, event.start, event.end)
                //termina ajax
            } else {
                revertFunc();
            }

        },
        eventDrop: function (event, dayDelta, minuteDelta, revertFunc) {
            alert(
                "La fecha de termino de " + event.title + " ha sido movida \n" +
                dayDelta + " dias y " +
                minuteDelta + " minutos."
            );

            if (confirm("is this okay?")) {
                //llama metodo actualizar
                UpdateEvent(event.id, event.start, event.end)
                //termina ajax
            } else {
                revertFunc();
            }

        },
        events: data
    });
}
});
});