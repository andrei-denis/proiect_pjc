using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected float speed = 1, max_life = 100, damage = 5, recoilForce = 1;
    protected float life = 0;

    public enum State {
        Idle,
        MoveLeft,
        MoveRight
    }

    protected Rigidbody2D rb;
    [SerializeField]
    protected SpriteRenderer spriteRenderer;
    protected State state;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        life = max_life;

        state = State.Idle;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        StateManagement();
    }

    protected virtual void StateManagement()
    {
        switch(state) 
        {
            case State.Idle:
                rb.velocity = Vector2.zero;
                break;
            case State.MoveLeft:
                rb.velocity = Vector2.left * speed;
                break;
            case State.MoveRight:
                rb.velocity = Vector2.right * speed;
                break;
        }
    }

    public abstract void DropCoin();
    protected abstract void Death();
    public abstract void Attack();
    protected abstract void CollectCoin(GameObject coin);

    public virtual void MoveLeft()
    {
        state = State.MoveLeft;
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    public virtual void MoveRight()
    {
        state = State.MoveRight;
        gameObject.transform.localScale = new Vector3(-1, 1, 1);
    }

    public virtual void StopMoving()
    {
        state = State.Idle;
    }

    private void getDamage(float amount)
    {
        life -= amount;

        StartCoroutine(Animations.SpriteFlash(spriteRenderer, Color.red, 2));

        if(life <= 0){
            Death();
        }
    }

    public virtual void TakeDamage(float amount, State direction)
    {
        getDamage(amount);

        if(direction == State.MoveLeft)
        {
            Recoil(-1);
        }
        else
        {
            Recoil(1);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D other) {
        switch(other.gameObject.tag){
            case "Coins":
                CollectCoin(other.gameObject);
                break;
        }
    }

    protected virtual void Recoil(int side)
    {
        gameObject.transform.position += new Vector3(side * recoilForce, 0, 0);
    }
}
