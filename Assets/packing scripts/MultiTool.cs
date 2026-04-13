using score_system;

namespace packing_scripts
{
    public class MultiTool : PackingItem
    {
        public override int GetPoints() 
        {
            points = ScoreScript.Instance?.chosenShelter == MapChoice.DesignatedShelter ? 0 : 5;
            return points;
        }
    }
}