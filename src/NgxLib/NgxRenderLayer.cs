using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace NgxLib
{
    public abstract class NgxRenderLayer : IDisposable
    {
        public List<NgxRenderSystem> Systems { get; private set; }
        public NgxContext Context { get; private set; }
        public int Index { get; private set; }

        protected NgxRenderLayer()
        {
            Systems = new List<NgxRenderSystem>();
        }

        public virtual void Initialize(NgxContext context, int index)
        {
            Context = context;
            Index = index;

            for (var i = 0; i < Systems.Count; i++)
            {
                var system = Systems[i];
                system.Enabled = true;
                system.BindContext(context);
                system.BindLayer(this);
                system.Initialize();
            }
        }

        public virtual T Get<T>() where T : NgxRenderSystem
        {
            var type = typeof(T);
            for (int i = 0; i < Systems.Count; i++)
            {
                if (Systems[i].GetType() == type)
                {
                    return Systems[i] as T;
                }
            }
            return null;
        }

        public virtual void Begin(SpriteBatch batch)
        {

        }

        public virtual void Draw(SpriteBatch batch)
        {
            for (var i = 0; i < Systems.Count; i++)
            {
                Systems[i].Draw(batch);
            }
        }

        public void End(SpriteBatch batch)
        {
            batch.End();
        }

        public virtual void Dispose()
        {
            
        }
    }
}