using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WaypointSystem : MonoBehaviour
{
    // Singleton instance
    public static WaypointSystem Instance
    {
        get;
        private set;
    }

    // Inidicate if the waypoints should be hidden
    public bool hideWaypoints
    {
        get;
        private set;
    }

    // Maximum randomized offset from the area position a waypoint can spawn
    public const int MAXIMUM_RAND_OFFSET = 15;

    // Reference to Waypoint object prefab
    private GameObject waypointPrefab;

    // Reference to Waypoint parent for all instantiations
    private GameObject waypointParent;

    // Coordinates of each waypoint area
    private Dictionary<string, Vector3> areaCoords;

    // Exact coordinates of each Waypoint object
    private Dictionary<string, Vector3> waypointCoords;

    // Awake is called on script initialization
    void Awake()
    {
        waypointPrefab = Resources.Load<GameObject>("Prefabs/Waypoint");
        Assert.IsNotNull(waypointPrefab);

        if (Instance == null)
        {
            Instance = this;
            hideWaypoints = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        waypointParent = GameObject.Find("Spawned Waypoints");
        Assert.IsNotNull(waypointParent);

        areaCoords = new();
        waypointCoords = new();

        // Get coordinates of waypoint areas
        areaCoords["Waypoint A"] = GameObject.FindWithTag("Waypoint A").transform.position;
        areaCoords["Waypoint B"] = GameObject.FindWithTag("Waypoint B").transform.position;
        areaCoords["Waypoint C"] = GameObject.FindWithTag("Waypoint C").transform.position;
        areaCoords["Waypoint D"] = GameObject.FindWithTag("Waypoint D").transform.position;
        areaCoords["Waypoint E"] = GameObject.FindWithTag("Waypoint E").transform.position;
        areaCoords["Waypoint F"] = GameObject.FindWithTag("Waypoint F").transform.position;

        // Spawn initiaial waypoints
        SpawnWaypoint("Waypoint A", 0);
        SpawnWaypoint("Waypoint B", 0);
        SpawnWaypoint("Waypoint C", 0);
        SpawnWaypoint("Waypoint D", 0);
        SpawnWaypoint("Waypoint E", 0);
        SpawnWaypoint("Waypoint F", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            hideWaypoints = !hideWaypoints;
        }
    }

    public Vector3 GetWaypointPosition(string waypoint)
    {
        return waypointCoords[waypoint];
    }

    public void RespawnWaypoint(string waypoint)
    {
        SpawnWaypoint(waypoint);
    }

    private void SpawnWaypoint(string waypoint, int maxOffset = MAXIMUM_RAND_OFFSET)
    {
        // Get area coords
        Vector3 coords;
        if (!areaCoords.TryGetValue(waypoint, out coords))
        {
            return;
        }
        
        // Randomize coords
        coords.x += Random.Range(0,  maxOffset * 2 + 1) - maxOffset;
        coords.y += Random.Range(0,  maxOffset * 2 + 1) - maxOffset;
        coords.z = 0f;

        // Instantiate Waypoint
        waypointCoords[waypoint] = coords;
        GameObject initializedWaypoint = Instantiate(waypointPrefab);
        initializedWaypoint.transform.position = coords;
        initializedWaypoint.GetComponent<WaypointBehavior>().Initialize(waypoint);
        initializedWaypoint.transform.parent = waypointParent.transform;
    }
}
