using Raylib_cs;

namespace Tower {
    class Castle: GamePiece {
        Texture2D texture;



        public Castle() {
            var image = Raylib.LoadImage("castle2.png");
            this.texture = Raylib.LoadTextureFromImage(image);
            Raylib.UnloadImage(image); 
        }

        public override void Draw() {
            Raylib.DrawTexture(this.texture, 565, 0, Color.WHITE);
        }
}   
}