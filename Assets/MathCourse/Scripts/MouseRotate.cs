using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotate : MonoBehaviour {
    public bool is3D = false;

    private float _sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private bool _isRotating;

    void Start()
    {
        _sensitivity = 0.4f;
        _rotation = Vector3.zero;
    }

    private void Update()
    {
        if (is3D)
            Update3D();
        else
            Update2D();
    }

    void Update2D()
    {
        if (_isRotating)
        {
            // offset
            _mouseOffset = (Input.mousePosition - _mouseReference);

            // apply rotation
            _rotation.z = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;

            // rotate
            transform.Rotate(_rotation);

            // store mouse
            _mouseReference = Input.mousePosition;
        }
    }

    void Update3D()
    {
        if (_isRotating)
        {
            // offset
            _mouseOffset = (Input.mousePosition - _mouseReference);
            // apply rotation
            //_rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
            _rotation.y = -(_mouseOffset.x) * _sensitivity;
            _rotation.x = -(_mouseOffset.y) * _sensitivity;
            // rotate
            //transform.Rotate(_rotation);
            transform.eulerAngles += _rotation;
            // store mouse
            _mouseReference = Input.mousePosition;
        }
    }

    void OnMouseDown()
    {
        // rotating flag
        _isRotating = true;

        // store mouse
        _mouseReference = Input.mousePosition;
    }

    void OnMouseUp()
    {
        // rotating flag
        _isRotating = false;
    }


}
