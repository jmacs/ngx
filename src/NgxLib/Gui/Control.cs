using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NgxLib.Gui
{
    public abstract class Control : IDisposable
    {
        protected NgxContext Context { get; set; }

        public Rectangle Surface { get; set; }

        public ControlCollection Controls { get; private set; }
        public Control Parent { get; set; }

        public bool MouseDown { get; protected set; }
        public bool HasMouse { get; protected set; }
        public bool WatchMouse { get; set; }
        public bool WatchKeyboard { get; set; }
        public bool WatchGamePad { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2 Offset { get; set; }
        public Vector2 Position { get; protected set; }
        public Vector2 Margin { get; set; }
        public Vector2 Padding { get; set; }
        public ControlLayout ControlLayout { get; set; }

        public bool HasFocus
        {
            get { return Context.Focused == this; }
        }

        protected Control()
        {           
            Controls = new ControlCollection(this);
        }

        public virtual void Initialize(NgxContext context)
        {
            Context = context;

            var position = Offset;

            if (Parent != null)
            {
                position = Parent.Offset + Parent.Padding + Parent.Margin;

                if (Parent.ControlLayout == ControlLayout.Vertical)
                {
                    var sibling = Parent.Controls.Before(this);
                    if (sibling != null)
                    {
                        position.Y += sibling.Height + sibling.Position.Y;
                    }
                }
                else
                {
                    var sibling = Parent.Controls.Before(this);
                    if (sibling != null)
                    {
                        position.X += sibling.Width +sibling.Position.X;
                    }
                }
            }

            Position = position;
            Surface = new Rectangle((int)position.X, (int)position.Y, Width, Height);
            Controls.Initialize(context);
        }

        public virtual void Update()
        {
            if (WatchMouse) DoWatchMouse();
            if (WatchKeyboard) DoWatchKeyboard();
            Controls.Update();
        }

        public virtual void Render(SpriteBatch batch)
        {           
        }

        public virtual void Dispose()
        {
            Controls.Dispose();
        }

        public virtual void OnFocus()
        {
        }

        public virtual void OnLeave()
        {    
        }

        protected virtual void OnMouseEnter()
        {
        }

        protected virtual void OnMouseLeave()
        {
        }

        protected virtual void OnMouseDown(MouseState mouse)
        {
        }

        protected virtual void OnMouseUp()
        {
        }

        protected virtual void DoWatchMouse()
        {
            var state = Context.MouseState;

            if (Surface.Contains(state.Position))
            {
                if (!HasMouse)
                {
                    HasMouse = true;
                    OnMouseEnter();
                }
            }
            else if (HasMouse)
            {
                HasMouse = false;
                OnMouseLeave();
            }

            if (HasMouse)
            {
                if (state.LeftButton == ButtonState.Pressed
                    || state.RightButton == ButtonState.Pressed
                    || state.MiddleButton == ButtonState.Pressed
                    || state.XButton1 == ButtonState.Pressed
                    || state.XButton2 == ButtonState.Pressed)
                {
                    if (!MouseDown)
                    {
                        MouseDown = true;
                        OnMouseDown(state);
                    }
                }
                else if (MouseDown)
                {
                    MouseDown = false;
                    OnMouseUp();
                }
            }
        }

        protected void DoWatchKeyboard()
        {
            if (HasFocus)
            {
            }
        }
    }
}
