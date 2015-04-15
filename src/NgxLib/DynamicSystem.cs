namespace NgxLib
{
    /// <summary>
    /// A dynamic system constantly evaluates its components
    /// to see if they should continue updating or be deactivated. 
    /// Dynamic systems allow system designers to build pseudo
    /// state machine machanics into an entity. 
    /// </summary>
    /// <typeparam name="TComponent">The type of the component.</typeparam>
    public class DynamicSystem<TComponent> : NgxGameSystem
        where TComponent : DynamicComponent, new()
    {
        protected NgxTable<TComponent> Table { get; set; }

        public override void BindContext(NgxContext context)
        {
            base.BindContext(context);
            Table = context.Database.Table<TComponent>();
        }

        public override void Update()
        {
            if(Table.Count == 0) return;

            var enumerator = Table.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var component = enumerator.Current.Value;

                if (!component.Enabled)
                {
                    if (component.Duration > 0)
                    {
                        Exit(component);
                        component.Duration = 0;
                    }
                    continue;
                }

                if (Evaluate(component))
                {
                    if (component.Duration == 0)
                    {
                        Enter(component);
                    }

                    Update(component);
                    component.Duration += Time.Delta;
                }
                else
                {
                    if (component.Duration > 0)
                    {
                        Exit(component);
                        component.Duration = 0;
                        component.Enabled = false;
                    }
                }    
            }          
            enumerator.Dispose();
        }

        /// <summary>
        /// Evaluates the component to see if it should 
        /// containue updating or if it should be deactivated.
        /// </summary>
        /// <param name="com">The component.</param>
        /// <returns>True if should Update(); false if should Exit()</returns>
        protected virtual bool Evaluate(TComponent com)
        {
            return true;
        }

        /// <summary>
        /// When the component is activated.
        /// </summary>
        /// <param name="com">The component.</param>
        protected virtual void Enter(TComponent com)
        {
        }

        /// <summary>
        /// Update behavior for a single frame
        /// </summary>
        /// <param name="com">The component.</param>
        protected virtual void Update(TComponent com)
        {
        }

        /// <summary>
        /// When the component is deactivated.
        /// </summary>
        /// <param name="com">The component.</param>
        protected virtual void Exit(TComponent com)
        {
        }        
    }
}