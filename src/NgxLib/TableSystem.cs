namespace NgxLib
{
    /// <summary>
    /// A system that iterates all components of a specified type.
    /// </summary>
    /// <typeparam name="TComponent">The component type to iterate</typeparam>
    public abstract class TableSystem<TComponent> : NgxGameSystem
        where TComponent : NgxComponent, new()
    {
        protected NgxTable<TComponent> Table { get; set; }

        public override void BindContext(NgxContext context)
        {
            base.BindContext(context);
            Table = context.Database.Table<TComponent>();
        }

        public override void Update()
        {
            if (Table.Count == 0) return;

            Begin();
            var enumerator = Table.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var component = enumerator.Current.Value;
                if (!component.Enabled) continue;
                Update(component);
            }
            enumerator.Dispose();
            End();
        }

        public override void Destroy()
        {
        }

        protected virtual void Begin()
        {
        }

        protected virtual void Update(TComponent component)
        {
        }

        protected virtual void End()
        {
        }

        public override void Dispose()
        {
            Table = null;
            Destroy();
        } 
    }
}