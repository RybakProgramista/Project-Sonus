using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject flashlightPrefab, flashlightManagerPrefab;
    public GameObject[] playerPrefabs;
    public PlayerInfo playerInfo;
    private void Start()
    {
        playerInfo = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>();

        GameObject player = PhotonNetwork.Instantiate(playerPrefabs[playerInfo.character].name, getPosition(), Quaternion.identity);
        GameObject flashlightManager = PhotonNetwork.Instantiate(flashlightManagerPrefab.name, getPosition(), Quaternion.identity);
        player.transform.Find("Rig").Find("FlashlightFollowRig").Find("FlashlightFollowRig_target").GetComponent<TargetProblemSolver>().realTarget = flashlightManager.transform;
        flashlightManager.GetComponent<FlashlightController>().flashlightPoint = player.transform.Find("Main Camera").Find("FlashlightPoint").gameObject;
        flashlightManager.GetComponent<FlashlightController>().flashlightAnim = player.transform.Find("Model").Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm").Find("mixamorig:RightHand").Find("Flashlight").GetComponent<Animation>();
       
    }
    Vector3 getPosition()
    {
        return new Vector3(0f, 1f, 0f);
    }
}
