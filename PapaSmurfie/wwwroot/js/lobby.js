"use strict";

console.log("lobby.js: Is Authenticated:", isAuthenticated);

if (isAuthenticated) {

    const connection = new signalR.HubConnectionBuilder().withUrl("/socialGroupsHub").build();

    connection.start().then(
        function () {
            console.log("social: SocialGroupsHub connection established");
            document.getElementById("sendLobbyMessageButton").disabled = false;
            document.getElementById("joinLobbyBtn").disabled = false;
        }).catch(function (err) {
            return console.error(err.toString());
        });

   
    // SignalR event handler for Receiving group messages
    connection.on("GroupChat", function (message) {
        var li = document.createElement("li");
        document.getElementById("lobbyMessagesList").appendChild(li);
        // We can assign user-supplied strings to an element's textContent because it
        // is not interpreted as markup. If you're assigning in any other way, you 
        // should be aware of possible script injection concerns.
        li.innerHTML = `${message}`;
    });

    connection.on("Joined", function (lobbyId) {
        console.log("Joined lobby with ID:", lobbyId);

        // Find the <p> element and update its text
        var lobbyParagraph = document.getElementById("lobbyParagraph");
        if (lobbyParagraph) {
            lobbyParagraph.innerText = `${lobbyId}`;
        } else {
            console.error("lobbyParagraph element not found.");
        }
    });
    connection.on("Disconnected", function (lobbyId) {
        console.log("Disconnected lobby with ID:", lobbyId);

        // Find the <p> element and update its text
        var lobbyParagraph = document.getElementById("lobbyParagraph");
        if (lobbyParagraph) {
            //lobbyParagraph.innerText = `Currently not in a lobby`;
        } else {
            console.error("lobbyParagraph element not found.");
        }
    });

    // Creating a lobby
    document.getElementById("createLobbyBtn").addEventListener("click", function (event) {
        event.preventDefault();

        createAndJoinLobby()

        document.getElementById("joinLobbyBtn").disabled = true;
    });

    // Send Group(lobby) Messages Logic
    var sendLobbyMessageButton = document.getElementById("sendLobbyMessageButton");
    var lobbyMessageInput = document.getElementById("lobbyMessageInput");

    function sendMessage() {
        var message = lobbyMessageInput.value;
        if (message) {
            connection.invoke("SendMessageToGroup", message).catch(function (err) {
                return console.error(err.toString());
            }).then(function () {
                lobbyMessageInput.value = "";
            }).catch(function (err) {
                console.error(err.toString());
            });

        }
    }
    lobbyMessageInput.addEventListener("keydown", function (event) {
        if (event.key === "Enter") {
            event.preventDefault();
            sendMessage();
        }
    });

    if (sendLobbyMessageButton) {
        sendLobbyMessageButton.addEventListener("click", function (event) {
            event.preventDefault();
            sendMessage();
        });
    }

    

    // Join lobby logic
    var joinLobbyBtn = document.getElementById("joinLobbyBtn");
    var lobbyNumberText = document.getElementById("lobbyNumberText");
    if (joinLobbyBtn) {
        if (lobbyNumberText) {

            joinLobbyBtn.addEventListener("click", function (event) {
                var lobbyId = lobbyNumberText.value;

                connection.invoke("JoinLobby", lobbyId).catch(function (err) {
                    return console.error(err.toString());
                });
                event.preventDefault();
            });

        } else {
            console.log("lobbyNumberText not found");
        }
    } else {
        console.log("JoinLobbyBtn not found");
    }

    // Unload called on browser window close
    window.addEventListener("unload", function (event) {
        leaveLobby();
    });


    function leaveLobby() {
        connection.invoke("DisconnectFromLobby").catch(function (err) {
            return console.erro("Error disconnecting from lobby", err.toString());
        });
    }

    function createAndJoinLobby() {
        connection.invoke("CreateAndJoinLobby").catch(function (err) {
            return console.error("Error creating/joining lobby:", err.toString());
        });
    }

} else {
    console.log("User is not authenticated, Lobby connection will not be started.");
}

