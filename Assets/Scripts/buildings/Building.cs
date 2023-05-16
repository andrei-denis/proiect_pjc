using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Building : MonoBehaviour
{
    public List<Stage> stages;

    private int currentStage = 0;

    public TextMeshProUGUI remainingCoinsTxt;

    // Start is called before the first frame update
    void Start()
    {
        if(stages.Count > 0){
            GetComponent<SpriteRenderer>().sprite = stages[currentStage].GetSprite();
            UpdateUI();
        }
    }

    public bool CoinReceived()
    {
        //check if the build is at final stage
        if(currentStage >= stages.Count - 1){
            return false;
        }

        if(stages[currentStage].ReceiveCoin()){
            Upgrade();
        }

        UpdateUI();

        return true;       
    }

    private void Upgrade()
    {
        GetComponent<SpriteRenderer>().sprite = stages[++currentStage].GetSprite();
    }

    private void UpdateUI()
    {
        if(currentStage >= stages.Count - 1){
            remainingCoinsTxt.gameObject.SetActive(false);
        }
        else{
            remainingCoinsTxt.text = stages[currentStage].getReceivedCoins().ToString() + "/" + stages[currentStage].getRequiredCoins();
        }
    }

    [Serializable]
    public class Stage{

        [SerializeField]
        private int requiredCoins;
        [SerializeField]
        private Sprite sprite;

        private int receivedCoins = 0;

        //return True if Stage complete
        public bool ReceiveCoin()
        {
            receivedCoins++;

            return (receivedCoins == requiredCoins);
        }

        public int getReceivedCoins(){
            return receivedCoins;
        }

        public int getRequiredCoins(){
            return requiredCoins;
        }

        public Sprite GetSprite(){
            return sprite;
        }
    }

}
