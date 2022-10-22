using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    // Camera visibility bounds
    public static Bounds Bounds
    {
        get;
        private set;
    }
    private static bool initialized;

    // Start is called before the first frame update
    void Start()
    {
        if (initialized)
        {
            return;
        }

        // Get camera boundaries
        float camY = Camera.main.orthographicSize * 2;
        float camX = Camera.main.orthographicSize * 2 * Camera.main.aspect;
        Vector3 camCenter = Camera.main.transform.position;
        camCenter.z = 0f;

        // Initialize cameraBounds
        Bounds cameraBounds = new();
        cameraBounds.center = camCenter;
        cameraBounds.size = new(camX, camY, 1f);
        Bounds = cameraBounds;
        initialized = true;
    }
}
