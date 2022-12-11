using Raylib_cs;
using System.Numerics;

namespace Tower{

    class Mouse: GamePiece{
        public List<Vector2> LocationList {get; set;} = new List<Vector2>();

        public Mouse(){
            this.Location = Raylib.GetMousePosition();

            for (int i = 1; i <= 1; i++){
                LocationList.Add(new Vector2(Location.X+i, Location.Y+i));
            }
        }

        public override void Draw()
        {
            foreach (Vector2 v in LocationList){
                Raylib.DrawCircleV(v, 3, Color.GRAY);
            }
        }

        public List<Rectangle> Rect()
        {
            List<Rectangle> theList = new List<Rectangle>();
            foreach (Vector2 v in LocationList){
                Rectangle r = new Rectangle(v.X, v.Y, 3, 3);
                theList.Add(r);
            }
            return theList;
        }

    }
}