using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EggSystem : MonoBehaviour
{
    // Singleton EggSystem instance
    private static EggSystem instance;

    // Egg object reference for spawning instantiation
    private GameObject eggPrefab;

    // Parent GameObject for all Egg instantiations.
    private GameObject eggParent;

    // Count of all eggs currently in the scene
    private int count;

    // Get singleton instance
    public static EggSystem Instance
    {
        get { return instance; }
    }

    // Get current egg count
    public int EggCount
    {
        get { return count; }
    }

    // Awake is called on script initialization
    void Awake()
    {
        eggPrefab = Resources.Load<GameObject>("Prefabs/Egg");
        Assert.IsNotNull(eggPrefab);

        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        eggParent = GameObject.Find("Spawned Eggs");
        Assert.IsNotNull(eggParent);
    }

    // Spawn an Egg at a given position and starting angle
    public void SpawnEgg(Vector3 position, Vector3 direction)
    {
        GameObject eggInstance = Instantiate(eggPrefab);
        eggInstance.transform.parent = eggParent.transform;
        eggInstance.transform.position = position;
        eggInstance.transform.up = direction;
    }

    // Increment the scene Egg count
    public void IncrementCount()
    {
        ++count;
    }

    // Decrement the scene Egg count
    public void DecrementCount()
    {
        if (count > 0)
        {
            --count;
        }
    }
}
