using UnityEngine;

public class TweenAlpha : RenderTweener
{
  /**********************************************************************************************/
  /* Métodos protegidos                                                                         */
  /**********************************************************************************************/

  #region Métodos protegidos

  protected override void updateTransform (float curvePosition)
  {
    executeOnRenderers ((canvasRenderer) => canvasRenderer.SetAlpha (Mathf.Lerp (initialAlpha, finalAlpha, curvePosition)));
  }

  #endregion // Métodos protegidos

  /**********************************************************************************************/
  /* Tipos y datos privados                                                                     */
  /**********************************************************************************************/

  #region Tipos y datos privados

  [SerializeField]
  private float initialAlpha;

  [SerializeField]
  private float finalAlpha;

  #endregion // Tipos y datos privados
}
