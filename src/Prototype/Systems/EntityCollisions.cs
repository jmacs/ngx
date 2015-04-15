using System.Collections.Generic;
using NgxLib;
using NgxLib.Collisions;
using Prototype.Components;

namespace Prototype.Systems
{
    public static class EntityCollisions
    {
        public static void Initialize()
        {
            CollisionMap.Register<JumpBoots, Shell>
                (Msg.On_JumpBoots_touch_Shell);

            CollisionMap.Register<JumpBoots, Stompable>
                (Msg.On_JumpBoots_touch_Stompable);

            CollisionMap.Register<Player, Interactor>
                (Msg.On_Player_touch_Interactor);

            CollisionMap.Register<Player, Pickup>
                (Msg.On_Player_touch_Pickup);
        }
    }
}
