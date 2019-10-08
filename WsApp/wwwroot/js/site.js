$(document).ready(function () {
    var connection = new WebSocketManager.Connection("ws://localhost:5000/Play");
    connection.enableLogging = true;

    connection.connectionMethods.onConnected = () => {

    }

    connection.connectionMethods.onDisconnected = () => {

    }

    connection.clientMethods["pingSelection"] = (socketId, selection) => {
        var selectionText = socketId + ' selected: ' + selection;
        $('#selections').append('<li>' + selectionText + '</li');
    }

    connection.start();

    var $selectioncontent = $('#selection-content');
    $selectioncontent.keyup(function (e) {
        if (e.keyCode == 13) {
            var selection = $selectioncontent.val().trim();
            if (selection.length == 0) {
                return false;
            }
            connection.invoke("SendSelection", connection.connectionId, selection);
            $selectioncontent.val('');
        }
    });

});