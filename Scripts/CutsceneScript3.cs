using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class CutsceneScript3 : MonoBehaviour
{
    public Image radialfill;
    public HeadController sakurahead, hazuhead;
    public TMP_Text subtitle;
    public int stage;
    public bool sakuralooking, hazulooking, worldchanging;
    public Animator sakuraanimator, hazuanimator, akimuraanimator;
    public AudioSource Hazu1, Hazu2, Hazu3, Hazu4, Hazu5, Hazu6, Hazu7, Akimura1, Akimura2, Akimura3, Akimura4, Akimura5, Akimura6, Akimura7;
    public PostProcessVolume volume;
    private ChromaticAberration _chromatic;
    private ColorGrading color;
    private Vignette _vignette;
    public GameObject Black, SkipButton;
    public CutsceneState Akimura;

    void Start()
    {
        this.volume.profile.TryGetSettings<Vignette>(out this._vignette);
        this.volume.profile.TryGetSettings<ChromaticAberration>(out this._chromatic);
        this.volume.profile.TryGetSettings<ColorGrading>(out this.color);
        base.StartCoroutine(this.StartCutscene());
    }
    private IEnumerator SkipToLoading()
    {
        SkipButton.SetActive(false);
        Black.SetActive(true);
        yield return new WaitForSeconds(2F);
        SceneManager.LoadScene("Bedroom");
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (this.radialfill.fillAmount < 0.1f)
            {
                StartCoroutine(this.SkipToLoading());
            }
            this.radialfill.fillAmount -= Time.deltaTime;
        }
        else
        {
            this.radialfill.fillAmount = 1f;
        }
    }
    IEnumerator StartCutscene()
    {
        Akimura1.Play();
        akimuraanimator.SetInteger("Greet", 1);
        subtitle.text = "Hey Hazu! I've got some good news!";
        yield return new WaitForSeconds(2.7F);
        akimuraanimator.SetInteger("Greet", 0);
        yield return new WaitForSeconds(1.3F);
        Hazu1.Play();
        subtitle.text = "What's up?";
        yield return new WaitForSeconds(2F);
        Akimura2.Play();
        subtitle.text = "I finally got the money to move to a new house!";
        yield return new WaitForSeconds(4F);
        Hazu2.Play();
        subtitle.text = "Really?? I'm so happy for you akimura!";
        yield return new WaitForSeconds(4F);
        Akimura3.Play();
        akimuraanimator.SetInteger("Greet", 2);
        subtitle.text = "But... there Is one thing..";
        yield return new WaitForSeconds(2F);
        akimuraanimator.SetInteger("Greet", 0);
        yield return new WaitForSeconds(4F);
        Akimura4.Play();
        subtitle.text = "It's going to be far away";
        yield return new WaitForSeconds(4F);
        Hazu3.Play();
        subtitle.text = "Does that mean.. we won't see eachother again?";
        yield return new WaitForSeconds(4F);
        Akimura5.Play();
        subtitle.text = "Well... I don't know, maybe we will but, not for now";
        yield return new WaitForSeconds(6F);
        Akimura6.Play();
        subtitle.text = "Hazu, I want to thank you for all the good memories we had, you were the only one who understood me.";
        yield return new WaitForSeconds(8F);
        Hazu5.Play();
        subtitle.text = "Akimura, I'm really sad to see you go..";
        yield return new WaitForSeconds(4F);
        Hazu6.Play();
        subtitle.text = "But wish you the best on whatever you do next!";
        yield return new WaitForSeconds(4F);
        Akimura7.Play();
        subtitle.text = "Thank you! I hope I'll see you again one day";
        yield return new WaitForSeconds(5F);
        Hazu7.Play();
        hazuanimator.SetInteger("Greet", 1);
        subtitle.text = "Goodbye!";
        yield return new WaitForSeconds(2.7F);
        hazuanimator.SetInteger("Greet", 0);
        hazuhead.enabled = true;
        subtitle.text = "";
        this.Akimura.enabled = true;
        Akimura.studentAnimator.SetTrigger("Walk");
        yield return new WaitForSeconds(4F);
        StartCoroutine(this.SkipToLoading());
    }
}
