using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_custom : MonoBehaviour
{
    private Canvas parentCanvas;
    // Start is called before the first frame update
    void Start()
    {
        parentCanvas = GameObject.FindWithTag("MainCanvas").GetComponent<Canvas>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GetMousePosition(); 
        transform.position = new Vector3(transform.position.x, transform.position.y, -5);
    }
    internal Vector3 GetMousePosition() {
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition, parentCanvas.worldCamera,
            out movePos);
        Vector3 positionToReturn = parentCanvas.transform.TransformPoint(movePos);
        positionToReturn.z = parentCanvas.transform.position.z - 0.1f;
        return positionToReturn;
    }
    
}
