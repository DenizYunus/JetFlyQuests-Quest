using UnityEngine;
using UnityEngine.SceneManagement;

public class CommonUIActions : MonoBehaviour
{
    public void DestroyItem(GameObject item)
    {
        Destroy(item);
    }

    public void HoverEnterSize(GameObject item)
    {
        item.transform.localScale = item.transform.localScale * 1.3f;
    }
    public void HoverExitSize(GameObject item)
    {
        item.transform.localScale = item.transform.localScale * 10 / 13f;
    }

    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}