"use strict";

console.log("Status: Is Authenticated:", isAuthenticated);

if (isAuthenticated) {
    //$(document).ready(
    //    function () {
    const connection = new signalR.HubConnectionBuilder().withUrl("/statusHub").build();

    connection.start().then(
        function () {
            console.log("StatusHub connection established");
        }).catch(function (err) {
            return console.error(err.toString());
        });




    //    });

   


} else {
    console.log("User is not authenticated, SocialHub connection will not be started.");
}

