using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera_Takip : MonoBehaviour
{
   public GameObject hedef;
   Vector3 uzaklik;

    void Start()
    {
        uzaklik = transform.position - hedef.transform.position;
    }

    private void LateUpdate()
    {
        if(Player.dustuMu) //true
        {
            return; //Takibi sonlandır.
        }
        transform.position = hedef.transform.position + uzaklik; //kameranın yeni pozisyonu playerın pozisyonu + uzaklık
    }
}
