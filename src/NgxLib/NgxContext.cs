using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NgxLib.Audio;
using NgxLib.Cameras;
using NgxLib.Gui;
using NgxLib.Maps;
using NgxLib.Processing;
using NgxLib.Tilesets;

namespace NgxLib
{
    /// <summary>
    /// An object that represents a specfic game context.
    /// Contexts may contain specfic game play systems and assets
    /// or respond to game messages in a way that is unique to 
    /// other parts or "contexts" in a game. For example, in a
    /// 2D platformer game, a Main Menu context would probably 
    /// require different systems and assets than the main 
    /// "Action/Platforming" context.
    /// </summary>
    public abstract class NgxContext : IDisposable
    {
        protected NgxRuntime Runtime { get; set; }

        public SoundBank SoundBank { get; private set; }

        /// <summary>
        /// Gets the context name
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// Gets the tile map used in this context
        /// </summary>
        public MapManager MapManager { get; private set; }
        
        /// <summary>
        /// Gets the collection of tiles used in this context
        /// </summary>
        public TilesetCollection Tilesets { get; private set; }
        
        /// <summary>
        /// Gets the collection of game systems used in this context
        /// </summary>
        public NgxGameEngine Engine { get; set; }

        /// <summary>
        /// Gets the game session object; a collection of key/value 
        /// pairs used for storing information about the current play session. 
        /// </summary>
        /// <value>
        /// The game session.
        /// </value>
        public NgxSession Session
        {
            get { return Runtime.Session; }
        }

        /// <summary>
        /// Gets the game database; the central repository of game components.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public NgxDatabase Database
        {
            get { return Runtime.Database; }
        }

        public NgxMessenger Messenger 
        {
            get { return Runtime.Messenger; }
        }

        /// <summary>
        /// Gets the process manager.
        /// </summary>
        /// <value>
        /// The process manager.
        /// </value>
        public ProcessManager ProcessManager
        {
            get { return Runtime.ProcessManager; }
        }

        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        /// <value>
        /// The graphics device.
        /// </value>
        public GraphicsDevice GraphicsDevice
        {
            get { return Runtime.GraphicsDevice; }
        }

        /// <summary>
        /// Gets the camera.
        /// </summary>
        /// <value>
        /// The camera.
        /// </value>
        public Camera2D Camera
        {
            get { return Runtime.Camera; }
        }

        public MouseState MouseState { get; private set; }
        public Control Focused { get; private set; }
        public ControlCollection Controls { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NgxContext"/> class.
        /// </summary>
        protected NgxContext()
        { 
            Name = GetType().Name;
        }

        /// <summary>
        /// Initializes the context.
        /// </summary>
        /// <param name="runtime">The game runtime object.</param>
        public void Initialize(NgxRuntime runtime)
        {
            Runtime = runtime;

            Engine = new NgxGameEngine();

            PreLoad();
                    
            Tilesets = new TilesetCollection();
            MapManager = new MapManager(this);
            Controls = new ControlCollection();
            SoundBank = new SoundBank();

            Load();

            Tilesets.LoadTextures(Runtime.GraphicsDevice);
            Engine.Initialize(this);

            PostLoad();
        }

        /// <summary>
        /// Updates one game frame.
        /// </summary>
        public void Update()
        {
            MouseState = Mouse.GetState();
            Engine.Update();
        }

        /// <summary>
        /// Renders one game frame.
        /// </summary>
        public void Draw()
        {
            Engine.Draw();
        }

        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public void Destroy()
        {
            Unload();
            Debugger.Unload();
            Tilesets.Dispose();
            Tilesets = null;
            Engine.Dispose();
            Engine = null;
            Database.Clear();
            Controls.Dispose();
            Controls = null;
        }

        /// <summary>
        /// Called before the context has loaded.
        /// </summary>
        protected virtual void PreLoad()
        {
        }

        /// <summary>
        /// Loads game systems and other assets.
        /// </summary>
        protected virtual void Load()
        {
        }

        /// <summary>
        /// Called after the context has loaded but before the first call to Update()
        /// </summary>
        protected virtual void PostLoad()
        {
        }

        /// <summary>
        /// Used by the implementer to unload context specific assets.
        /// </summary>
        protected virtual void Unload()
        {
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, 
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Destroy();
        }

        public void Focus(Control control)
        {
            if (Focused != null)
            {
                Focused.OnLeave();
            }
            Focused = control;
            Focused.OnFocus();
        }

        public int Create(string name, int x, int y)
        {
            return Ngx.Prefabs.Create(Database, name, new PrefabArgs(x,y));
        }

        public int Create<T>(int x, int y)
        {
            return Ngx.Prefabs.Create<T>(Database, x, y);
        }
    }
}