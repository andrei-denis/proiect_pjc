using UnityEngine;
using System.Collections;

public class Woodcutter : Character {

    private Stump home;

    private bool work = false;
    private GameObject targetTree;

    [SerializeField]
    private float cuttingTime = 1f;

    public void SetHome(Stump h)
    {
        home = h;
    }

    public override void DropCoin(){ }

    protected override void Death()
    {
        Destroy(this.gameObject);
    }

    public override void Attack(){ }

    protected override void CollectCoin(GameObject coin) { }


    public void StartWorking(GameObject tree)
    {
        if(!work){
            if(transform.position.x < tree.transform.position.x){
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
            targetTree = tree;
            work = true;
        }
    }

    private void BackHome()
    {
        if(transform.position.x < home.gameObject.transform.position.x){
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
        work = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch(other.gameObject.tag){
            case "Tree":
                if(work && other.gameObject == targetTree){
                    StartCoroutine(CutTree());
                }
                break;
            case "Building":
                if(!work && other.gameObject == home.gameObject){
                    StopMoving();
                }
                break;
        }
    }

    private IEnumerator CutTree()
    {
        StopMoving();

        yield return new WaitForSeconds(cuttingTime);

        targetTree.GetComponent<Tree>().Cut();

        targetTree=null;
        BackHome();
    
    }

}