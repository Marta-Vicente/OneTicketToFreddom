using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GamplayEvent", menuName = "Events/Gameplay Event")]
public class GameplayEvent : GameEvent<GameplayEventInfo>
{
    public GameplayEventInfo gameplayEventInfo;
}
