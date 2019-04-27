using UnityEngine;

public class TweenRotation : TransformTweener
{
  /**********************************************************************************************/
  /* Métodos protegidos                                                                         */
  /**********************************************************************************************/

  #region Métodos protegidos

  protected override void updateTransform (float curvePosition)
  {
    cachedRotation.z = Mathf.Lerp (initialRotation, finalRotation, curvePosition);
    rectTransform.localEulerAngles = cachedRotation;
  }

  #endregion // Métodos protegidos

  /**********************************************************************************************/
  /* Tipos y datos privados                                                                     */
  /**********************************************************************************************/

  #region Tipos y datos privados

  [SerializeField]
  private float initialRotation;

  [SerializeField]
  private float finalRotation;

  private Vector3 cachedRotation = Vector3.zero;

  #endregion // Tipos y datos privados
}
