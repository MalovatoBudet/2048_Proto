using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreElementBall : ScoreElement
{
    [SerializeField] TextMeshProUGUI _levelText;
    [SerializeField] RawImage _ballImage;
    [SerializeField] BallSettings _ballSettings;

    public override void Setup(Task task)
    {
        base.Setup(task);

        int number = (int)Mathf.Pow(2, task.Level + 1);
        _levelText.text = number.ToString();
        _ballImage.color = _ballSettings.BallMaterials[task.Level].color;

        Level = task.Level;
    }
}
