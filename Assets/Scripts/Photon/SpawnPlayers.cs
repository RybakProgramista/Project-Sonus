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
    private void Start()
    {
        playerInfo = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>();
        globalProfile.profile = profiles[playerInfo.character];
        GameObject player = PhotonNetwork.Instantiate(playerPrefabs[playerInfo.character].name, getPosition(), Quaternion.identity);
        if(playerInfo.character == 0)
        {
            GameObject flashlightManager = PhotonNetwork.Instantiate(flashlightManagerPrefab.name, getPosition(), Quaternion.identity);
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
    Vector3 getPosition()
    {
        if(playerInfo.character == 0)
        {
            return new Vector3(0f, 1f, 0f);
        }
        else
        {
            return new Vector3(0f, -0.5f, 0f);
        }
    }
}
