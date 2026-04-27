using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentID : MonoBehaviour
{
    public AttackScript AkimuraAttack, AoiAttack, BoyAttack, PurpleAttack, BlueAttack, GreenAttack, TrendyAttack, NarikoAttack, AganaAttack, ReinaAttack, SuzukiAttack, KoujiAttack, HanaAttack, ChiyokoAttack, ValentinoAttack, YukiraAttack, Sensei1Attack, Sensei2Attack;
    public StudentState trendystate, greenstate, aoistate;
    public DeadStudents dead;
    public HoldRadio RadioScript;
    public TalkingScript HazuTalkingScript;

    void Update()
    {
        PlayerPrefs.SetFloat("Lovebar", HazuTalkingScript.LoveBarSlider.value);
        if (HazuTalkingScript.PoemTopicZero)
        {
            PlayerPrefs.SetInt("PoemTopic", 0);
        }
        if (AkimuraAttack.cupcakescript.HasCupcake)
        {
            PlayerPrefs.SetInt("HasCupcake", 1);
        }
        else
        {
            PlayerPrefs.SetInt("HasCupcake", 0);
        }
        if (RadioScript.RadioHiddenInside)
        {
            PlayerPrefs.SetInt("RadioHiddenInside", 1);
        }
        else
        {
            PlayerPrefs.SetInt("RadioHiddenInside", 0);
        }
        if (AkimuraAttack.movementscript.bools.Phone.RobotBought)
        {
            PlayerPrefs.SetInt("RobotBought", 1);
        }
        if (AkimuraAttack.movementscript.bools.Phone.PoisonBought)
        {
            PlayerPrefs.SetInt("PoisonBought", 1);
        }
        PlayerPrefs.SetInt("UniformBought", AkimuraAttack.movementscript.bools.Phone.UniformBought);
        PlayerPrefs.SetFloat("amount", AkimuraAttack.movementscript.Money);
        PlayerPrefs.SetString("Club", AkimuraAttack.movementscript.Club);

        PlayerPrefs.SetInt("JoinedLiteratureBefore", AkimuraAttack.movementscript.bools.Phone.JoinedLiteratureBefore);
        PlayerPrefs.SetInt("JoinedGardeningBefore", AkimuraAttack.movementscript.bools.Phone.JoinedGardeningBefore);
        PlayerPrefs.SetInt("JoinedSportsBefore", AkimuraAttack.movementscript.bools.Phone.JoinedSportsBefore);
        PlayerPrefs.SetInt("JoinedScienceBefore", AkimuraAttack.movementscript.bools.Phone.JoinedScienceBefore);
        PlayerPrefs.SetInt("JoinedArtBefore", AkimuraAttack.movementscript.bools.Phone.JoinedArtBefore);

        PlayerPrefs.SetInt("LiteratureClubRelationship", AkimuraAttack.movementscript.bools.Phone.LiteratureClubRelationship);
        PlayerPrefs.SetInt("GardeningClubRelationship", AkimuraAttack.movementscript.bools.Phone.GardeningClubRelationship);
        PlayerPrefs.SetInt("SportsClubRelationship", AkimuraAttack.movementscript.bools.Phone.SportsClubRelationship);
        PlayerPrefs.SetInt("ScienceClubRelationship", AkimuraAttack.movementscript.bools.Phone.ScienceClubRelationship);
        PlayerPrefs.SetInt("ArtClubRelationship", AkimuraAttack.movementscript.bools.Phone.ArtClubRelationship);
        PlayerPrefs.SetInt("Pills", AkimuraAttack.movementscript.Pills);
        PlayerPrefs.Save();
        //Killed
        if (this.AkimuraAttack.IsKilled && PlayerPrefs.GetInt("AkimuraMovedSchools") == 0 && PlayerPrefs.GetInt("AkimuraKilled") == 0)
        {
            PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1);
            PlayerPrefs.SetInt("AkimuraKilled", 1);
        }
        if (this.AkimuraAttack.IsKilled && PlayerPrefs.GetInt("AkimuraMovedSchools") == 1)
        {
            PlayerPrefs.SetInt("AkimuraKilled", 1);
        }
        if (this.AoiAttack.IsKilled && PlayerPrefs.GetInt("AoiKilled") == 0)
        {
            PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1);
            PlayerPrefs.SetInt("AoiKilled", 1);
        }
        if (this.BoyAttack.IsKilled && PlayerPrefs.GetInt("BoyKilled") == 0)
        {
            PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1);
            PlayerPrefs.SetInt("BoyKilled", 1);
        }
        if (this.PurpleAttack.IsKilled && PlayerPrefs.GetInt("PurpleKilled") == 0)
        {
            PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1);
            PlayerPrefs.SetInt("PurpleKilled", 1);
        }
        if (this.BlueAttack.IsKilled && PlayerPrefs.GetInt("BlueKilled") == 0)
        {
            PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1);
            PlayerPrefs.SetInt("BlueKilled", 1);
        }
        if (this.ReinaAttack.IsKilled && PlayerPrefs.GetInt("ReinaKilled") == 0)
        {
            PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1);
            PlayerPrefs.SetInt("ReinaKilled", 1);
        }
        if (this.SuzukiAttack.IsKilled && PlayerPrefs.GetInt("SuzukiKilled") == 0)
        {
            PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1);
            PlayerPrefs.SetInt("SuzukiKilled", 1);
        }
        if (this.HanaAttack.IsKilled && PlayerPrefs.GetInt("HanaKilled") == 0)
        {
            PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1);
            PlayerPrefs.SetInt("HanaKilled", 1);
        }
        if (this.KoujiAttack.IsKilled && PlayerPrefs.GetInt("KoujiKilled") == 0)
        {
            PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1);
            PlayerPrefs.SetInt("KoujiKilled", 1);
        }
        if (this.GreenAttack.IsKilled)
        {
            PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1);
            PlayerPrefs.SetInt("GreenKilled", 1);
        }
        if (this.TrendyAttack.IsKilled && PlayerPrefs.GetInt("TrendyKilled") == 0)
        {
            PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1);
            PlayerPrefs.SetInt("TrendyKilled", 1);
        }
        if (this.NarikoAttack.IsKilled && PlayerPrefs.GetInt("NarikoKilled") == 0)
        {
            PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1);
            PlayerPrefs.SetInt("NarikoKilled", 1);
        }
        if (this.AganaAttack.IsKilled && PlayerPrefs.GetInt("AganaKilled") == 0)
        {
            PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1);
            PlayerPrefs.SetInt("AganaKilled", 1);
        }
        if (this.ChiyokoAttack.IsKilled && PlayerPrefs.GetInt("ChiyokoKilled") == 0)
        {
            PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1);
            PlayerPrefs.SetInt("ChiyokoKilled", 1);
        }
        if (this.ValentinoAttack.IsKilled && PlayerPrefs.GetInt("ValentinoKilled") == 0)
        {
            PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1);
            PlayerPrefs.SetInt("ValentinoKilled", 1);
        }
        if (this.YukiraAttack.IsKilled && PlayerPrefs.GetInt("YukiraKilled") == 0)
        {
            PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1);
            PlayerPrefs.SetInt("YukiraKilled", 1);
        }
        if (this.Sensei1Attack.IsKilled && PlayerPrefs.GetInt("Sensei1Killed") == 0)
        {
            PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1);
            PlayerPrefs.SetInt("Sensei1Killed", 1);
        }
        if (this.Sensei2Attack.IsKilled && PlayerPrefs.GetInt("Sensei2Killed") == 0)
        {
            PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1);
            PlayerPrefs.SetInt("Sensei2Killed", 1);
        }

        //TaskDone
        if (this.ReinaAttack.TaskDone)
        {
            PlayerPrefs.SetInt("ReinaComplete", 1);
        }
        if (this.SuzukiAttack.TaskDone)
        {
            PlayerPrefs.SetInt("SuzukiComplete", 1);
        }
        if (this.HanaAttack.TaskDone)
        {
            PlayerPrefs.SetInt("HanaComplete", 1);
        }
        if (this.KoujiAttack.TaskDone)
        {
            PlayerPrefs.SetInt("KoujiComplete", 1);
        }
        if (this.AkimuraAttack.TaskDone)
        {
            PlayerPrefs.SetInt("AkimuraComplete", 1);
        }
        if (this.AoiAttack.TaskDone)
        {
            PlayerPrefs.SetInt("AoiComplete", 1);
        }
        if (this.BoyAttack.TaskDone)
        {
            PlayerPrefs.SetInt("BoyComplete", 1);
        }
        if (this.PurpleAttack.TaskDone)
        {
            PlayerPrefs.SetInt("PurpleComplete", 1);
        }
        if (this.BlueAttack.TaskDone)
        {
            PlayerPrefs.SetInt("BlueComplete", 1);
        }
        if (this.GreenAttack.TaskDone)
        {
            PlayerPrefs.SetInt("GreenComplete", 1);
        }
        if (this.TrendyAttack.TaskDone)
        {
            PlayerPrefs.SetInt("TrendyComplete", 1);
        }
        if (this.NarikoAttack.TaskDone)
        {
            PlayerPrefs.SetInt("NarikoComplete", 1);
        }
        if (this.AganaAttack.TaskDone)
        {
            PlayerPrefs.SetInt("AganaComplete", 1);
        }
        if (this.ChiyokoAttack.TaskDone)
        {
            PlayerPrefs.SetInt("ChiyokoComplete", 1);
        }
        if (this.ValentinoAttack.TaskDone)
        {
            PlayerPrefs.SetInt("ValentinoComplete", 1);
        }

        //New
        if (this.ChiyokoAttack.CantTalk)
        {
            PlayerPrefs.SetInt("ChiyokoCantTalk", 1);
        }
        if (this.ValentinoAttack.CantTalk)
        {
            PlayerPrefs.SetInt("ValentinoCantTalk", 1);
        }
        if (this.AkimuraAttack.CantTalk)
        {
            PlayerPrefs.SetInt("AkimuraCantTalk", 1);
        }
        if (this.AoiAttack.CantTalk)
        {
            PlayerPrefs.SetInt("AoiCantTalk", 1);
        }
        if (this.BoyAttack.CantTalk)
        {
            PlayerPrefs.SetInt("BoyCantTalk", 1);
        }
        if (this.PurpleAttack.CantTalk)
        {
            PlayerPrefs.SetInt("PurpleCantTalk", 1);
        }
        if (this.BlueAttack.CantTalk)
        {
            PlayerPrefs.SetInt("BlueCantTalk", 1);
        }
        if (this.GreenAttack.CantTalk)
        {
            PlayerPrefs.SetInt("GreenCantTalk", 1);
        }
        if (this.TrendyAttack.CantTalk)
        {
            PlayerPrefs.SetInt("TrendyCantTalk", 1);
        }
        if (this.NarikoAttack.CantTalk)
        {
            PlayerPrefs.SetInt("NarikoCantTalk", 1);
        }
        if (this.AganaAttack.CantTalk)
        {
            PlayerPrefs.SetInt("AganaCantTalk", 1);
        }
        if (this.HanaAttack.CantTalk)
        {
            PlayerPrefs.SetInt("HanaCantTalk", 1);
        }
        if (this.ReinaAttack.CantTalk)
        {
            PlayerPrefs.SetInt("ReinaCantTalk", 1);
        }
        if (this.SuzukiAttack.CantTalk)
        {
            PlayerPrefs.SetInt("SuzukiCantTalk", 1);
        }
        if (this.KoujiAttack.CantTalk)
        {
            PlayerPrefs.SetInt("KoujiCantTalk", 1);
        }
    }
}
