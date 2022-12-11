using Raylib_cs;
using System.Numerics;

namespace Tower{
    public class Cource:GamePiece {
        public List<Vector2> PathList {get; set;} = new List<Vector2>();
        
        
        public Cource(String file){
            this.ReadPoints(file);
        }

        public override void Draw(){
            foreach (Vector2 spot in this.PathList){
                Raylib.DrawRectangle((int)spot.X+5, (int)spot.Y+30, 40, 40, Color.BEIGE);
                }
        }

        // 
        public void ReadPoints(String file){
            using (var reader = new StreamReader(file)){
                // Creates the Lists 
                //List<Vector2> Path = new List<Vector2>();
                int count = 0;

                //Goes through each line of the CSV
                while (!reader.EndOfStream){
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    Vector2 v = new Vector2(float.Parse(values[0]), float.Parse(values[1]));
                    PathList.Add(v);
                    count++;
                }
            }
        }
    }
}