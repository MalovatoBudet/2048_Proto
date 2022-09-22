using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    public int Level;
    public int Coins;
    public Color BackgroundColor;
    public bool MusicOn;

    public static Progress Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Load();
    }

    public void SetLevel(int level)
    {
        Level = level;
        Save();
    }

    public void AddCoins(int value)
    {
        Coins += value;
        Save();
    }

    [ContextMenu("DeleteSave")]
    public void DeleteSave()
    {
        SaveSystem.DeleteSave();
    }

    [ContextMenu("Save")]
    public void Save()
    {
        SaveSystem.Save(this);
    }

    [ContextMenu("Load")]
    public void Load()
    {
        ProgressData progressData = SaveSystem.Load();

        if (progressData != null)
        {
            Level = progressData.Level;
            Coins = progressData.Coins;

            Color color = new Color(0,0,0,1);
            color.r = progressData.BackgroundColor[0];
            color.g = progressData.BackgroundColor[1];
            color.b = progressData.BackgroundColor[2];
            BackgroundColor = color;

            MusicOn = progressData.MusicOn;
        }
        else
        {
            Level = 1;
            Coins = 0;
            BackgroundColor = Color.blue;
            MusicOn = true;
        }
    }
}
