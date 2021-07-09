using System;
using UnityEngine;
using UnityEngine.UI;

public class Customizer : MonoBehaviour
{
    public Slider rSlider;
    public Slider gSlider;
    public Slider bSlider;

    public InputField rInput;
    public InputField gInput;
    public InputField bInput;

    public Image tri;
    
    private int r, g, b;
    private Color color;
    private OwnedData owned;
    // Start is called before the first frame update
    void Start()
    {
        owned = SaveSystem.LoadOwned();
        if (owned?.customColor == null)
        {
            print("umm");
            r = 0;
            g = 0;
            b = 0;
        }
        else
        {
            r = owned.customColor[0];
            g = owned.customColor[1];
            b = owned.customColor[2];
            
        }
        
        Update_Color();
        Update_Triangle();
        Update_Slider();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void R_S_change(float val)
    {
        r = (int)val;
        rInput.text = val.ToString();
        Update_Color();
        Update_Triangle();
    }
    
    public void G_S_change(float val)
    {
        g = (int)val;
        gInput.text = val.ToString();
        Update_Color();
        Update_Triangle();
    }
    
    public void B_S_change(float val)
    {
        b = (int)val;
        bInput.text = val.ToString();
        Update_Color();
        Update_Triangle();
    }

    public void ResetAll()
    {
        r = 0;
        g = 0;
        b = 0;
        
        Update_Slider();
        Update_Color();
    }

    public void Update_Color()
    {
        color = new Color32(Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b), 255);
    }

    public void Update_Triangle()
    {
        tri.color = color;
    }

    public void Update_Slider()
    {
        rSlider.value = r;
        gSlider.value = g;
        bSlider.value = b;
        
        rInput.text = r.ToString();
        gInput.text = g.ToString();
        bInput.text = b.ToString();
    }
    

    public void Save()
    {
        Update_Color();
        SaveSystem.SaveOwned(owned.ownedPlayers, owned.currentPlayer, color);
    }
}
