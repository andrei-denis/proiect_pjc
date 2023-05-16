using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wall : SimpleBuilding, IDamageable
{
    [SerializeField]
    private float max_life, repair_amount;
    private float life;

    [SerializeField]
    private TextMeshProUGUI life_text;

    private bool destroyd = false;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite goodS, destroydS;

    void Start()
    {
        life = max_life;
        UpdateLifeText();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        life -= damage;

        UpdateLifeText();

        if(life <= 0){
            destroyd = true;
            spriteRenderer.sprite = destroydS;
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

    private bool repair()
    {
        if(!destroyd){
            return false;
        }  
        spriteRenderer.sprite = goodS;
        life = max_life;
        destroyd = false;
        UpdateLifeText();
        return true;
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
        if(destroyd){
            return repair();
        }

        return Heal(repair_amount);
    }

    public bool isDamageable(){
        return !destroyd;
    }
}
