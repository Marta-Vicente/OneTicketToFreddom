using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GamplayEventInfo", menuName = "Events/Gameplay Event Info")]
public class GameplayEventInfo : ScriptableObject
{
    public string name;
    public string description;

    [Header("Effects")] 
    public int availableSeatModifier;
    public float suspicionModifier;
}
