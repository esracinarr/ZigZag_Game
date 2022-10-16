using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zemin_Olusturma : MonoBehaviour
{
    public GameObject son_zemin;
    
    void Start()
    {
        for(int i =1; i<= 20; i++)
        {
            zemin_Olustur();
        }
    }

   public void zemin_Olustur()
    {
        Vector3 yon;
        int randomSayi = Random.Range(0,3);  //0 yada 1 gelecek

        if(randomSayi == 0 )
        {
            yon = Vector3.left;
        }
        else
        {
            yon = Vector3.back;
        }

        //son zemini değiştirip her seferinde bir o kadar ekler.
        son_zemin = Instantiate(son_zemin, son_zemin.transform.position + yon, son_zemin.transform.rotation);
    
    }
}
