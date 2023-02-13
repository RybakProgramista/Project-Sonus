using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public bool canMove;
    private CharacterController character;
    private Animator playerAnim;
    private AudioSource audio;
    PhotonView view;
    private void Start()
    {
        character = GetComponent<CharacterController>();
        playerAnim = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
        audio = GetComponent<AudioSource>();
        canMove = true;
    }
    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            if (canMove)
            {
                float moveVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
                float moveHorizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

                Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;

                playerAnim.SetFloat("playerSpeed", Mathf.Max(Mathf.Abs(move.x), Mathf.Abs(move.z)));

                if (Mathf.Abs(move.x) > 0 || Mathf.Abs(move.z) > 0)
                {
                    if (!audio.isPlaying)
                    {
                        audio.Play();
                    }
                }
                else
                {
                    audio.Stop();
                }
                character.Move(move);
            }
            else
            {
                audio.Stop();
                playerAnim.SetFloat("playerSpeed", 0f);
            }
        }
    }
}
