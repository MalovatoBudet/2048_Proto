using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : ActiveItem
{
    [SerializeField] BallSettings _ballSettings;
    [SerializeField] Renderer _renderer;

    [SerializeField] Transform _visualTransform;

    public override void SetLevel(int level)
    {
        base.SetLevel(level);        

        Radius = Mathf.Lerp(0.4f, 0.7f, level / 10f);
        Vector3 ballScale = Vector3.one * Radius * 2f;
        _visualTransform.localScale = ballScale;
        _collider.radius = Radius;
        _trigger.radius = Radius + 0.1f;

        _renderer.material = _ballSettings.BallMaterials[level];

        _projection.Setup(_ballSettings.BallTransparentMaterials[level], _levelText.text, Radius);

        if (ScoreManager.Instance.AddScore(ItemType, transform.position, level))
        {
            Die();
        }
    }

    public override void DoEffect()
    {
        base.DoEffect();

        IncreaseLevel();
    }
}
