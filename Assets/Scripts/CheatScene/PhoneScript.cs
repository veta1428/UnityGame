using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PhoneScript : MonoBehaviour
{
    public bool mouseDown = false;

    //progress for this object (pupil)
    public float progress;
    public bool isCheating;
    public bool cheated;

    private delegate void OnCheat();
    private delegate void OnWaitToCheat();
    private delegate void OnCheated();

    private event OnCheat OnCheatHandler;
    private event OnWaitToCheat OnWaitToCheatHandler;
    private event OnCheated OnCheatedHandler;


    void Start()
    {
        OnCheatHandler += BackendCheating;
        OnCheatHandler += UICheating;

        OnWaitToCheatHandler += BackendWaitToCheat;
        OnWaitToCheatHandler += UIWaitToCheat;

        isCheating = false;
        cheated = false;
        progress = 0;
        ManagerScript.Instance.students.Add(this);
    }

    void Update()
    {
        if (mouseDown)
        {
            ManagerScript.Instance.CurrentProgress += Time.deltaTime;
            progress += Time.deltaTime;
        }
        if (progress >= ManagerScript.ENOUGH)
        {
            ManagerScript.Instance.CurrentProgress += progress - ManagerScript.ENOUGH;
            ManagerScript.Instance.students.Remove(this);
            cheated = true;
            

            //now this student can do nothing
            Debug.Log("Disactivated");
        }
    }

    public void Cheated()
    {
        //TODO:
        //show that student has cheated
    }

    public void Sucks()
    {
        //TODO:
        //show somehow that person was caught by teacher
    }

    void BackendCheating()
    {
        isCheating = true;
    }

    void BackendWaitToCheat()
    {
        isCheating = false;
    }

    void UICheating()
    {
        //TODO:
        //show somehow that person is cheating
        GetComponent<SpriteRenderer>().color = new Color(0f, 1.0f, 0f);
    }

    void UIWaitToCheat()
    {
        //TODO:
        //show that person is not cheating
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
    }

    void OnMouseDown()
    {
        if (!mouseDown && !cheated)
        {
            //start of cheating animation
            mouseDown = true;
            OnCheatHandler();
            Debug.Log("cheating");
        }
        
    }

    void OnMouseUp()
    {
        if (mouseDown && !cheated)
        {
            //start of waiting to cheat animation
            mouseDown = false;
            OnWaitToCheatHandler();
            Debug.Log("not cheating");
        }
    }
}
