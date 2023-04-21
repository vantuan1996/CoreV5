
var JSHelper = {

    AJAX_Delete: function (url, id) {
        return $.ajax({
            type: "POST",
            datatype: "json",
            data: { id: id },
            url: url
        });
    },

}