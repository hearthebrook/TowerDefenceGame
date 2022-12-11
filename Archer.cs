using Raylib_cs;
using System.Numerics;

namespace Tower{
    class Archer: Enemy{

        public Archer() {
            this.Life = 3;
            this.kind = "Archer";
            var image = Raylib.LoadImage(@"midevileThiefcopy2.png");
            this.texture = Raylib.LoadTextureFromImage(image);
            Raylib.UnloadImage(image); 

            this.Location = Path.PathList[0];
        }

        public override void Draw() {
            Raylib.DrawTexture(this.texture, (int)Location.X, (int)Location.Y, Color.WHITE);
        }
    }

}