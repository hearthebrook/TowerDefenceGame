using Raylib_cs;
using System.Numerics;

namespace Tower {
    class Wallet: GamePiece {
        public int Balance {get; set;} = 100;

        public Wallet(){
            Location = new Vector2 (720, 70);
        }

        public void Pay(int Amount){
            this.Balance += Amount;
        }

        public void Spend(int Amount){
            this.Balance -= Amount;
        }


        public override void Draw()
        {
            Raylib.DrawRectangle((int)Location.X-8, (int)Location.Y-7, 80, 35, Color.DARKBROWN);
            Raylib.DrawText($"${Balance}", (int)Location.X, (int)Location.Y, 25, Color.WHITE);
        }
    }

    class Life: GamePiece{

        public int Health {get; set;} = 100;
         public Life(){
            Location = new Vector2 (20, 10);
        }

        public void Hit(int strength){
            this.Health -= strength;
        }

        public override void Draw()
        {
            Raylib.DrawText($"Lives: {Health}", (int)Location.X, (int)Location.Y, 20, Color.WHITE);
        }
    }
}