using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBehavior : MonoBehaviour
{
    // Text component
    TextMeshProUGUI tmComp;

    // Start is called before the first frame update
    void Start()
    {
        tmComp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        tmComp.text = "";

        // WAYPOINTS selection mode
        tmComp.text += "WAYPOINTS:(";
        if (EnemySystem.Instance.sequentialWaypoints)
        {
            tmComp.text += "Sequence)    ";
        }
        else
        {
            tmComp.text += "Random)    ";
        }

        // HERO movement mode
        tmComp.text += "HERO: Drive(";
        if (HeroBehavior.MouseMode)
        {
            tmComp.text += "Mouse) ";
        }
        else
        {
            tmComp.text += "Key) ";
        }

        // HERO touch count
        tmComp.text += "TouchedEnemies(" + EnemySystem.Instance.TouchDestroyedCount + ")    ";

        // EGG on screen count
        tmComp.text += "EGG: OnScreen(" + EggSystem.Instance.EggCount + ")    ";

        // ENEMY on screen count
        tmComp.text += "ENEMY: Count(" + EnemySystem.Instance.EnemyCount + ") ";

        // ENEMY destroyed count
        tmComp.text += "Destroyed(" + EnemySystem.Instance.DestroyedCount + ")";
    }
}
