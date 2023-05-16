using UnityEngine;

public class Buttons : MonoBehaviour {

    private void Update()
    {
        if(Input.GetKeyDown("a")){
            MoveLeft();
        }
        else if(Input.GetKeyDown("d")){
            MoveRight();
        }
        else if(Input.GetKeyDown("c")){
            DropCoin();
        }
        else if(Input.GetKeyDown("space")){
            Attack();
        }
        else if(Input.GetKeyDown("e")){
            DropCoin();
        }
        else if(Input.GetKeyDown("b")){
            Main.instance.BuildingCanvas();
        }
        else if(Input.GetKeyDown("1")){
            Main.instance.PlaceBuilding(0);
        }
        else if(Input.GetKeyDown("2")){
            Main.instance.PlaceBuilding(1);
        }
        else if(Input.GetKeyDown("3")){
            Main.instance.PlaceBuilding(2);
        }

        if(Input.GetKeyUp("a") || Input.GetKeyUp("d")){
            StopMoving();
        }
    }

    public void MoveLeft(){
        Main.instance.player.MoveLeft();
    }

    public void MoveRight(){
        Main.instance.player.MoveRight();
    }

    public void StopMoving(){
        Main.instance.player.StopMoving();
    }

    public void DropCoin(){
        Main.instance.player.DropCoin();
    }

    public void Attack(){
        Main.instance.player.Attack();
    }

    public void StartGame()
    {
        Main.instance.MenuCanvas.SetActive(false);
        Main.instance.InGameCanvas.SetActive(true);

        Time.timeScale = 1f;
    }

}