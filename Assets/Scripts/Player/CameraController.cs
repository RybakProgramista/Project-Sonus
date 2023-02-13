using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraController : MonoBehaviour
{
    public float xRotation = 0f, sensitivity;
    public Transform playerTransform;

    public float limitUp, limitDown;

    private PhotonView view;
    public bool canLook;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        if (!view.IsMine)
        {
            GetComponent<Camera>().enabled = false;
            GetComponent<AudioListener>().enabled = false;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            canLook = true;
        }
    }
    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            if (canLook)
            {
                float x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
                float y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

                xRotation -= y;
                xRotation = Mathf.Clamp(xRotation, limitDown, limitUp);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerTransform.Rotate(Vector3.up * x);
            }
        }
    }
}
