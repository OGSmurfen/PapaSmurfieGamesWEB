"use strict";

if (isAuthenticated) {
    const connection = new signalR.HubConnectionBuilder().withUrl("/socialPageUpdatesHub").build();

    connection.start().then(
        function () {
            console.log("social: socialPageUpdatesHub connection established");
        }).catch(function (err) {
            return console.error(err.toString());
        });
    /*This signal is sent directly from SocialController and is not a method of the SocialHub*/
    connection.on("ReceiveFriendRequestUpdate", function () {
        location.reload();
    });





    document.getElementById("createLobbyBtn").addEventListener("click", function (event) {
        event.preventDefault();

        createAndJoinGroup()
    });

   




}

