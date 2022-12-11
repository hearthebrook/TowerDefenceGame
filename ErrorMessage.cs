using Raylib_cs;
using System.Numerics;

namespace Tower {
    class ErrorMessage: GamePiece {
        public string message {get; set;} = "";

        public ErrorMessage (){
            this.Location = new Vector2 (100, 30);
        }

        public override void Draw()
        {
            Raylib.DrawText(this.message, (int)Location.X, (int)Location.Y, 20, Color.RED);
        }
    }
}