using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Pipe Channel")]
public class PipeChannel : ScriptableObject
{
    public delegate void PipeCallback(int trailID);
    public PipeCallback OnEnterPipeTrail;
    public PipeCallback OnExitPipeTrail;

    public void RaiseEnterPipeTrail(int trailID)
    {
        OnEnterPipeTrail?.Invoke(trailID);
    }

    public void RaiseExitPipeTrail(int trailID)
    {
        OnExitPipeTrail?.Invoke(trailID);
    }
}
