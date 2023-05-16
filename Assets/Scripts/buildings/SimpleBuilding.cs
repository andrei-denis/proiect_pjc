using System;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable{
    bool isDamageable();
    void TakeDamage(float damage);
}

public abstract class SimpleBuilding : MonoBehaviour
{
    public GameObject helpUI;

    public abstract bool CoinReceived();

    private void OnTriggerEnter2D(Collider2D other) {
        switch(other.gameObject.tag){
            case "Player":
                if(helpUI != null){
                    helpUI.SetActive(true);
                }
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        switch(other.gameObject.tag){
            case "Player":
                if(helpUI != null){
                    helpUI.SetActive(false);
                }
                break;
        }
    }

}
