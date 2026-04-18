using System.Collections.Generic;
using UnityEngine;

public class GameEntity : MonoBehaviour
{
    [SerializeField] private GameComponent[] _components;
    internal bool IsRegistered { get; private set; } = false;
    internal bool ShouldBeUnregistered { get; private set; } = false;

    internal IReadOnlyList<GameComponent> Components => _components;

    public bool TryGetGameComponent<T>(out T result) where T : GameComponent
    {
        result = null;
        if (_components == null ||
            _components.Length == 0)
        {
            return false;
        }
        foreach (var component in _components)
        {
            if (component == null)
            {
                continue;
            }
            if (component.RegistryType == typeof(T))
            {
                result = (T)component;
                return true;
            }
        }
        return false;
    }

    public void RequestUnregister()
    {
        if (!IsRegistered)
        {
            return;
        }
        ShouldBeUnregistered = true;
    }

    internal void SetRegistered()
    {
        IsRegistered = true;
        ShouldBeUnregistered = false;
    }

    internal void SetUnregistered()
    {
        IsRegistered = false;
        ShouldBeUnregistered = false;
    }
}
