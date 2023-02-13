using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    public CameraController cameraController;
    public PlayerMovement playerMovement;
    public Camera camera;

    public PhotonView view;

    private Text showedText;
    private GameObject showedTextBg;

    private bool actionPerforming, isDead;

    public AudioSource audioSource, deathAudio;
    public AudioClip[] clips;

    //Reading text
    private float timer;
    private bool isCounting;
    public float maxDistance, maxDisplayTime;
    void Start()
    {
        view = GetComponent<PhotonView>();
        if (view.IsMine)
        {
            isDead = false;
            showedText = GameObject.Find("showedText").GetComponent<Text>();
            showedTextBg = GameObject.Find("textShowedBg");
            showedTextBg.SetActive(false);
        }
    }

    void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetAxis("Fire1") == 0)
            {
                actionPerforming = false;
            }

            if (Input.GetAxis("Fire1") == 1 && !actionPerforming && !isCounting)
            {
                actionPerforming = true;
                RaycastHit hit;
                if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, maxDistance))
                {
                    Debug.Log(hit.transform.tag);
                    if (hit.transform.GetComponent<TextToDisplay>())
                    {
                        showedText.text = hit.transform.GetComponent<TextToDisplay>().text;
                        audioSource.clip = clips[0];
                        audioSource.Play();
                        showedTextBg.SetActive(true);
                        isCounting = true;
                        playerMovement.canMove = false;
                        timer = 0;
                    }
                }
            }
            if (isCounting)
            {
                timer += Time.deltaTime;
                if (timer > maxDisplayTime)
                {
                    isCounting = false;
                    showedTextBg.SetActive(false);
                    playerMovement.canMove = true;
                }
            }
            if(isDead && !deathAudio.isPlaying)
            {
                PhotonNetwork.Disconnect();
            }
        }
    }
    [PunRPC]
    public void death()
    {
        if (view.IsMine)
        {
            cameraController.canLook = false;
            playerMovement.canMove = false;
            isDead = true;

            camera.transform.LookAt(GameObject.FindGameObjectWithTag("Mimik").transform);
            
        }
    }
}
