using Raylib_cs;
using System.Numerics;


namespace Tower{
    class Defender: GamePiece {

        public Texture2D texture {get; set;}
        public int Cost {get; set;}
        public int size {get; set;}

        public List<Arrow> Arrows {get; set;} = new List<Arrow>();
        public List<Vector2> circles{get; set;} = new List<Vector2>();
        



        public override void Draw() {
            Raylib.DrawTexture(this.texture, (int)Location.X, (int)Location.Y, Color.WHITE);
        }

        public virtual void Attack(int count){

        }

        public virtual Rectangle Rect() {
            return new Rectangle();
        } 

         
    }

    class Wizard: Defender {
        
        Cource Path = new Cource(@"Cource.csv");
        List<Enemy> Army = new List<Enemy>();

        

        public Wizard(Cource path, Vector2 i, List<Enemy> army) {
            Path = path;
            var image = Raylib.LoadImage(@"wizard.png");
            this.texture = Raylib.LoadTextureFromImage(image);
            Raylib.UnloadImage(image); 

            this.Cost = 500;
            this.Location = i;

            Army = army;
        }

        public override void Attack(int count){
            if (count % 40 == 0){
                Vector2 a = new Vector2(this.Location.X+45, this.Location.Y+50);
                this.circles.Add(a);
            }

            size = 10;
            foreach (Vector2 v in this.circles){
                Raylib.DrawCircleV(v, size, Color.PURPLE);
                this.size +=10;
            }

            if (circles.Count > 7){
                circles.Clear();
            }
        }
        

        public override Rectangle Rect() {
            Raylib.DrawRectangle((int)this.Location.X+25, (int)this.Location.Y+5, 40, 75, Color.PURPLE);
            return new Rectangle(this.Location.X+25, this.Location.Y+5, 50, 100);
        } 
    }

    class Knight: Defender {
        Cource Path = new Cource(@"Cource.csv");

        public Knight(Cource path, Vector2 i) {
            Path = path;
            var image = Raylib.LoadImage(@"knight.png");
            this.texture = Raylib.LoadTextureFromImage(image);
            Raylib.UnloadImage(image); 
            

            this.Cost = 25;
            this.Location = i;

        }
        
        public override Rectangle Rect() {
            Raylib.DrawRectangle((int)this.Location.X+15, (int)this.Location.Y+10, 50, 90, Color.PURPLE);
            return new Rectangle(this.Location.X+15, this.Location.Y+10, 50, 90);
        } 
        public override void Attack(int count){
            //ArrowPositions = new Vector2(this.Location.X+35, this.Location.Y+45);
            if (count % 100 == 0){
                Arrow a = new Arrow(this, 2, 25, Color.DARKBROWN, 1); 
                a.Velocity = new Vector2(0, -1);
                this.Arrows.Add(a);

                Arrow b = new Arrow(this, 2, 25, Color.DARKBROWN, 1); 
                b.Velocity = new Vector2(0, 1);
                Arrows.Add(b);

                Arrow c = new Arrow(this, 25, 2, Color.DARKBROWN, 1); 
                c.Velocity = new Vector2(-1, 0);
                Arrows.Add(c);

                Arrow d = new Arrow(this, 25, 2, Color.DARKBROWN, 1); 
                d.Velocity = new Vector2(1, 0);
                Arrows.Add(d);
            }

            foreach (Arrow a in Arrows.ToList()){
                a.Draw();
                a.Move();
                
                if (a.Location.X < this.Location.X-100 | a.Location.X>this.Location.X +150 | a.Location.Y <this.Location.Y -100 | a.Location.Y > this.Location.Y + 100){
                    Arrows.Remove(a);
                }   

            }
        }

    }

    class Catapult: Defender {
        Cource Path = new Cource(@"Cource.csv");
        int ArrowStrength = 5;

        public Catapult(Cource path, Vector2 i) {
            Path = path;
            var image = Raylib.LoadImage(@"catapult.png");
            this.texture = Raylib.LoadTextureFromImage(image);
            Raylib.UnloadImage(image); 

            this.Cost = 200;
            this.Location = i;
        }
                
        public override Rectangle Rect() {
            Raylib.DrawRectangle((int)this.Location.X+25, (int)this.Location.Y+20, 50, 50, Color.PURPLE);
            return new Rectangle(this.Location.X+25, this.Location.Y+20, 50, 50);
        } 
        public override void Attack(int count){
            if (count % 130 == 0){
                Arrow a = new Arrow(this, 8, 8, Color.BLACK, 5); 
                a.Velocity = new Vector2(0, -2);
                Arrows.Add(a);
            }

            foreach (Arrow a in Arrows.ToList()){
                a.Draw();
                a.Move();

                // Checks if Aarow goes off the screen
                if (a.Location.X <-40){
                    Arrows.Remove(a);
                }   
            }
        }

        
    }

    class Arrow :GamePiece {
        public Vector2 Velocity {get; set;} = new Vector2();
        Defender BadGuy = new Defender();
        int Width;
        int Height;
        Color color;
        public int Strength {get; set;}

        public Arrow(Defender d, int w, int h, Color c, int s){
            BadGuy = d;
            this.Location = new Vector2 (BadGuy.Location.X+35, BadGuy.Location.Y+45);
            Width = w;
            Height = h;
            color = c;
            Strength = s;
        }

        public override void Draw()
        {
            Raylib.DrawRectangle((int)this.Location.X, (int)this.Location.Y, Width, Height, color);
        }

        public void Move() {
            Vector2 NewPosition = this.Location;
            NewPosition.X += Velocity.X;
            NewPosition.Y += Velocity.Y;
            Location = NewPosition;
    }

    public Rectangle Rect(){
        return new Rectangle((int)this.Location.X, (int)this.Location.Y, Width, Height);
    }

    public bool IsDead(){
        if (this.Strength <=0){
            return true;
        }
        else {return false; 
        }
    }
    }
}