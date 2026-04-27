using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Don't Forget This

public class QTE : MonoBehaviour
{

    public Slider quickTimeSlider;
    public Text keyToPress;

    bool freeze;//Stops slider from moving

    public bool rapidPress; //The Type Of Quick Time Event
    public int decreaseSpeed;//Speed Slider Decreases    

    KeyCode key;//The Key That Should Be Pressed To React To The QTE
    KeyCode[] availableOptions = { KeyCode.Alpha1, KeyCode.Alpha2 }; //Numerical Keys 1 And 2, Chosen Randomly For Player To React To


    // Use this for initialization
    void Start()
    {
        //Randomly Selects The Key That Should Be Pressed And Displays It To The Player
        int rand = Random.Range(0, 2);
        key = availableOptions[rand];
        keyToPress.text = availableOptions[rand].ToString();

        //The Type Of QTE Determines Where The Slider Starts
        if (rapidPress)
        {
            quickTimeSlider.value = 5;//Start In The Middle, The Player Has To Quickly Press The Key To Make The Meter Full
        }
        else
        {
            quickTimeSlider.value = 10;//Start At The End, The Player Has To Press The Button At Least Once To Stop The Timer
        }
    }


    void Update()
    {
        //Move Value Of Slider Towards 0, If The QTE Has Yet To Be Passed Or Failed       
        if (!freeze)
        {
            quickTimeSlider.value = Mathf.MoveTowards(quickTimeSlider.value, 0, decreaseSpeed * Time.deltaTime);
        }

        if (rapidPress)
        {
            if (Input.GetKeyDown(key) && quickTimeSlider.value > 0)
            {
                quickTimeSlider.value += 1;
                if (quickTimeSlider.value == 10)
                {
                    keyToPress.text = "Pass!";
                    freeze = true;
                }
            }
        }

        if (!rapidPress)
        {
            if (Input.GetKeyDown(key) && quickTimeSlider.value > 0)
            {
                keyToPress.text = "Pass!";
                freeze = true;
            }
        }

        if (quickTimeSlider.value == 0.9)
        {
            keyToPress.text = "Fail!";
            freeze = true;
        }
    }
}