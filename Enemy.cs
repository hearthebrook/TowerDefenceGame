using Raylib_cs;
using System.Numerics;

namespace Tower{
    class Enemy: GamePiece{

        public Texture2D texture{ get;set; }

        public Cource Path {get; set;} = new Cource(@"Cource.csv");
        public int EnemyCount {get; set;} = 0;
        public int Life {get; set;}
        public string kind {get; set;}

        public Enemy() {
            this.kind = "none";
            this.Location = Path.PathList[0];
        }

        public override void Draw() {
        }

        public Rectangle Rect() {
            return new Rectangle(this.Location.X+10, this.Location.Y+15, 25, 35);
        } 

        public void Move(){
            this.Location = Path.PathList[EnemyCount];
        }

        public bool IsAtEnd(List<Vector2> path){
            if (this.EnemyCount >= path.Count-5){
            return true;}

            else {return false;}
        }

        public bool IsDead() {
            if(Life <=0 ){
                return true;
            }
            else {
                return false;
            }
        }
    }

}