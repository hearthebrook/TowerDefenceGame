using Raylib_cs;
using System.Numerics;

namespace Tower {
    public class GamePiece{
        public Vector2 Location {get; set;}= new Vector2();

        public virtual void Draw(){

        }
    }
}