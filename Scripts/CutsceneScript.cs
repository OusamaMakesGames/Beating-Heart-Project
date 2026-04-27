using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class CutsceneScript : MonoBehaviour
{
    public Image radialfill;
    public CutsceneState sakura, hazu;
    public HeadController sakurahead, hazuhead;
    public TMP_Text subtitle;
    public int stage;
    public bool sakuralooking, hazulooking, worldchanging;
    public Animator sakuraanimator, hazuanimator, cameraanimator, akimuraanimator;
    public GameObject Akimura;
    public AudioSource Hazu1, Hazu2, Hazu3, Hazu4, Sakura1, Sakura2, Sakura3, Sakura4, Sakura5, Sakura6, Akimura1;
    public PostProcessVolume volume;
    private ChromaticAberration _chromatic;
    private ColorGrading color;
    private Vignette _vignette;
    public GameObject Black, SkipButton;
    public AudioSource Music;

    void Start()
    {
        this.volume.profile.TryGetSettings<Vignette>(out this._vignette);
        this.volume.profile.TryGetSettings<ChromaticAberration>(out this._chromatic);
        this.volume.profile.TryGetSettings<ColorGrading>(out this.color);
    }
    private IEnumerator SkipToLoading()
    {
        SkipButton.SetActive(false);
        Black.SetActive(true);
        yield return new WaitForSeconds(2F);
        SceneManager.LoadScene("LoadingScreen");
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
        if (sakura.InDestination && stage == 1)
        {
            base.StartCoroutine(this.StartCutscene());
        }
        if (sakuralooking && sakurahead.currentLookWeight < 0.26f)
        {
            sakurahead.currentLookWeight += 0.5f * Time.deltaTime;
        }
        if (!sakuralooking && sakurahead.currentLookWeight > 0f)
        {
            sakurahead.currentLookWeight -= 0.5f * Time.deltaTime;
        }
        if (hazulooking && hazuhead.currentLookWeight < 0.12f)
        {
            hazuhead.currentLookWeight += 0.4f * Time.deltaTime;
        }
        if (!hazulooking && hazuhead.currentLookWeight > 0f)
        {
            hazuhead.currentLookWeight -= 0.4f * Time.deltaTime;
        }
        if (worldchanging)
        {
            Music.pitch = Mathf.Lerp(this.Music.pitch, 0.4f, 3f * Time.deltaTime);
            this.color.saturation.value = Mathf.Lerp(this.color.saturation.value, -100, 3f * Time.deltaTime);
            _vignette.intensity.value = Mathf.Lerp(_vignette.intensity.value, 0.3f, 3f * Time.deltaTime);
            _chromatic.intensity.value = Mathf.Lerp(_chromatic.intensity.value, 1, 3f * Time.deltaTime);
        }
        else
        {
            Music.pitch = Mathf.Lerp(this.Music.pitch, 1, 3f * Time.deltaTime);
            this.color.saturation.value = Mathf.Lerp(this.color.saturation.value, 14.2f, 3f * Time.deltaTime);
            _vignette.intensity.value = Mathf.Lerp(_vignette.intensity.value, 0f, 3f * Time.deltaTime);
            _chromatic.intensity.value = Mathf.Lerp(_chromatic.intensity.value, 0, 3f * Time.deltaTime);
        }
    }
    IEnumerator StartCutscene()
    {
        sakura.enabled = false;
        Sakura1.Play();
        stage = 0;
        subtitle.text = "So...Hazu, what's your plans for this school year?";
        sakuralooking = true;
        yield return new WaitForSeconds(3.3F);
        yield return new WaitForSeconds(1.1F);
        Hazu1.Play();
        subtitle.text = "I don't really have any plans... I'm just trying to survive..";
        hazulooking = true;
        yield return new WaitForSeconds(3.3F);
        yield return new WaitForSeconds(1.1F);
        Sakura2.Play();
        sakuraanimator.SetBool("Upset", true);
        sakuraanimator.SetTrigger("Refuse");
        subtitle.text = "Hazu! don't be like that, I'm sure it'll be fun!";
        sakuralooking = true;
        hazulooking = false;
        yield return new WaitForSeconds(3.3F);
        yield return new WaitForSeconds(2F);
        hazu.enabled = false;
        hazuanimator.SetTrigger("Wave");
        subtitle.text = "Oh! there she is!";
        Hazu4.Play();
        yield return new WaitForSeconds(1.41F);
        yield return new WaitForSeconds(1.1F);
        Sakura3.Play();
        Akimura.SetActive(true);
        sakuralooking = false;
        subtitle.text = "Huh??";
        sakuraanimator.SetBool("Upset", false);
        yield return new WaitForSeconds(1.4F);
        subtitle.text = "";
        akimuraanimator.Play("Wave");
        cameraanimator.Play("AkimuraReveal");
        yield return new WaitForSeconds(2.5F);
        akimuraanimator.SetBool("OpenEyes", true);
        subtitle.text = "Hello! My name is Akimura! I'm Hazu's friend";
        Akimura1.Play();
        yield return new WaitForSeconds(3.4F);
        yield return new WaitForSeconds(2F);
        sakuraanimator.SetBool("Crazy", true);
        worldchanging = true;
        subtitle.text = "*No...I can't let her take him away from me!*";
        cameraanimator.Play("SakuraZoom");
        Sakura4.Play();
        yield return new WaitForSeconds(4.4F);
        sakuraanimator.SetBool("Crazy", false);
        Hazu2.Play();
        worldchanging = false;
        subtitle.text = "Sakura.. Are you alright?";
        hazulooking = true;
        yield return new WaitForSeconds(2.2F);
        yield return new WaitForSeconds(1.1F);
        hazulooking = false;
        sakuraanimator.Play("Sane");
        cameraanimator.Play("differentview");
        sakuraanimator.SetTrigger("Wave");
        subtitle.text = "Oh! I'm sorry, my name is Sakura! nice to meet you!";
        Sakura5.Play();
        this.sakuraanimator.ResetTrigger("Idle");
        yield return new WaitForSeconds(4.3F);
        sakuraanimator.ResetTrigger("Wave");
        this.sakuraanimator.SetTrigger("Idle");
        hazulooking = true;
        Hazu3.Play();
        subtitle.text = "Me and Akimura are actually going to hangout right now, can we talk later?";
        yield return new WaitForSeconds(4.2F);
        yield return new WaitForSeconds(1.1F);
        sakuraanimator.SetTrigger("Wave");
        this.sakuraanimator.ResetTrigger("Idle");
        subtitle.text = "Alright.. See you later...";
        sakuralooking = true;
        Sakura6.Play();
        yield return new WaitForSeconds(0.3F);
        hazulooking = false;
        yield return new WaitForSeconds(1.1F);
        this.sakuraanimator.ResetTrigger("Wave");
        sakuraanimator.SetTrigger("Idle");
        hazuanimator.ResetTrigger("Wave");
        this.hazuanimator.SetTrigger("Walk");
        hazu.enabled = true;
        subtitle.text = "";
        hazu.FirstDest = false;
        yield return new WaitForSeconds(1F);
        worldchanging = true;
        sakuraanimator.SetBool("Crazy", true);
        yield return new WaitForSeconds(3F);
        Black.SetActive(true);
        yield return new WaitForSeconds(2F);
        StartCoroutine(this.SkipToLoading());
    }
}
