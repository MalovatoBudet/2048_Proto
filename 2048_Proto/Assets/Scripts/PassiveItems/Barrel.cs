using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : PassiveItem
{
    [SerializeField] GameObject _barrelExplosion;

    [ContextMenu(nameof(OnAffect))]
    public override void OnAffect()
    {
        base.OnAffect();

        Die();
    }

    void Die()
    {
        Instantiate(_barrelExplosion, transform.position, Quaternion.Euler(-90, 0, 0));
        Destroy(gameObject);
        ScoreManager.Instance.AddScore(ItemType, transform.position);
    }
}
