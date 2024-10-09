using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class LobbyCode : MonoBehaviourPunCallbacks
{
    public InputField playerName;
    public Text otherPlayerNamesTextField;
    public List<string> playerNames = new List<string>();

    private string roomCode;
    public bool isMultiplayer = true;
    public TMP_InputField roomCodeInputField; // Reference to the input field for room code
    public Text pingText; // Text UI to display ping
    public Transform spawnPositionTransform;
    void Awake()
    {
        roomCode = "TestRoom"; // Default room code
    }
    
    public void IsMultiplayer()
    {
        isMultiplayer = true;
    }

    public void ConnectToPhoton()
    {
        Debug.Log("Connecting to Photon...");
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "cae"; // Set to Canada East or another region
        PhotonNetwork.ConnectUsingSettings(); // Connect to Photon
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server: " + PhotonNetwork.CloudRegion);
        PhotonNetwork.JoinLobby(); // Join the lobby to get room list updates
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }

    public void SetRoomCode()
    {
        roomCode = roomCodeInputField.text; // Get the room code from the input field
    }

    public void TryJoinOrCreateRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady) // Check if the client is connected and ready
        {
            PhotonNetwork.NickName = playerName.text; // Use the input field's text for the nickname

            RoomOptions roomOptions = new RoomOptions
            {
                MaxPlayers = 6,
                IsOpen = true, // Open for new players
                // Uncomment the following line if you use a dropdown to set visibility:
                // IsVisible = roomVisibilityDropdown.value == 0
            };

            PhotonNetwork.JoinOrCreateRoom(roomCode, roomOptions, TypedLobby.Default); // Try to join or create a room
            Debug.Log("Trying to join or create room: " + roomCode);
        }
        else
        {
            Debug.LogError("Not connected to Master Server. Current State: " + PhotonNetwork.NetworkClientState);
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name + " with ActorNumber: " + PhotonNetwork.LocalPlayer.ActorNumber);

        // Display the ping after joining the room
        int ping = PhotonNetwork.GetPing();
        pingText.text = "Ping: " + ping + " ms";
        Debug.Log("Ping: " + ping + " ms");

        // Clear the player list display
        otherPlayerNamesTextField.text = "";

        foreach (var player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            if (player.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
            {
                string otherPlayerName = player.NickName;
                playerNames.Add(otherPlayerName);
                otherPlayerNamesTextField.text += otherPlayerName + "\n"; // Display the other player's name
            }
        }

        if (PhotonNetwork.IsConnected)
        {
            Vector3 spawnPosition = spawnPositionTransform.position;
            // Instantiate the player prefab from the Resources folder
            PhotonNetwork.Instantiate("Network Player", spawnPosition, Quaternion.identity, 0);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player joined: " + newPlayer.NickName + " (ActorNumber: " + newPlayer.ActorNumber + ")");
        playerNames.Add(newPlayer.NickName);
        otherPlayerNamesTextField.text += newPlayer.NickName + "\n"; // Update the player list when a new player joins
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player left: " + otherPlayer.NickName);
        playerNames.Remove(otherPlayer.NickName);
        otherPlayerNamesTextField.text = otherPlayerNamesTextField.text.Replace(otherPlayer.NickName + "\n", ""); // Remove the player's name from the display
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError("Disconnected from Photon: " + cause.ToString());
    }

    // Optional: You can add this coroutine to periodically check the ping
    IEnumerator UpdatePingRoutine()
    {
        while (true)
        {
            if (PhotonNetwork.IsConnected)
            {
                pingText.text = "Ping: " + PhotonNetwork.GetPing() + " ms";
            }
            yield return new WaitForSeconds(5f); // Update ping every 5 seconds
        }
    }
}
