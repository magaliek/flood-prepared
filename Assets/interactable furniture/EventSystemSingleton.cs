using UnityEngine;
using UnityEngine.EventSystems;

public class SingletonEventSystem : MonoBehaviour
{
    void Awake()
    {
        if (FindObjectsByType<EventSystem>(FindObjectsSortMode.None).Length > 1)
            Destroy(gameObject);
    }
}