using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public static Main instance { set; get; }

    public Player player;
    public Statue statue;
    public Stump stump;
    public Armory armory;

    public List<Enemy> enemies = new List<Enemy>();

    public Transform[] portals;

    [SerializeField]
    public Prefabs prefabs;

    //UI objects
    public TMP_Text coinsTxt, lifeTxt, helpTxt;

    public GameObject MenuCanvas, InGameCanvas, PlaceBuildingCanvas;

    public int[] buildingsPrices;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        player.UpdateLifeText();
        UpdateCoins(0);
    }

    private void Update()
    {
        SpawnEnemy();
    }

    public void UpdateCoins(long amount)
    {
        Save.instance.coins += amount;

        coinsTxt.text = "Resources: " + Save.instance.coins.ToString();
    }

    public void GameOver()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void Help(string text)
    {
        helpTxt.text = text;

        StartCoroutine(DisplayHelp());
    }

    private IEnumerator DisplayHelp()
    {
        helpTxt.gameObject.SetActive(true);

        yield return new WaitForSeconds(2);

        helpTxt.gameObject.SetActive(false);
    }

    public void BuildingCanvas(){
        if(PlaceBuildingCanvas.active){
            HideBuildingCanvas();
        }
        else{
            DisplayBuildingsCanvas();
        }
    }

    private void DisplayBuildingsCanvas(){
        Time.timeScale = 0.7f;
        PlaceBuildingCanvas.SetActive(true);
    }

    private void HideBuildingCanvas(){
        Time.timeScale = 1f;
        PlaceBuildingCanvas.SetActive(false);
    }


    private float totalSpawnTime = 10f, spawnTime = 5f;
    private void SpawnEnemy()
    {
        if(spawnTime <= 0f){
            int sansa = UnityEngine.Random.Range(-100, 105);
            Vector3 portalPos = portals[UnityEngine.Random.Range(0, portals.Length)].position;
            portalPos.z = 0;
            if(sansa >= 10){
                for(int i=0; i<sansa/10; i++){
                    var e = Main.Prefabs.InstantiatePrefab(Main.instance.prefabs.enemy, portalPos);
                    enemies.Add(e.GetComponent<Enemy>());
                }
            }
            spawnTime = totalSpawnTime;
        }
        else{
            spawnTime -= Time.deltaTime;
        }
    }

    public void PlaceBuilding(int index){
        if(!PlaceBuildingCanvas.active){
            return;
        }

        if(Save.instance.coins < buildingsPrices[index]){
            StartCoroutine(Animations.TextFlash(coinsTxt, Color.red, 3));
            return;
        }

        UpdateCoins(-buildingsPrices[index]);

        Vector3 pos = new Vector3(player.gameObject.transform.position.x, Main.instance.prefabs.buildings[index].transform.position.y, 0);
        Main.Prefabs.InstantiatePrefab(Main.instance.prefabs.buildings[index], pos);
    }

    [Serializable]
    public class Prefabs{
        public GameObject coin, woodcutter, soldier, enemy;
        public GameObject[] buildings;

        public static GameObject InstantiatePrefab(GameObject obj, Vector3 position){
            return Instantiate(obj, position, Quaternion.identity);
        }
    }

}
