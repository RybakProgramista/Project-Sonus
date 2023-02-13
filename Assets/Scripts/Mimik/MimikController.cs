using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MimikController : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public CameraController cameraController;

    public AudioSource audioSource;
    public AudioClip[] clips;

    public Animator killAnim;

    private PhotonView view;
    public bool isAttacking;
    public float attackDistance, attackTime;
    public Camera camera;
    private void Start()
    {
        view = GetComponent<PhotonView>();
        if (view.IsMine)
        {
            GameObject.Find("textShowedBg").SetActive(false);
        }
    }
    private void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetAxis("Fire1") == 1 && !isAttacking)
            {
                isAttacking = true;

                Ray ray = new Ray(camera.transform.position, camera.transform.TransformDirection(Vector3.forward));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, attackDistance))
                {
                    
                    if (hit.transform.CompareTag("Player"))
                    {
                        playerMovement.canMove = false;
                        cameraController.canLook = false;
                        killAnim.SetTrigger("attack");
                        hit.transform.GetComponent<PlayerController>().view.RPC("death", RpcTarget.AllBuffered);
                    }
                }
            }
            if (!killAnim.GetCurrentAnimatorStateInfo(0).IsName("mimikAttack"))
            {
                playerMovement.canMove = true;
                cameraController.canLook = true;
            }
            if(Input.GetAxis("Fire1") == 0)
            {
                isAttacking = false;
            }
        }
    }
}
