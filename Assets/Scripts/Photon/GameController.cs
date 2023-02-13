using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameController : MonoBehaviourPunCallbacks
{
    public Transform[] playerSlots;
    public Transform mimikSlot;
    public bool[] isTaken;
    public bool startGame;
    public PhotonView view;


    public GameObject cages, adminPanel;
    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (Input.GetButtonDown("StartGame"))
            {
                start();
            }
        }
    }
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            startGame = false;
            view = GetComponent<PhotonView>();
        }
        else
        {
            adminPanel.SetActive(false);
        }
    }
    public void start()
    {
        startGame = true;
        PhotonNetwork.Destroy(cages);
        adminPanel.SetActive(false);
    }
    public Transform getFreeSlot()
    {
        return playerSlots[PhotonNetwork.CurrentRoom.PlayerCount - 1];
    }
    private void OnPlayerConnected(Player player)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount >= 5)
            {
                PhotonNetwork.CloseConnection(player);
            }
        }
    }
}
