using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
   private Rigidbody rb;
   private float moveSpeed = 1f;

   private void Start()
   {
      rb = GetComponent<Rigidbody>();
   }
    private void HandleMovement()
   {
      if (isLocalPlayer)
      {
         float moveHorizontal = Input.GetAxis("Horizontal");
         float moveVertical = Input.GetAxis("Vertical");
         Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
         rb.velocity = movement * moveSpeed;
      }
   }

   private void Update()
   {
      HandleMovement();
   }
}
