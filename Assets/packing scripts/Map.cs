using score_system;

namespace packing_scripts
{
    public class Map : PackingItem
    {
        public override int GetPoints() 
        {
            points = ScoreScript.Instance?.mapDone == true ? 4 : 2;
            return points;
        }
    }
}