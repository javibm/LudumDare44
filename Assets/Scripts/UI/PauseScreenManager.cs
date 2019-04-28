using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace LD44
{
  public class PauseScreenManager : MonoBehaviour
  {
    private void Awake()
    {
      parentGroup.interactable = false;
      parentGroup.blocksRaycasts = false;
      parentGroup.alpha = 0;
      resumeButton.onClick.AddListener(ResumeGame);
      mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    private void Start()
    {
      GameManager.Instance.OnPause += OnPause;
    }

    private void OnPause()
    {
      parentGroup.interactable = true;
      parentGroup.blocksRaycasts = true;
      parentGroup.alpha = 1;
    }

    private void ResumeGame()
    {
      parentGroup.interactable = false;
      parentGroup.blocksRaycasts = false;
      parentGroup.alpha = 0;
      GameManager.Instance.Resume();
    }

    private void GoToMainMenu()
    {
      ScenesFlowManager.GoToMainMenu();
    }

    [SerializeField] CanvasGroup parentGroup;
    [SerializeField] Button resumeButton;
    [SerializeField] Button mainMenuButton;
  }
}
