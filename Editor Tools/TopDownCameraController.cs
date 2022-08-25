using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraController : MonoBehaviour
{
    [Header("Camera Basics")]
    [SerializeField] private bool _allowMovement;
    [SerializeField] private bool _allowZoom;
    [SerializeField] private KeyCode _allowKey;
    [SerializeField] private float _panSpeed;

    [Header("Camera Mouse")]
    [SerializeField] private bool _useMouseToMove;
    [SerializeField] private float _mousePanBorder;
    [SerializeField] private float _zoomInSpeed;

    [Header("Camera Keys")]
    [SerializeField] private bool _useKeysToMove;
    [SerializeField] private KeyCode _moveCameraUp;
    [SerializeField] private KeyCode _moveCameraRight;
    [SerializeField] private KeyCode _moveCameraDown;
    [SerializeField] private KeyCode _moveCameraLeft;

    [Header("Camera Pan Limit")]
    [SerializeField] private bool _useCameraLimits;
    [SerializeField] private float _minCameraX;
    [SerializeField] private float _maxCameraX;
    [SerializeField] private float _minCameraY;
    [SerializeField] private float _maxCameraY;
    [SerializeField] private float _minCameraZ;
    [SerializeField] private float _maxCameraZ;

    private void Update()
    {
        Vector3 cameraPos = transform.position;

        #region Camera Allow
        if (Input.GetKeyDown(_allowKey))
        {
            _allowMovement = !_allowMovement;
        }
        if (!_allowMovement) return;
        #endregion

        #region Camera Limits
        if (_useCameraLimits)
        {
            cameraPos.x = Mathf.Clamp(cameraPos.x, _minCameraX, _maxCameraX);
            cameraPos.y = Mathf.Clamp(cameraPos.y, _minCameraY, _maxCameraY);
            cameraPos.z = Mathf.Clamp(cameraPos.z, _minCameraZ, _maxCameraZ);
        }
        #endregion

        #region Camera Zoom
        if (_allowZoom)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            cameraPos.y -= scroll * 100 * _zoomInSpeed * Time.deltaTime;
        }
        #endregion

        #region Key Movement

        if (_useKeysToMove)
        {
            //Camera forward
            if (Input.GetKey(_moveCameraUp))
            {
                cameraPos.z += _panSpeed * Time.deltaTime;
            }

            //Camera right
            if (Input.GetKey(_moveCameraRight))
            {
                cameraPos.x += _panSpeed * Time.deltaTime;
            }

            //Camera backward
            if (Input.GetKey(_moveCameraDown))
            {
                cameraPos.z -= _panSpeed * Time.deltaTime;
            }

            //Camera left
            if (Input.GetKey(_moveCameraLeft))
            {
                cameraPos.x -= _panSpeed * Time.deltaTime;
            }
        }

        #endregion

        #region Mouse Movement 

        if (_useMouseToMove)
        {
            //Camera foward
            if (Input.mousePosition.y >= Screen.height - _mousePanBorder)
            {
                cameraPos.z += _panSpeed * Time.deltaTime;
            }

            //Camera right
            if (Input.mousePosition.x >= Screen.width - _mousePanBorder)
            {
                cameraPos.x += _panSpeed * Time.deltaTime;
            }

            //Camera backward
            if (Input.mousePosition.y <= _mousePanBorder)
            {
                cameraPos.z -= _panSpeed * Time.deltaTime;
            }

            //Camera left
            if (Input.mousePosition.x <= _mousePanBorder)
            {
                cameraPos.x -= _panSpeed * Time.deltaTime;
            }
        }

        #endregion

        transform.localPosition = cameraPos;
    }
}
