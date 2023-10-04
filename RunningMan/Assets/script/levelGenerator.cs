using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int mapIndex =1;
    void Start()
    {
        Texture2D texture = Resources.Load<Texture2D>("Map/" + mapIndex);
        Color color;
        for(int y = 0;  y <texture.width; y++)
        {
            for(int x = 0;x< texture.height; x++) {
                color = texture.GetPixel(x, y);

                Debug.Log(color);
                switch (color.ToString()) 
                {
                    case "RGBA(0.937, 0.110, 0.129, 0.000)":
                        Instantiate(Resources.Load("Prefab/Wall"),new Vector3(x+.5f,y+.5f,0),Quaternion.identity);
                        break;
                    case "RGBA(0.290, 0.271, 0.290, 0.000)":
                        Instantiate(Resources.Load("Prefab/ExpandSpike"), new Vector3(x + .5f, y + .5f, 0), Quaternion.identity);
                        break;
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
