using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherScript : MonoBehaviour
{
    // time after last wathcing started
    public float time = 0;

    // how much time left for watching
    public float timeWatching = 0;
    public delegate void OnWatching();
    public event OnWatching IsWatchingHandler;

    public delegate void OnNotWatching();
    public event OnNotWatching IsNotWatchingHandler;

    void Start()
    {
        IsWatchingHandler += UIIsWatching;
        IsNotWatchingHandler += UIIsNotWatching;
    }

    // Update is called once per frame
    void Update()
    {
        if (ManagerScript.Instance.teacherWatching)
        {
            timeWatching -= Time.deltaTime;
            if (timeWatching < 0)
            {
                IsNotWatchingHandler();

                ManagerScript.Instance.teacherWatching = false;
                timeWatching = 0;
            }
        }        
        else
        {
            if (time >= ManagerScript.FREQUENCY)
            {
                IsWatchingHandler();

                ManagerScript.Instance.teacherWatching = true;
                time = 0;
                GenerateWatchingPeriod();             
            }
        }
        time += Time.deltaTime;
    }

    void GenerateWatchingPeriod()
    {
        System.Random rnd = new System.Random();
        timeWatching = rnd.Next(ManagerScript.FREQUENCY / 6, ManagerScript.FREQUENCY / 2);
    }

    void UIIsNotWatching()
    {
        Debug.Log("Busy!");
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
        //TODO
    }

    void UIIsWatching()
    {
        Debug.Log("Watching!");
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 0f, 0f);
        //TODO
    }
}
