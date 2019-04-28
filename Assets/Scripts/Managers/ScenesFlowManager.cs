using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD44
{

  public static class ScenesFlowManager
  {
    public static System.Action OnGameStartedLoading;

    public static void GoToGameplay()
    {
      OnGameStartedLoading?.Invoke();
      SceneManager.LoadSceneAsync(1).completed += OnGameplayLoaded;
    }

    public static void GoToMainMenu()
    {
      SceneManager.LoadSceneAsync(0).completed += OnMainMenuLoaded;
    }

    private static void OnGameplayLoaded(AsyncOperation asyncOperation)
    {
      AudioLibrary.Instance.MainMenuMusic.Stop();
    }

    private static void OnMainMenuLoaded(AsyncOperation asyncOperation)
    {
      AudioLibrary.Instance.GameplayMusic.Stop();
    }
  }
}
