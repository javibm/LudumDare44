using UnityEngine;

public class TweenColor : RenderTweener
{
  /**********************************************************************************************/
  /* Métodos protegidos                                                                         */
  /**********************************************************************************************/

  #region Métodos protegidos

  protected override void updateTransform (float curvePosition)
  {
    executeOnRenderers ((canvasRenderer) => canvasRenderer.SetColor (Color.Lerp (initialColor, finalColor, curvePosition)));
  }

  #endregion // Métodos protegidos

  /**********************************************************************************************/
  /* Tipos y datos privados                                                                     */
  /**********************************************************************************************/

  #region Tipos y datos privados

  [SerializeField]
  private Color initialColor;

  [SerializeField]
  private Color finalColor;

  #endregion // Tipos y datos privados
}
