using UnityEngine;

namespace LD44
{
  public static class AudioClipExtension
  {
    public static void Play(this AudioClip audioClip, bool loop)
    {
      AudioManager.Play(audioClip, loop);
    }

    public static void Stop(this AudioClip audioClip)
    {
      AudioManager.Stop(audioClip);
    }
  }
}
