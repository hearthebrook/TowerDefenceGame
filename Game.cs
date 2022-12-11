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
            var ErrorMessage = new Message(200, 20); 
            var LevelMessage = new Message(10, 450);

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
            Display.Add(LevelMessage);

            
            Raylib.SetTargetFPS(60);

            // Sets counters 
            int i = 0;
            int Count = 0;
            bool GameOver = false;

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
                                    ErrorMessage.message = "Keep Saving =D";
                                }
                            }
                            if (Raylib.CheckCollisionRecs(r, Wiz.Rect())){
                                if (Wallet.Balance >= Wiz.Cost){
                                    var w = new Wizard(thisPath, Raylib.GetMousePosition(), Army);
                                    BoughtPlayers.Add(w);
                                    Wallet.Balance -= Wiz.Cost;
                                }
                                else {
                                    ErrorMessage.message = "Can't get that!";
                                }
                            }
                            if (Raylib.CheckCollisionRecs(r, Cat.Rect())){
                                if (Wallet.Balance >= Cat.Cost){
                                    var c = new Catapult(thisPath, Raylib.GetMousePosition());
                                    BoughtPlayers.Add(c);
                                    Wallet.Balance -= Cat.Cost;
                                }
                                else {
                                    ErrorMessage.message = "You don't have enough money";
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
                    if(Army.Count() != 0){
                        d.Attack(Count);
                    }
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
                                if (e.kind == "Archer"){
                                    Army.Remove(e);
                                    Wallet.Balance += 50;
                                }
                                else if (e.kind == "Waggon"){
                                    for (i =1; i<5; i++){
                                        var BadGuy = new Archer();
                                        BadGuy.EnemyCount = e.EnemyCount-i*3;
                                        Army.Add(BadGuy);
                                    }
                                    Army.Remove(e);
                                }
                            }
                        }
                    }
                }

                // Wizard collisions
                foreach (Defender d in AlivePlayers) {
                    foreach (Vector2 v in d.circles){
                        foreach (Enemy e in Army.ToList()){
                            if (d.circles.Count() == 6){
                                if(Raylib.CheckCollisionCircleRec(v, d.size, e.Rect())){
                                    e.EnemyCount -=1;
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
                        if (g.kind == "Waggon"){
                            Army.Remove(g);
                            Life.Hit(15);
                        }
                        else if (g.kind == "Archer"){
                            Army.Remove(g);
                            Life.Hit(g.Life);
                        }
                    }
                    else {
                        g.Draw();
                    }
                }  

                // Draws the mouse and castle 
                Castle.Draw();
                M.Draw(); 


                
                // checks for game over and displays message
                if (Life.Health <= 0){
                    Raylib.DrawText("GAME OVER", 250, 200, 40, Color.WHITE);
                    GameOver = true;
                }  

                if (GameOver == true){
                    AlivePlayers.Clear();
                    BoughtPlayers.Clear();
                    Life.Health = 0;
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
                // Level 1
                if (Count < 450){
                    LevelMessage.message = "LEVEL 1";
                    if (Count % 45 == 0){
                        var BadGuy = new Archer();
                        Army.Add(BadGuy);
                    }
                }

                //Level 2
                if (Count > 1556 && Count < 2500){
                    LevelMessage.message = "LEVEL 2";
                    if (Count % 45 == 0){
                        var BadGuy = new Archer();
                        Army.Add(BadGuy);
                    }
                }
                //Level 3
                if (Count > 4000 && Count < 5000){
                    LevelMessage.message = "LEVEL 3";
                    if (Count % 45 == 0){
                        var BadGuy = new Archer();
                        Army.Add(BadGuy);
                    }
                    if (Count % 300 == 0){
                        var Waggon = new Waggon();
                        Army.Add(Waggon);
                    }
                }
                // Level 4
                if (Count > 6500 && Count < 8000){
                    LevelMessage.message = "LEVEL 4";
                    if (Count % 45 == 0){
                        var BadGuy = new Archer();
                        Army.Add(BadGuy);
                    }
                    if (Count % 300 == 0){
                        var Waggon = new Waggon();
                        Army.Add(Waggon);
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