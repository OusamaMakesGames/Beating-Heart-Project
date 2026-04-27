using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomizedConversations : MonoBehaviour
{
    public GameObject convocanvas;
    public TalkingScript talking;
    public TMP_Text convotext;
    public StudentState student;
    public string[] topics;
    public StudentID studentsids;

        void Start()
        {
            StartCoroutine(NumberGen());
        }
    void Update()
    {
        if (student.Conversating && !talking.isTalking || student.Conversating && talking.followed != 0 || this.student.Aoi && this.student.TimeScript.TimePeriod != "Festival" && student.Conversating && !talking.isTalking && PlayerPrefs.GetInt("TrendyKilled") != 1 && studentsids.trendystate.InDestination || this.student.Aoi && student.Conversating && !talking.attack.IsKilled && PlayerPrefs.GetInt("TrendyKilled") != 1 && studentsids.trendystate.InDestination)
        {
            convocanvas.SetActive(true);
        }
        if (talking.followed == 1 || talking.isTalking || talking.attack.IsKilled || !student.Conversating || this.student.Aoi && PlayerPrefs.GetInt("TrendyKilled") == 1 || this.student.Aoi && !studentsids.trendystate.InDestination || this.student.Aoi && this.student.TimeScript.TimePeriod == "Festival")
        {
            convocanvas.SetActive(false);
        }
    }
    IEnumerator NumberGen(){
        while(true){
                convotext.text = topics[Random.Range(0, topics.Length)];
                yield return new WaitForSeconds(5);
        }
}
}
