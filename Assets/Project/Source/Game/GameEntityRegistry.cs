using System.Collections.Generic;
using UnityEngine;

public class GameEntityRegistry : MonoBehaviour
{
    private readonly Dictionary<int, GameEntity> _entities = new();
    private readonly Queue<int> _freeEntityIds = new();
    private int _nextEntityId = 0;

    public bool TryRegister(GameEntity entity, out int entityId)
    {
        entityId = -1;
        if (entity == null)
        {
            return false;
        }
        var components = entity.Components;
        if (components == null ||
            components.Count == 0)
        {
            return false;
        }
        entityId = GetGameEntityId();
        foreach (var component in components)
        {
            if (component == null)
            {
                continue;
            }
            component.EntityId = entityId;

        }
        return false;
    }

    private int GetGameEntityId()
    {
        if (_freeEntityIds.TryDequeue(out int entityId))
        {
            return entityId;
        }
        entityId = _nextEntityId;
        ++_nextEntityId;
        return entityId;
    }

    public bool Unregister(int entityId)
    {
        if (!_entities.TryGetValue(entityId, out var entity))
        {
            return false;
        }

        _freeEntityIds.Enqueue(entityId);
        return true;
    }
}
