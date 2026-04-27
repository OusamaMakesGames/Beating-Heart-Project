using UnityEngine;

public class FridgeScript : MonoBehaviour
{
    public Prompt PromptScript;
    public PlayerController SakuraScript;

    void Update()
    {
        if (!SakuraScript.CanPoison && !SakuraScript.bools.CanGiveCupcake)
        {
            PromptScript.Distance = 4f;
        }
        else
        {
            PromptScript.Distance = 0f;
        }
        if (PromptScript.MePressed)
        {
            PromptScript.Distance = 0f;
            SakuraScript.CanPoison = true;
            SakuraScript.InfoSound.Play();
            SakuraScript.Info.Play("infoshow");
            SakuraScript.infotext.text = "Place the cupcake on the kitchen table to poison it";
            PromptScript.MePressed = false;
        }
    }
}
