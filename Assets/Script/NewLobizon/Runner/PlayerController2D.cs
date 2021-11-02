using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobizon.Runner
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController2D : MonoBehaviour
    {

        public Rigidbody2D Rig;
        public Transform checkGroundTransform;
        public LayerMask layerGround;
        public float jumpForce = 100f;
        public float velocity = 1f;

        float checkRange = 0.4f;
        bool onGround = true;
        bool isRunning = false;
        bool doubleJump = false;

        Animator anim;

        void Start()
        {
            Rig = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            transform.localRotation = Quaternion.identity;
            if (isRunning)
            {
                Rig.velocity = new Vector2(velocity, Rig.velocity.y);
            }
            anim.SetFloat("VelX", Rig.velocity.x);
            onGround = Physics2D.OverlapCircle(checkGroundTransform.position, checkRange, layerGround);
            if (onGround)
            {
                doubleJump = false;
            }

        }

        void Update()
        {
            Rig.rotation = 0;
            if (Input.GetMouseButtonDown(0))
            {
                if (isRunning)
                {
                    if (!doubleJump || onGround)

                    Rig.AddForce(new Vector2(0, jumpForce));
                    GetComponent<AudioSource>().Play();
                    anim.SetBool("isGrounded", onGround);
                    if (!doubleJump && !onGround)
                    {
                        doubleJump = true;
                    }
                }
                else
                {
                    isRunning = true;
                    //NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeEmpiezaACorrer");
                }
            }
        }
    }
}
