using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour
{
    
    //in inspector
    public GameObject prefab;

    public TeacherScript teacher;

    //singletone
    public static ManagerScript Instance { get; private set; }

    public delegate void OnWinning();

    public event OnWinning HasWon;

    public int studentAmount;

    public UnityEngine.UI.Button MyButton;

    //needed to cheat for 1 student
    public const float ENOUGH = 3;

    //summarised progress
    public float CurrentProgress;

    //how often teacher starts watching
    public const int FREQUENCY = 8;

    public List<PhoneScript> students;

    //current state of teacher
    public bool teacherWatching;

    public delegate void OnTheEnd();
    public event OnTheEnd OnTheEndHandler;

    public void EndGotHim()
    {

        Clear();
        EndScript end = EndScript.Instance;
        end.gameObject.SetActive(true);
        //end.btn.GetComponent<UnityEngine.UI.Button>().GetComponent<Text>().text = "You've lost! The teacher saw you cheating!";
        end.btn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClick);      
        //Debug.Log("button");       
    }

    private void Clear()
    {
        foreach (var item in students)
        {
            item.gameObject.SetActive(false);
        }

        teacher.gameObject.SetActive(false);
    }    

    public void OnClick()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void EndHappened()
    {
        OnTheEndHandler += EndGotHim;
        OnTheEndHandler();
    }

    public void EndedTime()
    {
        OnTheEndHandler += EndOfTime;
        OnTheEndHandler();
    }

    public void EndOfTime()
    {
        Clear();
        EndScript end = EndScript.Instance;
        end.gameObject.SetActive(true);
        //end.btn.GetComponent<Text>().text = "You've lost! The lesson is over!";
            
        end.btn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClick);
    }

    void Awake()
    {
        Instance = this;
        studentAmount = 4;
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
                    teacher.TeacherAngry();                  
                }         
            }
        }
    }
}
