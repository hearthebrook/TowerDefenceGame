using Raylib_cs;
using System.Numerics;



namespace Tower {
    class Game {
        public void Play()
        {
            // initiates window
            var ScreenHeight = 480;
            var ScreenWidth = 800;
            Raylib.InitWindow(ScreenWidth, ScreenHeight, "TowerDefence");

            // Creates the Players
            var Data = @"Cource.csv";
            var thisPath = new Cource(Data);
            var Castle = new Castle();
            var Wallet = new Wallet();
            var Life = new Life();
            List<Enemy> Army = new List<Enemy>();
            var ErrorMessage = new ErrorMessage(); 

            // Creates the Defenders in the store
            var Wiz = new Wizard(thisPath, new Vector2(715, 350), Army);
            var Cat = new Catapult(thisPath, new Vector2(705, 230));
            var Kni = new Knight(thisPath, new Vector2(715, 120));
            
            List<Defender> Shop = new List<Defender>();
            Shop.Add(Wiz);
            Shop.Add(Cat);
            Shop.Add(Kni);
            var Store = new Store(Shop);

            List<Defender> BoughtPlayers = new List<Defender>();
            List<Defender> AlivePlayers = new List<Defender>();

            // Makes a display list
            List<GamePiece> Display = new List<GamePiece>();
            Display.Add(thisPath);
            Display.Add(Store);
            Display.Add(Life);
            Display.Add(Wallet);
            Display.Add(ErrorMessage);

            
            Raylib.SetTargetFPS(60);

            // Sets counters 
            int i = 0;
            int Count = 0;

            while (!Raylib.WindowShouldClose())
            {   
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.DARKGREEN);

                // Creates a mouse dot
                var M = new Mouse();

                // Manages Store purchases 
                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON)){
                    if (BoughtPlayers.Count <= 1){
                        foreach (Rectangle r in M.Rect()){
                            if (Raylib.CheckCollisionRecs(r, Kni.Rect())){
                                if (Wallet.Balance >= Kni.Cost){
                                    var k = new Knight(thisPath, Raylib.GetMousePosition());
                                    BoughtPlayers.Add(k);
                                    Wallet.Balance -= Kni.Cost;
                                }
                                else {
                                    ErrorMessage.message = "You don't have enough money in your pouch to buy that!";
                                }
                            }
                            if (Raylib.CheckCollisionRecs(r, Wiz.Rect())){
                                if (Wallet.Balance >= Wiz.Cost){
                                    var w = new Wizard(thisPath, Raylib.GetMousePosition(), Army);
                                    BoughtPlayers.Add(w);
                                    Wallet.Balance -= Wiz.Cost;
                                }
                                else {
                                    ErrorMessage.message = "You don't have enough money in your pouch to buy that!";
                                }
                            }
                            if (Raylib.CheckCollisionRecs(r, Cat.Rect())){
                                if (Wallet.Balance >= Cat.Cost){
                                    var c = new Catapult(thisPath, Raylib.GetMousePosition());
                                    BoughtPlayers.Add(c);
                                    Wallet.Balance -= Cat.Cost;
                                }
                                else {
                                    ErrorMessage.message = "You don't have enough money in your pouch to buy that!";
                                }
                            }
                        }
                    }
                }
                // Resets error message 
                if (Count % 200 == 0){
                    ErrorMessage.message = "";
                }

                // moves the boought player to a location with the mouse
                foreach (Defender d in BoughtPlayers.ToList()){
                    d.Draw();
                    d.Location = Raylib.GetMousePosition();

                    if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_RIGHT_BUTTON)){
                        d.Location = d.Location;
                        AlivePlayers.Add(d);
                        BoughtPlayers.Remove(d);
                    }
                    
                }

                // Draws Path and Castle and the Shop defenders
                
                foreach (GamePiece a in Display){
                    a.Draw();
                }
                foreach (Defender d in Shop){
                    d.Draw();
                }

                // Draws the bought players and their attacks
                foreach (Defender d in AlivePlayers){
                    d.Attack(Count);
                    d.Draw();
                }   

                // Checks for collisions between the attacks and enemies
                foreach (Defender d in AlivePlayers) {
                    foreach (Arrow a in d.Arrows.ToList()){
                        foreach (Enemy e in Army.ToList()){
                            if(Raylib.CheckCollisionRecs(e.Rect(), a.Rect())){
                                a.Strength -=1;
                                e.Life -= 1;
                            }
                            if (a.Strength <= 0){
                                d.Arrows.Remove(a);
                            }
                            if (e.IsDead()) {
                                Army.Remove(e);
                                Wallet.Balance += 50;
                            }
                        }
                    }
                }

                foreach (Defender d in AlivePlayers) {
                    foreach (Vector2 v in d.circles){
                        foreach (Enemy e in Army.ToList()){
                            if (d.circles.Count() > 6){
                                if(Raylib.CheckCollisionCircleRec(v, d.size, e.Rect())){
                                    e.Life -= 1;
                                }
                            }
                            if (e.IsDead()) {
                                Army.Remove(e);
                                Wallet.Balance += 50;
                            }
                        }
                    }
                }

                // Draws the Army or removes them if they're at the end
                foreach (Enemy g in Army.ToList()){
                    if (g.IsAtEnd(thisPath.PathList)){
                        Army.Remove(g);
                        Life.Hit(g.Life);
                    }
                    else {
                        g.Draw();
                    }
                }  

                // Draws the mouse and castle 
                Castle.Draw();
                M.Draw(); 

                
                // checks for game over and displays message
                if (Life.Health == 0){
                    Raylib.DrawText("GAME OVER", 250, 200, 40, Color.WHITE);
                }  

                Raylib.EndDrawing(); {
                }

                // Moves Army forward
                if (i == 5){
                    foreach (Enemy a in Army){
                        a.Move();
                        a.EnemyCount += 1;
                    }
                    i = 0;
                }

                // Releases Enemies
                if (Count < 450){
                    if (Count % 45 == 0){
                        var BadGuy = new Enemy(thisPath);
                        Army.Add(BadGuy);
                    }
                }
                if (Count > 1556 && Count < 2500){
                    if (Count % 45 == 0){
                        var BadGuy = new Enemy(thisPath);
                        Army.Add(BadGuy);
                    }
                }
                if (Count > 4000 && Count < 5000){
                    if (Count % 45 == 0){
                        var BadGuy = new Enemy(thisPath);
                        Army.Add(BadGuy);
                    }
                }
                if (Count > 8000 && Count < 8000){
                    if (Count % 45 == 0){
                        var BadGuy = new Enemy(thisPath);
                        Army.Add(BadGuy);
                    }
                }
            
            // Adds to counts 
            Count ++;
            i ++;
        }
        Raylib.CloseWindow();
    }
    }
}