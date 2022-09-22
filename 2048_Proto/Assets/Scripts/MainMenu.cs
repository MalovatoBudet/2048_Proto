using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _levelText;
    [SerializeField] TextMeshProUGUI _coinsText;

    [SerializeField] Button _startButton;
    [SerializeField] Image _backgroundImage; 

    void Start()
    {
        _levelText.text = "Level: " + Progress.Instance.Level.ToString();
        _coinsText.text = Progress.Instance.Coins.ToString();

        _backgroundImage.color = Progress.Instance.BackgroundColor;

        _startButton.onClick.AddListener(StartLevel);
    }

    void StartLevel()
    {
        SceneManager.LoadScene(Progress.Instance.Level);
    }
}
