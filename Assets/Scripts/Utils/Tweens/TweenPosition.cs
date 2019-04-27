using UnityEngine;

public class TweenPosition : TransformTweener
{
  /**********************************************************************************************/
  /* Métodos protegidos                                                                         */
  /**********************************************************************************************/

  #region Métodos protegidos

  protected override void updateTransform (float curvePosition)
  {
    rectTransform.anchoredPosition = Vector2.Lerp (initialPosition, finalPosition, curvePosition);
  }

  #endregion // Métodos protegidos

  /**********************************************************************************************/
  /* Tipos y datos privados                                                                     */
  /**********************************************************************************************/

  #region Tipos y datos privados

  [SerializeField]
  private Vector2 initialPosition;

  [SerializeField]
  private Vector2 finalPosition;

  #endregion // Tipos y datos privados
}
