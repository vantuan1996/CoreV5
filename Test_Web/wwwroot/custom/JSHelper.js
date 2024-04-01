
var JSHelper = {

    AJAX_Delete: function (url, id) {
        return $.ajax({
            type: "POST",
            datatype: "json",
            data: { id: id },
            url: url
        });
    },
    AJAX_PUT: function (url, sender, receiver, message) {
        return $.ajax({
            type: "POST",
            datatype: "json",
            data: { sender: sender, receiver: receiver, message: message },
            url: url
        });
    },
    AJAX_SendRequest: function (url, model) {
        return $.ajax({
            type: "POST",
            datatype: "json",
            data: { model: model },
            url: url
        });
    },

    AJAX_SubmitAsync: function (url, model) {
        return $.ajax({
            type: "POST",
            datatype: "json",
            data: model,
            url: url,
            enctype: 'multipart/form-data',
            processData: false,
            contentType: false
        });
    },
}