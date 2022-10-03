using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private TextMeshProUGUI CurLevel;
    private bool AdsShow;
    private bool nextLevel;

        public void ShowWinScreen(int curLevel, bool ads)
    {
        winScreen.SetActive(true);
        AdsShow = ads;
        CurLevel.text = "Level "+curLevel+" \nComplited!";
    }

    public void HideWinScreen()
    {
        winScreen.SetActive(false);
    }

    public void NextLevel()
    {
        if (AdsShow)
        {
            //ads
        }
        else
        {
            nextLevel = true;
        }
    }

    void Update()
    {
        if (nextLevel)
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        HideWinScreen();
        //LeadNextLevel()
    }

}
