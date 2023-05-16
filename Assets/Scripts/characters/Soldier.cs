using UnityEngine;
using System.Collections;

public class Soldier : Character {

    private GameObject target;

    private Transform controlPoint;

    private int coins = 0;

    private bool atControl = false;

    public override void DropCoin()
    {
        for(int i=0; i<coins; i++){
            Main.Prefabs.InstantiatePrefab(Main.instance.prefabs.coin, new Vector3(transform.position.x, transform.position.y + 1, 0));
        }

        coins = 0;
    }

    protected override void Start()
    {
        base.Start();

        BackToControlPoint();
    }

    protected override void Update()
    {
        base.Update();

        if(target == null){
            FindTarget();
        }

    }

    protected override void CollectCoin(GameObject coin)
    {
        coins++;

        //Destroy(coin);
    }

    protected override void Death()
    {
        Main.instance.armory.DeadSoldier(this);
    }

    public override void Attack()
    {
        this.GetComponentInChildren<Animator>().SetTrigger("attack");
        if(state == State.MoveLeft){
            Recoil(1);
        }
        else{
            Recoil(-1);
        }
    }

    public void SetControlPoint(Transform cP)
    {
       controlPoint = cP;
    }

    private void BackToControlPoint()
    {
        if(transform.position.x < controlPoint.position.x){
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
    }

    private void AttackEnemy()
    {
        if(transform.position.x < target.transform.position.x){
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
    }

    private void FindTarget()
    {
        GameObject closest = null;

        float minDist = 4;

        foreach (Enemy e in Main.instance.enemies)
        {
            float dist = Vector2.Distance(e.gameObject.transform.position, gameObject.transform.position);

            if (dist < minDist)
            {
                closest = e.gameObject;
                minDist = dist;
            }
        }

        if(closest != null){
            target = closest;
            AttackEnemy();
            atControl = false;
        }
        else if(!atControl) {
            BackToControlPoint();
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch(other.gameObject.tag){
            case "ControlPoint":
                if(target == null && other.gameObject.transform == controlPoint.gameObject.transform){
                    StopMoving();
                    atControl = true;
                }
                break;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D other) {
        base.OnCollisionEnter2D(other);

        switch(other.gameObject.tag){
            case "Enemy":
                if(other.gameObject != target){
                    target = other.gameObject;
                }
                other.gameObject.GetComponent<Enemy>().TakeDamage(damage, state);
                Attack();
                break;
        }
    }


}
