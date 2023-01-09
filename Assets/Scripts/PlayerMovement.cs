using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private CharacterController character;
    PhotonView view;
    private void Start()
    {
        character = GetComponent<CharacterController>();
        view = GetComponent<PhotonView>();
    }
    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            float moveVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            float moveHorizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;


            Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;


            character.Move(move);
        }
    }
}
