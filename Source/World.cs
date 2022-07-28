using MonoGame1.Source.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame1.Source
{
    public class World
    {
        public Basic2D player;
        public World()
        {
            player = new Basic2D("Images/Sprites/redhead", Vector2.Zero, new Vector2(50, 50));
        }
        public virtual void Update()
        {
            player.Update();
        }
        public virtual void Draw()
        {
            player.Draw();
        }
    }
}
