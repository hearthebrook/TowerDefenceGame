using Raylib_cs;
using System.Numerics;

namespace Tower {
    class Waggon : Enemy {
        
        public Waggon() {
            this.Life = 10;
            this.kind = "Waggon";
            var image = Raylib.LoadImage(@"waggon1.png");
            this.texture = Raylib.LoadTextureFromImage(image);
            Raylib.UnloadImage(image); 

            this.Location = Path.PathList[0];
        }

        public override void Draw() {
            Raylib.DrawTexture(this.texture, (int)Location.X-10, (int)Location.Y-20, Color.WHITE);
        }
    }
}