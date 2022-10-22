using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBehavior : MonoBehaviour
{
    // Indicate movement mode (true = mouse, false = keyboard)
    private static bool mouseMode = true;

    // Translational movement info
    public float initialVelocity = 20f;
    public float acceleration = 100f;
    public float maximumSpeed = 200f;
    private float velocity = 0f;

    // Rotational movement info
    public float angularAcceleration = 500f;
    public float maximumAngularSpeed = 45f;
    private float angularVelocity = 0f;

    // Weapon firing info
    public static float fireRate = 0.2f;
    public static float prevFireTime
    {
        get;
        private set;
    }
    private bool canFire = true;

    // Get the current mouse mode
    // true = mouse mode
    // false = keyboard mode
    public static bool MouseMode
    {
        get { return mouseMode; }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.name = "Hero";
        prevFireTime = Time.time;
    }

    // Disable firing if out-of-bounds
    private void OnBecameInvisible()
    {
        canFire = false;
    }

    // Reenable firing when in-bounds
    private void OnBecameVisible()
    {
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckModeChange();
        UpdatePosition();
        CheckFireEgg();
    }

    // Check if the user has changed movement modes
    private void CheckModeChange()
    {
        if (!Input.GetKeyDown(KeyCode.M))
        {
            return;
        }

        if (mouseMode)
        {
            // Transition to keyboard input
            mouseMode = false;
            velocity = initialVelocity;
        }
        else
        {
            // Transition to mouse input
            mouseMode = true;
            velocity = 0f;
        }
    }

    // Check and update the position of the Hero
    private void UpdatePosition()
    {
        // Apply translations
        if (mouseMode)
        {
            // Use mouse controls

            // Move obj to mouse position
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
            transform.position = pos;
        }
        else
        {
            // Use keyboard controls

            // Check for acceleration/deceleration
            velocity += Input.GetAxis("Vertical") * acceleration * Time.deltaTime;
            if (Mathf.Abs(velocity) > maximumSpeed)
            {
                velocity = maximumSpeed * Mathf.Sign(velocity);
            }

            // Apply velocity
            transform.position += velocity * Time.smoothDeltaTime * transform.up;
        }

        // Apply rotaions
        // Check for angualar acceleration/deceleration
        float horizInput = -Input.GetAxis("Horizontal");
        if (horizInput != 0)
        {
            angularVelocity += horizInput * angularAcceleration * Time.deltaTime;
            if (Mathf.Abs(angularVelocity) > maximumAngularSpeed)
            {
                angularVelocity = maximumAngularSpeed * Mathf.Sign(angularVelocity);
            }
        }
        else
        {
            angularVelocity = 0f;
        }

        // Apply angular velocity
        transform.Rotate(Vector3.forward, angularVelocity * Time.deltaTime);
    }

    // Check if the Hero should fire an Egg
    private void CheckFireEgg()
    {
        if (Input.GetKey(KeyCode.Space) && canFire && Time.time - prevFireTime >= fireRate)
        {
            prevFireTime = Time.time;
            EggSystem.Instance.SpawnEgg(transform.position, transform.up);
        }
    }
}
