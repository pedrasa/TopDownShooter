using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    //var
    public Transform player;
    public float smooth = 0.3f;
    public float height;
    public float fixer;
    public float fixerZ;
    public Vector3 velocity = Vector3.zero;

    //metodos
    void Update()
    {
        Vector3 pos = new Vector3();
        pos.x = player.position.x + fixer;
        pos.z = player.position.z - fixerZ;
        pos.y = player.position.y + height;
        transform.position = Vector3.SmoothDamp(transform.position,pos,ref velocity,smooth);

    }
}
