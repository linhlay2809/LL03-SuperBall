using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private Camera _mainCamera;

    // to change the color every blank seconds 
    [SerializeField] protected float changeColorEvery;
    protected float _colorstep;
    [SerializeField] protected Color[] _colors;
    protected int _i;

    // the starting color to lerp with
    private Color _lerpedColor;
    void Awake()
    {
        _mainCamera = GetComponent<Camera>();

        if (_mainCamera == null)
        {
            Debug.Log("Main camera not found!");
        }
    }
    private void Update()
    {
        if (_colorstep < changeColorEvery)
        {
            _lerpedColor = Color.Lerp(_colors[_i], _colors[_i + 1], _colorstep); // đổi màu bg theo thời gian
            _mainCamera.backgroundColor = _lerpedColor;
            _colorstep += 0.002f;
        }
        else
        {
            _colorstep = 0;
            if (_i < (_colors.Length - 2))
                _i++;
            else
                _i = 0;
        }
    }
}
