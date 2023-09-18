using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadzone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Damageable>() != null)
        {
            Damageable dmg = collision.GetComponent<Damageable>();
            dmg.Hit(1000, new Vector2(0,0));
        }
    }
}
