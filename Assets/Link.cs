using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    // Start is called before the first frame update
    private Vector2 startPositonTouch;
    private Vector2 currentPositionTouch;
    private bool isIntouch = false;
    private Canvas parentCanvas;
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        parentCanvas = GameObject.FindWithTag("MainCanvas").GetComponent<Canvas>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CreateLineLink();
    }

    private void CreateLineLink()
    {
        /*if (Input.touchCount > 0)
        {
            if (!isIntouch)
            {
                startPositonTouch = Input.GetTouch(0).position;
                _lineRenderer.positionCount = 2;
                _lineRenderer.SetPosition(0,startPositonTouch);
            }
            else
            {
                currentPositionTouch = Input.GetTouch(0).position;
                _lineRenderer.SetPosition(1,currentPositionTouch);
            }
        }
        else
        {
            _lineRenderer.positionCount = 0;
        }*/
        // in editmode
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (!isIntouch)
            {
                startPositonTouch = GetMousePosition();
                _lineRenderer.positionCount = 2;
                _lineRenderer.SetPosition(0,startPositonTouch);
                isIntouch = true;
            }
            else
            {
                currentPositionTouch = GetMousePosition();
                _lineRenderer.SetPosition(1,currentPositionTouch);
            }
        }
        else
        {
            _lineRenderer.positionCount = 0;
            isIntouch = false;
        }
        
    }
    Vector3 GetMousePosition() {
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition, parentCanvas.worldCamera,
            out movePos);
        Vector3 positionToReturn = parentCanvas.transform.TransformPoint(movePos);
        positionToReturn.z = parentCanvas.transform.position.z - 0.01f;
        return positionToReturn;
    }
    Vector3 GetTouchPosition() {
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.GetTouch(0).position, parentCanvas.worldCamera,
            out movePos);
        Vector3 positionToReturn = parentCanvas.transform.TransformPoint(movePos);
        positionToReturn.z = parentCanvas.transform.position.z - 0.01f;
        return positionToReturn;
    }

    private void OnDisable()
    {
        _lineRenderer.positionCount = 0;
        
    }
}
