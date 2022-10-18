using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Menu Channel")]
public class MenuChannel : ScriptableObject
{
    public delegate void MenuCallback(string state);
    public MenuCallback OnMenuChange;
    public MenuCallback OnTabChange;

    public delegate void ModalCallback(string modal);
    public ModalCallback OnOpenModal;
    public ModalCallback OnCloseModal;

    public void RaiseMenu(string state)
    {
        OnMenuChange?.Invoke(state);
        OnTabChange?.Invoke(MenuTabState.None.ToString());

        if (state == "Game" || state == "Pause")
        {
            GameManager.Instance.SetPause(!GameManager.Instance.IsPaused);
            FlowStateMachine.Instance.Channel.RaiseFlowStateRequest(GameManager.Instance.IsPaused ? FlowStateMachine.Instance.StateByName("Pause") : FlowStateMachine.Instance.PreviousState);
        }
    }

    public void RaiseTab(string tab)
    {
        OnTabChange?.Invoke(tab);
    }

    public void RaiseOpenModal(string modal)
    {
        RaiseCloseModal(modal);
        OnOpenModal?.Invoke(modal);
    }

    public void RaiseCloseModal(string modal)
    {
        OnCloseModal?.Invoke(modal);
    }
}