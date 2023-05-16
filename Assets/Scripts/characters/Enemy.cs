using UnityEngine;

public class Enemy : Character {

    private int coins = 0;

    protected override void Start()
    {
        base.Start();

        if(transform.position.x < 0){
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void DropCoin()
    {
        if(coins > 0){
            Main.Prefabs.InstantiatePrefab(Main.instance.prefabs.coin, new Vector3(transform.position.x, transform.position.y + 2, 0));
            coins--;
        }
    }

    protected override void Death()
    {
        DropCoin();
        Main.instance.enemies.Remove(this);
        Destroy(this.gameObject);
    }

    protected override void CollectCoin(GameObject coin)
    {
        this.coins++;
        Destroy(coin);
        Retreat();
    }

    public override void Attack()
    {
        if(state == State.MoveLeft){
            Recoil(1);
        }
        else{
            Recoil(-1);
        }
    }

    private void Retreat()
    {
        gameObject.transform.Find("Coins").gameObject.SetActive(true);

        speed = 6;

        if(state == State.MoveLeft){
            MoveRight();
        }
        else{
            MoveLeft();
        }

        int LayerRetreatNr = LayerMask.NameToLayer("Retreat");
        gameObject.layer = LayerRetreatNr;
    }

    private void AttackPlayer(Player player)
    {
        if(Save.instance.coins > 0){
            Main.instance.UpdateCoins(-1);
            Retreat();
            this.coins++;
            player.TakeDamage(0, state);
        }
        else{
            player.TakeDamage(damage, state);
            Attack();
        }
    }

    protected override void OnCollisionEnter2D(Collision2D other) {
        base.OnCollisionEnter2D(other);

        switch(other.gameObject.tag){
            case "Player":
                AttackPlayer(other.gameObject.GetComponent<Player>());
                break;
            case "Soldier":
                other.gameObject.GetComponent<Soldier>().TakeDamage(damage, state);
                Attack();
                break;
            case "Worker":
                other.gameObject.GetComponent<Character>().TakeDamage(damage, state);
                Attack();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch(other.gameObject.tag){
            case "Building":
            {
                var building = other.gameObject.GetComponent<IDamageable>();
                if(building != null && building.isDamageable()){
                    building.TakeDamage(damage);
                    Attack();
                }
                break;
            }
            case "Trap":
            {
                var trap = other.gameObject.GetComponent<Trap>();
                if(trap != null && trap.DealDamege()){
                    this.TakeDamage(trap.getDamage(), state);
                }
                break;
            }
            case "Portal":
                if(coins > 0){
                    Main.instance.enemies.Remove(this);
                    Destroy(this.gameObject);
                }
                break;
        }
    }

}