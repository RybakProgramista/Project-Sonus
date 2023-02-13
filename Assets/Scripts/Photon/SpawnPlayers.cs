using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Rendering;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject flashlightPrefab, flashlightManagerPrefab, mimikLightPrefab;
    public GameObject[] playerPrefabs;
    public PlayerInfo playerInfo;
    public Volume globalProfile;
    public VolumeProfile[] profiles;

    public GameController gameController;
    private void Start()
    {
        playerInfo = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>();
        if (PhotonNetwork.IsMasterClient)
        {
            playerInfo.character = 1;
        }
        else
        {
            playerInfo.character = 0;
        }
        Vector3 spawnPos;

        globalProfile.profile = profiles[playerInfo.character];
        if (playerInfo.character == 0)
        {
            spawnPos = gameController.getFreeSlot().position;
        }
        else
        {
            spawnPos = gameController.mimikSlot.position;
        }

        GameObject player = PhotonNetwork.Instantiate(playerPrefabs[playerInfo.character].name, spawnPos, Quaternion.identity);
        if (playerInfo.character == 0)
        {
            GameObject flashlightManager = PhotonNetwork.Instantiate(flashlightManagerPrefab.name, player.transform.position, Quaternion.identity);
            player.transform.Find("Rig").Find("FlashlightFollowRig").Find("FlashlightFollowRig_target").GetComponent<TargetProblemSolver>().realTarget = flashlightManager.transform;
            flashlightManager.GetComponent<FlashlightController>().flashlightPoint = player.transform.Find("Main Camera").Find("FlashlightPoint").gameObject;
            flashlightManager.GetComponent<FlashlightController>().flashlightAnim = player.transform.Find("Model").Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm").Find("mixamorig:RightHand").Find("Flashlight").GetComponent<Animation>();
        }
        else
        {
            GameObject flashlight = Instantiate(mimikLightPrefab, player.transform);
            flashlight.transform.SetParent(player.transform);
        }
    }
}
