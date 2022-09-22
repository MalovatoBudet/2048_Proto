using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _winObject;
    [SerializeField] GameObject _loseObject;

    public UnityEvent OnWin;

    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Win()
    {
        _winObject.SetActive(true);

        OnWin.Invoke();

        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        Progress.Instance.SetLevel(currentLevelIndex + 1);
        Progress.Instance.AddCoins(10);
    }

    public void Lose()
    {
        _loseObject.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(Progress.Instance.Level);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
