using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TargetProblemSolver : MonoBehaviour
{
    public Transform realTarget;
    public PhotonView view;
    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    void FixedUpdate()
    {
        if (view.IsMine)
        {
            transform.position = realTarget.position;
            transform.rotation = realTarget.rotation;
        }
    }
}
