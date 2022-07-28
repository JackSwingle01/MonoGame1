using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MonoGame1.Source.Engine
{
    

    public class Basic2D
    {
        public Vector2 pos;
        public Vector2 dims;
        public Texture2D model;
        public Basic2D(string PATH, Vector2 POS, Vector2 DIMS)
        {
            pos = POS;
            dims = DIMS;

            model = Globals.content.Load<Texture2D>(PATH);
            

        }
        public virtual void Update()
        {

        }
        public virtual void Draw()
        {
            Globals.spriteBatch.Draw(model, pos, Color.White);
        }
    }
}
