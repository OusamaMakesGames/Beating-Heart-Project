using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SecondEndingCutscene : MonoBehaviour
{
    public GameObject Camera1, Camera2, Camera3, Camera4, Hands, TownSakura, Canvas;
    public Animator Cameraanim, Sakura;
    public GameObject Point, Point2;
    public float speed;
    public GameObject Blood;
    public Vector3 target;
    public HeadController Head;
    public AudioSource Notif;
    public SpriteRenderer SpriteRend;
    public Sprite spriteImage;
    public GameObject Text, Phone;
    public GameObject Text1, Text2, Text3, Text4;
    public bool GoCrazy;
    private PostProcessVolume volume;
	private ChromaticAberration _vig;
    public float increaseSpeed = 0.5f;
    private float targetWeight = 1.0f;
    private float currentWeight = 0.0f;
    public GameObject BlackScreen;
    public Text Reply;
    public AudioSource Music;
    public bool Distort;
    public GameObject Warning, SkipButton;
    public Image radialfill;

    public AIPathScript AI;

    public AudioSource Line;

    private IEnumerator SkipToLoading()
    {
        SkipButton.SetActive(false);
        BlackScreen.SetActive(true);
        yield return new WaitForSeconds(2F);
        PlayerPrefs.SetInt("Day", 3);
        SceneManager.LoadScene("Bedroom");
    }

    void Start()
    {
        base.StartCoroutine(this.StartCutscene());
        this.volume = FindObjectOfType<PostProcessVolume>();
		this.volume.profile.TryGetSettings<ChromaticAberration>(out this._vig);
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
        if (speed != 0f)
        {
        if (Point.transform.position.y < 1f)
        {
            Point.transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        }
        if (Head.enabled == false)
        {
            transform.localPosition = Vector3.MoveTowards (transform.localPosition, target, Time.deltaTime * 2f);
            transform.LookAt(target);
        }
        
        if (GoCrazy)
        {
            this.Music.pitch = Mathf.Lerp(this.Music.pitch, 0.3f, Time.deltaTime * 2f);
            this._vig.intensity.value = Mathf.Lerp(this._vig.intensity.value, 1f, Time.deltaTime * 2f);
            currentWeight = Mathf.Clamp(currentWeight + increaseSpeed * Time.deltaTime, 0.0f, targetWeight);
            GetComponent<Animator>().SetLayerWeight(1, currentWeight);
        }
        if (Distort)
        {
            this.Music.pitch = Mathf.Lerp(this.Music.pitch, 0.3f, Time.deltaTime * 2f);
            this._vig.intensity.value = Mathf.Lerp(this._vig.intensity.value, 1f, Time.deltaTime * 2f);
        }
    }


    public IEnumerator StartCutscene()
	{
        yield return new WaitForSeconds(10F);
        speed = 1f;
		yield return new WaitForSeconds(5F);
        Camera2.SetActive(true);
        Hands.SetActive(true);
        Camera1.SetActive(false);
        yield return new WaitForSeconds(0.1F);
        Distort = true;
        Blood.SetActive(true);
        yield return new WaitForSeconds(0.1F);
        Blood.SetActive(false);
        Distort = false;
        this.Music.pitch = 1f;
        this._vig.intensity.value = 0.4f;
        yield return new WaitForSeconds(0.1F);
        Blood.SetActive(true);
        Distort = true;
        yield return new WaitForSeconds(0.1F);
        Blood.SetActive(false);
        Distort = false;
        this.Music.pitch = 1f;
        this._vig.intensity.value = 0.4f;
        yield return new WaitForSeconds(0.1F);
        Blood.SetActive(true);
        Distort = true;
        yield return new WaitForSeconds(0.1F);
        Blood.SetActive(false);
        Distort = false;
        this.Music.pitch = 1f;
        this._vig.intensity.value = 0.4f;
        yield return new WaitForSeconds(0.1F);
        Blood.SetActive(true);
        Distort = true;
        yield return new WaitForSeconds(0.1F);
        Blood.SetActive(false);
        Distort = false;
        this.Music.pitch = 1f;
        this._vig.intensity.value = 0.4f;
        yield return new WaitForSeconds(0.1F);
        Blood.SetActive(true);
        yield return new WaitForSeconds(0.1F);
        Blood.SetActive(false);
        Distort = false;
        this.Music.pitch = 1f;
        this._vig.intensity.value = 0.4f;
        yield return new WaitForSeconds(0.1F);
        Blood.SetActive(true);
        Distort = true;
        yield return new WaitForSeconds(0.1F);
        Blood.SetActive(false);
        Distort = false;
        this.Music.pitch = 1f;
        this._vig.intensity.value = 0.4f;
        yield return new WaitForSeconds(0.1F);
        Distort = true;
        var modifiedPosition = transform.position;
        modifiedPosition.x = 18.61f;
        modifiedPosition.y = -0.27f;
        transform.position = modifiedPosition;
        Sakura.Play("Walk");
        Camera3.SetActive(true);
        Camera2.SetActive(false);
        Hands.SetActive(false);
        yield return new WaitForSeconds(0.3F);
        Distort = false;
        this.Music.pitch = 1f;
        this._vig.intensity.value = 0.4f;
        Camera3.SetActive(false);
        Camera1.SetActive(true);
        Camera2.SetActive(false);
        Cameraanim.Play("FootCloseUp");
        Head.enabled = false;
        yield return new WaitForSeconds(1.5F);
        Sakura.SetTrigger("Sad");
        yield return new WaitForSeconds(2F);
        Text.SetActive(true);
        Line.Play();
        yield return new WaitForSeconds(5F);
        Text.SetActive(false);
        yield return new WaitForSeconds(13F);
        Sakura.ResetTrigger("Sad");
        Sakura.Play("Text");
        Cameraanim.Play("PhoneCloseUp");
        yield return new WaitForSeconds(1F);
        Notif.Play();
        SpriteRend.sprite = spriteImage;
        Phone.SetActive(true);
        yield return new WaitForSeconds(4F);
        Cameraanim.Play("StandCloseUp");
        Text1.SetActive(true);
        yield return new WaitForSeconds(2F);
        Text2.SetActive(true);
        Notif.Play();
        yield return new WaitForSeconds(5F);
        Text3.SetActive(true);
        Notif.Play();
        yield return new WaitForSeconds(5F);
        Text4.SetActive(true);
        Reply.text = "a";
        yield return new WaitForSeconds(0.1F);
        Reply.text = "ag";
        yield return new WaitForSeconds(0.1F);
        Reply.text = "aga";
        yield return new WaitForSeconds(0.1F);
        Reply.text = "agai";
        yield return new WaitForSeconds(0.1F);
        Reply.text = "again";
        yield return new WaitForSeconds(0.1F);
        Reply.text = "again?";
        yield return new WaitForSeconds(0.1F);
        Reply.text = "again? I";
        yield return new WaitForSeconds(0.3F);
        Reply.text = "again? I'";
        yield return new WaitForSeconds(0.3F);
        Reply.text = "again? I'l";
        yield return new WaitForSeconds(0.3F);
        Reply.text = "again? I'll";
        yield return new WaitForSeconds(0.3F);
        Reply.text = "again? I'll k";
        yield return new WaitForSeconds(0.01F);
        GoCrazy = true;
        Reply.text = "again? I'll ki";
        yield return new WaitForSeconds(0.01F);
        Reply.text = "again? I'll kil";
        yield return new WaitForSeconds(0.01F);
        Reply.text = "again? I'll kill";
        yield return new WaitForSeconds(0.01F);
        Reply.text = "again? I'll kil";
        yield return new WaitForSeconds(0.01F);
        Reply.text = "again? I'll ki";
        yield return new WaitForSeconds(0.01F);
        Reply.text = "again? I'll k";
        yield return new WaitForSeconds(0.01F);
        Reply.text = "again? I'll k";
        yield return new WaitForSeconds(0.01F);
        Reply.text = "again? I'll ";
        yield return new WaitForSeconds(0.02F);
        GoCrazy = true;
        Reply.text = "again? I'll d";
        yield return new WaitForSeconds(0.05F);
        Reply.text = "again? I'll de";
        yield return new WaitForSeconds(0.05F);
        Reply.text = "again? I'll dea";
        yield return new WaitForSeconds(0.05F);
        Reply.text = "again? I'll deal";
        yield return new WaitForSeconds(0.05F);
        Reply.text = "again? I'll deal w";
        yield return new WaitForSeconds(0.05F);
        Reply.text = "again? I'll deal wi";
        yield return new WaitForSeconds(0.05F);
        Reply.text = "again? I'll deal wit";
        yield return new WaitForSeconds(0.05F);
        Reply.text = "again? I'll deal with";
        yield return new WaitForSeconds(0.05F);
        Reply.text = "again? I'll deal with h";
        yield return new WaitForSeconds(0.05F);
        Reply.text = "again? I'll deal with hi";
        yield return new WaitForSeconds(0.05F);
        Reply.text = "again? I'll deal with him";
        yield return new WaitForSeconds(5.3F);
        BlackScreen.SetActive(true);
        Music.volume = 0f;
        yield return new WaitForSeconds(5F);
        Camera4.SetActive(true);
        Text.SetActive(false);
        Text1.SetActive(false);
        Text2.SetActive(false);
        Text3.SetActive(false);
        Text4.SetActive(false);
        BlackScreen.SetActive(false);
        TownSakura.SetActive(true);
        AI.enabled = true;
        yield return new WaitForSeconds(1F);
        BlackScreen.SetActive(true);
        yield return new WaitForSeconds(1F);
        PlayerPrefs.SetInt("Day", 3);
        SceneManager.LoadScene("Bedroom");

	}

}
