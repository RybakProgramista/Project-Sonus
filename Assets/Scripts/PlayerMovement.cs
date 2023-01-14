using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private CharacterController character;
    private Animator playerAnim;
    PhotonView view;
    private void Start()
    {
        character = GetComponent<CharacterController>();
        playerAnim = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
    }
    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            float moveVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            float moveHorizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

            Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;

            playerAnim.SetFloat("playerSpeed", Mathf.Max(Mathf.Abs(move.x), Mathf.Abs(move.z)));

            character.Move(move);
        }
    }
}
