"use strict";

console.log("Social: Is Authenticated:", isAuthenticated);

if (isAuthenticated) {
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
            connection.on("ReceiveFriendStatusUpdate", function () {
                updateFriendStatuses();
            });


        }

    );
} else {
    console.log("User is not authenticated, SocialHub connection will not be started.");
}

function updateFriendStatuses() {
    // Implement logic to update friend statuses on the page
    // This could involve making an AJAX call to get the latest statuses and updating the UI
}