using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameras : MonoBehaviour
{
    public PlayerController sakura;
    public GameObject ChainSaw, Shovel;
    public GameObject BlackScreen;
    public GameObject GameOverS;
	public GameOver gameoverscript;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.sakura.Bloody || this.sakura.killing || this.sakura.carrying || this.sakura.CurrentItem == this.sakura.Knife && this.sakura.HasWeapon || this.sakura.CurrentItem == this.ChainSaw || this.sakura.CurrentItem == this.Shovel && this.Shovel.GetComponent<PickupScript>().Dangerous || this.sakura.CurrentItem != null && this.sakura.CurrentItem.GetComponent<BloodyUniform>().Bloody)
            {
                this.sakura.anim.Play("Idle");
                this.sakura.CanMove = false;
                BlackScreen.SetActive(true);
                base.Invoke("Reset", 3f);
            }
        }
    }

    public void Reset()
    {
        BlackScreen.SetActive(false);
        this.gameoverscript.GameOverText.text = "CAUGHT";
        this.gameoverscript.GameOverExplanation.text = "Oh man... these cameras are the worst!";
		this.GameOverS.SetActive(true);
    }


}
