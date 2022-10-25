using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalManager : MonoBehaviour
{
    public static ModalManager Instance;
    [SerializeField] private List<GameObject> m_Modals;
    [SerializeField] private Modal m_CurrentModal;

    private void Awake()
    {
        Instance = this;
    }

#if UNITY_EDITOR
    [ContextMenu("Generate Modal Enum")]
    private void GenerateModalsEnum()
    {
        string _modal = "Modal";
        List<string> _modalsName = new List<string>();

        foreach (GameObject item in m_Modals)
        {
            _modalsName.Add(item.name);
        }

        GenerateEnum.Go(_modal, _modalsName.ToArray());
    }
#endif
}