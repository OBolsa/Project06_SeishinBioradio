using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    [SerializeField] private UnityEvent m_OnLoadScene;
    [SerializeField] private UnityEvent m_OnEndScene;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        m_OnLoadScene?.Invoke();
    }

    public void LoadScene(string scene)
    {
        m_OnEndScene?.Invoke();
        SceneManager.LoadScene(scene);
    }
}