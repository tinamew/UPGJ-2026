using UnityEngine;

public class DamageSelect : MonoBehaviour
{
    private DamageType currentlySelected;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectDamageType(DamageType selectedType)
    {
        currentlySelected = selectedType;
    }

   
}
