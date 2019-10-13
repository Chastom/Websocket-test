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

    connection.on("pingMessage", function (userId, message) {
        var messageText = 'id [' + userId + ']: ' + message;
        $('#selections').append('<li>' + messageText + '</li');
    });

    connection.on("pingDisableType", function (socketId, buttonId) {
        //ideally (in a perfect world) reiktu naudot Groups. . .
        //https://www.youtube.com/watch?v=Kl3H4vMqYNo
        connection.invoke('getConnectionId')
            .then(function (connectionId) {
                console.log("disable: " + buttonId);
                if (socketId == connectionId) { //jei daug kur reiks might as well i sesija isimest ta id
                    //jeigu disablint:
                    //document.getElementById(buttonId).disabled = true;
                    //jeigu nematomu padaryt, kad isvis neliktu
                    document.getElementById(buttonId).style.display = "none";
                }                
            }).catch(err => console.error(err.toString()));;        
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


    //dinamiskai reiktu padaryt kad visu onclick toks pat tik i invoke argumentus paduoda id tarkim kaip type
    document.getElementById('typeA').onclick = function () {
        connection.invoke("SelectShipType", "typeA");
    };
    document.getElementById('typeB').onclick = function () {
        connection.invoke("SelectShipType", "typeB");
    };
    document.getElementById('typeC').onclick = function () {
        connection.invoke("SelectShipType", "typeC");
    };
    document.getElementById('typeD').onclick = function () {
        connection.invoke("SelectShipType", "typeD");
    };
    document.getElementById('typeE').onclick = function () {
        connection.invoke("SelectShipType", "typeE");
    };


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