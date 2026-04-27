using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public int ID;
    public string Name;
    public string HairColor;
    public string EyebrowsColor;
    public string EyelashesColor;
    public string EyeColor;
}
[System.Serializable]


public class CharactersData
{
    public List<CharacterData> characters;
}

public class CharacterManager : MonoBehaviour
{
    public List<CharacterData> characters;
    public Material HairMaterial, HairMaterial2;
    public Material EyeMaterial;
    public Color Black, Brown, Blonde, Red, White, Blue, Green, Cyan, Purple, Pink, Orange, Yellow;
    public Dictionary<string, Color> hairColors = new Dictionary<string, Color>();
    public int CharacterID;
    public Dictionary<string, Texture> eyeTextures = new Dictionary<string, Texture>();
    public Texture BlackEyes, BrownEyes, RedEyes, WhiteEyes, BlueEyes, GreenEyes, CyanEyes, PurpleEyes, PinkEyes, OrangeEyes, YellowEyes;

    public Color DefaultHairColor, DefaultEyebrowsColor, DefaultEyelashesColor;
    public Texture DefaultEyeTexture;



    void Start()
    {
        if (CharacterID != 16 || CharacterID != 17)
        {
            HairMaterial2 = HairMaterial;
        }
        eyeTextures.Add("Black", BlackEyes);
        eyeTextures.Add("Brown", BrownEyes);
        eyeTextures.Add("Red", RedEyes);
        eyeTextures.Add("White", WhiteEyes);
        eyeTextures.Add("Blue", BlueEyes);
        eyeTextures.Add("Green", GreenEyes);
        eyeTextures.Add("Cyan", CyanEyes);
        eyeTextures.Add("Purple", PurpleEyes);
        eyeTextures.Add("Pink", PinkEyes);
        eyeTextures.Add("Orange", OrangeEyes);
        eyeTextures.Add("Yellow", YellowEyes);

        hairColors.Add("Black", Black);
        hairColors.Add("Brown", Brown);
        hairColors.Add("Blonde", Blonde);
        hairColors.Add("Red", Red);
        hairColors.Add("White", White);
        hairColors.Add("Blue", Blue);
        hairColors.Add("Green", Green);
        hairColors.Add("Cyan", Cyan);
        hairColors.Add("Purple", Purple);
        hairColors.Add("Pink", Pink);
        hairColors.Add("Orange", Orange);
        hairColors.Add("Yellow", Yellow);

        string jsonPath = System.IO.Path.Combine(Application.streamingAssetsPath, "Characters.json");

        string json;
        if (Application.platform == RuntimePlatform.Android)
        {
            // Android needs a different way to load streaming assets
            UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(jsonPath);
            www.SendWebRequest();
            while (!www.isDone) { }
            json = www.downloadHandler.text;
        }
        else
        {
            json = System.IO.File.ReadAllText(jsonPath);
        }

        characters = JsonUtility.FromJson<CharactersData>(json).characters;

        CheckAndSetHairMaterial(CharacterID);
        CheckAndSetEyeMaterial(CharacterID);
    }

    void CheckAndSetHairMaterial(int characterID)
    {
        CharacterData character = characters.Find(c => c.ID == characterID);

        string hairColorName = character.HairColor;
        if (hairColors.ToString().Contains(hairColorName))
        {
            TalkingScript talkingScriptComponent = this.transform.GetComponent<TalkingScript>();
            talkingScriptComponent.studentName = character.Name;

            hairColors.TryGetValue(hairColorName, out Color hairColorValue);

            HairMaterial.color = hairColorValue;
            HairMaterial2.color = hairColorValue;

            Color outlineColor = hairColorValue * 0.6f;
            outlineColor.a = 1f;

            HairMaterial.SetColor("_OtlColor", outlineColor);
            HairMaterial2.SetColor("_OtlColor", outlineColor);
        }
        else
        {
            HairMaterial.color = DefaultHairColor;
            HairMaterial2.color = DefaultHairColor;
            Color outlineColor = DefaultHairColor * 0.6f;
            outlineColor.a = 1f;

            HairMaterial.SetColor("_OtlColor", outlineColor);
            HairMaterial2.SetColor("_OtlColor", outlineColor);
        }
        string eyebrowsName = character.EyebrowsColor;
        if (hairColors.ToString().Contains(eyebrowsName))
        {
            hairColors.TryGetValue(eyebrowsName, out Color hairColorValue);

            transform.Find("Face").GetComponent<SkinnedMeshRenderer>().materials[5].color = hairColorValue;
        }
        else
        {
            transform.Find("Face").GetComponent<SkinnedMeshRenderer>().materials[5].color = DefaultEyebrowsColor;
        }
        string eyelashesName = character.EyelashesColor;
        if (hairColors.ToString().Contains(eyelashesName))
        {
            hairColors.TryGetValue(eyelashesName, out Color hairColorValue);

            transform.Find("Face").GetComponent<SkinnedMeshRenderer>().materials[6].color = hairColorValue;
        }
        else
        {
            transform.Find("Face").GetComponent<SkinnedMeshRenderer>().materials[6].color = DefaultEyelashesColor;
        }
    }
    void CheckAndSetEyeMaterial(int characterID)
    {
        CharacterData character = characters.Find(c => c.ID == characterID);
        if (character == null) return;

        string eyeColorName = character.EyeColor;
        if (eyeTextures.ToString().Contains(eyeColorName))
        {
            TalkingScript talkingScriptComponent = GetComponent<TalkingScript>();
            talkingScriptComponent.studentName = character.Name;

            if (eyeTextures.TryGetValue(eyeColorName, out Texture eyeTexture))
            {
                EyeMaterial.SetTexture("_MainTex", eyeTexture);
            }
            else
            {
                Debug.LogWarning($"No texture found for eye color '{eyeColorName}'");
            }
        }
        else
        {
            EyeMaterial.SetTexture("_MainTex", DefaultEyeTexture);
        }
    }

}
