using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //StateGame stateGame;
    [SerializeField] private SnakeController snake;
    private Enemy enemy;
    [SerializeField] private StartScreen startScreen;
    [SerializeField] private GamePlayScreen gamePlayScreen;
    [SerializeField] private RestartScreen restartScreen;
    [SerializeField] private WinScreen winScreen;
    [SerializeField] private Obstacles obstacles;
    private Finish finish;

    float delay = .9f;
    float timer = .9f;

    //SOUND 
    [SerializeField] private AudioSource musicAudio;
    [SerializeField] private AudioSource effectAudio;
    [SerializeField] private AudioClip[] clips; // 0-music, 1-bubble-popping, 2-itempickup
    //VISUAL
    [SerializeField] GameObject explosion;

    //saveSystem
    private int Level = 1;
    private int BestBlock;
    private int curDeadBlock;

    private float distance;
    private float complited;

    // Start is called before the first frame update
    void Start()
    {
        //loadSaveData();
        startScreen.ShowStartScreen(Level, BestBlock);
        finish = obstacles.GetComponentInChildren<Finish>();
        distance = Vector3.Distance(snake.transform.position, finish.transform.position);
    }

    private void StartSession()
    {
        snake.StartSnake();
        gamePlayScreen.ShowGPScreen(Level);
        musicAudio.clip = clips[0];
        musicAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        ProgressLevel();
        GamePlayState();
        MainLogic();

    }

    private void MainLogic()
    {
        if (snake.enemy != null)
        {
            enemy = snake.enemy;

            if (timer <= delay)
            {
                effectAudio.clip = clips[1];
                effectAudio.Play();
                if (snake.Length >= 1)//collision block
                {
                    curDeadBlock++;
                    enemy.EnterSnaske();
                    snake.SnakeEnterBlock();
                    var go = Instantiate(explosion, snake.transform.position, Quaternion.identity);
                    Destroy(go, 2f);
                    timer = 1f;
                    Debug.Log("Touch Bloc");
                }
                else if(snake.Length<1 & snake.gameObject.activeSelf)//lose level
                {
                    var go = Instantiate(explosion, snake.transform.position, Quaternion.identity);
                    Destroy(go, 2f);
                    snake.Destroy();
                    restartScreen.ShowRestartScreen(BestBlock, curDeadBlock, Level);
                    gamePlayScreen.HideGPtScreen();
                    snake.StopSnake();
                    print("Game Lose");
                }
            }
            timer -= Time.deltaTime;
        }
        else if (snake.finish != null)//win level
        {
            //snake.StopSnake();
            winScreen.ShowWinScreen(Level, false);
        }
    }

    private void GamePlayState()
    {
        if (startScreen.toStart)
        {
            StartSession();
            startScreen.toStart = false;
            gamePlayScreen.ShowGPScreen(Level);
        }
    }

    private void ProgressLevel()
    {
        if (snake.finish==null)
        {
            var currentPos = Math.Abs(Vector3.Distance(snake.transform.position, finish.transform.position) - distance);
            complited = (currentPos) / ((distance) / 100);
            var posotoin = complited / 100;
            gamePlayScreen.ProgressLevel(posotoin);
            gamePlayScreen.AddDestroyBlock(curDeadBlock);
        }
    }
}
