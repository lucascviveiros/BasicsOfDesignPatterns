using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private Renderer _renderer;

    private MaterialPropertyBlock _propBlock;

    private void Awake() 
    {
        _renderer = GetComponent<Renderer>();
        _propBlock = new MaterialPropertyBlock();    
    }

    /// <summary>
    /// Inside the first Update is happening the copy of the material with a random color
    /// Instead what can be done to reduce memory usage is to update the existent data
    /// Change the material property in that specific instance in the second Update Bellow
    /// </summary>
    /*void Update()
    {
        _renderer.material.color = GetRandomColor();
        _renderer.material.SetColor("_Color", GetRandomColor());
    }*/

    void Update()
    {
        //Get the current value of the material properties in the renderer
        _renderer.GetPropertyBlock(_propBlock);
        //Assign the new color value
        _propBlock.SetColor("_Color", GetRandomColor());
        //Apply the edited values to the renderer
        _renderer.SetPropertyBlock(_propBlock);
    }

    private Color GetRandomColor()
    {
        return new Color (
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f));
    }
}
