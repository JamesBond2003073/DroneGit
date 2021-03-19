using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Color Color1, Color2;
    public float Speed = 1, Offset;
    public int passFlag = 0;
 
     public Renderer _renderer;
    public MaterialPropertyBlock _propBlock;
    void Start()
    {
        
    }
    void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
   
   void Update()
   {
     
   }

    void OnTriggerEnter(Collider other)
    {
      
         _renderer.material.SetColor("_BaseColor", Color1);
        // Get the current value of the material properties in the renderer.
       // _renderer.GetPropertyBlock(_propBlock);
        // Assign our new value.
      //  _propBlock.SetColor("_BaseColor", Color1);
        // Apply the edited values to the renderer.
      //  _renderer.SetPropertyBlock(_propBlock);
    }
}
