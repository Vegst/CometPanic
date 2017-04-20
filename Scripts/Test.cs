using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
    
    public Sprite ldpi; // 120 dpi
    public Sprite mdpi; // 160 dpi
    public Sprite hdpi; // 240 dpi
    public Sprite xhdpi; // 320 dpi
    public Sprite xxhdpi; // 480 dpi
    public Sprite xxxhdpi; // 640 dpi


    void Start ()
    {
        UpdateSprite();
    }

    Sprite GetSprite(float dpi)
    {
        Sprite sprite;
        if (dpi < 160) sprite = ldpi;
        else if (dpi < 240) sprite = mdpi;
        else if (dpi < 320) sprite = hdpi;
        else if (dpi < 480) sprite = xhdpi;
        else if (dpi < 640) sprite = xxhdpi;
        else sprite = xxxhdpi;
        return sprite;
    }
	
	void Update()
    {
        UpdateSprite();
    }

    void UpdateSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = GetSprite(Screen.dpi * Mathf.Max(gameObject.transform.localScale.x, gameObject.transform.localScale.y));
    }
}
