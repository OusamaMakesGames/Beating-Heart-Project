using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBG : MonoBehaviour
{
    public RawImage img;
    public float x, y;
    [SerializeField] RectTransform[] characterbutton;
    [SerializeField] RectTransform Heart;
    [SerializeField] float MoveDelay;

    public int HeartPosition;

    public AudioSource Select;

    public bool PoemScreenOpen;

    public GameObject Canvas, MainCanvas;

    public Texture Romance, Happy, Creepy;
    
    public AudioSource scribble;

    public GameObject Camera, Circle;

    public PlayerController Player;

    public DebugScript debug;
    
    void Start()
    {
        
    }
    void Update()
    {
        img.uvRect = new Rect (img.uvRect.position + new Vector2(x,y) * Time.deltaTime, img.uvRect.size);
        if (Input.GetKeyDown(KeyCode.DownArrow) && PoemScreenOpen || Input.GetKeyDown(KeyCode.S) && PoemScreenOpen)
        {
            if (HeartPosition < characterbutton.Length - 1)
            {
                    this.Select.Play();
                    HeartPosition++;
            }
            else
            {
                this.Select.Play();
                HeartPosition = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && PoemScreenOpen|| Input.GetKeyDown(KeyCode.W) && PoemScreenOpen)
        {
                if (HeartPosition > 0)
            {
                    this.Select.Play();
                    HeartPosition--;
            }
            else
            {
                this.Select.Play();
                HeartPosition = characterbutton.Length - 1;
            }
        }
        if (PoemScreenOpen)
        {
            Player.anim.Play("Idle");
            this.MainCanvas.SetActive(false);
            debug.enabled = false;
            Player.enabled = false;
            Camera.SetActive(true);
        }
        else
        {
            Player.enabled = true;
            debug.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.E) && PoemScreenOpen)
        {
            this.Select.Play();
            Camera.SetActive(false);
            scribble.Play();
            Circle.SetActive(false);
            this.PoemScreenOpen = false;
            this.Canvas.SetActive(false);
            this.MainCanvas.SetActive(true);
        }
        if (HeartPosition == 0)
        {
            img.texture = Romance;
            PlayerPrefs.SetInt("PoemTopic", 1);
        }
        if (HeartPosition == 1)
        {
            img.texture = Happy;
            PlayerPrefs.SetInt("PoemTopic", 2);
        }
        if (HeartPosition == 2)
        {
            img.texture = Creepy;
            PlayerPrefs.SetInt("PoemTopic", 3);
        }
        Heart.localPosition = characterbutton[HeartPosition].localPosition;
    }
}
