using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WaypointBehavior : MonoBehaviour
{
    public int hitpoints = 4;
    private int currHealth;

    // Table of image paths corresponding to waypoint names
    private static Dictionary<string, Sprite> spriteTable;

    // Initialized waypoint name
    private string waypointName;

    // Reference to the SpriteRender component
    private SpriteRenderer sr;

    // Reference to the BoxCollider2D component
    private new BoxCollider2D collider;

    // Awake is called on script initialization
    void Awake()
    {
        // Initialize sprite table
        spriteTable = new();
        spriteTable["Waypoint A"] = Resources.Load<Sprite>("Images/A_Walk");
        spriteTable["Waypoint B"] = Resources.Load<Sprite>("Images/B_Walk");
        spriteTable["Waypoint C"] = Resources.Load<Sprite>("Images/C_Walk");
        spriteTable["Waypoint D"] = Resources.Load<Sprite>("Images/D_Walk");
        spriteTable["Waypoint E"] = Resources.Load<Sprite>("Images/E_Walk");
        spriteTable["Waypoint F"] = Resources.Load<Sprite>("Images/F_Walk");
    }

    // Update is called once per frame
    void Update()
    {
        sr.enabled = !WaypointSystem.Instance.hideWaypoints;
        collider.enabled = !WaypointSystem.Instance.hideWaypoints;
    }

    // Collision detection
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Projectile")
        {
            return;
        }

        --currHealth;
        if (currHealth <= 0)
        {
            DestroySelf();
            return;
        }

        Color nextColor = sr.color;
        nextColor.a = (float)currHealth / hitpoints;
        sr.color = nextColor;
    }

    public void Initialize(string waypoint)
    {
        currHealth = hitpoints;
        sr = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(sr);

        collider = GetComponent<BoxCollider2D>();
        Assert.IsNotNull(collider);

        name = waypoint;
        waypointName = waypoint;
        sr.sprite = spriteTable[waypoint];
    }



    private void DestroySelf()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        gameObject.SetActive(false);
        Destroy(gameObject);
        WaypointSystem.Instance.RespawnWaypoint(name);
    }
}
