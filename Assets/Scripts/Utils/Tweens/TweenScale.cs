using UnityEngine;

public class TweenScale : TransformTweener
{
  /**********************************************************************************************/
  /* Métodos protegidos                                                                         */
  /**********************************************************************************************/

  #region Métodos protegidos

  protected override void updateTransform (float curvePosition)
  {
    rectTransform.localScale = Vector3.Lerp (initialScale, finalScale, curvePosition);
  }

  #endregion // Métodos protegidos

  /**********************************************************************************************/
  /* Tipos y datos privados                                                                     */
  /**********************************************************************************************/

  #region Tipos y datos privados

  [SerializeField]
  private Vector2 initialScale;

  [SerializeField]
  private Vector2 finalScale;

  #endregion // Tipos y datos privados
}
