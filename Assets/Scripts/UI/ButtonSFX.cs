using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Triggers UI hover and click sound effects via Unity Event System interfaces.
/// </summary>
public class ButtonSFX : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayHoverSFX();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayClickSFX();
        }
    }
}