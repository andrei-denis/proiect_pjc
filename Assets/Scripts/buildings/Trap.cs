using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Trap : SimpleBuilding
{
    [SerializeField]
    private float max_attacks, damage, max_cooldown;
    private float attack, cooldown;

    private bool canDealDamage = true;

    void Start()
    {
        attack = 0; 
        cooldown = 0;
    }

    private void Update() {
        if(!canDealDamage){
            if(cooldown < max_cooldown){
                cooldown += Time.deltaTime; 
            }
            else{
                canDealDamage = true;
                cooldown = 0;
            }
        }
    }

    public bool DealDamege(){
        if(canDealDamage){
            canDealDamage = false;
            return true;
        }

        return false;
    }

    public override bool CoinReceived()
    {
        return false;
    }

    public float getDamage(){
        attack++;
        if(attack >= max_attacks){
            Destroy(this.gameObject);
        }
        return damage;
    }
}
