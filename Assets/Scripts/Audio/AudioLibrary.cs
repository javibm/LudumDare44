using UnityEngine;

namespace LD44
{
  [CreateAssetMenu(fileName = "AudioLibrary", menuName = "Audio Library")]
  public class AudioLibrary : ScriptableObject
  {
    public AudioClip MainMenuMusic;
    public AudioClip GameplayMusic;

    public static AudioLibrary Instance
    {
      get
      {
        if (instance == null)
          instance = Resources.Load<AudioLibrary>("AudioLibrary");
        return instance;
      }
    }

    private static AudioLibrary instance;
  }
}
