var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
  
    var listItem = '<li><strong>' + user + '</strong>: ' + message + '</li>';
    $('#chatContent').append(listItem);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    // lưu vao db
    JSHelper.AJAX_PUT('/Chat/Send', user,"", message)
        .success(function (response) {
            if (response.isSuccess) {
                cmd.parent().parent().parent().fadeOut();
                toastr.success($("#del_Success").val(), $("#noti").val())
            } else {
                toastr.error(response.Message)
            }
        });
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});