#NGX 

##2D Game Engine
Ngx is my sandbox 2D game engine written in C#. I started this project to learn how to build Entity Component Sytsem (ECS), data driven architectures, and general game programming techniques. I chose to recreate the NES classic Super Mario Bros 3 to serve as my design reference since it one of the best examples of 2D platforming and it is simple enough to take on as my first moderately-sized game project.     

NGX uses MonoGame for window management, rendering, sound, and math. Levels are designed in [Tiled](http://www.angelcode.com/products/bmfont/), a tile-based map editor, and fonts are created using [bmfont](http://www.mapeditor.org/).

##Architecture
NGX is fairly lightweight; it simply allows for ECS design in a MonoGame environment. It's probably best to google around for entity component system design, I'm not going to cover those topics in detail. The following sections assume you have a general idea of ECS.

###Entity
There's actually no "entity" class, an entity is just an integer ID. There is no main "game object" at all. Nothing all game "things" inherit from or add components to. In NGX, an entity is a concept but in terms of implementation, its just a key. 

###NgxComponent
An object that contains the state data for one aspect of an entity. Think of a component as a table row in a database. It has no methods for behavior. If you want to create a component, make a class and make it inherit NgxComponet. Components are associated to one entity and an entity cannot have two or more of the same component types associated with it.

###NgxTable
A collection of game components. All components in the table  are the same type. A components in the table are keyed by an entity. So if you want a "Position" component for a player entity you could write something like "var pos = PositionTable[player]". Tables handle object pooling and other infrastructure related tasks. One thing to remember... adding (and removing) a component from the table is an asyncronous operation. You cannot add a comonent and start working with it right away.

###NgxDatabase
A collection of NgxTable objects. Nough said.


###Prefab
A prefab is a factory for creating entities. Remember there is no single "entity class" but you can think of an entity as a group of components that share the same key. You might have a prefab for a certain enemy type such as a goomba. You could call a prefab's create method to create a goomba rather than trying to remember all of the components and data that is needed everytime you need one.

###NgxGameSystem
The base class for all game systems. Game systems implement a specific feature or behavior in the game. All game logic is contained in systems. There are different subtypes of NgxGameSystem; some systems iterate over all components in a table or implement pseudo state machine mechanics. It's up to you if you want to use these systems or build your own type. Have a look at SMB3 to see how systems are used.

###NgxContext
Contexts contain one or more systems. Each tick of the game the context will loop through each system and call update(). You can have multiple contexts per game. Think of a context as different screens in a game, for example, you could have different systems involved in the Main Menu, Game Over, Platforming, Overworld, and Game Over. It doesn't make sense to have a physics system in the "GameOver" context.

###NgxProcess
A process is a long running task that executes over multiple game ticks. Processes is a tree of methods. Each tick the next node in the tree is selected and the method is triggered. If you are confused about the difference between systems and processes, think: systems are repeative work horses that just process data and have no real "end" other than when the context closes. Processes are typically short lived and are very sequental by nature. Loading a save game could be a process or animating a chain explosion, or making mario grow when he gets a mushroom.






