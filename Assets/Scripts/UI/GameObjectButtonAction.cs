using UnityEngine;
using UnityEngine.Events;

public class GameObjectButtonAction : MonoBehaviour
{
    [Header("Hover Action")]
    public UnityEvent onHoverEnterAction;
    public UnityEvent onHoverExitAction;
    [Header("Click Action")]
    public UnityEvent clickAction;
}