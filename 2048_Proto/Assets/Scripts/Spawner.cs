using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float _sensitivity = 25f;
    [SerializeField] float _maxXPosition = 2.5f;

    float _xPosition;
    float _oldMouseX;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _oldMouseX = Input.mousePosition.x;
        }

        if (Input.GetMouseButton(0))
        {
            float delta = Input.mousePosition.x - _oldMouseX;
            _oldMouseX = Input.mousePosition.x;
            _xPosition += delta * _sensitivity / Screen.width;
            _xPosition = Mathf.Clamp(_xPosition, -_maxXPosition, _maxXPosition);
            transform.position = new Vector3(_xPosition,transform.position.y, transform.position.z);
        }
    }
}
