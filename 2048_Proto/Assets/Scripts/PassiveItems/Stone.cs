using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : PassiveItem
{
    [SerializeField] GameObject _dieEffect;
    [Range(0, 2)]
    [SerializeField] int _level = 2;
    [SerializeField] Transform _visualTransform;
    [SerializeField] Stone _stonePrefab;

    public override void OnAffect()
    {
        base.OnAffect();

        if (_level > 0)
        {
            for (int i = 0; i < 2; i++)
            {
                CreateChildStone(_level - 1);
            }
        }
        else
        {
            ScoreManager.Instance.AddScore(ItemType, transform.position);
        }

        Die();
    }

    void CreateChildStone(int level)
    {
        Stone newStone = Instantiate(_stonePrefab, transform.position, Quaternion.identity);
        newStone.SetLevel(level);
    }

    public void SetLevel(int level)
    {
        _level = level;
        float scale = 1f;

        if (level == 2)
        {
            scale = 1f;
        }
        else if (level == 1)
        {
            scale = 0.7f;
        }
        else if (level == 0)
        {
            scale = 0.45f;
        }

        _visualTransform.localScale = Vector3.one * scale;
    }

    void Die()
    {
        Instantiate(_dieEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
