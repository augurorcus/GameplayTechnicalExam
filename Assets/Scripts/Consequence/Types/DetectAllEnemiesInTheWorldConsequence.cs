using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[CreateAssetMenu(menuName = "Consequence/DetectAllEnemiesInTheWorld")]
public class DetectAllEnemiesInTheWorldConsequence : Consequence
{
    [SerializeField] private SOListEnemy _currentEnemiesInTheWorld;

    protected override string ReceiveDataKey => "";

    protected override string SendDataKey => "Targets";

    protected override void ConsequenceEffect(Dictionary<string, ConsequenceAndValue> consequenceData)
    {
        Collider[] targets = _currentEnemiesInTheWorld.list
        .Select(go => go.GetComponent<Collider>())
        .Where(c => c != null) // omit nulls if some objects lack colliders
        .ToArray();

        consequenceData.Add(SendDataKey, new ConsequenceAndValue(this, targets));
    }
}
