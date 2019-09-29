using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Drive : MonoBehaviour
{

    float acceleration = 10f;
    float torque = 200f;
    float driftStandard = 0.9f;
    float driftSlide = 1f;
    float maxStandardVelocity = 2.5f;
    float minSlideVelocity = 1.5f; // Later if I want

    // float mousePosX = 0;
    // float mousePosY = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody2D playerBody = GetComponent<Rigidbody2D>();

        Debug.Log(RightVelocity().magnitude);

        float driftFactor = driftStandard;
        if(RightVelocity().magnitude > maxStandardVelocity)
        {
            driftFactor = driftSlide;
        }

        playerBody.velocity = ForwardVelocity() + RightVelocity() * driftStandard;

        // faceMouse();

        if (Input.GetButton("Accelerate"))
        {
            playerBody.AddForce(force: transform.up * acceleration);
        }

        if (Input.GetButton("Break"))
        {
            playerBody.AddForce(force: transform.up * (-acceleration / 2));
        }

        float activeTorque = Mathf.Lerp(0, torque, playerBody.velocity.magnitude / 2);

        playerBody.angularVelocity = Input.GetAxis("Mouse X") * activeTorque;

        // float mousePosX =+ Input.GetAxis("Mouse X") * activeTorque;
        // float mousePosY =+ Input.GetAxis("Mouse Y") * activeTorque;

        // var x = Input.GetAxis("Mouse X");
        // var y = Input.GetAxis("Mouse Y");

        // transform.Rotate(mousePosX, 0f, mousePosY);

        // playerBody.angularVelocity = direction;
        // transform.up = direction;

        // playerBody.angularVelocity = mousePosX;


        // playerBody.angularVelocity = Input.mousePosition.x * activeTorque;
        // playerBody.angularVelocity = Input.GetAxis("Mouse X") * activeTorque;
    }

    // Calculates forward velocity of the player
    Vector2 ForwardVelocity()
    {
        return transform.up * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.up);
    }

    // Calculates horizontal velocity of the player
    Vector2 RightVelocity()
    {
        return transform.right * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.right);
    }

    // Orientates player to always face the mouse
    void faceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );        

        transform.up = direction;
    }
}
