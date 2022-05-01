using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATCG_Projectile : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
