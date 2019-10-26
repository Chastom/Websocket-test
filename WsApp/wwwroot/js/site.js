$(document).ready(function () {
    var connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5000/Play")
        .build();

    connection.on("UserConnected", function (socketId) {
        var selectionText = 'New user connected with id: [' + socketId + ']';
        $('#selections').append('<li>' + selectionText + '</li');
        console.log("prisijunge");
    });

    connection.on("pingCreatedPlayer", function (socketId, selection) {
        var selectionText = socketId + ' CREATED! ' + '  ' + selection;
        $('#test').append('<li>' + selectionText + '</li');
        console.log("iskviesta");
    });
    connection.on("PingFullDual", function (socketId) {
        var selectionText = socketId + ' Unlucky! Arena is full ';
        $('#test').append('<li>' + selectionText + '</li');
        console.log("Trecias nereikalingas :)");
    });
    connection.on("PingWaitingOponent", function (socketId) {
        var selectionText = socketId + ' Laukia priesininko ';
        $('#selections').append('<li>' + selectionText + '</li');
        console.log("Laukiama priesininko");
    });
    connection.on("PingGameIsStarted", function (socketId) {
        var selectionText = 'Zaidimas pradetas';
        $('#selections').append('<li>' + selectionText + '</li');
        console.log("Zaidimas pradetas");
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
        var messageText = 'id [' + userId + ']: ' + message;
        $('#selections').append('<li>' + messageText + '</li');
    });

    connection.on("pingShipPlaced", function (row, col) {
        var cell = document.getElementById('grid1').rows[row].cells[col];
        cell.style.backgroundColor = '#696969';
    });

    connection.on("invalidSelection", function (row, col) {
        var cell = document.getElementById('grid1').rows[row].cells[col];
        var previous = cell.style.backgroundColor;
        cell.setAttribute('style', 'background-color:#ffb3b3 !important');
        setTimeout(function () {
            cell.style.backgroundColor = previous;
        }, 300);
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
        for (var i = 0; i < 5; i++) {
            if (i != buttonId[6]) {
                var button = document.getElementById("button" + i).disabled = false;
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




    //var counter = 0;
    //var button = document.getElementById("button" + counter);
    //var types = ["Bombardier", "Cruiser", "Submarin", "Kukuruznik", "Schnicel"];
    //while (button) {
    //    button.addEventListener("click", function () {
    //        connection.invoke("SelectShipType", types[counter]);
    //    })
    //    button = document.getElementById("button" + (++counter));
    //}


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

    document.getElementById('ready').onclick = function () {
        connection.invoke("Ready");
    };

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
    //connection.clientMethods["pingCreatedPlayer"] = (socketId) => {
    //    var selectionText = socketId + ' CREATED! ';
    //    $('#selections').append('<li>' + selectionText + '</li');
    //}
    //connection.clientMethods["pingFullArena"] = (socketId) => {
    //    var selectionText = socketId + ' NEPRIDETA ARBA PILNA! ';
    //    $('#selections').append('<li>' + selectionText + '</li');
    //}
    //connection.clientMethods["pingSelectedShipType"] = (socketId, selection) => {
    //    var selectionText = socketId + ' selected: ' + selection;
    //    $('#selections').append('<li>' + selectionText + '</li');
    //}
    //connection.clientMethods["AddInformation"] = (socketId, selection) => {
    //    var selectionText = socketId + ' pridetas ';
    //    $('#selections').append('<li>' + selectionText + '</li');
    //}

    //connection.clientMethods["pingAttack"] = (socketId, row, col) => {
    //    var attackText = socketId + ' attacked: [' + row + ' ' + col + ']';
    //    $('#attacks').append('<li>' + attackText + '</li');
    //    console.log(attackText);
    //    if (socketId != connection.connectionId) {
    //        var cell = document.getElementById('grid1').rows[row].cells[col];
    //        cell.style.backgroundColor = '#66a3ff';
    //    }
    //}


    //======================================================



    //$("#Kukuruznik").click(function (event) {
    //    connection.invoke("SelectShipType", connection.connectionId, "Kukurunzik")
    //});
    //$('#grid1').click(function (event) {
    //    var target = $(event.target);
    //    $td = target.closest('td');
    //    $td.attr("bgcolor", "dimgrey");
    //    //$td.html('X');
    //    //paspausto langelio koordintes
    //    var col = $td.index();
    //    var row = $td.closest('tr').index();
    //    connection.invoke("MetodasKurisPadedaLaiva", connection.connectionId, row.toString(), col.toString())
    //});

    //$('#grid2').click(function (event) {
    //    var target = $(event.target);
    //    $td = target.closest('td');
    //    $td.attr("bgcolor", "#ff6666");
    //    var col = $td.index();
    //    var row = $td.closest('tr').index();
    //    connection.invoke("SendAttack", connection.connectionId, row.toString(), col.toString());
    //});

    //var $selectioncontent = $('#selection-content');
    //$selectioncontent.keyup(function (e) {
    //    if (e.keyCode == 13) {
    //        var selection = $selectioncontent.val().trim();
    //        if (selection.length == 0) {
    //            return false;
    //        }
    //        connection.invoke("SendSelection", connection.connectionId, selection);
    //        $selectioncontent.val('');
    //    }
    //});

});