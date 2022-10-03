using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayScreen : MonoBehaviour
{
    [SerializeField] private GameObject gamePlayGameObject;
    [SerializeField] private Image Slider;
    [SerializeField] private TextMeshProUGUI CurLevel;
    [SerializeField] private TextMeshProUGUI NextLevel;
    [SerializeField] private TextMeshProUGUI DestroyBlockCount;
    [SerializeField][Range(0,1)]private float snakeCurPos;
    private int destroyBlockCount;

    // Update is called once per frame
    void Update()
    {
        if (gamePlayGameObject.activeSelf)
        {
            Slider.fillAmount = snakeCurPos;
            DestroyBlockCount.text = "" + destroyBlockCount;
        }
    }

    public void AddDestroyBlock(int value)
    {
        destroyBlockCount = value;
    }

    public void ProgressLevel(float progres)
    {
        snakeCurPos = progres;
    }

    public void ShowGPScreen(int curLevel)
    {
        gamePlayGameObject.SetActive(true);
        this.CurLevel.text = "" + curLevel;
        NextLevel.text = "" + (1 + curLevel);
    }

    public void HideGPtScreen()
    {
        gamePlayGameObject.SetActive(false);
    }
}
