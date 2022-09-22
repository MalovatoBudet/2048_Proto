using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Creator : MonoBehaviour
{
    [SerializeField] Transform _tube;
    [SerializeField] Transform _spawner;
    [SerializeField] ActiveItem _ballPrefab;

    ActiveItem _itemInTube;
    ActiveItem _itemInSpawner;

    [SerializeField] Transform _rayTransform;
    [SerializeField] LayerMask _layerMask;

    int _ballsLeft;
    [SerializeField] TextMeshProUGUI _ballsLeftText;

    Coroutine _waitForLose;

    void Start()
    {
        _ballsLeft = Level.Instance.NumberOfBalls;
        UpdateBallsLeftText();

        CreateItemInTube();
        StartCoroutine(MoveToSpawner());
    }

    void UpdateBallsLeftText()
    {
        _ballsLeftText.text = _ballsLeft.ToString();
    }

    void CreateItemInTube()
    {
        if (_ballsLeft == 0)
        {
            Debug.Log("0 balls left");
            return;
        }

        int itemLevel = Random.Range(0, 5);
        _itemInTube = Instantiate(_ballPrefab, _tube.position, Quaternion.identity);
        _itemInTube.SetLevel(itemLevel);
        _itemInTube.SetupToTube();

        _ballsLeft--;
        UpdateBallsLeftText();
    }

    IEnumerator MoveToSpawner()
    {
        _itemInTube.transform.parent = _spawner;

        for (float t = 0; t < 1f; t += Time.deltaTime / 0.4f)
        {
            _itemInTube.transform.position = Vector3.Lerp(_tube.position, _spawner.position, t);
            yield return null;
        }

        _itemInTube.transform.localPosition = Vector3.zero;
        _itemInSpawner = _itemInTube;

        _rayTransform.gameObject.SetActive(true);
        _itemInSpawner._projection.Show();

        _itemInTube = null;
        CreateItemInTube();
    }

    void LateUpdate()
    {
        if (_itemInSpawner)
        {

            Ray ray = new Ray(_spawner.position, Vector3.down);
            RaycastHit hit;

            if (Physics.SphereCast(ray, _itemInSpawner.Radius, out hit, 100, _layerMask, QueryTriggerInteraction.Ignore))
            {
                _rayTransform.localScale = new Vector3(_itemInSpawner.Radius * 2, hit.distance, 1);
                _itemInSpawner._projection.SetPosition(_spawner.position + Vector3.down * hit.distance);
            }


            if (Input.GetMouseButtonUp(0))
            {
                Drop();
            }
        }
    }

    void Drop()
    {
        _itemInSpawner.Drop();
        _itemInSpawner._projection.Hide();
        _itemInSpawner = null;

        _rayTransform.gameObject.SetActive(false);

        if (_itemInTube)
        {
            StartCoroutine(MoveToSpawner());
        }
        else
        {
            _waitForLose = StartCoroutine(WaitForLose());
            CollapseManager.Instance.OnCollapse.AddListener(ResetLoseTimer);
            GameManager.Instance.OnWin.AddListener(StopWaitForLose);
        }
    }

    void ResetLoseTimer()
    {
        if (_waitForLose != null)
        {
            StopCoroutine(_waitForLose);
            _waitForLose = StartCoroutine(WaitForLose());
        }
    }

    void StopWaitForLose()
    {
        if (_waitForLose != null)
        {
            StopCoroutine(_waitForLose);
        }
    }

    IEnumerator WaitForLose()
    {
        for (float t = 0; t < 5f; t += Time.deltaTime)
        {
            yield return null;
        }

        Debug.Log("Lose");

        GameManager.Instance.Lose();
    }
}
