using Raylib_cs;
using System.Numerics;

namespace Tower {
    class Store: GamePiece {

        Texture2D texture;
        List<Defender> People = new List<Defender>();
        public Store(List<Defender> l) {
            var image = Raylib.LoadImage(@"tent.png");
            this.texture = Raylib.LoadTextureFromImage(image);
            Raylib.UnloadImage(image); 

            People = l;
            this.Location = new Vector2(472, -135);
        }

        public override void Draw()
        {
            Raylib.DrawRectangle(710, 0, 100, 800, Color.LIGHTGRAY);
            Raylib.DrawTexture(this.texture, (int)Location.X, (int)Location.Y, Color.WHITE);
            foreach (Defender g in People){
                g.Draw();
                Raylib.DrawText($"${g.Cost}", (int)g.Location.X+20, (int)g.Location.Y+90, 20, Color.DARKGRAY);
            }

        }
    }
}