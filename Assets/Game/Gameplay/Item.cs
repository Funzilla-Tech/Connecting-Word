using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Cursor = UnityEngine.Cursor;

public class Item : MonoBehaviour
{
    [SerializeField] private Vector3 scaleWhenSelected;
    [SerializeField] private Texture2D curssor;
    [SerializeField] private Item MatchItem;
    public ItemType _itemType;
    [SerializeField] private Button btn;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private GameObject border;
    private EventTrigger eventTrigger;
    private Vector2 startPositonTouch;
    private Vector2 currentPositionTouch;
    private bool isIntouch = false;
    private Canvas parentCanvas;
    private ItemController _itemController;
    public bool isSelected = false;
    public bool isInLink;
    public Item head;
    public Item tail;
   // private GameObject hand;
    public enum ItemType
    {
        ICON,
        TEXT1,
        TEXT2
    }
    // Start is called before the first frame update
    private void Awake()
    {
       // hand = GameObject.FindWithTag("hand");
        parentCanvas = GameObject.FindWithTag("MainCanvas").GetComponent<Canvas>();
        _lineRenderer = GetComponentInChildren<LineRenderer>();
        eventTrigger = GetComponentInChildren<EventTrigger>();
        _itemController = GameObject.FindWithTag("ItemController").GetComponent<ItemController>();
        btn.onClick.AddListener(() =>
        {
            if (isInLink)
            {
                UnlinkAll();
               
            }
        });
        
        
    }
    // Update is called once per frame
    private void Start()
    {
        if (_itemController == null)
        {
            Debug.Log("null");
            return;
        }
        
        _itemController.AddNewItem(GetInstanceID(),gameObject);
    }

    private void Update()
    {
        if (isInLink || isSelected||isIntouch)
        {
           border.SetActive(true);
           //transform.localScale = scaleWhenSelected;
        }
        else
        {
            //transform.localScale = new Vector3(1, 1, 1);
            border.SetActive(false);
        }
        DrawLine();
    }
    private void DrawLine()
    {
        if (!_lineRenderer.enabled) return;
        _lineRenderer.SetPosition(0,startPositonTouch);
            _lineRenderer.SetPosition(1,currentPositionTouch);
    }
    
    public void BeginDrag()
    {
        SetCurrsor();
       // StartCoroutine(SelectEnterAnim());
        
        _lineRenderer.enabled = true;
        if (!isIntouch)
        {
            
                startPositonTouch = GetMousePosition();
               //
               // hand.transform.position = GetMousePosition();
                
            _lineRenderer.positionCount = 2;
            isIntouch = true;
        }
        isIntouch = true;
        _itemController.isInlinking = true;
    }

                    
    public void Drag()
    {
        SetCurrsor();
        if (tail != null)
        {
            foreach (var item in _itemController.GetItemList())
            {
                var _item = item.Value.GetComponent<Item>();
                if (_item.IsSlected())
                {
                    if (tail._itemType != _item._itemType && !_item.isInLink)
                    {
                        tail.tail = _item;
                        _item.head = tail;
                        _item.isInLink = true;
                        isInLink = true;
                        tail.BeginDrag();
                        tail.EndDrag();
                        return;
                    }
                }
            }
            return;
        }
        currentPositionTouch = GetMousePosition();
       // hand.transform.position = GetMousePosition();
        //Debug.Log("Draging");
       foreach (var item in _itemController.GetItemList())
       {
           var _item = item.Value.GetComponent<Item>();
           if (_item.IsSlected())
           {
               if (_itemType != _item._itemType && !_item.isInLink)
               {
                   tail = _item;
                   _item.head = this;
                   _item.isInLink = true;
                   isInLink = true;
                   EndDrag();
                   return;
               }
           }
       }

    }

    private void NormLineRenderer()
    {
        startPositonTouch = transform.position;
        currentPositionTouch = tail.transform.position;
    }
    public void EndDrag()
    {
        SetCurrsor();
        if (tail != null)
        {
            isIntouch = false;
            NormLineRenderer();
            return;
        }
        isIntouch = false;
        _lineRenderer.enabled = false;
        _itemController.isInlinking = false;
    }

    private bool HasSelectItem()
    {
        foreach (var item in _itemController.GetItemList())
        {
            var _item = item.Value.GetComponent<Item>();
            if (_item.IsSlected())
            {
                tail = _item;
                _item.head = this;
            }
        }
        return false;
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

    public bool IsSlected()
    {
        
        return isSelected;
    }

    public void OnPointerEnter()
    {
        
        SetCurrsor();
        isSelected = true;
        
    }

    public void OnPoiterExit()
    {
        isSelected = false;
    }
    private void OnDisable()
    {
        //_itemController.RemoveItem(GetInstanceID());
    }

    public void DisableLineRenderer()
    {
        _lineRenderer.positionCount = 0;
        _lineRenderer.enabled = false;
    }

    public void UnlinkAll()
    { 
        DisableLineRenderer();
        DeleteTail();
        DeleteHead();
       
    }

    
    public void DeleteHead()
    {
        if (head != null)
        {
            isInLink = false;
            head.isInLink = false;
            head.tail = null;
            head.DisableLineRenderer();
        }  
        head = null;
    }

    public void DeleteTail()
    {
        if (tail != null)
        {
            isInLink = false;
            tail.isInLink = false;
            tail.head = null;
            tail.DisableLineRenderer();
        }  
        tail = null;
    }

    public bool CheckValid()
    {
        if (tail == null) return true;
        if (tail != MatchItem)
        {
            _lineRenderer.material.color = Color.red;
            return false;
        }
        return true;
    }

    public void Reset()
    {
        head = null;
        tail = null;
        _lineRenderer.enabled = false;
        _lineRenderer.material.color = Color.green;
        isSelected = false;
        isIntouch = false;
        isInLink = false;
        
    }

    private void OnDestroy()
    {
        _itemController.RemoveItem(GetInstanceID());
    }

    IEnumerator SelectEnterAnim()
    {
        for (int i = 0; i < 10; i++)
        {
            var temp = 1f + 0.2f/ (100 - i);
            transform.localScale = new Vector3(temp, temp, 1);
            yield return new WaitForEndOfFrame();
        }
        for (int i = 0; i < 10; i++)
        {
            var temp = 1.2f -  0.2f / (100 - i);
            transform.localScale = new Vector3(temp, temp, 1);
            yield return new WaitForEndOfFrame();
        }
    }

    private void SetCurrsor()
    {
       /*var xspot = curssor.width/2;
       var yspot = 0;
       Vector2 hotSpot = new Vector2(xspot,yspot);
        Cursor.SetCursor(curssor,new Vector2(xspot,yspot),CursorMode.ForceSoftware);*/
    }
}
