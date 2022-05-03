using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Action LevelOver;
    
    public float nextSceneDelay;
    public float levelClearAnimDelay;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    public void LevelCleared()
    {
        StartCoroutine(WaitForDelay());
    }
    
    IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(levelClearAnimDelay);
        InterfaceManager.Instance.LevelClearAnim();
    }

    public void LoadNextLevel()
    {
        LevelOver?.Invoke();
        StartCoroutine(GoToSceneAfterDelay(SceneManager.GetActiveScene().buildIndex));
    }
    
    public void PlayerDefeat()
    {
        StartCoroutine(GoToSceneAfterDelay(SceneManager.GetActiveScene().buildIndex));
    }
    
    IEnumerator GoToSceneAfterDelay(int sceneIndex)
    {
        yield return new WaitForSeconds(nextSceneDelay);
        SceneManager.LoadScene(sceneIndex);
    }
}
