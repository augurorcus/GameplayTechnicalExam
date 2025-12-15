using System;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class ConsequenceHandler : Singleton<ConsequenceHandler>
{
    [SerializeField] public List<Consequence> _currentlyRunningConsequences;


    public void RegisterConsequenceCommand(Action methodToAdd)
    {

    }
}
