using UnityEngine;

public abstract class Consequence : ScriptableObject
{
    [SerializeField] private Consequence _targetConsequence;

    public void TriggerConsequence()
    {
        ConsequenceEffect();

       // UniTask.WaitUntil(IsConsequenceFinished);

        if (_targetConsequence != null)
        {
            ProcessTargetConseqeunce();
        }
    }
    protected abstract void ConsequenceEffect();

    protected abstract bool IsConsequenceFinished();

    private void ProcessTargetConseqeunce()
    {
        _targetConsequence.ConsequenceEffect();
    }
}

public class ConsequenceCommand
{
    
}