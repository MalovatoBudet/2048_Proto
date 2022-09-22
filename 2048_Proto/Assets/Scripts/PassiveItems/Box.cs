using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : PassiveItem
{
    [Range(0, 2)]
    public int Health = 1;

    [SerializeField] GameObject[] _levels;
    [SerializeField] GameObject _breakEffectPrefab;
    [SerializeField] Animator _animator;

    void Start()
    {
        SetHealth(Health);
    }

    [ContextMenu(nameof(OnAffect))]
    public override void OnAffect()
    {
        base.OnAffect();

        Health -= 1;
        Instantiate(_breakEffectPrefab, transform.position, Quaternion.Euler(-90, 0, 0));
        _animator.SetTrigger("Shake");

        if (Health < 0)
        {
            Die();
        }
        else
        {
            SetHealth(Health);
        }
    }

    void SetHealth(int value)
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            _levels[i].SetActive(i <= value);
        }
    }

    void Die()
    {
        Destroy(gameObject);
        ScoreManager.Instance.AddScore(ItemType, transform.position);
    }
}
