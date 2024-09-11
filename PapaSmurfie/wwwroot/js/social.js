"use strict";

console.log("social: Is Authenticated:", isAuthenticated);

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

    window.createAndJoinGroup = function() {
        connection.invoke("CreateAndJoinGroup").catch(function (err) {
            return console.error("Error creating/joining group:", err.toString());
        });
    }
    


   
    

} else {
    console.log("User is not authenticated, SocialHub connection will not be started.");
}

