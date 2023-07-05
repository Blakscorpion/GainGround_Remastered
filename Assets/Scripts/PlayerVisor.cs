using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisor : MonoBehaviour
{
    [SerializeField]
    Sprite Visor;
    public float angle;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawLine(transform.position, mouseWorldPosition, Color.red);
    }
}
