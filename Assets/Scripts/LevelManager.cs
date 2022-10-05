using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    int Count;
    Scene scene;

    private void Awake()
    {
         Count = SceneManager.sceneCountInBuildSettings;
         scene = SceneManager.GetActiveScene();
    }

    private void Start()
    {
        Debug.Log("Active Scene is '" + scene.name + "'.");
        print(scene.buildIndex);
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void ReloadThisLevel()
    {
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void  LoadNextScene()
    {
        if (scene.buildIndex+1 == Count)
        {
            SceneManager.LoadScene(0);
        }else SceneManager.LoadScene(scene.buildIndex + 1);
    }

    public int GetidLevel()
    {
        return scene.buildIndex;
    }
}
