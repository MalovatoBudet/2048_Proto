using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : ActiveItem
{
    [Header(nameof(Star))]
    [SerializeField] float _affectRadius = 1.5f;

    [SerializeField] GameObject _affectArea;
    [SerializeField] GameObject _effectPrefab;

    [SerializeField] LayerMask _layerMask;

    protected override void Start()
    {
        base.Start();

        _affectArea.SetActive(false);
    }

    IEnumerator AffectProcess()
    {
        _affectArea.SetActive(true);
        _animator.enabled = true;

        yield return new WaitForSeconds(1f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _affectRadius, _layerMask, QueryTriggerInteraction.Ignore);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].attachedRigidbody)
            {
                ActiveItem activeItem = colliders[i].attachedRigidbody.GetComponent<ActiveItem>();

                if (activeItem && activeItem.ItemType != ItemType.Star)
                {
                    activeItem.IncreaseLevel();
                }
            }
        }

        Instantiate(_effectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnValidate()
    {
        _affectArea.transform.localScale = Vector3.one * _affectRadius * 2f;
    }

    public override void DoEffect()
    {
        base.DoEffect();

        StartCoroutine(AffectProcess());
    }
}
