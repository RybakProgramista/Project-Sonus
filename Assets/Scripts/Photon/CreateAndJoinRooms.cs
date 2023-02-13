using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField joinInput, createInput, nicknameField;
    public Dropdown characterSelect;
    public GameObject playerInfo;
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
        DontDestroyOnLoad(gameObject);
    }
    public override void OnJoinedRoom()
    {
        DontDestroyOnLoad(playerInfo);
        playerInfo.GetComponent<PlayerInfo>().nickname = "";
        playerInfo.GetComponent<PlayerInfo>().character = characterSelect.value;
            
        PhotonNetwork.LoadLevel("Game");
    }
}
