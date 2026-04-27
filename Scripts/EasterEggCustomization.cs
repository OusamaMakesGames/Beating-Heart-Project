using UnityEngine;

public class EasterEggCustomization : MonoBehaviour
{
    public PhoneScript Phone;
    public GameObject[] Earrings;
    public GameObject[] Hats;
    public GameObject[] Clips;
    public GameObject[] Bags;
    public GameObject[] Bracelets;
    public GameObject[] Ties;
    public GameObject[] Hairs;
    private int Earring = 0;
    private int Hat = 0;
    private int Clip = 0;
    private int Bag = 0;
    private int Bracelet = 0;
    private int Tie = 0;
    private int Hair = 0;
    public SkinnedMeshRenderer skinnedMeshRenderer, AkimuraMaterials;
    public Material Transparent;
    public Material ShortTie;
    public Material LongTie;
    public Mesh OriginalMesh, LongTieMesh;
    public CustomHairScript CustomScript;
    public Material AkimuraHairFlowers;

    void Update()
    {
        Material[] mats = new Material[7];
        mats[0] = CustomScript.BodyMaterial;
        mats[1] = CustomScript.SkirtMaterial;
        mats[2] = CustomScript.SocksMaterial;
        mats[3] = CustomScript.ShoesMaterial;
        mats[4] = CustomScript.BackMaterial;
        mats[5] = CustomScript.TopMaterial;
        mats[4] = Transparent;
        switch (Tie)
        {
            case 0:
                skinnedMeshRenderer.quality = SkinQuality.Auto;
                skinnedMeshRenderer.sharedMesh = OriginalMesh;
                mats[6] = ShortTie;
                break;

            case 1:
                skinnedMeshRenderer.quality = SkinQuality.Bone2;
                skinnedMeshRenderer.sharedMesh = LongTieMesh;
                mats[6] = LongTie;
                break;

            case 2:
            case 3:
                skinnedMeshRenderer.quality = SkinQuality.Auto;
                skinnedMeshRenderer.sharedMesh = OriginalMesh;
                mats[6] = Transparent;
                break;
        }

        skinnedMeshRenderer.materials = mats;
        if (!this.Phone.NotepadScreenActivated && !this.Phone.PoemsScreenActivated)
        {
            if (PlayerPrefs.GetInt("Won") == 1)
            {
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    Earring++;
                    if (Earring >= Earrings.Length)
                        Earring = 0;

                    ShowAccessory(Earring);
                }
                if (Input.GetKeyDown(KeyCode.O))
                {
                    Hat++;
                    if (Hat >= Hats.Length)
                        Hat = 0;

                    ShowAccessory2(Hat);
                }
                if (Input.GetKeyDown(KeyCode.V))
                {
                    Bag++;
                    if (Bag >= Bags.Length)
                        Bag = 0;

                    ShowAccessory3(Bag);
                }
                if (Input.GetKeyDown(KeyCode.C))
                {
                    Clip++;
                    if (Clip >= Clips.Length)
                        Clip = 0;

                    ShowAccessory4(Clip);
                }
                if (Input.GetKeyDown(KeyCode.X))
                {
                    Bracelet++;
                    if (Bracelet >= Bracelets.Length)
                        Bracelet = 0;

                    ShowAccessory5(Bracelet);
                }
                if (Input.GetKeyDown(KeyCode.G))
                {
                    Tie++;
                    if (Tie >= Ties.Length)
                        Tie = 0;

                    ShowAccessory6(Tie);
                }
                if (Input.GetKeyDown(KeyCode.H))
                {
                    Hair++;
                    if (Hair >= Hairs.Length)
                        Hair = 0;

                    ShowAccessory7(Hair);
                }
            }
        }
    }

    void ShowAccessory7(int index)
    {
        for (int i = 0; i < Hairs.Length; i++)
        {
            Hairs[i].SetActive(i == index);
        }
    }

    void ShowAccessory(int index)
    {
        for (int i = 0; i < Earrings.Length; i++)
        {
            Earrings[i].SetActive(i == index);
        }
    }
    void ShowAccessory2(int index)
    {
        for (int i = 0; i < Hats.Length; i++)
        {
            Hats[i].SetActive(i == index);
        }
    }
    void ShowAccessory3(int index)
    {
        for (int i = 0; i < Bags.Length; i++)
        {
            Bags[i].SetActive(i == index);
        }
    }
    void ShowAccessory4(int index)
    {
        for (int i = 0; i < Clips.Length; i++)
        {
            Clips[i].SetActive(i == index);
        }
    }
    void ShowAccessory5(int index)
    {
        for (int i = 0; i < Bracelets.Length; i++)
        {
            Bracelets[i].SetActive(i == index);
        }
    }
    void ShowAccessory6(int index)
    {
        for (int i = 0; i < Ties.Length; i++)
        {
            Ties[i].SetActive(i == index);
        }
    }
}
