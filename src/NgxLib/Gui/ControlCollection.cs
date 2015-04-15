using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace NgxLib.Gui
{
    public class ControlCollection : IDisposable
    {
        public Control Parent { get; private set; }
        protected List<Control> Controls { get; set; }

        public ControlCollection(Control parent) : this()
        {
            Parent = parent;
        }

        public ControlCollection()
        {
            Controls = new List<Control>();
        }

        public void Initialize(NgxContext context)
        {
            for (var i = 0; i < Controls.Count; i++)
            {
                Controls[i].Initialize(context);
            }
        }

        public void Update()
        {
            for (var i = 0; i < Controls.Count; i++)
            {
                Controls[i].Update();
            }
        }

        public void Render(SpriteBatch batch)
        {
            for (var i = 0; i < Controls.Count; i++)
            {
                Controls[i].Render(batch);
            }   
        }

        public void Dispose()
        {
            for (var i = 0; i < Controls.Count; i++)
            {
                Controls[i].Dispose();
            }
            Controls.Clear();
            Parent = null;
        }

        public void Add(Control control)
        {
            var p = Parent;
            while(p != null)            
            {
                if (p == control)
                {
                    throw new InvalidOperationException("Recursive control hierarchy not supported.");
                }
                p = p.Parent;
            }
            control.Parent = Parent;
            Controls.Add(control);
        }

        public Control Before(Control control)
        {
            var index = Controls.IndexOf(control);
            if (index == 0) return null;
            return Controls[index - 1];
        }

        public Control After(Control control)
        {
            var index = Controls.IndexOf(control);
            if (index + 1 >= Controls.Count) return null;
            return Controls[index + 1];
        }
    }
}