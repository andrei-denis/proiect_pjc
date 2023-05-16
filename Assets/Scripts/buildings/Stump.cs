using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stump : SimpleBuilding
{   
    public Woodcutter worker;

    public override bool CoinReceived()
    {
        if(worker != null){
            return false;
        }

        var w = Main.Prefabs.InstantiatePrefab(Main.instance.prefabs.woodcutter, new Vector3(transform.position.x, transform.position.y, 0));
        worker = w.GetComponent<Woodcutter>();
        worker.SetHome(this);

        return true;
    }
}
