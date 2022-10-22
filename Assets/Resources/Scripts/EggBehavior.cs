using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehavior : MonoBehaviour
{
    // Projectile speed of the Egg
    public float speed = 40f;

    // Start is called before the first frame update
    void Start()
    {
        this.name = "Egg";
        EggSystem.Instance.IncrementCount();

        // Check if spawned out-of-bounds
        if (!CameraSystem.Bounds.Contains(transform.position))
        {
            DestroySelf();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime * transform.up;
    }

    // Collision handler
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            DestroySelf();
        }
    }

    // Exit screen handler
    private void OnBecameInvisible()
    {
        DestroySelf();
    }

    // Destroy the Egg instance and update the EggSystem count
    private void DestroySelf()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        EggSystem.Instance.DecrementCount();
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
