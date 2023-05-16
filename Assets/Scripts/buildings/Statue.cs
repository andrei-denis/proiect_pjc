using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Statue : SimpleBuilding, IDamageable
{
    private float max_life = 100;
    private float life;

    [SerializeField]
    private TextMeshProUGUI life_text;

    void Start()
    {
        life = max_life;
        UpdateLifeText();
    }

    public void TakeDamage(float damage)
    {
        life -= damage;

        UpdateLifeText();

        if(life <= 0){
            Main.instance.GameOver();
        }
    }

    private bool Heal(float heal)
    {
        if(life < max_life){
            life += heal;
            if(life > max_life){
                life = max_life;
            }
            UpdateLifeText();
            return true;
        }
        return false;
    }

    public float getLife()
    {
        return life;
    }

    private void UpdateLifeText()
    {
        life_text.text = life.ToString() + "/" + max_life.ToString();
    }

    public override bool CoinReceived()
    {
        return Heal(10);       
    }

    public bool isDamageable(){
        return true;
    }
}
