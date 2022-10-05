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
    [SerializeField] private LevelManager levelManager;

    float delay = .9f;
    float timer = .9f;

    //SOUND 
    [SerializeField] private AudioSource musicAudio;
    [SerializeField] private AudioSource effectAudio;
    [SerializeField] private AudioClip[] clips; // 0-music, 1-bubble-popping, 2-itempickup
    //VISUAL
    [SerializeField] GameObject explosion;

    //saveSystem
    [SerializeField] private GameData gameData;
    private int Level;
    private int LenghtSnake;
    private int BestBlock;
    private int curDeadBlock;

    private float distance;
    private float complited;

    // Start is called before the first frame update
    void Start()
    {
        //Save system
        gameData= GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
        if (gameData.GetLengtSnake() > 0)  { LenghtSnake = gameData.GetLengtSnake(); }
        else {
            LenghtSnake = 4;
            gameData.Live = 5;
        }
        BestBlock = gameData.GetSaveDestroyBlock();
        //LevelSystem
        levelManager = GetComponent<LevelManager>();
        var a = levelManager.GetidLevel();
        if (a == 0) BestBlock = 0;  
        Level = a + 1;
        curDeadBlock = 0;
        //StartGame
        startScreen.ShowStartScreen(Level, BestBlock);
        finish = obstacles.GetComponentInChildren<Finish>();
        distance = Vector3.Distance(snake.transform.position, finish.transform.position);
    }

    private void StartSession()
    {
        snake.StartSnake(LenghtSnake);
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
        if (Input.GetKeyDown(KeyCode.W))
        {
            levelManager.LoadNextScene();
        }
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
                if (snake.Length > 1)//collision block
                {
                    curDeadBlock++;
                    enemy.EnterSnaske();
                    snake.SnakeEnterBlock();
                    var go = Instantiate(explosion, snake.transform.position, Quaternion.identity);
                    Destroy(go, 2f);
                    timer = 1f;
                    Debug.Log("Touch Bloc");
                    snake.enemy = null;
                    enemy = null;
                    gameData.SaveDestroyBlock(curDeadBlock);
                }
                else if(snake.Length<=1 & snake.gameObject.activeSelf)//lose level
                {
                    var go = Instantiate(explosion, snake.transform.position, Quaternion.identity);
                    Destroy(go, 2f);
                    snake.Destroy();
                    restartScreen.ShowRestartScreen(BestBlock, curDeadBlock,gameData.Live);
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
            gameData.SaveLengtSnake(snake.Length);
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

        if (restartScreen.restart)
        {
            if (gameData.Live <= 0)
            {
                gameData.Live--;
                levelManager.ReloadThisLevel();
            }
            gameData.SaveLengtSnake(4);
            levelManager.ReloadThisLevel();
        }

        if (winScreen.loadNextLevel)
        {
            levelManager.LoadNextScene();
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
