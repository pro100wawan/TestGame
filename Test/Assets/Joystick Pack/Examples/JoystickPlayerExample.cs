using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public FloatingJoystick floatingJoystick;
    public Rigidbody rb;

    CharacterController controller;

    private void Start()
    {
        if(floatingJoystick == null)
        {
            floatingJoystick = FindObjectOfType<FloatingJoystick>();
        }
        if (gameObject.GetComponent<CharacterController>() == null)
            controller = gameObject.AddComponent<CharacterController>();
    }

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
        //rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        controller.Move(direction * speed * Time.fixedDeltaTime);

    }
}