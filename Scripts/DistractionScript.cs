using UnityEngine;

public class DistractionScript : MonoBehaviour
{
    public float distractionRadius = 5f;
    public LayerMask studentmask;
    public bool isActivated = false;
    public Prompt PromptScript;
    public AudioSource broken;
    public ParticleSystem Noise;
    public StudentState Yandere;
    public StudentState StudentChosen;
    public bool InBathroom;

    private void Start()
    {

    }

    public void ActivateDistraction()
    {
        isActivated = true;
        this.PromptScript.CurrentMode = Prompt.PromptMode.Deactivate;
        this.PromptScript.Text = "Deactivate";
        broken.Play();
        Noise.Play();
    }

    public void DeactivateD()
    {
        this.Yandere.talkingscript.SakuraMovement.ManagingText.CancelInvoke("NoText");
        isActivated = false;
        this.PromptScript.CurrentMode = Prompt.PromptMode.Activate;
        this.PromptScript.Text = "Activate";
        broken.Stop();
        if (StudentChosen != null)
        {
            StudentChosen.ResetDistractionFromOtherScript();
            StudentChosen.reachedradio = false;
        }
        StudentChosen = null;
        Noise.Stop();
    }

    private void Update()
    {
        if (isActivated && StudentChosen == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, distractionRadius, studentmask);

            foreach (Collider collider in colliders)
            {
                StudentState studentdistracted = collider.GetComponentInParent<StudentState>();
                if (studentdistracted != null && !studentdistracted.reachedradio)
                {
                    if (Mathf.Abs(studentdistracted.transform.position.y - transform.position.y) < 0.5f)
                    {
                        if (studentdistracted != Yandere && !InBathroom && !studentdistracted.talkingscript.Alarmed && !studentdistracted.talkingscript.attack.fov.Investigating && !studentdistracted.talkingscript.attack.fov.Turn)
                        {
                            StudentChosen = studentdistracted;
                        }

                    }
                }
            }
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BathroomCollider"))
        {
            InBathroom = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BathroomCollider"))
        {
            InBathroom = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BathroomCollider"))
        {
            InBathroom = false;
        }
    }
}