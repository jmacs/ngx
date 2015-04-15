#Ngx - a 2D Game Engine

Ngx is a 2D game engine written in C#. It mainly serves as a sandbox to learn Entity Component Sytsems (ECS), data driven architecture, and general game programming techniques. 

The NES classic Super Mario Bros 3 is the design reference. It's one of the best examples of 2D platforming and it is simple enough to take on as a first moderately-sized game project.

This is in no way a perfect emulation of SMB3. There's a ton of work left and there's a lot of things in here that I kind of hacked together just to get something working. 

Most of the code is custom but I did use some 3rd party stuff: 
- MonoGame for window management, rendering, sound, and vector stuff. 
- The maps are designed in [Tiled](http://www.angelcode.com/products/bmfont/). 
- Fonts are created using [bmfont](http://www.mapeditor.org/).

##Ngx Building Blocks (ze Architecture)
The following are a few of the main architecture concepts in the engine.

###Entity
An entity is just an integer ID. There is no main "game object". An entity is basically a primary key.

###Component
An object that contains the state data for one aspect of an entity. Think of a component as a table row in a database. It has no methods for behavior. If you want to create a component, make a class and make it inherit NgxComponet. Components are associated to one entity and an entity cannot have two or more of the same component types associated with it.

###Table
A table is a collection of game components. Each "row" in the table is a component and each component has a entity key. So if you want a "Position" component for a player entity you could write something like "var pos = PositionTable[player]". Tables handle object pooling and other infrastructure related tasks. One thing to remember... adding (and removing) a component from the table is an asyncronous operation. You cannot add a comonent and start working with it right away.

###Database
A collection of Tables.

###Prefab
A prefab is a factory for creating entities. For example, the Mario entity prefab creates components in these tables:
- MetaData
- Spatial
- RigidBody
- Controller
- Animator
- NullPower / SuperPower
- Sprite
- Mobility
- JumpBoots

All of those components make up the entity that is the player character Mario.

###System
Systems iterate over components in a table or implement pseudo state machine mechanics. Components are data, systems contain the logic that that uses that data.

###Context
A context contains a database and a colelction of systems. Think of a context as different screens in a game. The world map context and level/stage context are different; each has different game play systems.

###Process
A process is a long running task that executes over multiple game ticks. 

###Command
A command is a one time event handler. A command responds to a message.

###Message
A message is like an event notification. Commands respond to messages.



Disclaimer - All of the game assets, characters, music, etc do not belong to me. They are used for educational purposes only.