"use strict";

$(document).ready(
    function () {
        const connection = new signalR.HubConnectionBuilder().withUrl("/socialHub").build();

        connection.start().then(
            function () {
                console.log("SocialHub established");
            }).catch(function (err) {
                return console.error(err.toString());
            });

            /*This signal is sent directly from SocialController and is not a method of the SocialHub*/
        connection.on("ReceiveFriendRequestUpdate", function () {
            location.reload();
        });


    }
    
);