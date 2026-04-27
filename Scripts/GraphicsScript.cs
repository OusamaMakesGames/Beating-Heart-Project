using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using System;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GraphicsScript : MonoBehaviour
{
   public PostProcessVolume volume;
   private DepthOfField _depthOfField;
   private Bloom _bloom;
   private ChromaticAberration _chromatic;
   private AmbientOcclusion _ambient;
   public PostProcessVolume postProcessVolume;
   public CinemachineFreeLook camera;
   public bool fullscreen = false;

   public AudioSource Music, GOMusic, Select, ConfirmSelect, Notification, TaskComplete, Coins, Type, InvSFX1, InvSFX2, Hit1, Hit2;

   private void Start()
   {
      this.volume.profile.TryGetSettings<ChromaticAberration>(out this._chromatic);
      this.volume.profile.TryGetSettings<Bloom>(out this._bloom);
      this.volume.profile.TryGetSettings<DepthOfField>(out this._depthOfField);
      this.volume.profile.TryGetSettings<AmbientOcclusion>(out this._ambient);
      if (SceneManager.GetActiveScene().name != "MainMenu")
      {
         GOMusic.volume = PlayerPrefs.GetFloat("music");
         if (PlayerPrefs.GetInt("Day") != 2)
         {
            Music.volume = PlayerPrefs.GetFloat("music");
         }
         else
         {
            Music.volume = PlayerPrefs.GetFloat("music") - 0.2f;
         }
         if (SceneManager.GetActiveScene().name != "Job")
         {
            Select.volume = PlayerPrefs.GetFloat("sound");
            ConfirmSelect.volume = PlayerPrefs.GetFloat("sound");
            Notification.volume = PlayerPrefs.GetFloat("sound");
            TaskComplete.volume = PlayerPrefs.GetFloat("sound");
         }
         Coins.volume = PlayerPrefs.GetFloat("sound");
      }
      if (SceneManager.GetActiveScene().name == "SampleScene")
      {
         Type.volume = PlayerPrefs.GetFloat("sound");
         InvSFX1.volume = PlayerPrefs.GetFloat("sound");
         InvSFX2.volume = PlayerPrefs.GetFloat("sound");
         Hit1.volume = PlayerPrefs.GetFloat("sound");
         Hit2.volume = PlayerPrefs.GetFloat("sound");
      }
   }


   public void Update()
   {
      if (PlayerPrefs.GetInt("aliasing") == 0)
      {
         QualitySettings.antiAliasing = 8;
      }
      if (PlayerPrefs.GetInt("aliasing") == 1)
      {
         QualitySettings.antiAliasing = 4;
      }
      if (PlayerPrefs.GetInt("aliasing") == 2)
      {
         QualitySettings.antiAliasing = 2;
      }
      if (PlayerPrefs.GetInt("aliasing") == 3)
      {
         QualitySettings.antiAliasing = 0;
      }
      if (PlayerPrefs.GetInt("DOF") == 0)
      {
         this._depthOfField.enabled.value = true;
      }
      if (PlayerPrefs.GetInt("DOF") == 1)
      {
         this._depthOfField.enabled.value = false;
      }
      if (PlayerPrefs.GetInt("chromatic") == 0)
      {
         this._chromatic.enabled.value = true;
      }
      if (PlayerPrefs.GetInt("chromatic") == 1)
      {
         this._chromatic.enabled.value = false;
      }
      if (PlayerPrefs.GetInt("texture") == 0)
      {
         QualitySettings.globalTextureMipmapLimit = 0;
      }
      if (PlayerPrefs.GetInt("texture") == 1)
      {
         QualitySettings.globalTextureMipmapLimit = 1;
      }
      if (PlayerPrefs.GetInt("texture") == 2)
      {
         QualitySettings.globalTextureMipmapLimit = 2;
      }
      if (PlayerPrefs.GetInt("texture") == 3)
      {
         QualitySettings.globalTextureMipmapLimit = 3;
      }
      if (PlayerPrefs.GetInt("distance") == 0)
      {
         camera.m_Lens.FarClipPlane = 180;
      }
      if (PlayerPrefs.GetInt("distance") == 1)
      {
         camera.m_Lens.FarClipPlane = 170;
      }
      if (PlayerPrefs.GetInt("distance") == 2)
      {
         camera.m_Lens.FarClipPlane = 160;
      }
      if (PlayerPrefs.GetInt("distance") == 3)
      {
         camera.m_Lens.FarClipPlane = 150;
      }
      if (PlayerPrefs.GetInt("distance") == 4)
      {
         camera.m_Lens.FarClipPlane = 140;
      }
      if (PlayerPrefs.GetInt("distance") == 5)
      {
         camera.m_Lens.FarClipPlane = 130;
      }
      if (PlayerPrefs.GetInt("distance") == 6)
      {
         camera.m_Lens.FarClipPlane = 120;
      }
      if (PlayerPrefs.GetInt("distance") == 7)
      {
         camera.m_Lens.FarClipPlane = 110;
      }
      if (PlayerPrefs.GetInt("distance") == 8)
      {
         camera.m_Lens.FarClipPlane = 100;
      }
      if (PlayerPrefs.GetInt("distance") == 9)
      {
         camera.m_Lens.FarClipPlane = 90;
      }
      if (PlayerPrefs.GetInt("distance") == 10)
      {
         camera.m_Lens.FarClipPlane = 80;
      }
      if (PlayerPrefs.GetInt("distance") == 11)
      {
         camera.m_Lens.FarClipPlane = 70;
      }
      if (PlayerPrefs.GetInt("distance") == 121)
      {
         camera.m_Lens.FarClipPlane = 60;
      }
      if (PlayerPrefs.GetInt("distance") == 13)
      {
         camera.m_Lens.FarClipPlane = 50;
      }
      if (PlayerPrefs.GetInt("distance") == 14)
      {
         camera.m_Lens.FarClipPlane = 40;
      }
      if (PlayerPrefs.GetInt("distance") == 15)
      {
         camera.m_Lens.FarClipPlane = 30;
      }
      if (PlayerPrefs.GetInt("distance") == 16)
      {
         camera.m_Lens.FarClipPlane = 20;
      }
      if (PlayerPrefs.GetInt("distance") == 17)
      {
         camera.m_Lens.FarClipPlane = 10;
      }
      if (PlayerPrefs.GetInt("shadows") == 0)
      {
         QualitySettings.shadows = ShadowQuality.HardOnly;
      }
      if (PlayerPrefs.GetInt("shadows") == 1)
      {
         QualitySettings.shadows = ShadowQuality.Disable;
      }
      if (PlayerPrefs.GetInt("bones") == 0)
      {
         QualitySettings.skinWeights = SkinWeights.FourBones;
      }
      if (PlayerPrefs.GetInt("bones") == 1)
      {
         QualitySettings.skinWeights = SkinWeights.TwoBones;
      }
      if (PlayerPrefs.GetInt("ambient") == 0)
      {
         this._ambient.enabled.value = false;
      }
      if (PlayerPrefs.GetInt("ambient") == 1)
      {
         this._ambient.enabled.value = true;
      }
   }

}
