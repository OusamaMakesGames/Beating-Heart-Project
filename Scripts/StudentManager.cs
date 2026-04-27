using System.Collections.Generic;
using UnityEngine;

public class StudentManager : MonoBehaviour
{
    public List<StudentState> students = new List<StudentState>();

    public bool IsDestinationTaken(Transform destination)
    {
        foreach (StudentState student in students)
        {
            if (student.Destination == destination)
            {
                return true; // Destination is already taken by another student
            }
        }
        return false; // Destination is not taken
    }
}
