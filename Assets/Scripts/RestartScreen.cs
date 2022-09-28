using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;

public class RestartScreen : MonoBehaviour
{
    [SerializeField] private GameObject restartGameObject;
    [SerializeField] private GameObject Revive;
    [SerializeField] private GameObject RevievShowVidio;
    [SerializeField] private TextMeshProUGUI CurrentFood;
    [SerializeField] private TextMeshProUGUI BestFood;
    [SerializeField] private TextMeshProUGUI CurrentLive;
    [SerializeField] private TextMeshProUGUI Timer;
    private float CurrentTime;
    private float timer;
    private int currentFood;
    private int bestFood;
    private int currentLive;
    private bool showADS;

    // Start is called before the first frame update
    void Start()
    {
        timer = 5f;
        CurrentTime = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (restartGameObject.activeSelf)
        {
            Process();
        }
    }

    private void Process()
    {
        if (CurrentTime <= 0)
        {
            if (Revive.transform.position.x<400f)
            {

                RevievShowVidio.SetActive(false);
                Revive.transform.position =
                    Vector2.MoveTowards(transform.position, 
                        new Vector2(transform.position.x + .1f, 0f), 0.1f);
            }
        }
        else
        {
            showADS = true;
            CurrentTime -= Time.deltaTime;
            var text = Mathf.Round(CurrentTime +0.5f).ToString();
            Timer.text = text;
            CurrentFood.text = "" + currentFood;
            CurrentLive.text = "" + currentLive;
            BestFood.text = "" + bestFood;
        }
    }

    public void RestartGame()
    {
        if (showADS)
        {
            //показать рекламу
        }else if (currentLive <= 0)
        {
            //показать рекламу
        }
        else
        {
            HideRestartScreen();
            //рестарт уровная
        }
    }

    public void ShowRestartScreen(int BestFood, int CurrentFood, int CurrentLive)
    {
        currentFood = CurrentFood;
        bestFood = BestFood;
        currentLive = CurrentLive;
        restartGameObject.SetActive(true);
    }

    public void HideRestartScreen()
    {
        restartGameObject.SetActive(false);
    }

}
