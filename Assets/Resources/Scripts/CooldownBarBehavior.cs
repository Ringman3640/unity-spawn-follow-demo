using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownBarBehavior : MonoBehaviour
{
    public float maxXScale = 5f;

    private Vector3 emptyScale;

    // Start is called before the first frame update
    void Start()
    {
        emptyScale = transform.localScale;
        emptyScale.x = 0;
        transform.localScale = emptyScale;
    }

    // Update is called once per frame
    void Update()
    {
        float timeSinceLastFire = Time.time - HeroBehavior.prevFireTime;
        if (HeroBehavior.fireRate > timeSinceLastFire)
        {
            Vector3 newScale = emptyScale;
            newScale.x = maxXScale * (1 - (timeSinceLastFire / HeroBehavior.fireRate));
            transform.localScale = newScale;
        }
        else
        {
            transform.localScale = emptyScale;
        }
    }
}
