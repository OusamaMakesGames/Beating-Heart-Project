using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class CustomHairScript : MonoBehaviour
{
    public Material HairMaterial, ChiyokoHairMaterial, ToshikoHairMaterial, SuzukiHairMaterial, HanaHairMaterial, ReinaHairMaterial, NarikoHairMaterial, AganaHairMaterial, EtsukoHairMaterial, AikaHairMaterial, AoiHairMaterial, AkimuraHairMaterial, SoraHairMaterial, Sensei2HairMaterial, Sensei1HairMaterial;
    public Material BackMaterial;
    public Material EyebrowMaterial, EyelashesMaterial;
    public Material FaceMaterial;
    public Material TopMaterial;
    public Material BodyMaterial;
    public Material SocksMaterial;
    public Material TieMaterial;
    public Material ShoesMaterial;
    public Material SkirtMaterial;
    public Material IrisMaterial;
    public Material AccessoryMaterial1, AccessoryMaterial2, AccessoryMaterial3, RibbonMaterial, BeretMaterial, BowMaterial, BraceletMaterial, EarMuffsMaterial, EarringsMaterial, HeartEarringsMaterial, HoopsMaterial, LongTieMaterial, SpikyMaterial, StarMaterial, FlowerLeavesMaterial, FlowerPistilMaterial, HelloKittyMaterial, ReinaClipMaterial, HairBowMaterial, AkimuraHairFlowersMaterial, AkimuraHairTiesMaterial;
    public Texture OriginalRibbon, OriginalEyebrows, OriginalEyelashes, OriginalHair, OriginalFace, OriginalTop, OriginalBody, OriginalSocks, OriginalTie, OriginalShoes, OriginalSkirt, OriginalIris, OriginalClip1, OriginalClip2, OriginalClip3, OriginalBeret, OriginalBow, OriginalBracelet, OriginalEarMuffs, OriginalEarrings, OriginalHeartEarrings, OriginalHoops, OriginalLongTie, OriginalSpiky, OriginalStar, OriginalFlowerLeaves, OriginalFlowerPistil, OriginalHelloKitty, OriginalReinaClip, OriginalHairBow, OriginalAkimuraHairFlowers, OriginalAkimuraHairTies;
    public Texture OriginalRepeatedHair, OriginalToshikoHair, OriginalReinaHair;

    void Start()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath);
        FileInfo[] allFiles = directoryInfo.GetFiles("*.*");
        StartCoroutine("LoadHair");
        StartCoroutine("LoadEyebrows");
        StartCoroutine("LoadEyelashes");
        StartCoroutine("LoadChiyokoHair");
        StartCoroutine("LoadToshikoHair");
        StartCoroutine("LoadSuzukiHair");
        StartCoroutine("LoadHanaHair");
        StartCoroutine("LoadReinaHair");
        StartCoroutine("LoadNarikoHair");
        StartCoroutine("LoadAganaHair");
        StartCoroutine("LoadEtsukoHair");
        StartCoroutine("LoadAikaHair");
        StartCoroutine("LoadAoiHair");
        StartCoroutine("LoadAkimuraHair");
        StartCoroutine("LoadSoraHair");
        StartCoroutine("LoadSensei1Hair");
        StartCoroutine("LoadSensei2Hair");
        StartCoroutine("LoadFace");
        StartCoroutine("LoadTop");
        StartCoroutine("LoadBody");
        StartCoroutine("LoadSkirt");
        StartCoroutine("LoadSocks");
        StartCoroutine("LoadShoes");
        StartCoroutine("LoadTie");
        StartCoroutine("LoadIris");
        StartCoroutine("LoadAcc1");
        StartCoroutine("LoadAcc2");
        StartCoroutine("LoadAcc3");
        StartCoroutine("LoadRibbon");
        StartCoroutine("LoadBeret");
        StartCoroutine("LoadBow");
        StartCoroutine("LoadFlowerLeaves");
        StartCoroutine("LoadFlowerPistil");
        StartCoroutine("LoadHelloKitty");
        StartCoroutine("LoadBracelet");
        StartCoroutine("LoadEarMuffs");
        StartCoroutine("LoadEarrings");
        StartCoroutine("LoadHeartEarrings");
        StartCoroutine("LoadHoops");
        StartCoroutine("LoadLongTie");
        StartCoroutine("LoadSpiky");
        StartCoroutine("LoadStar");
        StartCoroutine("LoadHairBow");
        StartCoroutine("LoadAkimuraHairFlowers");
        StartCoroutine("LoadAkimuraHairTies");
    }
    void Disable()
    {
        base.enabled = false;
    }
    Color GetAverageColor(Texture2D tex)
{
    Color[] pixels = tex.GetPixels();
    float r = 0, g = 0, b = 0;

    foreach (Color c in pixels)
    {
        r += c.r;
        g += c.g;
        b += c.b;
    }

    int total = pixels.Length;
    return new Color(r / total, g / total, b / total);
}
    IEnumerator LoadChiyokoHair()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Hairs/" + "CustomChiyokoHair.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                ChiyokoHairMaterial.SetTexture("_MainTex", OriginalRepeatedHair);
                UpdateOutlineColorFromTexture2(OriginalRepeatedHair, ChiyokoHairMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                ChiyokoHairMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, ChiyokoHairMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadToshikoHair()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Hairs/" + "CustomToshikoHair.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                ToshikoHairMaterial.SetTexture("_MainTex", OriginalToshikoHair);
                UpdateOutlineColorFromTexture2(OriginalToshikoHair, ToshikoHairMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                ToshikoHairMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, ToshikoHairMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadSuzukiHair()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Hairs/" + "CustomSuzukiHair.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                SuzukiHairMaterial.SetTexture("_MainTex", OriginalRepeatedHair);
                UpdateOutlineColorFromTexture2(OriginalRepeatedHair, SuzukiHairMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                SuzukiHairMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, SuzukiHairMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadHanaHair()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Hairs/" + "CustomHanaHair.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                HanaHairMaterial.SetTexture("_MainTex", OriginalRepeatedHair);
                UpdateOutlineColorFromTexture2(OriginalRepeatedHair, HanaHairMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                HanaHairMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, HanaHairMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadReinaHair()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Hairs/" + "CustomReinaHair.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                ReinaHairMaterial.SetTexture("_MainTex", OriginalReinaHair);
                UpdateOutlineColorFromTexture2(OriginalReinaHair, ReinaHairMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                ReinaHairMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, ReinaHairMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadNarikoHair()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Hairs/" + "CustomNarikoHair.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                NarikoHairMaterial.SetTexture("_MainTex", OriginalRepeatedHair);
                UpdateOutlineColorFromTexture2(OriginalRepeatedHair, NarikoHairMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                NarikoHairMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, NarikoHairMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadAganaHair()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Hairs/" + "CustomAganaHair.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                AganaHairMaterial.SetTexture("_MainTex", OriginalRepeatedHair);
                UpdateOutlineColorFromTexture2(OriginalRepeatedHair, AganaHairMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                AganaHairMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, AganaHairMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadEtsukoHair()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Hairs/" + "CustomEtsukoHair.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                EtsukoHairMaterial.SetTexture("_MainTex", OriginalRepeatedHair);
                UpdateOutlineColorFromTexture2(OriginalRepeatedHair, EtsukoHairMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                EtsukoHairMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, EtsukoHairMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadAikaHair()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Hairs/" + "CustomAikaHair.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                AikaHairMaterial.SetTexture("_MainTex", OriginalRepeatedHair);
                UpdateOutlineColorFromTexture2(OriginalRepeatedHair, AikaHairMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                AikaHairMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, AikaHairMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadAoiHair()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Hairs/" + "CustomAoiHair.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                AoiHairMaterial.SetTexture("_MainTex", OriginalRepeatedHair);
                UpdateOutlineColorFromTexture2(OriginalRepeatedHair, AoiHairMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                AoiHairMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, AoiHairMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadAkimuraHair()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Hairs/" + "CustomAkimuraHair.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                AkimuraHairMaterial.SetTexture("_MainTex", OriginalRepeatedHair);
                UpdateOutlineColorFromTexture2(OriginalRepeatedHair, AkimuraHairMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                AkimuraHairMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, AkimuraHairMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadSoraHair()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Hairs/" + "CustomSoraHair.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                SoraHairMaterial.SetTexture("_MainTex", OriginalRepeatedHair);
                UpdateOutlineColorFromTexture2(OriginalRepeatedHair, SoraHairMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                SoraHairMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, SoraHairMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadSensei1Hair()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Hairs/" + "CustomSensei1Hair.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Sensei1HairMaterial.SetTexture("_MainTex", OriginalRepeatedHair);
                UpdateOutlineColorFromTexture2(OriginalRepeatedHair, Sensei1HairMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                Sensei1HairMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, Sensei1HairMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadSensei2Hair()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Hairs/" + "CustomSensei2Hair.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Sensei2HairMaterial.SetTexture("_MainTex", OriginalRepeatedHair);
                UpdateOutlineColorFromTexture2(OriginalRepeatedHair, Sensei2HairMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                Sensei2HairMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, Sensei2HairMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadAkimuraHairFlowers()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomAkimuraHairFlowers.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                AkimuraHairFlowersMaterial.SetTexture("_MainTex", OriginalAkimuraHairFlowers);
                UpdateOutlineColorFromTexture2(OriginalAkimuraHairFlowers, AkimuraHairFlowersMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                AkimuraHairFlowersMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, AkimuraHairFlowersMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadAkimuraHairTies()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomAkimuraHairTies.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                AkimuraHairTiesMaterial.SetTexture("_MainTex", OriginalAkimuraHairTies);
                UpdateOutlineColorFromTexture2(OriginalAkimuraHairTies, AkimuraHairTiesMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                AkimuraHairTiesMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, AkimuraHairTiesMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadHairBow()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomHairBow.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                HairBowMaterial.SetTexture("_MainTex", OriginalHairBow);
                UpdateOutlineColorFromTexture2(OriginalHairBow, HairBowMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                HairBowMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, HairBowMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadReinaClip()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomReinaClip.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                ReinaClipMaterial.SetTexture("_MainTex", OriginalReinaClip);
                UpdateOutlineColorFromTexture2(OriginalReinaClip, ReinaClipMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                ReinaClipMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, ReinaClipMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadStar()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomStarClip.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                StarMaterial.SetTexture("_MainTex", OriginalStar);
                UpdateOutlineColorFromTexture2(OriginalStar, StarMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                StarMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, StarMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadSpiky()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomSpiky.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                SpikyMaterial.SetTexture("_MainTex", OriginalSpiky);
                UpdateOutlineColorFromTexture2(OriginalSpiky, SpikyMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                SpikyMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, SpikyMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadLongTie()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomLongTie.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                LongTieMaterial.SetTexture("_MainTex", OriginalLongTie);
                UpdateOutlineColorFromTexture2(OriginalLongTie, LongTieMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                LongTieMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, LongTieMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadHoops()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomHoops.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                HoopsMaterial.SetTexture("_MainTex", OriginalHoops);
                UpdateOutlineColorFromTexture2(OriginalHoops, HoopsMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                HoopsMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, HoopsMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadEarrings()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomEarrings.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                EarringsMaterial.SetTexture("_MainTex", OriginalEarrings);
                UpdateOutlineColorFromTexture2(OriginalEarrings, EarringsMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                EarringsMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, EarringsMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadHeartEarrings()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomHeartEarrings.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                HeartEarringsMaterial.SetTexture("_MainTex", OriginalHeartEarrings);
                UpdateOutlineColorFromTexture2(OriginalHeartEarrings, HeartEarringsMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                HeartEarringsMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, HeartEarringsMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadEarMuffs()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomEarMuffs.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                BraceletMaterial.SetTexture("_MainTex", OriginalEarMuffs);
                UpdateOutlineColorFromTexture2(OriginalEarMuffs, EarMuffsMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                EarMuffsMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, EarMuffsMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadBracelet()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomBracelet.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                BraceletMaterial.SetTexture("_MainTex", OriginalBracelet);
                UpdateOutlineColorFromTexture2(OriginalBracelet, BraceletMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                BraceletMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, BraceletMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadBow()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomBow.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                BowMaterial.SetTexture("_MainTex", OriginalBow);
                UpdateOutlineColorFromTexture2(OriginalBow, BowMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                BowMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, BowMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadHelloKitty()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomHelloKitty.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                HelloKittyMaterial.SetTexture("_MainTex", OriginalHelloKitty);
                UpdateOutlineColorFromTextureVRM2(OriginalHelloKitty, HelloKittyMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                HelloKittyMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTextureVRM(texture, HelloKittyMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadFlowerLeaves()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomFlowerLeaves.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                FlowerLeavesMaterial.SetTexture("_MainTex", OriginalFlowerLeaves);
                UpdateOutlineColorFromTexture2(OriginalFlowerLeaves, FlowerLeavesMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                FlowerLeavesMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, FlowerLeavesMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadFlowerPistil()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomFlowerPistil.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                FlowerPistilMaterial.SetTexture("_MainTex", OriginalFlowerPistil);
                UpdateOutlineColorFromTexture2(OriginalFlowerPistil, FlowerPistilMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                FlowerPistilMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, FlowerPistilMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadBeret()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomBeret.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                BeretMaterial.SetTexture("_MainTex", OriginalBeret);
                UpdateOutlineColorFromTexture2(OriginalBeret, BeretMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                BeretMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, BeretMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadRibbon()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomRibbon.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                RibbonMaterial.SetTexture("_MainTex", OriginalRibbon);
                UpdateOutlineColorFromTexture2(OriginalRibbon, RibbonMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                RibbonMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, RibbonMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadIris()
    {
        var url = Path.Combine(Application.streamingAssetsPath, "CustomIris.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                IrisMaterial.SetTexture("_MainTex", OriginalIris);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                IrisMaterial.SetTexture("_MainTex", texture);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadAcc1()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomClip1.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                AccessoryMaterial1.SetTexture("_MainTex", OriginalClip1);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                AccessoryMaterial1.SetTexture("_MainTex", texture);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadAcc2()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomClip2.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                AccessoryMaterial2.SetTexture("_MainTex", OriginalClip2);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                AccessoryMaterial2.SetTexture("_MainTex", texture);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadAcc3()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomClip3.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                AccessoryMaterial3.SetTexture("_MainTex", OriginalClip3);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                AccessoryMaterial3.SetTexture("_MainTex", texture);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadBody()
    {
        var url = Path.Combine(Application.streamingAssetsPath, "CustomBody.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                BodyMaterial.SetTexture("_MainTex", OriginalBody);
                BodyMaterial.SetTexture("_MainTex", OriginalBody);
                UpdateOutlineColorFromTexture2(OriginalBody, BodyMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                BodyMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, BodyMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadSkirt()
    {
        var url = Path.Combine(Application.streamingAssetsPath, "CustomSkirt.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                SkirtMaterial.SetTexture("_MainTex", OriginalSkirt);
                UpdateOutlineColorFromTexture2(OriginalSkirt, SkirtMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                SkirtMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, SkirtMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadShoes()
    {
        var url = Path.Combine(Application.streamingAssetsPath, "CustomShoes.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                ShoesMaterial.SetTexture("_MainTex", OriginalShoes);
                UpdateOutlineColorFromTexture2(OriginalShoes, ShoesMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                ShoesMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, ShoesMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadTop()
    {
        var url = Path.Combine(Application.streamingAssetsPath, "CustomTop.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                TopMaterial.SetTexture("_MainTex", OriginalTop);
                UpdateOutlineColorFromTexture2(OriginalTop, TopMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                TopMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, TopMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadSocks()
    {
        var url = Path.Combine(Application.streamingAssetsPath, "CustomSocks.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                SocksMaterial.SetTexture("_MainTex", OriginalSocks);
                UpdateOutlineColorFromTexture2(OriginalSocks, SocksMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                SocksMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, SocksMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadTie()
    {
        var url = Path.Combine(Application.streamingAssetsPath + "/Accessories/" + "CustomTie.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                TieMaterial.SetTexture("_MainTex", OriginalTie);
                UpdateOutlineColorFromTextureVRM2(OriginalTie, TieMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                TieMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTextureVRM(texture, TieMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }
    IEnumerator LoadHair()
{
    var url = Path.Combine(Application.streamingAssetsPath + "/Hairs/" + "CustomHair.png");

    using (var uwr = UnityWebRequestTexture.GetTexture(url))
    {
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            HairMaterial.SetTexture("_MainTex", OriginalHair);
            BackMaterial.SetTexture("_MainTex", OriginalHair);
            UpdateOutlineColorFromTexture2(OriginalHair, HairMaterial);
            base.Invoke("Disable", 1f);
        }
        else
        {
            var texture = DownloadHandlerTexture.GetContent(uwr);
            HairMaterial.SetTexture("_MainTex", texture);
            BackMaterial.SetTexture("_MainTex", texture);
            UpdateOutlineColorFromTexture(texture, HairMaterial);

            base.Invoke("Disable", 1f);
        }
    }


}
IEnumerator LoadEyebrows()
{
    var url = Path.Combine(Application.streamingAssetsPath + "CustomEyebrows.png");

    using (var uwr = UnityWebRequestTexture.GetTexture(url))
    {
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            EyebrowMaterial.SetTexture("_MainTex", OriginalEyebrows);
            base.Invoke("Disable", 1f);
        }
        else
        {
            var texture = DownloadHandlerTexture.GetContent(uwr);
            EyebrowMaterial.SetTexture("_MainTex", texture);
            base.Invoke("Disable", 1f);
        }
    }


}
IEnumerator LoadEyelashes()
{
    var url = Path.Combine(Application.streamingAssetsPath + "CustomEyelashes.png");

    using (var uwr = UnityWebRequestTexture.GetTexture(url))
    {
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            EyelashesMaterial.SetTexture("_MainTex", OriginalEyelashes);
            base.Invoke("Disable", 1f);
        }
        else
        {
            var texture = DownloadHandlerTexture.GetContent(uwr);
            EyelashesMaterial.SetTexture("_MainTex", texture);
            base.Invoke("Disable", 1f);
        }
    }


}
void UpdateOutlineColorFromTexture2(Texture texture, Material material)
{
    Texture2D tex2D = texture as Texture2D;
    if (tex2D == null)
    {
        return;
    }

    if (!tex2D.isReadable)
    {
        return;
    }

    Color[] pixels = tex2D.GetPixels();
    float r = 0f, g = 0f, b = 0f;

    foreach (Color pixel in pixels)
    {
        r += pixel.r;
        g += pixel.g;
        b += pixel.b;
    }

    int total = pixels.Length;
    Color avgColor = new Color(r / total, g / total, b / total);

    Color outlineColor = avgColor * 0.6f;
    outlineColor.a = 1f;

    material.SetColor("_OtlColor", outlineColor);
}
void UpdateOutlineColorFromTextureVRM2(Texture texture, Material material)
{
    Texture2D tex2D = texture as Texture2D;
    if (tex2D == null)
    {
        return;
    }

    if (!tex2D.isReadable)
    {
        return;
    }

    Color[] pixels = tex2D.GetPixels();
    float r = 0f, g = 0f, b = 0f;

    foreach (Color pixel in pixels)
    {
        r += pixel.r;
        g += pixel.g;
        b += pixel.b;
    }

    int total = pixels.Length;
    Color avgColor = new Color(r / total, g / total, b / total);

    Color outlineColor = avgColor * 0.6f;
    outlineColor.a = 1f;

    material.SetColor("_OutlineColor", outlineColor);
}
void UpdateOutlineColorFromTexture(Texture2D texture, Material material)
{
    if (!texture.isReadable)
    {
        return;
    }

    Color[] pixels = texture.GetPixels();
    float r = 0f, g = 0f, b = 0f;

    foreach (Color pixel in pixels)
    {
        r += pixel.r;
        g += pixel.g;
        b += pixel.b;
    }

    int total = pixels.Length;
    Color avgColor = new Color(r / total, g / total, b / total);

    Color outlineColor = avgColor * 0.6f;
    outlineColor.a = 1f;

    material.SetColor("_OtlColor", outlineColor);
}
void UpdateOutlineColorFromTextureVRM(Texture2D texture, Material material)
{
    if (!texture.isReadable)
    {
        return;
    }

    Color[] pixels = texture.GetPixels();
    float r = 0f, g = 0f, b = 0f;

    foreach (Color pixel in pixels)
    {
        r += pixel.r;
        g += pixel.g;
        b += pixel.b;
    }

    int total = pixels.Length;
    Color avgColor = new Color(r / total, g / total, b / total);

    Color outlineColor = avgColor * 0.6f;
    outlineColor.a = 1f;

    material.SetColor("_OutlineColor", outlineColor);
}
    IEnumerator LoadFace()
    {
        var url = Path.Combine(Application.streamingAssetsPath, "CustomFace.png");

        // UnityWebRequest can also be used for reading local files
        // also from streaming assets
        using (var uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                FaceMaterial.SetTexture("_MainTex", OriginalFace);
                UpdateOutlineColorFromTexture2(OriginalFace, FaceMaterial);
                base.Invoke("Disable", 1f);
            }
            else
            {
                // Get downloaded texture
                var texture = DownloadHandlerTexture.GetContent(uwr);
                FaceMaterial.SetTexture("_MainTex", texture);
                UpdateOutlineColorFromTexture(texture, FaceMaterial);
                base.Invoke("Disable", 1f);
            }
        }
    }



}
