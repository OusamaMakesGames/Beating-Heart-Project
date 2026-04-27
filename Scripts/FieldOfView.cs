using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;

public class FieldOfView : MonoBehaviour
{
    public LayerMask CorpseMask, PlayerMask, BloodMask, ObstacleMask, AimingMask, EmptyMask, YandereMask;

    public bool CanReact, Alarmed, CanSeePlayer, SakuraBeingSeen, CanSeeAiming, CanSeeCorpse, CanSeeBlood, Teacher, Hazu, Akimura, CorpseFound, PlayerFound, BloodFound, Fighting, HasPhone, HasPlayedAnimation, StartSakuraRotation, IsGoingAfterSakura, Kicked, FightReacted;

    public Animator StudentAnimator, SakuraAnimator;

    public Transform StudentTransform, RunAway, SakuraTransform, Teacher2;

    public NavMeshAgent PathAgent;

    public StudentState StudentState;

    public PlayerController SakuraScript;

    public AudioSource Scream;
    public AudioClip[] audioSources;

    public Text StudentText;
    public string StudentReaction, BloodyReaction, MurderReaction, CorpseReaction, BloodPuddleReaction;

    public FollowPlayer FollowScript;
    public Prompt PromptScript;
    public TalkingScript TalkingSc;
    public EvidenceScript EvidenceSc;
    public TalkingBools Bools;
    public TeacherBools BoolScript;
    public DetectionIcon Detection;

    public GameObject BlackScreen, EvidenceScreen, GameOverScreen, Sakura, Sakura2;

    public GameOver GameOverScript;

    public float Distance;
    public float ViewRadius = 18f;
    public float ViewAngle = 90f;
    public Vector3 Quaternion2;

    public PostProcessVolume volume;
    Bloom bloom;

    public Vector3 DestroyedPosition;

    public bool CallingPolice, Detected, wasBeingSeen;

    public GameObject PromptCanvas;

    public HazuScript hazu;

    public GameObject StruggleKey;

    public bool RunningAway, Yandere, CanChase;
    public CupcakeScript Cupcake;

    public float ValentinoDuration;

    public bool PlayedCorpseReaction, PlayedBloodyReaction;

    HashSet<GameObject> SeenCorpses = new HashSet<GameObject>();
    GameObject CurrentCorpse;
    HashSet<GameObject> SeenBlood = new HashSet<GameObject>();
    GameObject CurrentBlood;

    public AudioSource LoudHit, NormalHit;

    public bool Investigating, Looking, Turn;

    public AudioSource GirlScream, BoyScream;

    private float distance3;

    public float DistanceToSakura;

    Vector3 LastSeenPosition;

    float LoseSightTimer, IdleTimer;

    public bool DoneStuff;

    public int WeaponNotices, BloodyNotices, MurderNotices, CorpsesDiscovered, BloodDiscovered;

    public bool SakuraNoticed;

    public float PreviousStoppingDistance;

    public void Start()
    {
        ValentinoDuration = 5f;
        volume.profile.TryGetSettings(out bloom);
    }

    public void DropNonWeapons()
    {
        if (SakuraScript.CurrentItem != null)
        {
            var ItemScript2 = SakuraScript.CurrentItem.GetComponent<AttackScript>();
            var ItemScript3 = SakuraScript.CurrentItem.GetComponent<HeadScript>();
            var ItemScript4 = SakuraScript.CurrentItem.GetComponent<HoldBucketScript>();
            var ItemScript6 = SakuraScript.CurrentItem.GetComponent<BloodyUniform>();
            var ItemScript5 = SakuraScript.CurrentItem.GetComponent<HoldRadio>();
            var ItemScript7 = SakuraScript.CurrentItem.GetComponent<MoppingScript>();
            var ItemScript8 = SakuraScript.CurrentItem.GetComponent<BleachScript>();

            if (ItemScript7 != null)
            {
                SakuraScript.CurrentItem = null;
                ItemScript7.Drop();
            }
            if (ItemScript8 != null)
            {
                SakuraScript.CurrentItem = null;
                ItemScript8.Drop();
            }
            if (ItemScript2 != null)
            {
                SakuraScript.CurrentItem = null;
                ItemScript2.DropFunction();
            }
            if (ItemScript3 != null)
            {
                SakuraScript.CurrentItem = null;
                ItemScript3.Drop();
            }
            if (ItemScript4 != null)
            {
                SakuraScript.CurrentItem = null;
                ItemScript4.Dropped();
            }
            if (ItemScript6 != null)
            {
                SakuraScript.CurrentItem = null;
                ItemScript6.Drop();
            }
            if (ItemScript5 != null)
            {
                SakuraScript.CurrentItem = null;
                ItemScript5.Dropped();
            }
        }
    }
    public void DropNonWeaponsTeachers()
    {
        if (SakuraScript.CurrentItem != null)
        {
            var ItemScript2 = SakuraScript.CurrentItem.GetComponent<AttackScript>();
            var ItemScript3 = SakuraScript.CurrentItem.GetComponent<HeadScript>();
            var ItemScript4 = SakuraScript.CurrentItem.GetComponent<HoldBucketScript>();
            var ItemScript6 = SakuraScript.CurrentItem.GetComponent<BloodyUniform>();
            var ItemScript5 = SakuraScript.CurrentItem.GetComponent<HoldRadio>();

            if (ItemScript2 != null)
            {
                ItemScript2.DropFunction();
            }
            if (ItemScript3 != null)
            {
                ItemScript3.Drop();
            }
            if (ItemScript4 != null)
            {
                ItemScript4.Dropped();
            }
            if (ItemScript6 != null)
            {
                ItemScript6.Drop();
            }
            if (ItemScript5 != null)
            {
                ItemScript5.Dropped();
            }
        }
    }

    public void DropKnife()
    {
        GameObject KnifeObject = GameObject.FindWithTag("Knife");
        if (KnifeObject != null)
        {
            var KnifeScript = KnifeObject.GetComponent<PickupScript>();
            if (KnifeScript != null)
            {
                if (KnifeScript.PickedUp)
                {
                    SakuraScript.CurrentItem = null;
                    KnifeScript.Hidden();
                    KnifeScript.currentWeight = 0f;
                    KnifeScript.WeaponHidden = true;
                    KnifeScript.PromptScript.Distance = 0f;
                    KnifeScript.Item.transform.position = KnifeScript.Nothing.position;
                    KnifeScript.PromptScript.MePressed = false;
                    KnifeScript.PickedUp = false;
                }
            }
        }
    }

    public void DropOtherItems()
    {
        if (SakuraScript.CurrentItem != null)
        {
            var ItemScript = SakuraScript.CurrentItem.GetComponent<PickupScript>();

            if (ItemScript != null)
            {
                if (ItemScript.Enum == PickupScript.ItemType.Shovel || ItemScript.Enum == PickupScript.ItemType.Saw)
                {
                    if (ItemScript.KeyToPress != 99)
                    {
                        ItemScript.inventory.isFull[ItemScript.KeyToPress] = false;
                    }
                    ItemScript.WeaponHidden = false;
                    Destroy(ItemScript.InstantiatedObject);
                }
                if (ItemScript.Enum == PickupScript.ItemType.Knife)
                {
                    SakuraScript.CurrentItem = null;
                    ItemScript.Hidden();
                    ItemScript.WeaponHidden = true;
                    ItemScript.PromptScript.Distance = 0f;
                    ItemScript.Mesh.enabled = false;
                    ItemScript.PromptScript.MePressed = false;
                    ItemScript.PickedUp = false;
                }
                else
                {
                    ItemScript.Drop();
                    if (SakuraScript.CurrentItem != null)
                    {
                        SakuraScript.CurrentItem.transform.parent = null;
                    }
                    SakuraScript.CurrentItem.transform.localScale = ItemScript.ItemScale;
                    SakuraScript.CurrentItem = null;
                    ItemScript.PromptScript.MePressed = false;
                    ItemScript.PickedUp = false;
                    ItemScript.rb.isKinematic = false;
                    ItemScript.Item.transform.SetParent(null);
                    ItemScript.Item.transform.localScale = ItemScript.ItemScale;
                    ItemScript.DropTimer = 0f;
                }
            }
        }
    }



    public IEnumerator BloodyReactionFunction()
    {
        BloodyNotices = BloodyNotices + 1;
        ////PlayerPrefs.SetInt("BloodyNotices", PlayerPrefs.GetInt("BloodyNotices") + 1);
        StudentState.InEvent = false;
        if (!Teacher && !Hazu && !TalkingSc.Valentino)
        {
            SakuraNoticed = true;
            this.NormalHit.Play();
            StudentState.CancelInvoke("ResetDistraction");
            this.SakuraScript.ManagingText.CancelInvoke("NoText");
            this.StudentText.text = TalkingSc.studentName + ": " + BloodyReaction;
            if (!PathAgent.isOnNavMesh)
            {
                Debug.LogWarning("PathAgent is not on NavMesh!");
                yield break;
            }
            PathAgent.isStopped = false;
            PathAgent.speed = 4f;
            RunningAway = true;
            PathAgent.SetDestination(RunAway.position);
            this.StudentState.enabled = false; CancelInvoke("BackToState"); Looking = false; Turn = false; CancelInvoke("Investigate");
            this.Detection.HideDetection();
            this.Detected = false;
            this.TalkingSc.enabled = true;
            this.TalkingSc.QuitMenu();
            this.TalkingSc.followed = 0;
            this.TalkingSc.attack.CantTalk = true;
            bloom.intensity.value = SakuraScript.CaughtBloom;
            this.SakuraScript.ManagingText.Invoke("NoText", 3f);
            this.CanReact = false;
            this.TalkingSc.Alarmed = true; Investigating = false;
            this.Alarmed = true;
            this.FollowScript.enabled = false;
            Scream.clip = audioSources[Random.Range(0, audioSources.Length)];
            this.Scream.Play();
            SakuraScript.Noise.transform.position = transform.position;
            if (HasPhone && TalkingSc.attack.IsKilled)
            {
                this.StudentAnimator.SetLayerWeight(5, 0f);
            }
            this.StudentState.enabled = false; CancelInvoke("BackToState"); Looking = false; Turn = false; CancelInvoke("Investigate");
            StudentState.NearVendingMachine = false;
            if (!PathAgent.hasPath)
            {
                this.StudentAnimator.SetTrigger("Idle");
                if (StudentState.AnimationName != "Idle")
                {
                    this.StudentAnimator.ResetTrigger(StudentState.AnimationName);
                }
                this.StudentAnimator.ResetTrigger(StudentState.WalkName);
            }
            else
            {
                StudentAnimator.ResetTrigger("Idle");
                StudentAnimator.SetTrigger("Run");
                this.StudentAnimator.ResetTrigger(StudentState.IdleName);
                this.StudentAnimator.ResetTrigger(StudentState.AnimationName);
                this.StudentAnimator.ResetTrigger(StudentState.WalkName);
            }
            this.StudentAnimator.SetLayerWeight(3, 1f);
            yield return new WaitForSeconds(0F);
        }
        if (Hazu)
        {
            this.LoudHit.Play();
            if (!PlayedBloodyReaction)
            {
                PlayedBloodyReaction = true;
                TalkingSc.attack.BloodyReaction.Play();
            }
            this.SakuraScript.BlindEveryone = true;
            this.StudentAnimator.ResetTrigger(StudentState.WalkName);
            this.StudentAnimator.SetTrigger(StudentState.IdleName);
            this.Detection.HideDetection();
            this.Detected = false;
            this.Bools.Phone.OnCooldown = true;
            this.Bools.Prompts.ClearAllPrompts = true;
            this.PlayerFound = true;
            StudentState.CancelInvoke("ResetDistraction");
            this.SakuraScript.ManagingText.CancelInvoke("NoText");
            this.StudentText.text = TalkingSc.studentName + ": " + BloodyReaction;
            this.TalkingSc.QuitMenu();
            bloom.intensity.value = SakuraScript.CaughtBloom;
            StudentState.AnimationName = "Idle";
            this.StudentAnimator.SetTrigger(StudentState.AnimationName);
            this.StudentAnimator.SetLayerWeight(6, 1f);
            this.SakuraScript.ManagingText.Invoke("NoText", 3f);
            this.CanReact = false;
            this.TalkingSc.Alarmed = true; Investigating = false;
            this.Alarmed = true;
            Scream.clip = audioSources[Random.Range(0, audioSources.Length)];
            this.Scream.Play();
            SakuraScript.Noise.transform.position = transform.position;
            this.FollowScript.enabled = false;
            StudentState.NearVendingMachine = false;
            this.StudentState.enabled = false; CancelInvoke("BackToState"); Looking = false; Turn = false; CancelInvoke("Investigate");
            this.SakuraScript.anim.SetLayerWeight(9, 1f);
            this.SakuraScript.anim.SetLayerWeight(10, 1f);
            this.SakuraScript.UpdateAnimationsIdle(0f, 0f);
            this.PathAgent.enabled = false;
            this.StudentAnimator.SetLayerWeight(4, 1f);
            this.SakuraScript.enabled = false;
            this.SakuraScript.CanMove = false;
            this.Bools.CaughtByHazu = true;
            this.PromptCanvas.SetActive(false);
            GameObject Canvas = GameObject.FindWithTag("Canvas");
            if (Canvas.GetComponent<InventoryScript>().inventoryCoroutine != null)
            {
                StopCoroutine(Canvas.GetComponent<InventoryScript>().inventoryCoroutine);
                Canvas.GetComponent<InventoryScript>().inventoryCoroutine = null;
            }
            if (Canvas.GetComponent<InventoryScript>().inventoryEnabled)
            {
                Canvas.GetComponent<InventoryScript>().inventoryanim.Play("inventoryclose");
                Canvas.GetComponent<InventoryScript>().inventoryEnabled = false;
                Canvas.GetComponent<InventoryScript>().CloseInventory.Play();
            }
            if (SakuraScript.poisoning)
            {
                SakuraScript.PoisonScript.Poisoned();
            }
            yield return new WaitForSeconds(5F);
            StartCoroutine(this.HazuGameOver());
            Time.timeScale = 1f;
        }
        if (Teacher || TalkingSc.Valentino)
        {
            SakuraNoticed = true;
            this.LoudHit.Play();
            this.Detection.HideDetection();
            this.Detected = false;
            this.Fighting = true;
            this.SakuraScript.BeingChased = true;
            PathAgent.isStopped = false;
            StudentState.CancelInvoke("ResetDistraction");
            this.SakuraScript.ManagingText.CancelInvoke("NoText");
            this.StudentText.text = TalkingSc.studentName + ": " + BloodyReaction;
            if (TalkingSc.Valentino && !PlayedBloodyReaction)
            {
                PlayedBloodyReaction = true;
                TalkingSc.attack.BloodyReaction.Play();
            }
            bloom.intensity.value = SakuraScript.CaughtBloom;
            this.SakuraScript.ManagingText.Invoke("NoText", 3f);
            this.PathAgent.stoppingDistance = 1f;
            this.CanReact = false;
            this.TalkingSc.Alarmed = true; Investigating = false;
            DoneStuff = false;
            this.Alarmed = true;
            Scream.clip = audioSources[Random.Range(0, audioSources.Length)];
            this.Scream.Play();
            SakuraScript.Noise.transform.position = transform.position;
            this.FollowScript.enabled = false;
            this.StudentState.enabled = false; CancelInvoke("BackToState"); Looking = false; Turn = false; CancelInvoke("Investigate");
            this.PathAgent.speed = 9f;
            if (!PathAgent.hasPath)
            {
                this.StudentAnimator.SetTrigger("Idle");
                if (StudentState.AnimationName != "Idle")
                {
                    this.StudentAnimator.ResetTrigger(StudentState.AnimationName);
                }
                this.StudentAnimator.ResetTrigger(StudentState.WalkName);
                StartCoroutine(this.CheckRunAnimation());
            }
            else
            {
                StudentAnimator.ResetTrigger("Idle");
                StudentAnimator.SetTrigger("Run");
                this.StudentAnimator.ResetTrigger(StudentState.IdleName);
                this.StudentAnimator.ResetTrigger(StudentState.AnimationName);
                this.StudentAnimator.ResetTrigger(StudentState.WalkName);
            }
            this.StudentAnimator.SetLayerWeight(3, 1f);
            yield return new WaitForSeconds(0F);
        }
    }
    public IEnumerator CorpseReactionFunction()
    {
        StudentState.InEvent = false;
        StudentState.CancelInvoke("ResetDistraction");
        this.SakuraScript.ManagingText.CancelInvoke("NoText");
        this.StudentText.text = TalkingSc.studentName + ": " + CorpseReaction;
        if ((TalkingSc.Valentino || Hazu || Akimura || TalkingSc.Chiyoko) && !PlayedCorpseReaction)
        {
            PlayedCorpseReaction = true;
            TalkingSc.attack.CorpseReaction.Play();
        }
        this.Detection.HideDetection();
        this.Detected = false;
        PathAgent.isStopped = false;
        PathAgent.speed = 4f;
        RunningAway = true;
        PathAgent.SetDestination(RunAway.position);
        this.StudentState.enabled = false;
        CancelInvoke("BackToState");
        Looking = false; Turn = false; CancelInvoke("Investigate");
        this.TalkingSc.QuitMenu();
        this.TalkingSc.followed = 0;
        this.TalkingSc.attack.CantTalk = true;
        bloom.intensity.value = SakuraScript.CaughtBloom;
        this.SakuraScript.ManagingText.Invoke("NoText", 3f);
        this.TalkingSc.Alarmed = true;
        Investigating = false;
        DoneStuff = false;
        this.Alarmed = true;
        this.FollowScript.enabled = false;
        Scream.clip = audioSources[Random.Range(0, audioSources.Length)];
        this.Scream.Play();
        SakuraScript.Noise.transform.position = transform.position;
        if (HasPhone && TalkingSc.attack.IsKilled)
        {
            this.StudentAnimator.SetLayerWeight(5, 0f);
        }
        StudentState.NearVendingMachine = false;
        if (!PathAgent.hasPath)
        {
            this.StudentAnimator.SetTrigger("Idle");
            if (StudentState.AnimationName != "Idle")
            {
                this.StudentAnimator.ResetTrigger(StudentState.AnimationName);
            }
            this.StudentAnimator.ResetTrigger(StudentState.WalkName);
            StartCoroutine(this.CheckRunAnimation());
        }
        else
        {
            StudentAnimator.ResetTrigger("Idle");
            StudentAnimator.SetTrigger("Run");
            this.StudentAnimator.ResetTrigger(StudentState.IdleName);
            this.StudentAnimator.ResetTrigger(StudentState.AnimationName);
            this.StudentAnimator.ResetTrigger(StudentState.WalkName);
        }
        this.StudentAnimator.SetLayerWeight(3, 1f);
        yield return new WaitForSeconds(0F);
    }
    public IEnumerator FightReactionFunction()
    {
        IsGoingAfterSakura = false;
        EvidenceSc.PoliceBeingCalled = true;
        this.CallingPolice = true;
        this.PathAgent.enabled = true;
        StudentState.InEvent = false;
        if (!PathAgent.isOnNavMesh)
        {
            Debug.LogWarning("PathAgent is not on NavMesh!");
            yield break;
        }
        if ((TalkingSc.Valentino || Hazu || Akimura || TalkingSc.Chiyoko) && !PlayedCorpseReaction)
        {
            PlayedCorpseReaction = true;
            TalkingSc.attack.CorpseReaction.Play();
        }
        this.Detected = false;
        PathAgent.isStopped = false;
        PathAgent.speed = 4f;
        RunningAway = true;
        PathAgent.SetDestination(RunAway.position);
        this.StudentState.enabled = false; CancelInvoke("BackToState"); Looking = false; Turn = false; CancelInvoke("Investigate");
        this.TalkingSc.QuitMenu();
        this.TalkingSc.followed = 0;
        this.TalkingSc.attack.CantTalk = true;
        bloom.intensity.value = SakuraScript.CaughtBloom;
        this.TalkingSc.Alarmed = true; Investigating = false;
        DoneStuff = false;
        this.Alarmed = true;
        this.FollowScript.enabled = false;
        Scream.clip = audioSources[Random.Range(0, audioSources.Length)];
        this.Scream.Play();
        SakuraScript.Noise.transform.position = transform.position;
        if (HasPhone && TalkingSc.attack.IsKilled)
        {
            this.StudentAnimator.SetLayerWeight(5, 0f);
        }
        this.StudentState.enabled = false; CancelInvoke("BackToState"); Looking = false; Turn = false; CancelInvoke("Investigate");
        StudentState.NearVendingMachine = false;
        if (!PathAgent.hasPath)
        {
            this.StudentAnimator.SetTrigger("Idle");
            if (StudentState.AnimationName != "Idle")
            {
                this.StudentAnimator.ResetTrigger(StudentState.AnimationName);
            }
            this.StudentAnimator.ResetTrigger(StudentState.WalkName);
            StartCoroutine(this.CheckRunAnimation());
        }
        else
        {
            StudentAnimator.ResetTrigger("Idle");
            StudentAnimator.SetTrigger("Run");
            this.StudentAnimator.ResetTrigger(StudentState.IdleName);
            this.StudentAnimator.ResetTrigger(StudentState.AnimationName);
            this.StudentAnimator.ResetTrigger(StudentState.WalkName);
        }
        this.StudentAnimator.SetLayerWeight(3, 1f);
        yield return new WaitForSeconds(0F);
    }
    public IEnumerator BloodReactionFunction()
    {
        StudentState.InEvent = false;
        StudentState.CancelInvoke("ResetDistraction");
        this.SakuraScript.ManagingText.CancelInvoke("NoText");
        this.StudentText.text = TalkingSc.studentName + ": " + BloodPuddleReaction;
        if (!PathAgent.isOnNavMesh)
        {
            Debug.LogWarning("PathAgent is not on NavMesh!");
            yield break;
        }
        PathAgent.isStopped = false;
        PathAgent.speed = 4f;
        RunningAway = true;
        this.Detection.HideDetection();
        this.Detected = false;
        PathAgent.SetDestination(RunAway.position);
        this.StudentState.enabled = false; CancelInvoke("BackToState"); Looking = false; Turn = false; CancelInvoke("Investigate");
        this.TalkingSc.QuitMenu();
        this.TalkingSc.followed = 0;
        this.TalkingSc.attack.CantTalk = true;
        bloom.intensity.value = SakuraScript.CaughtBloom;
        this.SakuraScript.ManagingText.Invoke("NoText", 3f);
        this.TalkingSc.Alarmed = true; Investigating = false;
        DoneStuff = false;
        this.Alarmed = true;
        this.FollowScript.enabled = false;
        Scream.clip = audioSources[Random.Range(0, audioSources.Length)];
        this.Scream.Play();
        SakuraScript.Noise.transform.position = transform.position;
        if (TalkingSc.Voicelines && !TalkingSc.attack.CorpseReaction.isPlaying)
        {
            TalkingSc.attack.BloodReaction.Play();
        }
        if (HasPhone && TalkingSc.attack.IsKilled)
        {
            this.StudentAnimator.SetLayerWeight(5, 0f);
        }
        this.StudentState.enabled = false; CancelInvoke("BackToState"); Looking = false; Turn = false; CancelInvoke("Investigate");
        StudentState.NearVendingMachine = false;
        if (!PathAgent.hasPath)
        {
            this.StudentAnimator.SetTrigger("Idle");
            if (StudentState.AnimationName != "Idle")
            {
                this.StudentAnimator.ResetTrigger(StudentState.AnimationName);
            }
            this.StudentAnimator.ResetTrigger(StudentState.WalkName);
            StartCoroutine(this.CheckRunAnimation());
        }
        else
        {
            StudentAnimator.ResetTrigger("Idle");
            StudentAnimator.SetTrigger("Run");
            this.StudentAnimator.ResetTrigger(StudentState.IdleName);
            this.StudentAnimator.ResetTrigger(StudentState.AnimationName);
            this.StudentAnimator.ResetTrigger(StudentState.WalkName);
        }
        this.StudentAnimator.SetLayerWeight(3, 1f);
        yield return new WaitForSeconds(0F);
    }
    public IEnumerator HazuGameOver()
    {
        BlackScreen.SetActive(true);
        this.GameOverScript.GameOverText.text = "CAUGHT BY HAZU";
        this.GameOverScript.GameOverExplanation.text = "Hazu would never be yours after that!";
        yield return new WaitForSeconds(2F);
        this.GameOverScreen.SetActive(true);
    }
    public IEnumerator GameOver2()
    {
        yield return new WaitForSeconds(2F);

        BlackScreen.SetActive(true);
        yield return new WaitForSeconds(2F);
        if (!TalkingSc.Valentino)
        {
            this.GameOverScript.GameOverText.text = "CAUGHT BY SENSEI";
            this.GameOverScript.GameOverExplanation.text = "Dang! these teachers are strong...";
        }
        else
        {
            this.GameOverScript.GameOverText.text = "HAZU IS UNSAFE";
            this.GameOverScript.GameOverExplanation.text = "How could he ruin... EVERYTHING!";
        }
        this.GameOverScreen.SetActive(true);
    }

    public IEnumerator MurderReactionFunction()
    {
        StudentState.InEvent = false;
        if (!Teacher && !Hazu && !TalkingSc.Valentino)
        {
            SakuraNoticed = true;
            this.NormalHit.Play();
            StudentState.CancelInvoke("ResetDistraction");
            this.SakuraScript.ManagingText.CancelInvoke("NoText");
            this.StudentText.text = TalkingSc.studentName + ": " + MurderReaction;
            if (!PathAgent.isOnNavMesh)
            {
                Debug.LogWarning("PathAgent is not on NavMesh!");
                yield break;
            }
            PathAgent.isStopped = false;
            PathAgent.speed = 4f;
            RunningAway = true;
            PathAgent.SetDestination(RunAway.position);
            this.Detection.HideDetection();
            this.Detected = false;
            this.TalkingSc.QuitMenu();
            this.TalkingSc.followed = 0;
            this.TalkingSc.attack.CantTalk = true;
            bloom.intensity.value = SakuraScript.CaughtBloom;
            this.SakuraScript.ManagingText.Invoke("NoText", 3f);
            this.CanReact = false;
            this.Alarmed = true;
            this.FollowScript.enabled = false;
            Scream.clip = audioSources[Random.Range(0, audioSources.Length)];
            this.TalkingSc.attack.Scream.Play();
            SakuraScript.Noise.transform.position = transform.position;
            if (HasPhone && TalkingSc.attack.IsKilled)
            {
                this.StudentAnimator.SetLayerWeight(5, 0f);
            }
            this.StudentState.enabled = false; CancelInvoke("BackToState"); Looking = false; Turn = false; CancelInvoke("Investigate");
            StudentState.NearVendingMachine = false;
            StudentAnimator.ResetTrigger("Sit");
            if (!PathAgent.hasPath)
            {
                this.StudentAnimator.SetTrigger("Idle");
                if (StudentState.AnimationName != "Idle")
                {
                    this.StudentAnimator.ResetTrigger(StudentState.AnimationName);
                }
                this.StudentAnimator.ResetTrigger(StudentState.WalkName);
                StartCoroutine(CheckRunAnimation());
            }
            else
            {
                StudentAnimator.ResetTrigger("Idle");
                StudentAnimator.SetTrigger("Run");
                StudentAnimator.ResetTrigger(StudentState.IdleName);
                StudentAnimator.ResetTrigger(StudentState.AnimationName);
                StudentAnimator.ResetTrigger(StudentState.WalkName);
            }
            this.StudentAnimator.SetLayerWeight(3, 1f);
            yield return new WaitForSeconds(0F);
        }
        if (Hazu)
        {
            if (TalkingSc.Voicelines)
            {
                TalkingSc.attack.MurderReaction.Play();
            }
            this.LoudHit.Play();
            this.SakuraScript.BlindEveryone = true;
            this.StudentAnimator.ResetTrigger(StudentState.WalkName);
            this.StudentAnimator.SetTrigger(StudentState.IdleName);
            this.Detection.HideDetection();
            this.Detected = false;
            this.Bools.Phone.OnCooldown = true;
            this.Bools.Prompts.ClearAllPrompts = true;
            this.PlayerFound = true;
            StudentState.CancelInvoke("ResetDistraction");
            this.SakuraScript.ManagingText.CancelInvoke("NoText");
            this.StudentText.text = TalkingSc.studentName + ": " + MurderReaction;
            this.TalkingSc.QuitMenu();
            bloom.intensity.value = SakuraScript.CaughtBloom;
            StudentState.AnimationName = "Idle";
            this.StudentAnimator.SetTrigger(StudentState.AnimationName);
            this.StudentAnimator.SetLayerWeight(6, 1f);
            StudentState.enabled = false; CancelInvoke("BackToState"); Looking = false; Turn = false; CancelInvoke("Investigate");
            this.SakuraScript.ManagingText.Invoke("NoText", 3f);
            this.CanReact = false;
            this.TalkingSc.Alarmed = true; Investigating = false;
            this.Alarmed = true;
            Scream.clip = audioSources[Random.Range(0, audioSources.Length)];
            this.Scream.Play();
            SakuraScript.Noise.transform.position = transform.position;
            this.FollowScript.enabled = false;
            StudentState.NearVendingMachine = false;
            this.StudentState.enabled = false; CancelInvoke("BackToState"); Looking = false; Turn = false; CancelInvoke("Investigate");
            this.SakuraScript.anim.SetInteger("running", 0);
            this.SakuraScript.anim.SetInteger("testing", 0);
            this.PathAgent.enabled = false;
            this.StudentAnimator.SetLayerWeight(4, 1f);
            this.Bools.CaughtByHazu = true;
            this.SakuraScript.enabled = false;
            this.SakuraScript.CanMove = false;
            this.PromptCanvas.SetActive(false);
            GameObject Canvas = GameObject.FindWithTag("Canvas");
            if (Canvas.GetComponent<InventoryScript>().inventoryCoroutine != null)
            {
                StopCoroutine(Canvas.GetComponent<InventoryScript>().inventoryCoroutine);
                Canvas.GetComponent<InventoryScript>().inventoryCoroutine = null;
            }
            if (Canvas.GetComponent<InventoryScript>().inventoryEnabled)
            {
                Canvas.GetComponent<InventoryScript>().inventoryanim.Play("inventoryclose");
                Canvas.GetComponent<InventoryScript>().inventoryEnabled = false;
                Canvas.GetComponent<InventoryScript>().CloseInventory.Play();
            }
            if (SakuraScript.poisoning)
            {
                SakuraScript.PoisonScript.Poisoned();
            }
            yield return new WaitForSeconds(5F);
            this.SakuraScript.ManagingText.Invoke("NoText", 0f);
            Time.timeScale = 1f;
            StartCoroutine(this.HazuGameOver());
        }
        if (Teacher || TalkingSc.Valentino)
        {
            SakuraNoticed = true;
            this.LoudHit.Play();
            this.Detection.HideDetection();
            this.Detected = false;
            this.Fighting = true;
            this.SakuraScript.BeingChased = true;
            StudentState.CancelInvoke("ResetDistraction");
            this.SakuraScript.ManagingText.CancelInvoke("NoText");
            this.StudentText.text = TalkingSc.studentName + ": " + MurderReaction;
            if (TalkingSc.Voicelines)
            {
                TalkingSc.attack.MurderReaction.Play();
            }

            bloom.intensity.value = SakuraScript.CaughtBloom;
            this.SakuraScript.ManagingText.Invoke("NoText", 3f);
            this.PathAgent.stoppingDistance = 2f;
            this.CanReact = false;
            this.TalkingSc.Alarmed = true; Investigating = false;
            DoneStuff = false;
            this.Alarmed = true;
            Scream.clip = audioSources[Random.Range(0, audioSources.Length)];
            this.Scream.Play();
            SakuraScript.Noise.transform.position = transform.position;
            this.FollowScript.enabled = false;
            this.StudentState.enabled = false; CancelInvoke("BackToState"); Looking = false; Turn = false; CancelInvoke("Investigate");
            this.PathAgent.speed = 9f;
            if (!PathAgent.hasPath)
            {
                this.StudentAnimator.SetTrigger("Idle");
                if (StudentState.AnimationName != "Idle")
                {
                    this.StudentAnimator.ResetTrigger(StudentState.AnimationName);
                }
                this.StudentAnimator.ResetTrigger(StudentState.WalkName);
                StartCoroutine(this.CheckRunAnimation());
            }
            else
            {
                StudentAnimator.ResetTrigger("Idle");
                StudentAnimator.SetTrigger("Run");
                this.StudentAnimator.ResetTrigger(StudentState.AnimationName);
                this.StudentAnimator.ResetTrigger(StudentState.IdleName);
                this.StudentAnimator.ResetTrigger(StudentState.WalkName);
            }
            this.StudentAnimator.SetLayerWeight(3, 1f);
            yield return new WaitForSeconds(0F);
        }
    }

    void Investigate()
    {
        PathAgent.enabled = true;
        PathAgent.isStopped = false;
        Turn = false;
        if (Vector3.Distance(transform.position, SakuraScript.Noise.transform.position) > 6f)
        {
            PathAgent.stoppingDistance = 2f;
            Investigating = true;
            StudentAnimator.SetTrigger(StudentState.WalkName);
            StudentAnimator.ResetTrigger(StudentState.IdleName);
            StudentState.studentAnimator.ResetTrigger(StudentState.AnimationName);
            PathAgent.SetDestination(SakuraScript.Noise.transform.position);
        }
        else
        {
            StudentState.reachedDestination = true;
            StudentState.enabled = true;
            if (StudentState.Arrived)
            {
                StudentAnimator.SetTrigger(StudentState.AnimationName);
                if (StudentState.IdleName != StudentState.AnimationName)
                {
                    StudentState.studentAnimator.ResetTrigger(StudentState.IdleName);
                }
                StudentState.studentAnimator.ResetTrigger(StudentState.WalkName);
            }
            else
            {
                StudentAnimator.SetTrigger(StudentState.WalkName);
                StudentState.studentAnimator.ResetTrigger(StudentState.IdleName);

            }
        }
    }
    void BackToState()
    {
        PathAgent.stoppingDistance = PreviousStoppingDistance;
        StudentState.enabled = true;
        StudentAnimator.SetTrigger(StudentState.WalkName);
        StudentState.studentAnimator.ResetTrigger(StudentState.IdleName);
    }

    void Update()
    {
        if (EvidenceSc.TimeUp)
        {
            PlayerPrefs.SetInt("WeaponNotices", WeaponNotices);
            PlayerPrefs.SetInt("BloodyNotices", BloodyNotices);
            PlayerPrefs.SetInt("MurderNotices", MurderNotices);
            PlayerPrefs.SetInt("CorpsesDiscovered", SeenCorpses.Count);
            PlayerPrefs.SetInt("BloodDiscovered", SeenBlood.Count);
        }
        distance3 = Vector3.Distance(transform.position, SakuraScript.Noise.transform.position);
        if (!Yandere && !Alarmed)
        {
            if (StudentState.TimeScript.TimePeriod != "Festival" || StudentState.TimeScript.TimePeriod == "Festival" && !StudentState.InDestination)
            {
                if (GirlScream.isPlaying || BoyScream.isPlaying)
                {
                    if (!Turn && !Looking && !Alarmed && !Fighting && SakuraScript.Noise != null && !StudentState.Distracted)
                    {
                        if (PlayerPrefs.GetInt("Deaths") < 1 && distance3 < 10 || PlayerPrefs.GetInt("Deaths") > 0 && PlayerPrefs.GetInt("Deaths") < 2 && distance3 < 12 || PlayerPrefs.GetInt("Deaths") > 1 && distance3 < 14)
                        {
                            StudentAnimator.ResetTrigger("Run");
                            StudentAnimator.ResetTrigger(StudentState.WalkName);
                            StudentAnimator.SetTrigger(StudentState.IdleName);
                            if (StudentState.IdleName != StudentState.AnimationName)
                            {
                                StudentState.studentAnimator.ResetTrigger(StudentState.AnimationName);
                            }
                            PathAgent.enabled = false;
                            if (PathAgent.hasPath)
                            {
                                PathAgent.isStopped = true;
                            }
                            StudentState.enabled = false;
                            if (TalkingSc.isTalking)
                            {
                                TalkingSc.QuitMenu();
                            }
                            Looking = true;
                            Turn = true;
                            PreviousStoppingDistance = PathAgent.stoppingDistance;
                            Invoke("Investigate", 2f);
                        }
                    }
                }
            }
        }
        if (Looking && !Detected && !CanSeePlayer && !CanSeeBlood && !CanSeeCorpse && !CanSeeAiming)
        {
            Detection.ShowDetection();
            Detection.duration = 0.4f;
            Looking = false;
            Detected = true;
        }
        if (Turn && !Alarmed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(SakuraScript.Noise.transform.position - this.StudentTransform.position);
            this.StudentTransform.rotation = Quaternion.Slerp(StudentTransform.rotation, targetRotation, 6 * Time.deltaTime);
        }
        if (Detection.FullyDetected && (Turn || Investigating))
        {
            this.Detection.HideDetection();
            Turn = false;
        }
        if (Investigating && !Alarmed)
        {
            if (!PathAgent.pathPending && PathAgent.remainingDistance <= this.PathAgent.stoppingDistance && (!this.PathAgent.hasPath || this.PathAgent.velocity.sqrMagnitude == 0f))
            {
                CancelInvoke("Investigate");
                StudentState.Conversating = false;
                StudentAnimator.ResetTrigger(StudentState.WalkName);
                StudentAnimator.SetTrigger(StudentState.IdleName);
                if (StudentState.IdleName != StudentState.AnimationName)
                {
                    StudentState.studentAnimator.ResetTrigger(StudentState.AnimationName);
                }
                Investigating = false;
                Invoke("BackToState", 2f);
            }
        }
        if (!Yandere && this.SakuraScript.Club != "Art")
        {
            if (Yandere)
            {
                ViewRadius = 36f;
            }
            else
            {
                if (PlayerPrefs.GetInt("Deaths") < 1)
                {
                    ViewRadius = 11f;
                }
                else if (PlayerPrefs.GetInt("Deaths") > 0 && PlayerPrefs.GetInt("Deaths") < 2)
                {
                    ViewRadius = 12f;
                }
                else if (PlayerPrefs.GetInt("Deaths") > 1)
                {
                    ViewRadius = 13f;
                }
            }
        }
        if (this.SakuraScript.Club == "Art")
        {
            if (Yandere)
            {
                ViewRadius = 33f;
            }
            else
            {
                if (PlayerPrefs.GetInt("Deaths") < 1)
                {
                    ViewRadius = 8f;
                }
                else if (PlayerPrefs.GetInt("Deaths") > 0 && PlayerPrefs.GetInt("Deaths") < 2)
                {
                    ViewRadius = 9f;
                }
                else if (PlayerPrefs.GetInt("Deaths") > 1)
                {
                    ViewRadius = 10f;
                }
            }
        }
        if (this.SakuraScript.bools.SakuraBeingSeen && Yandere && this.StudentState.OriginalDestination == this.Sakura.transform && !TalkingSc.attack.TeleportYukira)
        {
            this.StudentState.NavAgent.isStopped = true;
            this.StudentAnimator.ResetTrigger("LookAround");
            this.StudentAnimator.ResetTrigger(StudentState.WalkName);
            CancelInvoke("BackToState");
            CancelInvoke("Investigate");
            this.StudentAnimator.SetTrigger(StudentState.AnimationName);
        }
        else if (CanChase && !this.StudentState.Patrolling && Yandere && this.StudentState.OriginalDestination == this.Sakura.transform && !this.SakuraScript.bools.SakuraBeingSeen)
        {
            this.StudentState.NavAgent.isStopped = false;
            this.StudentAnimator.ResetTrigger("LookAround");
            this.StudentAnimator.SetTrigger(StudentState.WalkName);
        }
        if (this.StudentState.Patrolling && Yandere && CanChase && !this.SakuraScript.bools.SakuraBeingSeen)
        {
            LoseSightTimer += Time.deltaTime;
            if (this.StudentState.OriginalDestination != this.StudentState.PatrolPoints[0] && this.StudentState.NavAgent.isStopped && LoseSightTimer > 8f || this.StudentState.OriginalDestination != this.StudentState.PatrolPoints[0] && this.StudentState.OriginalDestination == this.Sakura.transform && LoseSightTimer > 6f)
            {
                IdleTimer += Time.deltaTime;
                if (IdleTimer > 3f)
                {
                    ObstacleMask = YandereMask;
                    this.StudentState.NavAgent.isStopped = false;
                    this.StudentState.OriginalDestination = this.StudentState.PatrolPoints[0];
                    this.StudentAnimator.ResetTrigger("LookAround");
                    this.StudentAnimator.ResetTrigger(StudentState.AnimationName);
                    this.StudentAnimator.SetTrigger(StudentState.WalkName);
                    LoseSightTimer = 0f;
                    IdleTimer = 0f;
                }
                else
                {
                    this.StudentState.NavAgent.isStopped = true;
                    this.StudentAnimator.ResetTrigger(StudentState.WalkName);
                    this.StudentAnimator.SetTrigger("LookAround");
                }
            }
        }
        CanChase = !this.SakuraScript.killing && !this.SakuraScript.bools.isTalking && !this.SakuraScript.InClass && !this.Cupcake.IsPoisoning && !TalkingSc.attack.TeleportYukira;
        if (!CanChase && this.StudentState.OriginalDestination == this.Sakura.transform && Yandere && !TalkingSc.attack.TeleportYukira)
        {
            this.StudentState.NavAgent.isStopped = true;
            this.StudentAnimator.ResetTrigger("LookAround");
            this.StudentAnimator.ResetTrigger(StudentState.WalkName);
            CancelInvoke("BackToState");
            CancelInvoke("Investigate");
            this.StudentAnimator.SetTrigger(StudentState.AnimationName);
        }
        if (!this.StudentState.Patrolling && Yandere && this.StudentState.OriginalDestination != this.Sakura.transform && CanChase && !this.SakuraScript.bools.SakuraBeingSeen)
        {
            this.StudentState.NavAgent.isStopped = false;
            LastSeenPosition = this.Sakura.transform.position;
            LoseSightTimer = 0f;
            this.StudentState.OriginalDestination = this.Sakura.transform;
            this.StudentAnimator.ResetTrigger("LookAround");
            this.StudentAnimator.ResetTrigger(StudentState.AnimationName);
            this.StudentAnimator.SetTrigger(StudentState.WalkName);
        }
        if (IsGoingAfterSakura && !FightReacted)
        {
            PathAgent.acceleration = 80f;
            PathAgent.SetDestination(Sakura.transform.position);
        }
        if (Fighting)
        {
            this.StudentAnimator.SetLayerWeight(3, 1f);
            this.PromptScript.Distance = 0f;
            this.CanReact = false;
            if (BoolScript.lost)
            {
                this.SakuraScript.Fighting = false;
                if (DistanceToSakura < 2f)
                {
                    this.PromptCanvas.SetActive(false);
                    this.PathAgent.enabled = false;
                    this.StartSakuraRotation = true;
                    if (!HasPlayedAnimation)
                    {
                        this.SakuraScript.anim.Play("Down");
                        HasPlayedAnimation = true;
                    }
                    this.SakuraScript.CanMove = false;
                    this.SakuraScript.enabled = false;
                    if (!Kicked)
                    {
                        Kicked = true;
                        StudentAnimator.ResetTrigger(StudentState.WalkName);
                        this.StudentAnimator.Play("Kick");
                    }
                    Time.timeScale = 1f;
                    StartCoroutine(this.GameOver2());
                    this.BoolScript.lost = false;
                    this.DropNonWeapons();
                    this.DropOtherItems();
                }
            }
        }
        float distance = Vector3.Distance(StudentTransform.position, Sakura.transform.position);
        if (distance < 3 && SakuraScript.killing && CanReact)
        {
            this.TalkingSc.Alarmed = true; Investigating = false;
            this.StudentState.Alarmed = true;
            if (!Teacher && !Hazu && !TalkingSc.Valentino)
            {
                EvidenceSc.PoliceBeingCalled = true;
                this.CallingPolice = true;
            }
            else if (Teacher || TalkingSc.Valentino)
            {
                IsGoingAfterSakura = true;
            }
            if (Hazu)
            {
                this.SakuraScript.enabled = false;
                this.SakuraScript.CanMove = false;
            }
            MurderNotices = MurderNotices + 1;
            ////PlayerPrefs.SetInt("MurderNotices", PlayerPrefs.GetInt("MurderNotices") + 1);
            base.StartCoroutine(this.MurderReactionFunction());
        }
        FieldOfViewCheck();
        CorpseCheck();
        BloodCheck();
        if (TalkingSc.Valentino)
        {
            AimingCheck();
        }
        if (CanSeeAiming)
        {
            if (!SakuraScript.Bloody && !SakuraScript.killing && !SakuraScript.carrying && CanReact)
            {
                this.Detection.duration = ValentinoDuration;
                this.Detection.ShowDetection();
                this.Detected = true;
                if (Detection.FullyDetected)
                {
                    this.Detection.HideDetection();
                    this.TalkingSc.Alarmed = true; Investigating = false;
                    IsGoingAfterSakura = true;
                    this.StudentState.Alarmed = true;
                    base.StartCoroutine(this.MurderReactionFunction());
                }

            }
        }
        if (StudentState.Alarmed && !Teacher && !Hazu && !TalkingSc.Valentino)
        {
            TalkingSc.FollowTimerCircle.SetActive(false);
        }
        if (CanSeePlayer && Yandere)
        {
            this.StudentState.Patrolling = false;
        }
        if (!CanSeePlayer && Yandere)
        {
            this.StudentState.Patrolling = true;
        }
        if (CanSeePlayer)
        {
            if (SakuraScript.Sweeping && SeenBlood.Count != 0)
            {
                this.TalkingSc.Alarmed = true; Investigating = false;
                if (!Teacher && !TalkingSc.Valentino)
                {
                    EvidenceSc.PoliceBeingCalled = true;
                    this.CallingPolice = true;
                }
                if (Teacher || TalkingSc.Valentino)
                {
                    IsGoingAfterSakura = true;
                }
                this.StudentState.Alarmed = true;
                if (Hazu)
                {
                    this.SakuraScript.enabled = false;
                    this.SakuraScript.CanMove = false;
                }
                if (Detected)
                {
                    MurderNotices = MurderNotices + 1;
                    ////PlayerPrefs.SetInt("MurderNotices", PlayerPrefs.GetInt("MurderNotices") + 1);
                }
                base.StartCoroutine(this.MurderReactionFunction());
            }
            if (SakuraScript.Bloody && !SakuraScript.killing && !SakuraScript.carrying && CanReact)
            {
                this.Detection.duration = 4f;
                this.Detection.ShowDetection();
                this.Detected = true;
                if (Detection.FullyDetected)
                {
                    this.Detection.HideDetection();
                    this.TalkingSc.Alarmed = true; Investigating = false;
                    if (!Teacher && !TalkingSc.Valentino)
                    {
                        EvidenceSc.PoliceBeingCalled = true;
                        this.CallingPolice = true;
                    }
                    if (Teacher || TalkingSc.Valentino)
                    {
                        IsGoingAfterSakura = true;
                    }
                    this.StudentState.Alarmed = true;
                    if (Hazu)
                    {
                        this.Bools.Prompts.ClearAllPrompts = true;
                        this.Bools.Phone.StartCoroutine(Bools.Phone.QuitPhoneCaught());
                        this.SakuraScript.enabled = false;
                        this.SakuraScript.CanMove = false;
                        Detected = false;
                        StartSakuraRotation = true;
                        SakuraScript.UpdateAnimationsIdle(0f, 0f);
                        SakuraScript.anim.SetLayerWeight(9, 1f);
                        SakuraScript.anim.SetLayerWeight(10, 1f);
                        if (SakuraScript.CurrentItem != null)
                        {
                            DropNonWeapons();
                            DropOtherItems();
                            DropKnife();
                        }
                    }
                    base.StartCoroutine(this.BloodyReactionFunction());
                }
            }
            if (SakuraScript.killing && !SakuraScript.carrying && CanReact)
            {
                this.TalkingSc.Alarmed = true; Investigating = false;
                if (!Teacher && !TalkingSc.Valentino)
                {
                    EvidenceSc.PoliceBeingCalled = true;
                    this.CallingPolice = true;
                }
                if (Teacher || TalkingSc.Valentino)
                {
                    IsGoingAfterSakura = true;
                }
                this.StudentState.Alarmed = true;
                if (Hazu)
                {
                    this.SakuraScript.enabled = false;
                    this.SakuraScript.CanMove = false;
                }
                if (Detected)
                {
                    MurderNotices = MurderNotices + 1;
                    ////PlayerPrefs.SetInt("MurderNotices", PlayerPrefs.GetInt("MurderNotices") + 1);
                }
                base.StartCoroutine(this.MurderReactionFunction());
            }
            if (SakuraScript.poisoning && CanReact)
            {
                this.TalkingSc.Alarmed = true; Investigating = false;
                if (!Teacher && !TalkingSc.Valentino)
                {
                    EvidenceSc.PoliceBeingCalled = true;
                    this.CallingPolice = true;
                }
                if (Teacher || TalkingSc.Valentino)
                {
                    IsGoingAfterSakura = true;
                }
                this.StudentState.Alarmed = true;
                if (Hazu)
                {
                    this.SakuraScript.enabled = false;
                    this.SakuraScript.CanMove = false;
                }
                base.StartCoroutine(this.MurderReactionFunction());
            }
            if (!SakuraScript.killing && SakuraScript.carrying && CanReact || SakuraScript.NearBody && SakuraScript.gameObject.layer == 15 && CanSeeCorpse && CanReact || SakuraScript.Fighting && CanReact || SakuraScript.Bloody && SakuraScript.HasWeapon && PlayerPrefs.GetInt("Deaths") > 1 && CanReact)
            {
                this.TalkingSc.Alarmed = true; Investigating = false;
                if (!Teacher)
                {
                    EvidenceSc.PoliceBeingCalled = true;
                    this.CallingPolice = true;
                }
                if (Teacher)
                {
                    IsGoingAfterSakura = true;
                }
                this.StudentState.Alarmed = true;
                if (Hazu)
                {
                    this.Bools.Prompts.ClearAllPrompts = true;
                    this.Bools.Phone.StartCoroutine(Bools.Phone.QuitPhoneCaught());
                    this.SakuraScript.enabled = false;
                    this.SakuraScript.CanMove = false;
                    Detection.HideDetection();
                    Detected = false;
                    StartSakuraRotation = true;
                    SakuraScript.UpdateAnimationsIdle(0f, 0f);
                    SakuraScript.anim.SetLayerWeight(9, 1f);
                    SakuraScript.anim.SetLayerWeight(10, 1f);
                    if (SakuraScript.Fighting && SakuraScript.CurrentFightingCharacter.GetComponent<FightSakura>())
                    {
                        this.SakuraScript.CanMove = false;
                        SakuraScript.CurrentFightingCharacter.GetComponent<FightSakura>().CaughtByHazu();
                    }
                    if (SakuraScript.CurrentItem != null)
                    {
                        DropNonWeapons();
                        DropOtherItems();
                        DropKnife();
                    }
                }
                if (Detected)
                {
                    MurderNotices = MurderNotices + 1;
                    ////PlayerPrefs.SetInt("MurderNotices", PlayerPrefs.GetInt("MurderNotices") + 1);
                }
                base.StartCoroutine(this.MurderReactionFunction());
            }
            if (!SakuraScript.killing && !SakuraScript.carrying && SakuraScript.HasWeapon && CanReact)
            {
                var WeaponScript = SakuraScript.CurrentItem.GetComponent<PickupScript>();
                if (WeaponScript != null)
                {
                    if (WeaponScript.Enum == PickupScript.ItemType.Knife && !WeaponScript.Bloody)
                    {
                        this.Detection.duration = 4f;
                    }
                    else if (WeaponScript.Enum == PickupScript.ItemType.Shovel && !WeaponScript.Bloody)
                    {
                        this.Detection.duration = 2f;
                    }
                    else if (WeaponScript.Bloody || WeaponScript.Enum == PickupScript.ItemType.Saw)
                    {
                        this.Detection.duration = 1f;
                    }
                }
                this.Detection.ShowDetection();
                this.Detected = true;
                if (Detection.FullyDetected)
                {
                    this.Detection.HideDetection();
                    this.TalkingSc.Alarmed = true; Investigating = false;
                    if (WeaponScript.Bloody && WeaponScript != null)
                    {
                        if (!Teacher && !TalkingSc.Valentino)
                        {
                            EvidenceSc.PoliceBeingCalled = true;
                            this.CallingPolice = true;
                        }
                    }
                    if (Teacher || TalkingSc.Valentino)
                    {
                        IsGoingAfterSakura = true;
                    }
                    this.StudentState.Alarmed = true;
                    if (Detected)
                    {
                        WeaponNotices = WeaponNotices + 1;
                        ////PlayerPrefs.SetInt("WeaponNotices", PlayerPrefs.GetInt("WeaponNotices") + 1);
                    }
                    base.StartCoroutine(this.MurderReactionFunction());
                    if (Hazu)
                    {
                        this.Bools.Prompts.ClearAllPrompts = true;
                        this.Bools.Phone.StartCoroutine(Bools.Phone.QuitPhoneCaught());
                        this.SakuraScript.enabled = false;
                        this.SakuraScript.CanMove = false;
                        Detection.HideDetection();
                        Detected = false;
                        StartSakuraRotation = true;
                        SakuraScript.UpdateAnimationsIdle(0f, 0f);
                        SakuraScript.anim.SetLayerWeight(9, 1f);
                        SakuraScript.anim.SetLayerWeight(10, 1f);
                        if (SakuraScript.CurrentItem != null)
                        {
                            DropNonWeapons();
                            DropOtherItems();
                            DropKnife();
                        }
                    }
                }
            }
        }
        if (!CanSeePlayer && !CanSeeBlood && !CanSeeCorpse && !CanSeeAiming && !Turn || Alarmed && !PlayerFound)
        {
            this.Detection.HideDetection();
            this.Detected = false;
        }
        if (CanSeeBlood)
        {
            if (CanReact)
            {
                if (CurrentBlood != null && !SeenBlood.Contains(CurrentBlood))
                {
                    ////PlayerPrefs.SetInt("BloodDiscovered", SeenBlood.Count);
                    if (!this.Detected)
                    {
                        this.Detection.duration = 0.4f;
                        this.Detection.ShowDetection();
                        this.Detected = true;
                    }
                    if (Detection.FullyDetected)
                    {
                        this.Detection.HideDetection();
                        this.TalkingSc.Alarmed = true;
                        Investigating = false;
                        EvidenceSc.PoliceBeingCalled = true;
                        this.CallingPolice = true;
                        this.StudentState.Alarmed = true;
                        if (SeenBlood.Count < 1)
                        {
                            base.StartCoroutine(this.BloodReactionFunction());
                        }
                        SeenBlood.Add(CurrentBlood);
                    }
                }
            }
        }
        if (CanSeeCorpse)
        {
            if (CanReact)
            {
                if (CurrentCorpse != null && !SeenCorpses.Contains(CurrentCorpse))
                {
                    ////PlayerPrefs.SetInt("CorpsesDiscovered", SeenCorpses.Count);
                    if (!this.Detected)
                    {
                        this.Detection.duration = 0.4f;
                        this.Detection.ShowDetection();
                        this.Detected = true;
                    }
                    if (Detection.FullyDetected)
                    {
                        this.Detection.HideDetection();
                        this.TalkingSc.Alarmed = true;
                        Investigating = false;
                        EvidenceSc.PoliceBeingCalled = true;
                        this.CallingPolice = true;
                        this.StudentState.Alarmed = true;
                        if (SeenCorpses.Count < 1)
                        {
                            base.StartCoroutine(this.CorpseReactionFunction());
                        }
                        SeenCorpses.Add(CurrentCorpse);
                    }
                }
            }
        }
        float DistanceToStudent = Vector3.Distance(transform.position, RunAway.position);
        if (DistanceToStudent <= 2f)
        {
            this.StudentTransform.localPosition = DestroyedPosition;
            this.PathAgent.isStopped = true;
            this.PathAgent.enabled = false;
            if (CallingPolice)
            {
                this.EvidenceScreen.SetActive(true);
                this.EvidenceSc.TimerOn = true;
            }
        }
        DistanceToSakura = Vector3.Distance(StudentTransform.position, Sakura.transform.position);
        if (DistanceToSakura < 2f && this.TalkingSc.attack.CanFight && this.Fighting)
        {
            if (SakuraScript.CurrentFightingCharacter != null && SakuraScript.CurrentFightingCharacter != gameObject)
            {
                if (SakuraScript.CurrentFightingCharacter.GetComponent<FieldOfView>())
                {
                    SakuraScript.CurrentFightingCharacter.GetComponent<FieldOfView>().Detection.HideDetection();
                    SakuraScript.CurrentFightingCharacter.GetComponent<FieldOfView>().StruggleKey.SetActive(false);
                    SakuraScript.CurrentFightingCharacter.GetComponent<FieldOfView>().StartCoroutine("FightReactionFunction");
                    SakuraScript.CurrentFightingCharacter.GetComponent<FieldOfView>().Fighting = false;
                }
                else
                {
                    SakuraScript.CurrentFightingCharacter.GetComponent<FightSakura>().StruggleKey.SetActive(false);
                    SakuraScript.CurrentFightingCharacter.GetComponent<FightSakura>().StartCoroutine("FightReactionFunction");
                    SakuraScript.CurrentFightingCharacter.GetComponent<FightSakura>().Fighting = false;
                }
            }
            SakuraScript.CurrentFightingCharacter = gameObject;
            this.Bools.Prompts.ClearAllPrompts = true;
            Detection.HideDetection();
            Detected = false;
            if (!DoneStuff)
            {
                this.Bools.Phone.StartCoroutine(Bools.Phone.QuitPhoneCaught());
                if (SakuraScript.poisoning)
                {
                    SakuraScript.PoisonScript.Poisoned();
                }
                DoneStuff = true;
            }
            this.StudentState.enabled = false; CancelInvoke("BackToState"); Looking = false; Turn = false;
            if (this.EvidenceSc.weaponscript[0].GetComponent<PickupScript>().PickedUp)
            {
                DropNonWeaponsTeachers();
                StruggleKey.SetActive(true);
                this.Sakura.layer = 15;
                this.SakuraScript.Fighting = true;
                this.PathAgent.enabled = false;
                this.Sakura2.transform.position = this.Teacher2.position;
                this.Sakura2.transform.rotation = this.Teacher2.rotation;
                if (!this.BoolScript.won && !this.BoolScript.lost)
                {
                    this.SakuraScript.anim.Play("Struggle");
                    StudentAnimator.ResetTrigger(StudentState.WalkName);
                    this.StudentAnimator.Play("Struggle");
                }
                this.SakuraScript.CanMove = false;
                this.SakuraScript.enabled = false;
                this.Fighting = true;
            }
            else
            {
                StruggleKey.SetActive(false);
                this.Fighting = true;
                this.BoolScript.lost = true;
            }
        }
        if (this.Hazu && this.Alarmed && this.Bools.CaughtByHazu || SakuraNoticed && !PathAgent.hasPath || Fighting)
        {
            Vector3 dirToOther = SakuraTransform.position - StudentTransform.position;
            dirToOther.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(dirToOther);
            this.StudentTransform.rotation = Quaternion.Slerp(StudentTransform.rotation, targetRotation, 6 * Time.deltaTime);
        }
        if (this.StartSakuraRotation && (!TalkingSc.attack.HazuFieldOfView.StartSakuraRotation && !Hazu || Hazu))
        {
            SakuraScript.CanMove = false;
            Vector3 dirToOther2 = StudentTransform.position - SakuraTransform.position;
            dirToOther2.y = 0;
            Quaternion targetRotation2 = Quaternion.LookRotation(dirToOther2);
            this.SakuraTransform.rotation = Quaternion.Slerp(SakuraTransform.rotation, targetRotation2, 6 * Time.deltaTime);
        }

    }

    public bool HasNavMeshAgentReachedDestination()
    {
        if (!PathAgent.isOnNavMesh)
        {
            return false;
        }

        if (!PathAgent.pathPending)
        {
            if (PathAgent.remainingDistance <= PathAgent.stoppingDistance)
            {
                if (!PathAgent.hasPath || PathAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }

    void FieldOfViewCheck()
    {
        CanSeePlayer = false;
        SakuraBeingSeen = false;

        if (SakuraTransform == null)
            return;

        Vector3 dirToPlayer = (SakuraTransform.position - transform.position).normalized;
        float dstToPlayer = Vector3.Distance(transform.position, SakuraTransform.position);

        if (dstToPlayer < ViewRadius)
        {
            if (Yandere)
            {
                if (SakuraScript.MakingNoise)
                {
                    LoseSightTimer = 0f;
                    ObstacleMask = EmptyMask;
                    CanSeePlayer = true;
                }
                else
                {
                    ObstacleMask = YandereMask;
                }
            }
            float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleToPlayer < ViewAngle / 2f)
            {
                Vector3 eyePosition = transform.position + Vector3.up * 1.6f;
                Ray ray = new Ray(eyePosition, (SakuraTransform.position + Vector3.up * 1.6f - eyePosition).normalized);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, dstToPlayer, ObstacleMask))
                {
                    return;
                }

                if ((PlayerMask.value & (1 << SakuraTransform.gameObject.layer)) != 0 && !SakuraScript.BlindEveryone)
                {
                    CanSeePlayer = true;
                }
                if (!SakuraScript.BlindEveryone && !Yandere)
                {
                    SakuraBeingSeen = true;
                }
            }
        }
    }
    void AimingCheck()
    {
        CanSeeAiming = false;

        if (SakuraTransform == null)
            return;

        Vector3 dirToPlayer = (SakuraTransform.position - transform.position).normalized;
        float dstToPlayer = Vector3.Distance(transform.position, SakuraTransform.position);

        if (dstToPlayer < ViewRadius)
        {
            float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleToPlayer < ViewAngle / 2f)
            {
                Vector3 eyePosition = transform.position + Vector3.up * 1.6f;
                Ray ray = new Ray(eyePosition, (SakuraTransform.position + Vector3.up * 1.6f - eyePosition).normalized);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, dstToPlayer, ObstacleMask))
                {
                    return;
                }

                if ((AimingMask.value & (1 << SakuraTransform.gameObject.layer)) != 0 && !SakuraScript.BlindEveryone)
                {
                    CanSeeAiming = true;
                }
            }
        }
    }
    void CorpseCheck()
    {
        CanSeeCorpse = false;

        Vector3 eyePosition = transform.position + Vector3.up * 1.6f;

        Collider[] corpseInView = Physics.OverlapSphere(transform.position, ViewRadius, CorpseMask);

        foreach (Collider corpse in corpseInView)
        {
            Vector3 corpsePosition = corpse.transform.position + Vector3.up * 0.05f;
            Vector3 dirToCorpse = (corpsePosition - transform.position).normalized;

            float angleToCorpse = Vector3.Angle(transform.forward, dirToCorpse);

            if (angleToCorpse < ViewAngle / 2f)
            {
                float distance = Vector3.Distance(eyePosition, corpsePosition);
                Vector3 direction = (corpsePosition - eyePosition).normalized;

                if (!Physics.Raycast(eyePosition, direction, distance, ObstacleMask))
                {
                    CanSeeCorpse = true;
                    CurrentCorpse = corpse.gameObject;
                    return;
                }
            }
        }
    }

    void BloodCheck()
    {
        CanSeeBlood = false;

        Vector3 eyePosition = transform.position + Vector3.up * 1.6f;

        Collider[] bloodInView = Physics.OverlapSphere(transform.position, ViewRadius, BloodMask);

        foreach (Collider blood in bloodInView)
        {
            Vector3 bloodPosition = blood.transform.position + Vector3.up * 0.05f;
            Vector3 dirToBlood = (bloodPosition - transform.position).normalized;

            float angleToBlood = Vector3.Angle(transform.forward, dirToBlood);

            if (angleToBlood < ViewAngle / 2f)
            {
                float distance = Vector3.Distance(eyePosition, bloodPosition);
                Vector3 direction = (bloodPosition - eyePosition).normalized;

                if (!Physics.Raycast(eyePosition, direction, distance, ObstacleMask))
                {
                    CanSeeBlood = true;
                    CurrentBlood = blood.gameObject;
                    return;
                }
            }
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ViewRadius);

        Vector3 angleA = DirFromAngle(-ViewAngle / 2, false);
        Vector3 angleB = DirFromAngle(ViewAngle / 2, false);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + angleA * ViewRadius);
        Gizmos.DrawLine(transform.position, transform.position + angleB * ViewRadius);

        if (CanSeePlayer && SakuraTransform != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, SakuraTransform.position);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
            angleInDegrees += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public IEnumerator CheckRunAnimation()
    {
        yield return new WaitUntil(() => PathAgent.hasPath);

        StudentAnimator.ResetTrigger("Idle");
        StudentAnimator.SetTrigger("Run");
        this.StudentAnimator.ResetTrigger(StudentState.AnimationName);
        this.StudentAnimator.ResetTrigger(StudentState.IdleName);
        this.StudentAnimator.ResetTrigger(StudentState.WalkName);
    }

}
