using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour
{
    public string[] List;
    public int Xbox = 0;
    public int PS4 = 0;
    public int NoController = 0;

    void Update()
    {
        List = Input.GetJoystickNames();
        for (int x = 0; x < List.Length; x++)
        {
            print(List[x].Length);
            if (List[x].Length == 19)
            {
                PS4 = 1;
                Xbox = 0;
                NoController = 1;
            }
            if (List[x].Length == 33)
            {
                PS4 = 0;
                Xbox = 1;
                NoController = 1;
            }
            if (List[x].Length == 0)
            {
                PS4 = 0;
                Xbox = 0;
                NoController = 0;
            }
        }

    }
}
