"use strict";

console.log("lobby.js: Is Authenticated:", isAuthenticated);

if (isAuthenticated) {

    const connection = new signalR.HubConnectionBuilder().withUrl("/socialGroupsHub").build();

    connection.start().then(
        function () {
            console.log("social: SocialGroupsHub connection established");
        }).catch(function (err) {
            return console.error(err.toString());
        });

   
    // SignalR event handler for group messages
    connection.on("ReceiveGroupMessage", function (message) {
        console.log("Group message received: " + message);
    });

    
    

    document.getElementById("createLobbyBtn").addEventListener("click", function (event) {
        event.preventDefault();

        createAndJoinLobby()
    });
   






    function createAndJoinLobby() {
        connection.invoke("CreateAndJoinLobby").catch(function (err) {
            return console.error("Error creating/joining lobby:", err.toString());
        });
    }

} else {
    console.log("User is not authenticated, Lobby connection will not be started.");
}

