using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIJFScript : MonoBehaviour
{
    public Slider exampleSlider;
    public TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = exampleSlider.value.ToString();
    }
}
