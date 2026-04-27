using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFunction : MonoBehaviour
{
    public BloodRemover Blood;

    void Start()
    {
        StartCoroutine(ExecuteAfterTime(1));
    }

    IEnumerator ExecuteAfterTime(float time)
{
    yield return new WaitForSeconds(time);

    Blood.enabled = true;
}
}
