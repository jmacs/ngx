namespace NgxLib
{
    /// <summary>
    /// A dynamic component is used in in systems that inherit from DynamicSystem. 
    /// Dynamic components are constantly evaluating to 
    /// conditions to see if they should continue updating
    /// or self-deactivate. Dynamic components allow a system 
    /// designer to build pseudo state machine machanics into an entity. 
    /// </summary>
    public abstract class DynamicComponent : NgxComponent
    {
        public float Duration { get; set; }

    }
}
