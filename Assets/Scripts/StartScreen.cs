using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private GameObject startScreed;
    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] private TextMeshProUGUI BestFoodText;
   
    
    // Start is called before the first frame update
    void ShowStartScreen(int Level, int bestFood)
    {
        LevelText.text = "Level " + Level;
        BestFoodText.text = "" + bestFood;
    }

    public void HideStartScreen()
    {
        startScreed.SetActive(false);
    }

    public void TapToStart()
    {
        HideStartScreen();
        //startGame
    }
}
