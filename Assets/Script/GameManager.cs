using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    [SerializeField] float delayOnPlayerDeath = 1f;

    [Header("Player and Enemy Properties")]
    public PlayerHealth Player;
	
	void Awake () {
        if (Instance == null)
        {
            Debug.Log("awake");
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
	}
    void Update()
    {
        if (GvrControllerInput.AppButtonDown)
        {

            Invoke("LoadStartScene", delayOnPlayerDeath);
        }

    }


    public void PlayerDeathComplete()
    {
        Invoke("ReloadScene", delayOnPlayerDeath);
    }

    public void PlayerClearComplete()
    {
        Debug.Log("PlayerClearComplete");
        if (SceneManager.GetActiveScene().name == "Cell")
        {
            Invoke("LoadNextScene", delayOnPlayerDeath);
        }
        else if(SceneManager.GetActiveScene().name == "Stage2")
        {
            Invoke("LoadStartScene", delayOnPlayerDeath);
        }
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("Stage2");
    }

    void LoadStartScene()
    {
        SceneManager.LoadScene("Start");
    }

}
