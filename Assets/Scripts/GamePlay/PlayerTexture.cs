using System;
using UnityEngine;

public class PlayerTexture : MonoBehaviour
{
    public Sprite[] sprites;
    public Material[] materials;
    // Start is called before the first frame update
    void Start()
    {
        OwnedData data = SaveSystem.LoadOwned();

        SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
        TrailRenderer trail = gameObject.GetComponent<TrailRenderer>();
        
        if (data != null)
        {
            // if current player is 14, it is the custom one
            if (data.currentPlayer == 14)
            {
                Color32 color = new Color32(Convert.ToByte(data.customColor[0]),
                    Convert.ToByte(data.customColor[1]),
                    Convert.ToByte(data.customColor[2]), 255);
                
                materials[14].color = color;
                spr.sprite = sprites[14];
                spr.color = color;
                trail.material = materials[14];

            }

            spr.sprite = sprites[data.currentPlayer];
            trail.material = materials[data.currentPlayer];
        }
    }
}
