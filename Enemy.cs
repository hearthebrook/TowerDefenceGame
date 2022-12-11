using Raylib_cs;
using System.Numerics;

namespace Tower{
    class Enemy: GamePiece{

        Texture2D texture;

        Cource Path = new Cource(@"Cource.csv");
        public int EnemyCount {get; set;} = 0;
        public int Life {get; set;} = 5;

        public Enemy(Cource path) {
            Path = path;
            var image = Raylib.LoadImage(@"midevileThiefcopy2.png");
            this.texture = Raylib.LoadTextureFromImage(image);
            Raylib.UnloadImage(image); 

            this.Location = Path.PathList[0];
        }

        public override void Draw() {
            Raylib.DrawTexture(this.texture, (int)Location.X, (int)Location.Y, Color.WHITE);
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

        public bool IsHit(Defender d) {
            return true;
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