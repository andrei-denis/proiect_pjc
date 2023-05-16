using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armory : SimpleBuilding
{   
    private List<Soldier> soldiers = new List<Soldier>();

    public Transform[] controlPoints;

    public override bool CoinReceived()
    {
        var s = Main.Prefabs.InstantiatePrefab(Main.instance.prefabs.soldier, new Vector3(transform.position.x, transform.position.y, 0));
        var sol = s.GetComponent<Soldier>();
        sol.SetControlPoint(controlPoints[Random.Range(0, controlPoints.Length)]);
        soldiers.Add(sol);

        return true;
    }

    public void DeadSoldier(Soldier soldier)
    {
        soldiers.Remove(soldier);

        Destroy(soldier.gameObject);
    }

    
}
