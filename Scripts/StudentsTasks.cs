using UnityEngine;

public class StudentsTasks : MonoBehaviour
{
    public StudentID IDScript;

    void Start()
    {
        if (!IDScript.trendystate.InDestination)
        {
            IDScript.aoistate.AnimationName = "Phone";
        }
        if (IDScript.trendystate.InDestination && PlayerPrefs.GetInt("TrendyKilled") != 1)
        {
            IDScript.aoistate.AnimationName = "Talking2";
        }
        if (PlayerPrefs.GetInt("BoyKilled") == 1)
        {
            IDScript.dead.sorastate.head.enabled = false;
        }
        else
        {
            IDScript.dead.sorastate.head.enabled = true;
        }
        if (PlayerPrefs.GetInt("BlueKilled") == 1)
        {
            IDScript.dead.youkistate.head.enabled = false;
        }
        else
        {
            IDScript.dead.youkistate.head.enabled = true;
        }
        if (PlayerPrefs.GetInt("NarikoKilled") == 1)
        {
            IDScript.dead.aganastate.NavAgent.speed = 2f;
            IDScript.dead.aganastate.head.enabled = false;
        }
        if (PlayerPrefs.GetInt("AganaKilled") == 1)
        {
            IDScript.dead.narikostate.NavAgent.speed = 2f;
            IDScript.dead.narikostate.head.enabled = false;
        }
    }

    void Update()
    {
        //Checking
        if (PlayerPrefs.GetInt("ReinaComplete") == 1)
        {
            this.IDScript.ReinaAttack.TaskDone = true;
        }
        if (PlayerPrefs.GetInt("SuzukiComplete") == 1)
        {
            this.IDScript.SuzukiAttack.TaskDone = true;
        }
        if (PlayerPrefs.GetInt("HanaComplete") == 1)
        {
            this.IDScript.HanaAttack.TaskDone = true;
        }
        if (PlayerPrefs.GetInt("KoujiComplete") == 1)
        {
            this.IDScript.KoujiAttack.TaskDone = true;
        }
        if (PlayerPrefs.GetInt("AkimuraComplete") == 1)
        {
            this.IDScript.AkimuraAttack.TaskDone = true;
        }
        if (PlayerPrefs.GetInt("AoiComplete") == 1)
        {
            this.IDScript.AoiAttack.TaskDone = true;
        }
        if (PlayerPrefs.GetInt("BoyComplete") == 1)
        {
            this.IDScript.BoyAttack.TaskDone = true;
        }
        if (PlayerPrefs.GetInt("PurpleComplete") == 1)
        {
            this.IDScript.PurpleAttack.TaskDone = true;
        }
        if (PlayerPrefs.GetInt("BlueComplete") == 1)
        {
            this.IDScript.BlueAttack.TaskDone = true;
        }
        if (PlayerPrefs.GetInt("GreenComplete") == 1)
        {
            this.IDScript.GreenAttack.TaskDone = true;
        }
        if (PlayerPrefs.GetInt("TrendyComplete") == 1)
        {
            this.IDScript.TrendyAttack.TaskDone = true;
        }
        if (PlayerPrefs.GetInt("NarikoComplete") == 1)
        {
            this.IDScript.NarikoAttack.TaskDone = true;
        }
        if (PlayerPrefs.GetInt("AganaComplete") == 1)
        {
            this.IDScript.AganaAttack.TaskDone = true;
        }
        if (PlayerPrefs.GetInt("ChiyokoComplete") == 1)
        {
            this.IDScript.ChiyokoAttack.TaskDone = true;
        }
        if (PlayerPrefs.GetInt("ValentinoComplete") == 1)
        {
            this.IDScript.ValentinoAttack.TaskDone = true;
        }

        if (PlayerPrefs.GetInt("AkimuraCantTalk") == 1)
        {
            this.IDScript.AkimuraAttack.talkingsc.enabled = false;
        }
        if (PlayerPrefs.GetInt("AoiCantTalk") == 1)
        {
            this.IDScript.AoiAttack.talkingsc.enabled = false;
        }
        if (PlayerPrefs.GetInt("BoyCantTalk") == 1)
        {
            this.IDScript.BoyAttack.talkingsc.enabled = false;
        }
        if (PlayerPrefs.GetInt("PurpleCantTalk") == 1)
        {
            this.IDScript.PurpleAttack.talkingsc.enabled = false;
        }
        if (PlayerPrefs.GetInt("BlueCantTalk") == 1)
        {
            this.IDScript.BlueAttack.talkingsc.enabled = false;
        }
        if (PlayerPrefs.GetInt("GreenCantTalk") == 1)
        {
            this.IDScript.GreenAttack.talkingsc.enabled = false;
        }
        if (PlayerPrefs.GetInt("TrendyCantTalk") == 1)
        {
            this.IDScript.TrendyAttack.talkingsc.enabled = false;
        }
        if (PlayerPrefs.GetInt("NarikoCantTalk") == 1)
        {
            this.IDScript.NarikoAttack.talkingsc.enabled = false;
        }
        if (PlayerPrefs.GetInt("AganaCantTalk") == 1)
        {
            this.IDScript.AganaAttack.talkingsc.enabled = false;
        }
        if (PlayerPrefs.GetInt("KoujiCantTalk") == 1)
        {
            this.IDScript.KoujiAttack.talkingsc.enabled = false;
        }
        if (PlayerPrefs.GetInt("SuzukiCantTalk") == 1)
        {
            this.IDScript.SuzukiAttack.talkingsc.enabled = false;
        }
        if (PlayerPrefs.GetInt("ReinaCantTalk") == 1)
        {
            this.IDScript.ReinaAttack.talkingsc.enabled = false;
        }
        if (PlayerPrefs.GetInt("HanaCantTalk") == 1)
        {
            this.IDScript.HanaAttack.talkingsc.enabled = false;
        }
        if (PlayerPrefs.GetInt("ChiyokoCantTalk") == 1)
        {
            this.IDScript.ChiyokoAttack.talkingsc.enabled = false;
        }
        if (PlayerPrefs.GetInt("ValentinoCantTalk") == 1)
        {
            this.IDScript.ValentinoAttack.talkingsc.enabled = false;
        }
    }
}
