// Set the classes that TableTools uses to something suitable for Bootstrap
$.extend(true, $.fn.DataTable.TableTools.classes, {
    "container": "btn-group",
    "buttons": {
        "normal": "btn",
        "disabled": "btn disabled"
    },
    "collection": {
        "container": "DTTT_dropdown dropdown-menu",
        "buttons": {
            "normal": "",
            "disabled": "disabled"
        }
    }
});

// Have the collection use a bootstrap compatible dropdown
$.extend(true, $.fn.DataTable.TableTools.DEFAULTS.oTags, {
    "collection": {
        "container": "ul",
        "button": "li",
        "liner": "a"
    }
});