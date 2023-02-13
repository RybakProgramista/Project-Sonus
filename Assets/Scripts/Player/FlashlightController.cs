using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FlashlightController : MonoBehaviour
{
    public GameObject flashlightPoint;
    public Animation flashlightAnim;
    public AnimationClip[] animations;
    private PhotonView view;
    public float rotSpeed, posSpeed;
    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            transform.position = flashlightPoint.transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, flashlightPoint.transform.rotation, rotSpeed * Time.deltaTime);
            int random = Random.Range(1, 500);
            if(random == 25 && !flashlightAnim.isPlaying)
            {
                flashlightAnim.clip = animations[0];
                flashlightAnim.Play();
            }
            else if(random == 50 && !flashlightAnim.isPlaying)
            {
                flashlightAnim.clip = animations[1];
                flashlightAnim.Play();
            }
        }
    }
}
