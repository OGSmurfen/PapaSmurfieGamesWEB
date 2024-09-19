"use strict";

console.log("Chat: Is Authenticated:", isAuthenticated);

if (isAuthenticated) {
    var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

    //Disable the send button until connection is established.
    document.getElementById("sendButton").disabled = true;

    connection.on("ReceiveMessage", function (user, message) {
        var li = document.createElement("li");
        document.getElementById("messagesList").appendChild(li);
        // We can assign user-supplied strings to an element's textContent because it
        // is not interpreted as markup. If you're assigning in any other way, you 
        // should be aware of possible script injection concerns.
        li.innerHTML = `<strong>${user}:</strong> ${message}`;
    });
    connection.start().then(function () {
        document.getElementById("sendButton").disabled = false;
    }).catch(function (err) {
        return console.error(err.toString());
    });

    var userInput = document.getElementById("userInput");
    var messageInput = document.getElementById("messageInput");
    function sendMessage() {
        var user = userInput.value;
        var message = messageInput.value;
        connection.invoke("SendMessage", user, message).catch(function (err) {
            return console.error(err.toString());
        }).then(function () {
            messageInput.value = "";
        }).catch(function (err) {
            console.error(err.toString());
        });
    }
    messageInput.addEventListener("keydown", function (event) {
        if (event.key === "Enter") {
            event.preventDefault();
            sendMessage();
        }
    });

    document.getElementById("sendButton").addEventListener("click", function (event) {
        event.preventDefault();
        sendMessage();
    });
} else {
    console.log("User is not authenticated, ChatHub connection will not be started.");
}
