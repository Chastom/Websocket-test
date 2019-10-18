$(document).ready(function () {
    var connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5000/Play")
        .build();    
    var BombardierCount = 0;
    var CruiserCount = 0;
    var SubmarinCount = 0;
    var KukuruznikCount = 0;
    var SchnicelCount = 0;
    var selectedShipType = null;
    var selectedSize = -1;
    //Laikinai socketID 
    var SOCKETID = null;
    connection.on("UserConnected", function (socketId) {
        var selectionText = 'New user connected with id: [' + socketId + ']';
        $('#selections').append('<li>' + selectionText + '</li');
        SOCKETID = socketId;
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
    //meta errora kazkoki bet lyg pazymi (kazkas su anonymous susije)
    connection.on("pingSelectedShipType", function (socketId, selection, size) {
        var selectionText = socketId + ' Selected ' + '  ' + selection;
        $('#test').append('<li>' + selectionText + '</li');
        selectedShipType = selection;
        selectedSize = size;
        console.log("pasirinko " + selected + "KurioDydis " + selectedSize);
        
    });

    connection.on("pingDisableType", function (socketId, selection, size) {
        //ideally (in a perfect world) reiktu naudot Groups. . .
        //https://www.youtube.com/watch?v=Kl3H4vMqYNo
        var selectionText = socketId + ' Selected ' + '  ' + selection;
        $('#test').append('<li>' + selectionText + '</li');
        selectedShipType = selection;
        selectedSize = size;
        console.log("pasirinko " + selectedShipType + "KurioDydis " + selectedSize);
        connection.invoke('getConnectionId')
            .then(function (connectionId) {
                console.log("disable: " + selection);
                
                if (socketId == connectionId) { //jei daug kur reiks might as well i sesija isimest ta id
                    //jeigu disablint:
                    //document.getElementById(buttonId).disabled = true;
                    //jeigu nematomu padaryt, kad isvis neliktu
                    
                    document.getElementById(selection).style.display = "none";
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
    //$("#typeE").click(function (event) {
    //    connection.invoke("SelectShipType", "typeE")
    //});
    document.getElementById('Bombardier').onclick = function () {
        BombardierCount++;
        connection.invoke("SelectShipType", "Bombardier", BombardierCount);
    };
    document.getElementById('Cruiser').onclick = function () {
        CruiserCount++;
        connection.invoke("SelectShipType", "Cruiser", CruiserCount);
    };
    document.getElementById('Submarin').onclick = function () {
        SubmarinCount++;
        connection.invoke("SelectShipType", "Submarin", SubmarinCount);
    };
    document.getElementById('Kukuruznik').onclick = function () {
        KukuruznikCount++;
        connection.invoke("SelectShipType", "Kukuruznik", KukuruznikCount);
    };
    document.getElementById('Schnicel').onclick = function () {
        SchnicelCount++;
        connection.invoke("SelectShipType", "Schnicel", SchnicelCount);
    };
    document.getElementById('Ready').onclick = function () {
        connection.invoke("Ready", SOCKETID);
    };
    //nepaduoda socketId
    $('#grid1').click(function (event) {
        if (selectedShipType != null && selectedSize != 0) {
           
                var target = $(event.target);
                $td = target.closest('td');
                $td.attr("bgcolor", "dimgrey");

                //$td.html('X');
                //paspausto langelio koordintes
                var col = $td.index();
                var row = $td.closest('tr').index();
                selectedSize = selectedSize - 1;
            console.log("Padejo " + selectedShipType + " Laiva ");
            connection.invoke("MetodasKurisPadedaLaiva", SOCKETID, row.toString(), col.toString(), selectedShipType)
            
            
        }
        
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