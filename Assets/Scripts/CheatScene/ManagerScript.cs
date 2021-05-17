using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    //in inspector
    public GameObject prefab;

    //singletone
    public static ManagerScript Instance { get; private set; }

    public delegate void OnWinning();

    public event OnWinning HasWon;

    public int studentAmount;

    //needed to cheat for 1 student
    public const float ENOUGH = 3;

    //summarised progress
    public float CurrentProgress;

    //how often teacher starts watching
    public const int FREQUENCY = 8;

    public List<PhoneScript> students;

    //current state of teacher
    public bool teacherWatching;
    
    void Awake()
    {
        Instance = this;

        //HELP: how to locate them???

        int length = (int)studentAmount / 4;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < length; j++)
            {
                Instantiate(prefab, new Vector3(2 * i, 2 * j - 4, 0), Quaternion.identity);
            }           
        }
    }

    void Start()
    {
        HasWon += UIHasWon;
        CurrentProgress = 0;
        teacherWatching = false;
        students = new List<PhoneScript>();
    }

    void UIHasWon()
    {
        //TODO
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentProgress >= ENOUGH * studentAmount)
        {
            Debug.Log("WON");
            HasWon();
        }
        else if (teacherWatching)
        {
            foreach (var item in students)
            {
                if (item.isCheating)
                {
                    Debug.Log("Got you!");
                    item.Sucks();
                }         
            }
        }
    }
}
