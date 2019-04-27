using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseTweener : MonoBehaviour
{

  #region Tipos y datos públicos

  public enum TweenExecutionType
  {
    Simple,
    Loop,
  }

  #endregion // Tipos y datos públicos

  /**********************************************************************************************/
  /* Métodos públicos                                                                           */
  /**********************************************************************************************/

  #region Métodos públicos

  public void PlayForward ()
  {
    goForward = true;
    startTweener ();
  }

  public void PlayBackwards ()
  {
    goForward = false;
    startTweener ();
  }

  public void ResetToBeginning ()
  {
    updateTransform (0);
  }

  public void SetOnFinishedCallback (UnityAction callback)
  {
    onFinished.AddListener (callback);
  }

  public void RemoveOnFinishedCallback (UnityAction callback)
  {
    onFinished.RemoveListener (callback);
  }

  public void ClearOnFinishedCallbacks ()
  {
    onFinished.RemoveAllListeners ();
  }

  #endregion // Métodos públicos

  /**********************************************************************************************/
  /* Métodos de MonoBehaviour                                                                   */
  /**********************************************************************************************/

  #region Métodos de MonoBehaviour

  private void OnEnable ()
  {
    if (startTweenerOnEnable)
    {
      startTweener ();
    }
  }

  #endregion // Métodos de MonoBehaviour

  /**********************************************************************************************/
  /* Métodos protegidos                                                                         */
  /**********************************************************************************************/

  #region Métodos protegidos

  protected abstract void updateTransform (float curvePosition);

  #endregion // Métodos protegidos

  /**********************************************************************************************/
  /* Métodos privados                                                                           */
  /**********************************************************************************************/

  #region Métodos privados

  private void startTweener ()
  {
    StopAllCoroutines ();
    switch (executionType)
    {
      case TweenExecutionType.Simple:
        StartCoroutine (executeSimpleTweener ());
        break;
      case TweenExecutionType.Loop:
        StartCoroutine (executeLoopTweener ());
        break;
    }
  }

  private IEnumerator executeSimpleTweener ()
  {
    float time = 0;
    updateTransform (animationCurve.Evaluate (goForward ? 0 : 1));
    yield return null;
    do
    {
      time += Time.deltaTime;
      updateTransform (animationCurve.Evaluate (Mathf.InverseLerp (0, duration, goForward ? time : duration - time)));
      yield return null;
    } while (time < duration);
    updateTransform (animationCurve.Evaluate (goForward ? 1 : 0));
    onFinished.Invoke ();
  }

  private IEnumerator executeLoopTweener ()
  {
    while (true)
    {
      yield return executeSimpleTweener ();
    }
  }

  #endregion // Métodos privados

  /**********************************************************************************************/
  /* Tipos y datos privados                                                                     */
  /**********************************************************************************************/

  #region Tipos y datos privados

  [SerializeField]
  private TweenExecutionType executionType;

  [SerializeField]
  private AnimationCurve animationCurve;

  [SerializeField]
  private float duration;

  [SerializeField]
  private bool startTweenerOnEnable;

  [SerializeField]
  private UnityEvent onFinished;

  private bool goForward = true;

  #endregion // Tipos y datos privados
}
