using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : SimpleBuilding
{
    public override bool CoinReceived()
    {
        return false;
    }

    public void Cut()
    {
        Main.Prefabs.InstantiatePrefab(Main.instance.prefabs.coin, new Vector3(transform.position.x, transform.position.y + 2, 0));

        Destroy(this.gameObject);
    }
}
