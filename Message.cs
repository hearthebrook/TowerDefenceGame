using Raylib_cs;
using System.Numerics;

namespace Tower {
    class Message: GamePiece {
        public string message {get; set;} = "";
        

        public Message (int x, int y){
            this.Location = new Vector2 (x, y);
        }

        public override void Draw()
        {
            Raylib.DrawText(this.message, (int)Location.X, (int)Location.Y, 30, Color.BLACK);
        }
    }
}