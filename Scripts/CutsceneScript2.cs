using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class CutsceneScript2 : MonoBehaviour
{
    public Image radialfill;
    public Text subtitle;
    public bool worldchanging;
    public PostProcessVolume volume;
    private ChromaticAberration _chromatic;
    private ColorGrading color;
    private Vignette _vignette;
    public GameObject Black;
    public AudioSource Music, Line1, Line2;
    public GameObject Hearts, SkipButton;
    public Color Origin;
    public Transform targetPosition;
    public float speed = 2f;
    public float arrivalThreshold = 0.1f;
    public Animator Sakura;
    public AudioSource Suspense;
    public bool TestCompetitor;
    public Material Afternoon;
    public Color endColor;

    void Start()
    {
        RenderSettings.skybox.SetColor("_Tint", Origin);
        this.volume.profile.TryGetSettings<Vignette>(out this._vignette);
        this.volume.profile.TryGetSettings<ChromaticAberration>(out this._chromatic);
        this.volume.profile.TryGetSettings<ColorGrading>(out this.color);
        if (TestCompetitor)
        {
            color.temperature.value = 72f;
            Afternoon.color = endColor;
        }
        base.StartCoroutine(this.StartCutscene());
    }
    private IEnumerator SkipToLoading()
    {
        SkipButton.SetActive(false);
        Black.SetActive(true);
        yield return new WaitForSeconds(2F);
        if (!TestCompetitor)
        {
            SceneManager.LoadScene("LoadingScreen");
        }
        else
        {
            SceneManager.LoadScene("Bedroom");
        }
    }
    void Update()
    {
        if (targetPosition != null)
        {
            Vector3 direction = targetPosition.position - transform.position;
            float distance = direction.magnitude;
            if (distance >= arrivalThreshold)
            {
                direction.Normalize();
                transform.Translate(direction * speed * Time.deltaTime);
            }
            if (distance <= arrivalThreshold)
            {
                Sakura.Play("Idle");
            }
        }
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
        if (worldchanging)
        {
            Music.pitch = Mathf.Lerp(this.Music.pitch, 0.1f, 3f * Time.deltaTime);
            this.color.saturation.value = Mathf.Lerp(this.color.saturation.value, -100, 3f * Time.deltaTime);
            _vignette.intensity.value = Mathf.Lerp(_vignette.intensity.value, 0.6f, 3f * Time.deltaTime);
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
        yield return new WaitForSeconds(14F);
        Sakura.SetBool("Crazy", true);
        worldchanging = true;
        Suspense.Play();
        yield return new WaitForSeconds(14F);
        StartCoroutine(this.SkipToLoading());
    }
}
