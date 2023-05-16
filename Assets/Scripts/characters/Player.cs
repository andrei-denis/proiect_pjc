using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private GameObject nearBuilding, nearTree;

    protected override void Death()
    {
        Main.instance.GameOver();
    }

    public override void DropCoin()
    {
        if(nearTree != null){
            if(Main.instance.stump.worker != null){
                Main.instance.stump.worker.StartWorking(nearTree);
            }
            else{
                Main.instance.Help("You don't have any worker! Go to the Stump and hire one!");
                Main.Prefabs.InstantiatePrefab(Main.instance.prefabs.coin, new Vector3(transform.position.x, transform.position.y + 5, 0));
                Main.instance.UpdateCoins(-1);
            }
        }
        else if(Save.instance.coins > 0){
            if(nearBuilding == null || !nearBuilding.GetComponent<SimpleBuilding>().CoinReceived()) {
                Main.Prefabs.InstantiatePrefab(Main.instance.prefabs.coin, new Vector3(transform.position.x, transform.position.y + 5, 0));
            }

            Main.instance.UpdateCoins(-1);
        }
    }

    public override void TakeDamage(float amount, State direction)
    {
        base.TakeDamage(amount, direction);

        UpdateLifeText();
    }

    public override void Attack()
    {
      //Not doing  
    }


    protected override void CollectCoin(GameObject coin){
        Main.instance.UpdateCoins(1);

        Destroy(coin);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch(other.gameObject.tag){
            case "Building":
                nearBuilding = other.gameObject;
                break;
            case "Tree":
                nearTree = other.gameObject;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        switch(other.gameObject.tag){
            case "Building":
                nearBuilding = null;
                break;
            case "Tree":
                nearTree = null;
                break;
        }
    }

    public void UpdateLifeText()
    {
        Main.instance.lifeTxt.text = "Life: " + life.ToString() + "/" + max_life.ToString();
    }
}
