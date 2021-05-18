using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapScript : MonoBehaviour
{

    public bool mouseDown;
    public float time;
    public float timeDown;
    public float tapSpeed;
    public float newTapSpeed;
    public bool isEnding;
    public const float maxLoad = 100;
    public const float minLoad = 0;

    public float delta;
    public float A;
    public float B;

    public float curLoad;

    public delegate void OnTapSpeedChanged();
    public event OnTapSpeedChanged TapSpeedChangedHandler;

    public delegate void OnStart();
    public event OnStart AnimationStartHandler;

    // Start is called before the first frame update

    void Start()
    {
        mouseDown = false;
        timeDown = 0;
        isEnding = false;

        //params
        curLoad = 15;
        
        AnimationStartHandler += UIStartAnimation;
        AnimationStartHandler();
        //params
        A = 5;
        B = 6;
        delta = (float)0.01;

        ManagerTapScript.Instance.tap = this;
    }

    void UIStartAnimation()
    {
        //TODO or not
        //if you want to put some animation
        //again if you need some object reference just ask
    }

    void Update()
    {
        
        time += Time.deltaTime;                
        newTapSpeed = 1 / (time - timeDown);
        if (newTapSpeed >= B)
        {
            if (newTapSpeed - B >= 3)
            {
                curLoad += -(float)delta * 3;
            }
            else if (newTapSpeed - B >= 2)
            {
                curLoad += -(float)delta * 2;
            }
            else
            {
                curLoad += -(float)delta;
            }
        }
        if (newTapSpeed < A)
        {
            if (A - newTapSpeed >= 3)
            {
                curLoad += (float)delta * 3;
            }
            else if (A - newTapSpeed >= 2)
            {
                curLoad += (float)delta * 2;
            }
            else
            {
                curLoad += (float)delta;
            }
        }
        tapSpeed = newTapSpeed;
        if (curLoad < minLoad)
        {
            curLoad = minLoad;
        }
        else if (curLoad > maxLoad)
        {
            curLoad = maxLoad;
        }
    }
     

    void OnMouseDown()
    {
        if (!mouseDown && !isEnding)
        {       
            mouseDown = true;
            timeDown = time;
        }
    }

    void OnMouseUp()
    {
        if (mouseDown && !isEnding)
        {
            mouseDown = false;
        }
    }
}
