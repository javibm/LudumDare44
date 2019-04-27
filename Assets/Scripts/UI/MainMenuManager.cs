using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LD44
{
  public class MainMenuManager : MonoBehaviour
  {
    private void Awake()
    {
      GoToMainMenu();
      loadingScreenParentGroup.alpha = 0;
      loadingScreenParentGroup.interactable = false;

      playButton.onClick.AddListener(GoToGameplay);
      creditsButton.onClick.AddListener(GoToCredtis);
      backToMainMenuButton.onClick.AddListener(GoToMainMenu);
      exitButton.onClick.AddListener(Exit);
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
        if (creditsParentGroup.interactable)
          GoToMainMenu();
        else if (mainMenuParentGroup.interactable)
          Exit();
      }
    }

    private void Exit()
    {
      Application.Quit();
    }

    private void GoToGameplay()
    {
      loadingScreenParentGroup.alpha = 1;
      loadingScreenParentGroup.interactable = true;
      mainMenuParentGroup.alpha = 0;
      mainMenuParentGroup.interactable = false;
      creditsParentGroup.alpha = 0;
      creditsParentGroup.interactable = false;
      SceneManager.LoadSceneAsync(1);
    }

    private void GoToMainMenu()
    {
      mainMenuParentGroup.alpha = 1;
      mainMenuParentGroup.interactable = true;
      creditsParentGroup.alpha = 0;
      creditsParentGroup.interactable = false;
    }

    private void GoToCredtis()
    {
      mainMenuParentGroup.alpha = 0;
      mainMenuParentGroup.interactable = false;
      creditsParentGroup.alpha = 1;
      creditsParentGroup.interactable = true;
    }

    [Header("Main Menu")]
    [SerializeField] CanvasGroup mainMenuParentGroup;
    [SerializeField] Button playButton;
    [SerializeField] Button creditsButton;
    [SerializeField] Button exitButton;

    [Header("Credtis")]
    [SerializeField] CanvasGroup creditsParentGroup;
    [SerializeField] Button backToMainMenuButton;

    [Header("Loading Screen")]
    [SerializeField] CanvasGroup loadingScreenParentGroup;
  }
}
