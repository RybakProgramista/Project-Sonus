using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MimikController : MonoBehaviour
{
    private Animator mimikAnim;
    private PhotonView view;
    private bool isAttacking;
    public float attackDistance;
    private void Start()
    {
        mimikAnim = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetAxis("Fire1") == 1 && !isAttacking)
            {
                isAttacking = true;

                mimikAnim.SetTrigger("attack");
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, attackDistance))
                {
                    Debug.Log(hit.collider.tag);
                }
            }
            if(Input.GetAxis("Fire1") == 0)
            {
                isAttacking = false;
            }
        }
    }
}
