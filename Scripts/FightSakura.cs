using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;

public class FightSakura : MonoBehaviour
{

    public bool CanReact, Alarmed, Hazu, Fighting, HasPlayedAnimation, StartSakuraRotation, Kicked;

    public Animator StudentAnimator, SakuraAnimator;

    public Transform StudentTransform, SakuraTransform, Teacher2;

    public NavMeshAgent PathAgent;

    public StudentState StudentState;

    public PlayerController SakuraScript;
    public Prompt PromptScript;
    public TalkingScript TalkingSc;
    public EvidenceScript EvidenceSc;
    public TalkingBools Bools;
    public TeacherBools BoolScript;
    public DetectionIcon Detection;

    public GameObject GameOverScreen, Sakura, Sakura2;

    public GameOver GameOverScript;

    public bool Detected;

    public GameObject PromptCanvas;


    public GameObject StruggleKey;

    public GoHomeScript GoHome;

    public KillSakura Kill;

    Bloom bloom;

    public NavMeshAgent Nav;

    public GameObject FakeKnife, RealKnife, HomeSpawn, FOVObject;

    public TimeManager TimeScript;

    public GameObject Weapon;

    public FieldOfView FOV;
    public bool StartYandereRotation;
    public Transform HazuTransform, RealKnifeTransform;

    public AudioSource Music;

    public AudioClip PreviousMusic, NewMusic, WinningMusic;

    void Start()
    {
        FOV.volume.profile.TryGetSettings(out bloom);
    }

    void Update()
    {
        if (!HomeSpawn.activeSelf)
        {
            this.TalkingSc.attack.enabled = true;
        }
        if (Fighting && !gameObject.GetComponent<AttackScript>().IsKilled)
        {
            this.StudentAnimator.SetLayerWeight(3, 1f);
            this.PromptScript.Distance = 0f;
            this.CanReact = false;
            if (this.BoolScript.won)
            {
                var children = transform.GetComponentsInChildren<Transform>(includeInactive: true);
                foreach (var child in children)
                {
                    child.gameObject.layer = 17;
                }
                Music.enabled = true;
                FakeKnife.SetActive(false);
                Music.clip = PreviousMusic;
                Music.Play();
                this.SakuraScript.Fighting = false;
                this.Kill.CanKill = false;
                GoHome.enabled = true;
                GoHome.PromptScript.Distance = 1f;
                FOVObject.SetActive(false);
                SakuraScript.bools.Phone.OnCooldown = false;
                this.SakuraScript.anim.SetLayerWeight(12, 0f);
                PromptScript.enabled = true;
                StruggleKey.SetActive(false);
                TimeScript.enabled = true;
                Bools.Prompts.ClearAllPrompts = false;
                PromptCanvas.SetActive(true);
                TalkingSc.attack.boxcol.enabled = false;
                TalkingSc.attack.charactercont.enabled = false;
                TalkingSc.attack.enabled = true;
                RealKnife.SetActive(true);
                RealKnife.transform.position = RealKnifeTransform.position;
                if (!StartYandereRotation)
                {
                    this.SakuraScript.CanMove = true;
                }
                this.SakuraScript.enabled = true;
                this.Nav.enabled = false;
                this.Fighting = false;
                this.PathAgent.enabled = false;
                if (!HasPlayedAnimation)
                {
                    this.StudentAnimator.Play("Down");
                    HasPlayedAnimation = true;
                }
                if (!Kicked)
                {
                    Kicked = true;
                    this.SakuraScript.anim.Play("Kick");
                }
                this.BoolScript.won = false;
                this.BoolScript.lost = false;
            }
            if (this.BoolScript.lost)
            {
                this.SakuraScript.Fighting = false;
                Music.enabled = true;
                Music.clip = PreviousMusic;
                Music.Play();
                this.Kill.enabled = true;
                this.Kill.CanKill = true;
                this.PromptCanvas.SetActive(false);
                this.PathAgent.enabled = false;
                this.BoolScript.won = false;
                GoHome.fov.DropNonWeapons();
                GoHome.fov.DropOtherItems();
                GoHome.fov.BoolScript.lost = false;
            }
        }
        float DistanceToSakura = Vector3.Distance(StudentTransform.position, Sakura.transform.position);
        if (DistanceToSakura <= 2f && this.Fighting && PromptCanvas.activeSelf && !gameObject.GetComponent<AttackScript>().IsKilled)
        {
            GoHome.AIPath2.enabled = false;
            if (SakuraScript.CurrentFightingCharacter != null && SakuraScript.CurrentFightingCharacter != gameObject)
            {
                SakuraScript.CurrentFightingCharacter.GetComponent<FieldOfView>().Detection.HideDetection();
                SakuraScript.CurrentFightingCharacter.GetComponent<FieldOfView>().StruggleKey.SetActive(false);
                SakuraScript.CurrentFightingCharacter.GetComponent<FieldOfView>().StartCoroutine("FightReactionFunction");
                SakuraScript.CurrentFightingCharacter.GetComponent<FieldOfView>().Fighting = false;
            }
            SakuraScript.CurrentFightingCharacter = gameObject;
            FOVObject.SetActive(false);
            FakeKnife.SetActive(true);
            Detected = false;
            this.BoolScript.won = false;
            this.BoolScript.lost = false;
            this.Bools.Prompts.ClearAllPrompts = true;
            if (!FOV.DoneStuff)
            {
                PreviousMusic = Music.clip;
                this.Bools.Phone.StartCoroutine(Bools.Phone.QuitPhoneCaught());
                Music.enabled = true;
                Music.clip = NewMusic;
                Music.Play();
                Music.UnPause();
                FOV.DoneStuff = true;
            }
            this.StudentState.enabled = false;
            this.StudentState.ThirstUpdating = false;
            GoHome.fov.DropNonWeaponsTeachers();
            if (!StruggleKey.activeSelf)
            {
                Time.timeScale = 1f;
                StruggleKey.SetActive(true);
            }
            this.SakuraScript.Sakura.layer = 15;
            this.SakuraScript.Fighting = true;
            this.PathAgent.enabled = false;
            this.Sakura2.transform.position = this.Teacher2.position;
            this.Sakura2.transform.rotation = this.Teacher2.rotation;
            if (!this.BoolScript.won && !this.BoolScript.lost)
            {
                this.SakuraScript.anim.Play("Struggle1");
                this.StudentAnimator.Play("Struggle1");
            }
            this.SakuraScript.CanMove = false;
            this.SakuraScript.enabled = false;
            this.Fighting = true;
        }
        if (StartYandereRotation)
        {
            Quaternion targetRotation = Quaternion.LookRotation(HazuTransform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 6 * Time.deltaTime);
        }
    }

    public void GameOver2()
    {
        this.GameOverScript.GameOverText.text = "MURDERED";
        this.GameOverScript.GameOverExplanation.text = "That was unfortunate...";
        this.GameOverScreen.SetActive(true);
    }

    public void CaughtByHazu()
    {
        StruggleKey.GetComponent<Struggle>().VoiceLine.Stop();
        StruggleKey.GetComponent<Struggle>().VoiceLine1.Stop();
        SakuraScript.enabled = false;
        SakuraScript.CanMove = false;
        StruggleKey.SetActive(false);
        FakeKnife.SetActive(false);
        StartYandereRotation = true;
        StudentAnimator.SetLayerWeight(10, 1f);
        StudentAnimator.SetLayerWeight(11, 1f);
    }

    void ActivateFighting()
    {
        Fighting = true;
    }

    public IEnumerator FightReactionFunction()
    {
        Music.enabled = true;
        Music.clip = PreviousMusic;
        Music.Play();
        FakeKnife.SetActive(false);
        Invoke("ActivateFighting", 5f);
        FOV.CallingPolice = true;
        FOV.PathAgent.enabled = true;
        FOV.StudentState.InEvent = false;
        FOV.Detected = false;
        FOV.PathAgent.isStopped = false;
        FOV.PathAgent.speed = 4f;
        FOV.RunningAway = true;
        FOV.PathAgent.SetDestination(FOV.RunAway.position);
        FOV.StudentState.enabled = false; FOV.CancelInvoke("BackToState"); FOV.Looking = false; FOV.Turn = false; FOV.CancelInvoke("Investigate");
        FOV.TalkingSc.QuitMenu();
        FOV.TalkingSc.followed = 0;
        FOV.TalkingSc.attack.CantTalk = true;
        bloom.intensity.value = FOV.SakuraScript.CaughtBloom;
        FOV.TalkingSc.Alarmed = true; FOV.Investigating = false;
        FOV.Alarmed = true;
        FOV.FollowScript.enabled = false;
        FOV.Scream.Play();
        FOV.SakuraScript.Noise.transform.position = FOV.transform.position;
        FOV.StudentState.enabled = false; FOV.CancelInvoke("BackToState"); FOV.Looking = false; FOV.Turn = false; FOV.CancelInvoke("Investigate");
        FOV.StudentState.NearVendingMachine = false;
        FOV.StudentAnimator.ResetTrigger(FOV.StudentState.WalkName);
        FOV.StudentAnimator.SetTrigger("Run");
        FOV.StudentAnimator.ResetTrigger(FOV.StudentState.AnimationName);
        FOV.StudentAnimator.SetLayerWeight(3, 1f);
        yield return new WaitForSeconds(0F);
    }
}
