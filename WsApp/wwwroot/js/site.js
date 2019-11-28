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
        var btn = document.getElementById("shipPlacementMenu").hidden = true;
        var btn = document.getElementById("attackStrategyMenu").hidden = false;
        ChangeButtonColor('btnBasicAttack');
    });

    connection.on("pingArmorCount", function (count) {
        var text = 'Armor left: ' + count;
        document.getElementById("armorCount").innerText = text;
    });
    
    connection.on("changedTurn", function (direction) {
        var arrow = document.getElementById('turnArrow');
        if (direction == 'right') {
            arrow.className = 'right';
            arrow.style.transform = "rotate(-45deg)";            
        } else {
            arrow.className = 'left';
            arrow.style.transform = "rotate(135deg)";
        }
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

    connection.on("undoShip", function (row, col) {
        var cell = document.getElementById('grid1').rows[row].cells[col];
        cell.style.backgroundColor = "";
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

    connection.on("undoButtons", function (activeButton, removedButtons) {
        for (var i = 0; i < 5; i++) {
            var button = document.getElementById("button" + i);
            button.style.display = "inline";
            button.disabled = false;
        }
        for (var i = 0; i < removedButtons.length; i++) {
            var button = document.getElementById("button" + removedButtons[i]).style.display = "none";
        }
        for (var i = 0; i < 5; i++) {
            if (i != activeButton && !removedButtons.includes(i)) {
                var button = document.getElementById("button" + i).disabled = true;
            }
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
        document.getElementById("button" + buttonId).style.display = "none";
        var count = 0;
        for (var i = 0; i < 5; i++) {
            if (i != buttonId) {
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
        connection.invoke("SelectShipType", "Bombardier", "0");
    };
    document.getElementById('button1').onclick = function () {
        connection.invoke("SelectShipType", "Cruiser", "1");
    };
    document.getElementById('button2').onclick = function () {
        connection.invoke("SelectShipType", "Submarin", "2");
    };
    document.getElementById('button3').onclick = function () {
        connection.invoke("SelectShipType", "Kukuruznik", "3");
    };
    document.getElementById('button4').onclick = function () {
        connection.invoke("SelectShipType", "Schnicel", "4");
    };

    document.getElementById('btnUndo').onclick = function () {
        connection.invoke("CommandUndo");
    };

    document.getElementById('btnArmor').onclick = function () {
        connection.invoke("SelectArmor");
        var btn = document.getElementById("btnArmor");
        var color = btn.style.backgroundColor;
        if (color == "rgb(204, 163, 0)") {
            btn.style.backgroundColor = "#997a00";
            $('#grid1').addClass('armorGrid');
        } else {
            btn.style.backgroundColor = "#cca300";
            $('#grid1').removeClass('armorGrid');
        }
    };

    document.getElementById('btnBasicAttack').onclick = function () {
        connection.invoke("SelectStrategy", "Basic");
        ChangeButtonColor('btnBasicAttack');
    };
    document.getElementById('btnLaserAttack').onclick = function () {
        connection.invoke("SelectStrategy", "Laser");
        ChangeButtonColor('btnLaserAttack');
    };
    document.getElementById('btnBombAttack').onclick = function () {
        connection.invoke("SelectStrategy", "Bomb");
        ChangeButtonColor('btnBombAttack');
    };
    document.getElementById('btnCrossAttack').onclick = function () {
        connection.invoke("SelectStrategy", "Cross");
        ChangeButtonColor('btnCrossAttack');
    };

    function ChangeButtonColor(id) {
        var btn = document.getElementById(id);
        btn.style.backgroundColor = "#0000ff";
        ResetOtherButtonsColors(id);
    }

    function ResetOtherButtonsColors(id) {
        if (id != 'btnBasicAttack') {
            var btn = document.getElementById('btnBasicAttack');
            btn.style.backgroundColor = "#337ab7";
        }
        if (id != 'btnLaserAttack') {
            var btn = document.getElementById('btnLaserAttack');
            btn.style.backgroundColor = "#337ab7";
        }
        if (id != 'btnBombAttack') {
            var btn = document.getElementById('btnBombAttack');
            btn.style.backgroundColor = "#337ab7";
        }
        if (id != 'btnCrossAttack') {
            var btn = document.getElementById('btnCrossAttack');
            btn.style.backgroundColor = "#337ab7";
        }
    }

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