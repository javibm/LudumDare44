using UnityEngine;

[RequireComponent (typeof (CanvasRenderer))]
public abstract class RenderTweener : BaseTweener
{
  /**********************************************************************************************/
  /* Métodos de MonoBehaviour                                                                   */
  /**********************************************************************************************/

  #region Métodos de MonoBehaviour

  private void Awake ()
  {
    canvasRenderers = executeOnChildren ?
       GetComponentsInChildren<CanvasRenderer> () : new CanvasRenderer[] { GetComponent<CanvasRenderer> () };
  }

  #endregion // Métodos de MonoBehaviour

  /**********************************************************************************************/
  /* Métodos protegidos                                                                         */
  /**********************************************************************************************/

  #region Métodos protegidos

  protected void executeOnRenderers (System.Action<CanvasRenderer> action)
  {
    foreach (CanvasRenderer canvasRenderer in canvasRenderers)
    {
      action (canvasRenderer);
    }
  }

  #endregion // Métodos protegidos

  /**********************************************************************************************/
  /* Tipos y datos privados                                                                     */
  /**********************************************************************************************/

  #region Tipos y datos privados

  [SerializeField]
  private bool executeOnChildren = true;

  private CanvasRenderer[] canvasRenderers;

  #endregion // Tipos y datos privados
}
