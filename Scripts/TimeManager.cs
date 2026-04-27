using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeManager : MonoBehaviour
{
    public Text timeText;
    public float secondsPerRealSecond = 60f;

    public DateTime currentTime;
    public DateTime classTime, lateClassTime;
    public DateTime lunchTime;
    public DateTime secondClassTime, lateSecondClassTime;
    public DateTime cleaningTime;
    public DateTime homeTime;
    public DateTime originalendTime;
    public DateTime endTime;
    public EvidenceScript evidencescript;
    public GameObject GoHomeCollider;

    public string TimePeriod;

    public Color Origin;
    public Color Night;
    public float t = 0.0f;
    public float duration = 2.0f;

    public AudioSource Bell;

    public ParticleSystem BellEffect;

    public Text PeriodText;

    public Image Icon;

    public Sprite Moon;

    public SunRotation Rotation;

    public ValentinoEvidence Valentino;

    public GameObject Cigarette;

    public UnityEngine.AI.NavMeshAgent Nav;

    public StudentState Yandere;

    public FieldOfView ValentinoFOV;

    public int Info;

    public bool LateForClass1, LateForClass2;

    public ClassScript SakuraClass;

    private void Start()
    {
        RenderSettings.skybox.SetColor("_Tint", Origin);
        currentTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 30, 0);
        if (PlayerPrefs.GetInt("Day") == 2)
        {
            endTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 20, 0, 0);
        }
        else
        {
            endTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 18, 0, 0);
        }
    }

    void Update()
    {
        float realSecondsPassed = Time.deltaTime;
        float gameSecondsPassed = realSecondsPassed * secondsPerRealSecond;
        currentTime = currentTime.AddSeconds(gameSecondsPassed);

        DateTime startTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 7, 30, 0);
        classTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 8, 30, 0);
        lateClassTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 8, 40, 0);
        lunchTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 13, 0, 0);
        secondClassTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 13, 30, 0);
        lateSecondClassTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 13, 40, 0);
        cleaningTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 15, 30, 0);
        homeTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 16, 0, 0);
        originalendTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 18, 0, 0);
        if (currentTime > lateClassTime && currentTime < lunchTime && currentTime < cleaningTime && currentTime < secondClassTime && currentTime < homeTime && currentTime < originalendTime && !SakuraClass.WentToFirstClass)
        {
            if (!LateForClass1)
            {
                this.ValentinoFOV.SakuraScript.InfoSound.Play();
                this.ValentinoFOV.SakuraScript.Info.Play("infoshow");
                this.ValentinoFOV.SakuraScript.infotext.text = "You're late to class!";
                LateForClass1 = true;
            }
        }
        if (currentTime > lateSecondClassTime && currentTime < cleaningTime && currentTime < secondClassTime && currentTime < homeTime && currentTime < originalendTime && !SakuraClass.WentToSecondClass)
        {
            if (!LateForClass2)
            {
                this.ValentinoFOV.SakuraScript.InfoSound.Play();
                this.ValentinoFOV.SakuraScript.Info.Play("infoshow");
                this.ValentinoFOV.SakuraScript.infotext.text = "You're late to class!";
                LateForClass2 = true;
            }
        }

        if (currentTime > classTime && currentTime < lunchTime && currentTime < cleaningTime && currentTime < secondClassTime && currentTime < homeTime && currentTime < originalendTime && TimePeriod != "Class")
        {
            this.Bell.Play();
            this.BellEffect.Play();
            TimePeriod = "Class";
            PeriodText.text = "CLASS TIME";
            this.ValentinoFOV.SakuraScript.InfoSound.Play();
                this.ValentinoFOV.SakuraScript.Info.Play("infoshow");
                this.ValentinoFOV.SakuraScript.infotext.text = "It's time for class!";
            if (PlayerPrefs.GetInt("Day") == 3)
            {
                ValentinoFOV.ValentinoDuration = 3f;
            }
            if (PlayerPrefs.GetInt("Day") == 5)
            {
                Nav.speed = 2.5f;
            }
        }
        if (currentTime > classTime && currentTime < lunchTime && currentTime < cleaningTime && currentTime < secondClassTime && currentTime < homeTime && currentTime < originalendTime)
        {
            if (PlayerPrefs.GetInt("Day") == 3 && !Valentino.SkippingClass && ValentinoFOV.StudentState.InDestination && ValentinoFOV.StudentState.OriginalDestination == ValentinoFOV.StudentState.ClassDestination)
            {
                Valentino.SkippingClass = true;
                if (Info == 0)
                {
                    Info = 1;
                    this.ValentinoFOV.SakuraScript.InfoSound.Play();
                    this.ValentinoFOV.SakuraScript.Info.Play("infoshow");
                    this.ValentinoFOV.SakuraScript.infotext.text = "Valentino Asahi is smoking and skipping class!";
                }
            }
        }
        if (currentTime > classTime && currentTime > lunchTime && currentTime < cleaningTime && currentTime < secondClassTime && currentTime < homeTime && currentTime < originalendTime && TimePeriod != "Lunch")
        {
            this.Bell.Play();
            this.BellEffect.Play();
            TimePeriod = "Lunch";
            PeriodText.text = "LUNCH TIME";
            if (PlayerPrefs.GetInt("Day") == 3)
            {
                Valentino.SkippingClass = false;
            }
            if (PlayerPrefs.GetInt("Day") == 5)
            {
                Nav.speed = 3f;
            }
        }
        if (currentTime > classTime && currentTime > lunchTime && currentTime < cleaningTime && currentTime > secondClassTime && currentTime < homeTime && currentTime < originalendTime && TimePeriod != "Class")
        {
            this.Bell.Play();
            this.BellEffect.Play();
            TimePeriod = "Class";
            PeriodText.text = "CLASS TIME";
            this.ValentinoFOV.SakuraScript.InfoSound.Play();
                this.ValentinoFOV.SakuraScript.Info.Play("infoshow");
                this.ValentinoFOV.SakuraScript.infotext.text = "It's time for class!";
            if (PlayerPrefs.GetInt("Day") == 5)
            {
                Nav.speed = 3.5f;
            }
        }
        if (currentTime > classTime && currentTime > lunchTime && currentTime < cleaningTime && currentTime > secondClassTime && currentTime < homeTime && currentTime < originalendTime)
        {
            if (PlayerPrefs.GetInt("Day") == 3 && !Valentino.SkippingClass && ValentinoFOV.StudentState.InDestination && ValentinoFOV.StudentState.OriginalDestination == ValentinoFOV.StudentState.ClassDestination)
            {
                Valentino.SkippingClass = true;
                if (Info == 1)
                {
                    Info = 2;
                    this.ValentinoFOV.SakuraScript.InfoSound.Play();
                    this.ValentinoFOV.SakuraScript.Info.Play("infoshow");
                    this.ValentinoFOV.SakuraScript.infotext.text = "Valentino Asahi is smoking and skipping class!";
                }
            }
        }
        if (currentTime > classTime && currentTime > lunchTime && currentTime > cleaningTime && currentTime > secondClassTime && currentTime < homeTime && currentTime < originalendTime && TimePeriod != "Cleaning")
        {
            this.Bell.Play();
            this.BellEffect.Play();
            TimePeriod = "Cleaning";
            PeriodText.text = "CLEANING TIME";
            if (PlayerPrefs.GetInt("Day") == 3)
            {
                ValentinoFOV.ValentinoDuration = 1f;
                Valentino.SkippingClass = false;
            }
            if (PlayerPrefs.GetInt("Day") == 5)
            {
                Nav.speed = 4f;
                Yandere.WalkName = "Run";
            }
        }
        if (currentTime > classTime && currentTime > lunchTime && currentTime > cleaningTime && currentTime > secondClassTime && currentTime < homeTime && currentTime < originalendTime)
        {
            if (PlayerPrefs.GetInt("Day") == 3 && !Valentino.SkippingDuties && ValentinoFOV.StudentState.InDestination && ValentinoFOV.StudentState.OriginalDestination == ValentinoFOV.StudentState.CleanDestination)
            {
                Valentino.SkippingDuties = true;
                if (Info == 2)
                {
                    Info = 3;
                    this.ValentinoFOV.SakuraScript.InfoSound.Play();
                    this.ValentinoFOV.SakuraScript.Info.Play("infoshow");
                    this.ValentinoFOV.SakuraScript.infotext.text = "Valentino Asahi is smoking and skipping cleaning duties!";
                }
            }
        }
        if (currentTime > homeTime)
        {
            GoHomeCollider.SetActive(true);
        }
        if (currentTime > classTime && currentTime > lunchTime && currentTime > cleaningTime && currentTime > secondClassTime && currentTime > homeTime && currentTime < originalendTime && TimePeriod != "EndOfDay")
        {
            this.Bell.Play();
            this.BellEffect.Play();
            TimePeriod = "EndOfDay";
            PeriodText.text = "AFTER SCHOOL";
            if (PlayerPrefs.GetInt("Day") == 3)
            {
                Valentino.SkippingDuties = false;
            }
            if (PlayerPrefs.GetInt("Day") == 5)
            {
                Nav.speed = 4.5f;
                Yandere.WalkName = "Run";
            }
        }
        if (currentTime > classTime && currentTime > lunchTime && currentTime > cleaningTime && currentTime > secondClassTime && currentTime > homeTime && currentTime > originalendTime && PlayerPrefs.GetInt("Day") == 2 && TimePeriod != "Festival")
        {
            this.Bell.Play();
            this.BellEffect.Play();
            TimePeriod = "Festival";
            PeriodText.text = "FESTIVAL TIME";

            Rotation.moving = false;
            Rotation.rectTransform.eulerAngles = new Vector3(0f, 0f, 0f);
            Icon.sprite = Moon;
            t += Time.deltaTime / duration;
            t = Mathf.Clamp01(t);
            RenderSettings.skybox.SetColor("_Tint", Color.Lerp(Origin, Night, t));
        }

        // Update the text
        timeText.text = currentTime.ToString("h:mm tt");
    }
}
