using score_system;

namespace packing_scripts
{
    public class Charger : PackingItem
    {
        public override int GetPoints() 
        {
            points = ScoreScript.Instance?.chosenShelter == MapChoice.Hill ? 1 : 3;
            return points;
        }
    }
}