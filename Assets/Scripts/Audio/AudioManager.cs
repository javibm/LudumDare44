using System.Collections.Generic;
using UnityEngine;

namespace LD44
{
  public static class AudioManager
  {
    public static void Play(AudioClip audioClip, bool loop)
    {
      var audioSource = audioSources.Find(IsAudioSourceAvailable);
      if (audioSource == null)
      {
        audioSource = gameObjectInstance.AddComponent<AudioSource>();
        audioSources.Add(audioSource);
      }
      audioSource.clip = audioClip;
      audioSource.loop = loop;
      audioSource.Play();
    }

    public static void Stop(AudioClip audioClip)
    {
      var audioSource = audioSources.Find((s) => s.clip == audioClip);
      if (audioSource != null)
        audioSource.Stop();
    }

    static AudioManager()
    {
      gameObjectInstance = new GameObject("SoundsContainer");
      GameObject.DontDestroyOnLoad(gameObjectInstance);
      audioSources = new List<AudioSource>();
    }

    private static bool IsAudioSourceAvailable(AudioSource audioSource)
    {
      return !audioSource.isPlaying;
    }

    private static GameObject gameObjectInstance;
    private static List<AudioSource> audioSources;
  }
}
