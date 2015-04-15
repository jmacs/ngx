using System.Collections.Generic;

namespace NgxLib
{
    /// <summary>
    /// Represents a game entity. Used by the internal engine to index
    /// what components belong to an specific entity.
    /// </summary>
    public class EntityComposition
    {
        public int Entity { get; private set; }
        public Mask Mask { get; private set; }
        public Dictionary<Mask, NgxComponent> Components { get; private set; }

        public EntityComposition(int entity)
        {
            Components = new Dictionary<Mask, NgxComponent>();
            Entity = entity;
        }

        public void AddComponent(NgxComponent component)
        {
            var mask = component.GetMetaData().ComponentMask;
            Components.Add(mask, component);
            Mask = Mask + mask;
        }

        public void RemoveComponent(NgxComponent component)
        {
            var mask = component.GetMetaData().ComponentMask;
            Components.Remove(mask);
            Mask = Mask - mask;
        }
    }
}
