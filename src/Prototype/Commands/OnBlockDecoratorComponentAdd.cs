using System;
using Microsoft.Xna.Framework;
using NgxLib;
using Prototype.Components;

namespace Prototype.Commands
{
    public class OnBlockDecoratorComponentAdd : INgxCommand
    {
        public long GetMessageKey()
        {
            return Ngx.Components.Get<Decorator>().AddMessageKey;
        }

        public void Execute(NgxContext context, NgxMessage message)
        {
            var component = context.Database.Component<Decorator>(message.Entity1);

            var entity = component.Entity;
            var spatial = context.Database.Component<Spatial>(entity);
            var offset = spatial.Position + new Vector2(4, -4);

            var map = context.MapManager.Map;
            var cell = map.PositionToCell(offset);

            for (var i = 0; i < component.AdjacentX + 1; i++)
            {
                cell = map.GetCell(cell.X + i, cell.Y);
                cell.Decorator = entity;
                cell.DecoratorType = component.DecoratorType;
            }
        }
    }
}
