using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollHands : MonoBehaviour
{
    public EasterEggs EasterScript;
    public Vector3 Scale;

    void Update()
    {
        if (EasterScript != null)
        {
            if (EasterScript.CurrentEasterEgg == "DollHands")
            {
                this.transform.localScale = this.Scale;
            }
            else if (EasterScript.CurrentEasterEgg == "NormalHands")
            {
                this.transform.localScale = Vector3.one;
            }
        }
    }
}
