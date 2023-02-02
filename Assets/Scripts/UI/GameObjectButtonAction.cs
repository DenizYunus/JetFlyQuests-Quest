using UnityEngine;
using UnityEngine.Events;

public class GameObjectButtonAction : MonoBehaviour
{
    public UnityEvent action;

    public void DestroyThisAction()
    {
        Destroy(gameObject);
    }
}
