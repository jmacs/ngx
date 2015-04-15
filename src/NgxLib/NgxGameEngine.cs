using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NgxLib
{
    /// <summary>
    /// A collection of game systems. Each system implements a specfic
    /// feature or behavior in the game. 
    /// </summary>
    public class NgxGameEngine : IDisposable
    {
        public Color ClearColor { get; set; }

        protected GraphicsDevice GraphicsDevice { get; set; }
        protected NgxContext Context { get; set; }
        protected SpriteBatch SpriteBatch { get; set; }

        public List<NgxGameSystem> Systems { get; private set; }
        public List<NgxRenderLayer> Layers { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NgxGameEngine"/> class.
        /// </summary>
        public NgxGameEngine()
        {
            Layers = new List<NgxRenderLayer>();
            Systems = new List<NgxGameSystem>();
            ClearColor = Color.Black;
        }

        public T GetSystem<T>() where T : NgxGameSystem
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

        public void Initialize(NgxContext context)
        {
            Context = context;
            GraphicsDevice = context.GraphicsDevice;
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            for (var i = 0; i < Systems.Count; i++)
            {
                var system = Systems[i];
                system.Enabled = true;
                system.BindContext(context);
                system.Initialize();
            }

            for (var i = 0; i < Layers.Count; i++)
            {
                var layer = Layers[i];
                layer.Initialize(context, i);
            }
        }

        public void Update()
        {
            for (var i = 0; i < Systems.Count; i++)
            {
                var system = Systems[i];
                if (!system.Enabled) continue;
                system.Update();
            }
        }

        public virtual void Draw()
        {
            GraphicsDevice.Clear(ClearColor);

            for (var i = 0; i < Layers.Count; i++)
            {
                var layer = Layers[i];
                layer.Begin(SpriteBatch);
                layer.Draw(SpriteBatch);
                layer.End(SpriteBatch);
            }
        }
       
        public void Destroy()
        {            
            for (var i = 0; i < Systems.Count; i++)
            {
                Systems[i].Dispose();
            }
            for (var i = 0; i < Layers.Count; i++)
            {
                Layers[i].Dispose();
            }
            Systems.Clear();
            SpriteBatch.Dispose();
            SpriteBatch = null;
            GraphicsDevice = null;
            Context = null;
        }
      
        public void Dispose()
        {
            Destroy();
        }     
    }
}