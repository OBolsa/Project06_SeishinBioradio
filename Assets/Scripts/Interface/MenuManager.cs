using UnityEngine;
using UnityEngine.SceneManagement;

#region States
[System.Serializable]
public enum MenuState
{
    MainMenu,
    Options,
    Credits,
    Pause,
    Loading
}

[System.Serializable]
public enum MenuTabState
{
    None,
    Video,
    Graphics,
    Controls,
    Audio
}
#endregion

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] private MenuChannel m_Channel;
    [SerializeField] private FlowChannel m_FlowChannel;
    [SerializeField] private FlowState m_ModalState;
    [SerializeField] private MenuState m_StarterState;
    private FlowState previousState;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Invoke(nameof(StartMenu), 0.3f);
    }

    private void StartMenu()
    {
        m_Channel.RaiseMenu(m_StarterState.ToString());
    }

    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}