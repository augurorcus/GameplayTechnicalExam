using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    [SerializeField] private HeroClass _class;
    //add more hero specific variables here
}

public enum HeroClass
{
    Warrior,
    Sorcerer,
    Rogue
}
