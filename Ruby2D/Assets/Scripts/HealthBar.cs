
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance { get; private set; }

    public Image mask;
    float originalSize;

    private void Awake()
    {
        instance = this; // this = the object that currently runs that function
    }

    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    } 

    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
