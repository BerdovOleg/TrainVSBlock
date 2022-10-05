using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    int LengthSnake;
    int MaxDestroyBlock;
    public int Live;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameData");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        Debug.Log("Load save data. SnakeLenght: " + LengthSnake+", Block count: "+ MaxDestroyBlock);
    }

    public int GetLengtSnake()
    { return LengthSnake; }

    public void SaveLengtSnake(int value)
    { LengthSnake = value; Debug.Log("length: "+LengthSnake); }

    public int GetSaveDestroyBlock()
    { return MaxDestroyBlock; }

    public void SaveDestroyBlock(int value)
    {
        if (value < MaxDestroyBlock) return;
        MaxDestroyBlock = value; Debug.Log("Block: " + MaxDestroyBlock);
    }
}
