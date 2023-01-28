using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MimikController : MonoBehaviour
{
    private PhotonView view;
    public bool isAttacking, hitConfirmed;
    public float attackDistance, attackTime;
    private float t;
    public Vector3 hitPos;
    public Camera camera;
    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (view.IsMine)
        {
            if (hitConfirmed)
            {
                if(t >= attackTime)
                {
                    hitConfirmed = false;
                    t = 0;
                }
                else
                {
                    t += Time.deltaTime;
                }
            }
            if (Input.GetAxis("Fire1") == 1 && !isAttacking)
            {
                isAttacking = true;

                Ray ray = new Ray(camera.transform.position, camera.transform.TransformDirection(Vector3.forward));
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, attackDistance))
                {
                    hitPos = hit.point;
                    hitConfirmed = true;
                }
                else
                {
                    hitConfirmed = false;
                }
            }
            if(Input.GetAxis("Fire1") == 0)
            {
                isAttacking = false;
            }
        }
    }
}
