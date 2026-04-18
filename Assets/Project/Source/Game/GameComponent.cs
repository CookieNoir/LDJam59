using System;
using UnityEngine;

public abstract class GameComponent : MonoBehaviour
{
    public int EntityId { get; internal set; }

    public abstract Type RegistryType { get; }
}
