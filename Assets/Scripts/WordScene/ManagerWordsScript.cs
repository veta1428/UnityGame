using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerWordsScript : MonoBehaviour
{
    public GameObject wordPrefab;
    public GameObject tilePrefab;
    public bool mouseDown;
    public float time;

    public static ManagerWordsScript Instance { get; private set; }
    char[] array;
    string text;
    List<GameObject> words = new List<GameObject>();
    List<GameObject> tiles = new List<GameObject>();
    // Start is called before the first frame update
    private void Awake()
    {
        time = 5;
        mouseDown = false;
        Instance = this;
        //params
        text = "I ____ you!";
        array = new char[] { 'l', 'o', 'v', 'e' };

        int i = 0;
        gameObject.GetComponentInChildren<Canvas>().gameObject.GetComponentInChildren<Text>().text = text;  

        foreach (var item in array)
        {
            GameObject gameOb = Instantiate(tilePrefab, new Vector3(2 * i, 2, 0), Quaternion.identity);
            tiles.Add(gameOb);
            i++;
        }
        i = 0;
        foreach (var item in array)
        {
            GameObject gameOb = Instantiate(wordPrefab, new Vector3(2 * i, 0, 0), Quaternion.identity);
            Debug.Log("word was made");         
            gameOb.GetComponentInChildren<Canvas>().gameObject.GetComponentInChildren<Text>().text = array[i].ToString();
            words.Add(gameOb);
            i++;
        }
    }
    void Start()
    {
        
    }

    public bool IsFree(Collider2D collider)
    {
        foreach (var item in words)
        {
            if (item != collider.gameObject && item.transform.position == collider.gameObject.transform.position)
            {
                return false;
            }
        }
        return true;
    }
    void Clear()
    {
        int len = array.Length;
        for (int i = 0; i < len; i++)
        {
            words[i].SetActive(false);
            tiles[i].SetActive(false);
        }
    }
    bool HasWon()
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (words[i].transform.position != tiles[i].transform.position)
            {
                return false;
            }
        }      
        return true;
    }
    void OnClick()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0 )
        {
            TheEndTileScript.Instance.gameObject.GetComponentInChildren<Button>().onClick.AddListener(OnClick);
            if (HasWon())
            {
                TheEndTileScript.Instance.gameObject.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "You have won!";
                
            }
            else
            {
                TheEndTileScript.Instance.gameObject.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "You have lost!";
            }
            TheEndTileScript.Instance.gameObject.SetActive(true);
            Clear();
        }
    }
}
