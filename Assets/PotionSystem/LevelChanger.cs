using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LevelChanger : MonoBehaviour
{
    public void FinishLevel()
    {
        if(TableUI.accuracy > 70f)
        {
            InfluenceBar.increaseInfluence();

        }
        else
        {
            InfluenceBar.decreaseInfluence();
        }
    }


}
