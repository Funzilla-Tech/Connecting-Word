using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private ParticleSystem confety;
    [SerializeField]private Dictionary<int, GameObject> ItemList = new Dictionary<int, GameObject>();
    
    private Canvas WinCanvas;
    private Canvas GameoverCanvas;
    private const int LevelCount = 6;
    private int CurrentLevelIndex;
    [SerializeField]private GameObject currentLevel;
    [SerializeField] private TextMeshProUGUI levelLabel;
    private GameObject MainCanvas;
    enum GameState
    {
        BEGIN,
        PAYING,
        GAMEOVER,
        WINGAME
    }

    private GameState state;
    public bool isInlinking = false;
    // Start is called before the first frame update
    
    public void RemoveItem(int itemInstanceId)
    {
        ItemList.Remove(itemInstanceId);
    }

    public void AddNewItem(int itemInstanceId, GameObject itemGameObject)
    {
        ItemList.Add(itemInstanceId,itemGameObject);
    }

    public Dictionary<int, GameObject> GetItemList()
    {
        return ItemList;
    }

    private void Awake()
    {
        
        state = GameState.PAYING;
        WinCanvas = GameObject.FindWithTag("WinCanvas").GetComponent<Canvas>();
        GameoverCanvas = GameObject.FindWithTag("GameOverCanvas").GetComponent<Canvas>();
        CurrentLevelIndex = 0;
        MainCanvas = GameObject.FindWithTag("MainCanvas");
        levelLabel.text = $"LEVEL : {(CurrentLevelIndex + 1)}";
        LoadLevel();
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.PAYING :
                GamePlaying();
                break;
           case GameState.GAMEOVER :
                GameOver();
               break;
        }
    }

    private void SetGameOver()
    {
        GameoverCanvas.enabled = true;
        StartCoroutine(GameOverCanvasAnim());
    }

    IEnumerator GameOverCanvasAnim()
    {
        for (int i = 0; i < 100; i+=2)
        {
            GameoverCanvas.gameObject.transform.localScale = new Vector3(i/100f,i/100f,1);
            yield return new WaitForSeconds(0.0001f*Time.deltaTime);
        }
        
    }
    IEnumerator WinCanvasAnim()
    {
        for (int i = 0; i < 100; i+=2)
        {
            WinCanvas.gameObject.transform.localScale = new Vector3(i/100f,i/100f,1);
            yield return new WaitForSeconds(0.1f*Time.deltaTime);
        }
        
    }
    private void GameOver()
    {
        
    }
    private void SetWinGame()
    {
        WinCanvas.enabled = true;
        confety.Play();
        StartCoroutine(WinCanvasAnim());
        
    }
    private void GamePlaying()
    {
        
        if (CheckComplete())
        {
            foreach (var item in ItemList)
            {
                var _item = item.Value.GetComponent<Item>();
                if (!_item.CheckValid())
                {
                    ChangeState(GameState.GAMEOVER);
                }
            }

            if (state == GameState.GAMEOVER) return;
            ChangeState(GameState.WINGAME);
        }
    }
    private void ChangeState(GameState _state)
    {
        if (_state == state) return;
        // exit old state 
        switch (state)
        {
                case GameState.BEGIN :
                    break;
                case GameState.PAYING :
                    break;
                case GameState.GAMEOVER :
                    GameoverCanvas.enabled = false;
                    break;
                case GameState.WINGAME :
                    WinCanvas.enabled = false;
                    break;
        }

        state = _state;
        // begin new state

        switch (_state)
        {
            case GameState.BEGIN :
                break;
            case GameState.PAYING :
                break;
            case GameState.GAMEOVER :
                SetGameOver();
                break;
            case GameState.WINGAME :
                SetWinGame();
                break;
        }
    }

    private bool CheckComplete()
    {
        if (ItemList.Count == 0) return false;
        if (currentLevel == null) return false;
        foreach (var item in ItemList)
        {
            var _item = item.Value.GetComponent<Item>();
            if (!_item.isInLink)
            {
                return false;
            }
        }

        return true;
    }

    public void ReloadLevel()
    {
       
        foreach (var item in ItemList)
        {
            var _item = item.Value.GetComponent<Item>();
            _item.Reset();
        }
        ChangeState(GameState.PAYING);
    }
    
    public void LoadNewLevel()
    {
        CurrentLevelIndex = CurrentLevelIndex < (LevelCount - 1) ? (CurrentLevelIndex + 1 ) : 0;
        LoadLevel();
    }

    public void LoadBackLevel()
    {
        CurrentLevelIndex = CurrentLevelIndex > 0 ? (CurrentLevelIndex - 1 ) : (LevelCount-1);
        LoadLevel();
    }

    private void LoadLevel()
    {
        GameObject g = Resources.Load<GameObject>(levelPath[CurrentLevelIndex]);
        foreach (var item in ItemList)
        {
            var _item = item.Value.GetComponent<Item>();
            _item.Reset();
        }
        if (g != null)
        {
            ItemList.Clear();
            Destroy(currentLevel);
            currentLevel = null;
            currentLevel = Instantiate(g, transform);
            currentLevel.transform.parent = MainCanvas.transform;
            WinCanvas.transform.SetAsLastSibling();
            GameoverCanvas.transform.SetAsLastSibling();
            levelLabel.text = $"LEVEL : {(CurrentLevelIndex + 1)}";
        }
        
        else
        {
            Debug.Log("Cant load new level");
        }
        ChangeState(GameState.PAYING);
    }
    private string[] levelPath = new string[] {"Level1","Level2","Level3","Level4","Level5","Level6"};
}
