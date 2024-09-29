using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    public void CreateRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.CreateRoom(createInput.text);
        }
        else
        {
            Debug.LogError("Not connected to Master Server. Ensure OnConnectedToMaster or OnJoinedLobby is called before CreateRoom.");
        }
    }

    public void JoinRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinRoom(joinInput.text);
        }
        else
        {
            Debug.LogError("Not connected to Master Server. Ensure OnConnectedToMaster or OnJoinedLobby is called before JoinRoom.");
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("CitySimulator 1");
    }
}
