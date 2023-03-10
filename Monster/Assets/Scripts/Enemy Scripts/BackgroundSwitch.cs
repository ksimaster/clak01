using UnityEngine;

public class BackgroundSwitch : MonoBehaviour
{
    public GameObject[] backgrounds = new GameObject[2];
    
    public void SelectBackground(int index)
    {
        foreach (var image in backgrounds)
        {
            image.SetActive(false);
        }

        backgrounds[index].SetActive(true);
    }
}
