using UnityEngine;
using SpecialRelativity;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerWorldline", menuName = "Scriptable Objects/PlayerWorldline")]
public class PlayerWorldline : ScriptableObject
{
    public List<Vector4D> Line;
}
