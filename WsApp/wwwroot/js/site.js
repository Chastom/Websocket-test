$(document).ready(function () {
    var connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5000/Play")
        .build();

    connection.on("UserConnected", function (socketId) {
        var selectionText = 'New user connected with id: [' + socketId + ']';
        $('#selections').append('<li>' + selectionText + '</li');
        console.log("prisijunge");
    });

    connection.on("pingGameStarted", function () {
        var btn = document.getElementById("ready");
        btn.innerText = "The game has started!";
    });

    connection.on("pingArmorCount", function (count) {
        var text = 'Armor left: ' + count;
        document.getElementById("armorCount").innerText = text;
    });



    connection.on("invalidTurn", function (row, col) {
        var cell = document.getElementById('grid2').rows[row].cells[col];
        var previous = cell.style.backgroundColor;
        cell.setAttribute('style', 'background-color:#ffb3b3 !important');
        setTimeout(function () {
            cell.style.backgroundColor = previous;
        }, 300);
        var selectionText = 'Ne jusu ejimas';
        $('#selections').append('<li>' + selectionText + '</li');
        console.log("Ne jusu ejimas");
    });
    connection.on("pingMessage", function (userId, message) {
        var messageText = message;
        if (userId != null) {
            messageText = 'id [' + userId + ']: ' + message;
        }
        $('#selections').append('<li>' + messageText + '</li');
    });

    connection.on("pingShipPlaced", function (row, col, armored) {
        var armorColor = "#cca300";
        var shipColor = '#696969';
        var cell = document.getElementById('grid1').rows[row].cells[col];
        if (armored) {
            cell.style.backgroundColor = armorColor;
        } else {
            cell.style.backgroundColor = shipColor;
        }
    });

    connection.on("invalidSelection", function (row, col) {
        var cell = document.getElementById('grid1').rows[row].cells[col];
        var previous = cell.style.backgroundColor;
        cell.setAttribute('style', 'background-color:#ffb3b3 !important');
        setTimeout(function () {
            cell.style.backgroundColor = previous;
        }, 300);
    });

    connection.on("pingAttack", function (row, col, attackOutcome, isAttacker) {
        var tableId = isAttacker == true ? 'grid2' : 'grid1';
        var cell = document.getElementById(tableId).rows[row].cells[col];
        if (attackOutcome == 'Hit') {
            cell.style.backgroundColor = '#ff6666';
        } else if (attackOutcome == 'Armor') {
            cell.style.backgroundColor = '#696969';
        } else {
            cell.style.backgroundColor = '#386C99';
        }
    });

    connection.on("pingDisable", function (buttonId) {
        for (var i = 0; i < 5; i++) {
            if (i != buttonId) {
                var button = document.getElementById("button" + i).disabled = true;
            }
        }
    });

    connection.on("pingRemove", function (buttonId) {
        document.getElementById(buttonId).style.display = "none";
        var count = 0;
        for (var i = 0; i < 5; i++) {
            if (i != buttonId[6]) {
                var button = document.getElementById("button" + i).disabled = false;
            }
            if (document.getElementById("button" + i).style.display == "none") {
                count++;
            }
            //checking if all ships were selected
            if (count == 5) {
                document.getElementById("ready").disabled = false;
            }
        }
    });

    connection.start().catch(function (err) {
        return console.error(err.toString());
    });

    var $selectioncontent = $('#selection-content');
    $selectioncontent.keyup(function (e) {
        if (e.keyCode == 13) {
            var message = $selectioncontent.val().trim();
            if (message.length == 0) {
                return false;
            }
            connection.invoke("SendMessage", message);
            $selectioncontent.val('');
        }
    });

    document.getElementById('button0').onclick = function () {
        connection.invoke("SelectShipType", "Bombardier", "button0");
    };
    document.getElementById('button1').onclick = function () {
        connection.invoke("SelectShipType", "Cruiser", "button1");
    };
    document.getElementById('button2').onclick = function () {
        connection.invoke("SelectShipType", "Submarin", "button2");
    };
    document.getElementById('button3').onclick = function () {
        connection.invoke("SelectShipType", "Kukuruznik", "button3");
    };
    document.getElementById('button4').onclick = function () {
        connection.invoke("SelectShipType", "Schnicel", "button4");
    };

    document.getElementById('btnArmor').onclick = function () {
        connection.invoke("SelectArmor");
        var btn = document.getElementById("btnArmor");
        var color = btn.style.backgroundColor;
        if (color == "rgb(204, 163, 0)") {
            btn.style.backgroundColor = "#997a00";
        } else {
            btn.style.backgroundColor = "#cca300";
        }
    };

    document.getElementById('ready').onclick = function () {
        connection.invoke("ReadySingleton");
        var btn = document.getElementById("ready");
        btn.innerText = "Searching for opponent...";
        btn.disabled = true;
    };

    //========== can be removed later ===========
    //for testing purposes adding a button to start the game without placing all the ships beforehand
    document.getElementById('buttonTesting').onclick = function () {
        document.getElementById("ready").disabled = false;
    };

    document.getElementById('buttonTesting2').onclick = function () {
        connection.invoke("DeleteDuels");
    };
    //========== can be removed later ===========

    $('#grid1').click(function (event) {
        var target = $(event.target);
        $td = target.closest('td');
        var col = $td.index();
        var row = $td.closest('tr').index();
        connection.invoke("PlaceShipValidation", row.toString(), col.toString())
    });
    $('#grid2').click(function (event) {
        var target = $(event.target);
        $td = target.closest('td');
        var col = $td.index();
        var row = $td.closest('tr').index();
        connection.invoke("Atack", row.toString(), col.toString())
    });
});