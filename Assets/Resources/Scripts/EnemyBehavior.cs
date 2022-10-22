using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // Health system info
    public int hitpoints = 4;
    private int currHealth;
    private const int DEFAULT_HITPOINTS = 4;

    // Speed of plane
    public float speed = 20f;

    // Rate of rotation
    public float angularSpeed = 10f;

    // Distance from waypoint before plane changes waypoint
    public float waypointDistance = 10;

    // Waypoint list
    private List<string> waypointList;
    int currWaypointIdx;

    // Reference to self's Material component
    private Material materialComp;

    private void Awake()
    {
        waypointList = new();
        waypointList.Add("Waypoint A");
        waypointList.Add("Waypoint B");
        waypointList.Add("Waypoint C");
        waypointList.Add("Waypoint D");
        waypointList.Add("Waypoint E");
        waypointList.Add("Waypoint F");
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "Enemy";
        materialComp = GetComponent<Renderer>().material;
        currWaypointIdx = Random.Range(0, waypointList.Count);

        if (hitpoints <= 0)
        {
            hitpoints = DEFAULT_HITPOINTS;
        }
        currHealth = hitpoints;

        if (waypointDistance < 0)
        {
            waypointDistance = 10;
        }
    }

    private void Update()
    {
        transform.position += transform.up * (speed * Time.deltaTime);

        // Check if need to change waypoint
        Vector3 waypointCoord = WaypointSystem.Instance.GetWaypointPosition(waypointList[currWaypointIdx]);
        Vector3 difference = waypointCoord - transform.position;
        if (difference.magnitude <= waypointDistance)
        {
            if (EnemySystem.Instance.sequentialWaypoints)
            {
                currWaypointIdx = (++currWaypointIdx) % waypointList.Count;
            }
            else
            {
                int oldIdx = currWaypointIdx;
                while (currWaypointIdx == oldIdx)
                {
                    currWaypointIdx = Random.Range(0, waypointList.Count);
                }
            }

            waypointCoord = WaypointSystem.Instance.GetWaypointPosition(waypointList[currWaypointIdx]);
            difference = waypointCoord - transform.position;
        }

        // Update rotation
        difference.Normalize();
        transform.up = Vector3.LerpUnclamped(transform.up, difference, angularSpeed * Time.deltaTime);
    }

    // Collision handler
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hero")
        {
            DestroySelf(true);
            return;
        }

        if (collision.gameObject.name == "Egg")
        {
            // Decrement health
            --currHealth;
            if (currHealth <= 0)
            {
                DestroySelf(false);
            }

            // Change transparency
            float transparency = (float)currHealth / hitpoints;
            Color nextColor = materialComp.color;
            nextColor.a = transparency;
            materialComp.color = nextColor;
        }
    }

    // Destroy the Enemy instance and decrement the total Enemy count
    private void DestroySelf(bool destroyedByTouch)
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        EnemySystem.Instance.DecrementSpawnCount();
        EnemySystem.Instance.IncrementDestroyedCount(destroyedByTouch);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
