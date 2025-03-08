using UnityEngine;

public class HoldManager : MonoBehaviour
{
    public static HoldManager Instance;

    public IngredientDuplicator currentItem;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
