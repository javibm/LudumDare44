using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace LD44
{
  public class GameOverScreenManager : MonoBehaviour
  {
    private void Awake()
    {
      parentGroup.interactable = false;
      parentGroup.blocksRaycasts = false;
      parentGroup.alpha = 0;
      restartButton.onClick.AddListener(RestartGame);
      mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    private void Start()
    {
      GameManager.Instance.OnGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
      parentGroup.interactable = true;
      parentGroup.blocksRaycasts = true;
      StartCoroutine(AppearCoroutine());
    }

    private IEnumerator AppearCoroutine()
    {
      float animProgressNormalized = 0;
      while (animProgressNormalized <= 0)
      {
        animProgressNormalized += Time.deltaTime / appearAnimDuration;
        parentGroup.alpha = animProgressNormalized;
        yield return null;
      }
    }

    private void RestartGame()
    {
      ScenesFlowManager.GoToGameplay();
    }

    private void GoToMainMenu()
    {
      ScenesFlowManager.GoToMainMenu();
    }

    [SerializeField] CanvasGroup parentGroup;
    [SerializeField] float appearAnimDuration;
    [SerializeField] Button restartButton;
    [SerializeField] Button mainMenuButton;
  }
}
