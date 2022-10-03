using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] private TextMeshProUGUI BestBlockText;
    public bool toStart;
   
    
    // Start is called before the first frame update
    public void ShowStartScreen(int Level, int BestBlock)
    {
        LevelText.text = "Level " + Level;
        BestBlockText.text = "" + BestBlock;
        startScreen.SetActive(true);
    }

    public void HideStartScreen()
    {
        startScreen.SetActive(false);
    }

    public void TapToStart()
    {
        HideStartScreen();
        toStart = true;
    }
}
