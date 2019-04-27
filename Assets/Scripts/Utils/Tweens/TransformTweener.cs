using UnityEngine;

[RequireComponent (typeof (RectTransform))]
public abstract class TransformTweener : BaseTweener
{
  /**********************************************************************************************/
  /* Métodos de MonoBehaviour                                                                   */
  /**********************************************************************************************/

  #region Métodos de MonoBehaviour

  private void Awake ()
  {
    rectTransform = transform as RectTransform;
  }

  #endregion // Métodos de MonoBehaviour

  /**********************************************************************************************/
  /* Tipos y datos protegidos                                                                   */
  /**********************************************************************************************/

  #region Tipos y datos protegidos

  protected RectTransform rectTransform;

  #endregion // Tipos y datos protegidos
}
