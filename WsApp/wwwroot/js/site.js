﻿$(document).ready(function () {
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

    connection.clientMethods["pingAttack"] = (socketId, row, col) => {
        var attackText = socketId + ' attacked: [' + row + ' ' + col + ']';
        $('#attacks').append('<li>' + attackText + '</li');
        console.log(attackText);
        if (socketId != connection.connectionId) {
            var cell = document.getElementById('grid1').rows[row].cells[col];
            cell.style.backgroundColor = '#66a3ff';
        }        
    }

    connection.start();

    $('#grid1').click(function (event) {
        var target = $(event.target);
        $td = target.closest('td');
        $td.attr("bgcolor", "dimgrey");
        //$td.html('X');
        //paspausto langelio koordintes
        var col = $td.index();
        var row = $td.closest('tr').index();
    });

    $('#grid2').click(function (event) {
        var target = $(event.target);
        $td = target.closest('td');
        $td.attr("bgcolor", "#ff6666");
        var col = $td.index();
        var row = $td.closest('tr').index();
        connection.invoke("SendAttack", connection.connectionId, row.toString(), col.toString());
    });

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