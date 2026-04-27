using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyWaterScript : MonoBehaviour
{
    private Prompt PromptScript;
    public AttackScript SportyGuy;
    public PlayerController SakuraScript;
    public bool PlayedSound;
    public AudioSource Vending;

    void Start()
    {
        PromptScript = GetComponent<Prompt>();
    }
    void Update()
    {
        if (PromptScript.MePressed)
        {
            if (SakuraScript.Money > 99f && !SportyGuy.HasTaskItem)
            {
                SportyGuy.WaterTaskActivated = false;
                SakuraScript.Coins.Play();
                Vending.Play();
		        SakuraScript.MoneyAnimator.Play("Fade");
		        SakuraScript.MoneyAnimatorText.text = "¥100-";
                SakuraScript.Money -= 100;
                PlayerPrefs.SetFloat("amount", SakuraScript.Money);
                SportyGuy.HasTaskItem = true;
                this.SakuraScript.InfoSound.Play();
                this.SakuraScript.Info.Play("infoshow");
                this.SakuraScript.infotext.text = "You bought a water bottle!";
            }
            else
            if (SakuraScript.Money < 100f && !PlayedSound)
            {
                this.PlayedSound = true;
                this.SakuraScript.InfoSound.Play();
                this.SakuraScript.Info.Play("infoshow");
                this.SakuraScript.infotext.text = "You need ¥100 to buy that!";
            }
        }
        if (SportyGuy.WaterTaskActivated)
        {
            PromptScript.Distance = 4f;
        }
        if (SportyGuy.HasTaskItem)
        {
            PromptScript.Distance = 0f;
        }
    }
}
