/* mhosster - Merry Hospelhorn
 * 2021
 * weaving (a+b+c+d)^2
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdaWeave : MonoBehaviour
{
    RawImage img;
    Texture2D drawTexture;

    [SerializeField]
    int squareLength = 100;

    [SerializeField]
    float shiftSpeed = 0.5f;

    [SerializeField]
    Color colorA, colorB, colorC, colorD, colorBlank;

    Color[,] colorArray2D;
    Color[] colorArray;

    [SerializeField]
    string weavePattern = "aaaabaacaadbbbbcbbdccccddd";
    string permWeavePattern;

    bool shifted = false;
    bool resetting = false;
    int shiftCounter = 0;



    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<RawImage>();

        drawTexture = new Texture2D(squareLength, squareLength, TextureFormat.ARGB32, false); 
        drawTexture.filterMode = FilterMode.Point;

        permWeavePattern = weavePattern;
        

        Weave(); //(a+b+c+d)^2
        

        img.texture = drawTexture; 
        img.SetNativeSize();


    }

    // Update is called once per frame
    void Update()
    {
        if (shifted == true)
        {
            shifted = false;

            //StartCoroutine(ScrollTexture());
            StartCoroutine(ShiftTexture());
            
        }
        
    }

    //initial weaving of the pattern 
    void Weave()
    {
        colorArray = new Color[drawTexture.width * drawTexture.height];
        colorArray2D = new Color[drawTexture.width, drawTexture.height];


        for (int x = 0; x < drawTexture.width; x++)
        {
            for (int y = 0; y < drawTexture.height; y++)
            {
                if (weavePattern[x % weavePattern.Length] == weavePattern[y % weavePattern.Length])
                {
                    if (weavePattern[x % weavePattern.Length] == 'a')
                    {
                        colorArray[(y * drawTexture.width) + x] = colorA;
                        colorArray2D[x, y] = colorA;
                    }
                    else if (weavePattern[x % weavePattern.Length] == 'b')
                    {
                        colorArray[(y * drawTexture.width) + x] = colorB;
                        colorArray2D[x, y] = colorB;
                    }
                    else if (weavePattern[x % weavePattern.Length] == 'c')
                    {
                        colorArray[(y * drawTexture.width) + x] = colorC;
                        colorArray2D[x, y] = colorC;
                    }
                    else
                    {
                        colorArray[(y * drawTexture.width) + x] = colorD;
                        colorArray2D[x, y] = colorD;
                    }

                }
                else
                {
                    colorArray[(y * drawTexture.width) + x] = colorBlank;
                    colorArray2D[x, y] = colorBlank;
                }

            }
            
        }

        drawTexture.SetPixels(colorArray);
        drawTexture.Apply();

        shifted = true;

    }

    IEnumerator ScrollTexture()
    {
        yield return new WaitForSeconds(shiftSpeed);
        Color aboveColor;

        //this scrolls it
        for (int i = 1; i < colorArray2D.GetUpperBound(0) + 1; i++)
        {
            for (int j = 0; j < colorArray2D.GetUpperBound(1) + 1; j++)
            {
                if (i == colorArray2D.GetUpperBound(0))
                {
                    colorArray2D[i - 1, j] = colorArray2D[0, j];
                }
                else
                {
                    colorArray2D[i - 1, j] = colorArray2D[i, j];
                }
            }
        }


        //transfer 2d array info into one the setpixels can read
        int width = colorArray2D.GetLength(0);
        int height = colorArray2D.GetLength(1);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                aboveColor = colorArray2D[j, i];
                colorArray[i * height + j] = aboveColor;

            }
        }

        drawTexture.SetPixels(colorArray);
        drawTexture.Apply();

        shifted = true;

    }

    IEnumerator ShiftTexture()
    {
        yield return new WaitForSeconds(shiftSpeed);

        //shift each letter in the weavepattern string to the right
        string tempString = "";

        if (resetting == true)
        {
            for (int c = 0; c < weavePattern.Length; c++)
            {
                if (c < weavePattern.Length - 1)
                {
                    tempString += weavePattern[c + 1];
                }
                else
                {
                    tempString += permWeavePattern[weavePattern.Length - shiftCounter];
                }
            }
            shiftCounter--;
        }
        else 
        {
            for (int c = 0; c < weavePattern.Length; c++)
            {
                if (c < weavePattern.Length - 1)
                {
                    tempString += weavePattern[c + 1];
                }
                else
                {
                    tempString += weavePattern[c];
                }
            }
            shiftCounter++;
        }

        if(shiftCounter == weavePattern.Length - 1)
        {
            resetting = true;
        }
        if (shiftCounter == 0)
        {
            resetting = false;
        }



            weavePattern = tempString;
        Debug.Log(weavePattern);

        for (int x = 0; x < drawTexture.width; x++)
        {
            for (int y = 0; y < drawTexture.height; y++)
            {
                if (weavePattern[x % weavePattern.Length] == weavePattern[y % weavePattern.Length])
                {
                    if (weavePattern[x % weavePattern.Length] == 'a')
                    {
                        colorArray[(y * drawTexture.width) + x] = colorA;
                        colorArray2D[x, y] = colorA;
                    }
                    else if (weavePattern[x % weavePattern.Length] == 'b')
                    {
                        colorArray[(y * drawTexture.width) + x] = colorB;
                        colorArray2D[x, y] = colorB;
                    }
                    else if (weavePattern[x % weavePattern.Length] == 'c')
                    {
                        colorArray[(y * drawTexture.width) + x] = colorC;
                        colorArray2D[x, y] = colorC;
                    }
                    else
                    {
                        colorArray[(y * drawTexture.width) + x] = colorD;
                        colorArray2D[x, y] = colorD;
                    }

                }
                else
                {
                    colorArray[(y * drawTexture.width) + x] = colorBlank;
                    colorArray2D[x, y] = colorBlank;
                }

            }

        }

        drawTexture.SetPixels(colorArray);
        drawTexture.Apply();

        shifted = true;


    }
}
