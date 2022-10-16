using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Vector3 yon; //Playerın yönünü belirleyecek işlem

    [SerializeField]
    float hiz;

    public Zemin_Olusturma zemin_olusturr;
    
    public static bool dustuMu = true; //topun düşmesini kontrol eder.

    public float hizlanma_zorlugu;

    float skor = 0f, artisMiktari = 1f;

    [SerializeField]
    Text skorText, bestScoreText, scoreText;

    int enIyiSkor;

    public Text enIyiSkorText;

    public GameObject restartGamePanel, playGamePanel;



    void Start()
    {
        bestScoreText.text = "Best Score: "+PlayerPrefs.GetInt("enIyiSkor").ToString();

        if(RestartGame.isRestart == true)
        {
            playGamePanel.SetActive(false);
            dustuMu = false;
            skorText.gameObject.SetActive(true);
            enIyiSkorText.gameObject.SetActive(true);
            
        }
        
        yon = Vector3.left; //oyun başlar başlamaz düz gitsin

        enIyiSkor = PlayerPrefs.GetInt("enIyiSkor");

        enIyiSkorText.text = "Best Score: "+ enIyiSkor.ToString();

    }
    public void startGame()
    {
        dustuMu = false;
        playGamePanel.SetActive(false);
        skorText.gameObject.SetActive(true);
        enIyiSkorText.gameObject.SetActive(true);
    }
 
    void Update()
    {
        if(dustuMu)
        {
            return;//Eğer şart sağlanıyorsa altındaki hiç bir komut çalışmaz.
        }

        if(Input.GetMouseButtonDown(0)) 
        {
            if(yon.z == 0) //düz gitmek
            {
                yon = Vector3.back; //sağa gitmesini sağlar
            }
            else
            {
                yon = Vector3.left; //düz gitmesini sağlar
            }
        }
        if(transform.position.y < 0.65f)
        {
            dustuMu = true;

            restartGamePanel.SetActive(true);

            Destroy(gameObject, 1f);

            skorText.gameObject.SetActive(false);

            enIyiSkorText.gameObject.SetActive(false);

            scoreText.text = "Score: "+((int)skor).ToString();

            if(enIyiSkor < skor)
            {
                enIyiSkor = (int)skor;
                PlayerPrefs.SetInt("enIyiSkor", enIyiSkor);
                PlayerPrefs.Save();
            }
        }
    }

    private void FixedUpdate()
    {
        if(dustuMu) // duserken puan artmasın diye
        {
            return;
        }

        Vector3 hareket = yon * hiz * Time.deltaTime; //player için pozisyon oluşturduk
        
        transform.position += hareket;  //hareketi sürekli playerın pozisyonuna ekler yani sürekli hareket eder
        
        hiz += hizlanma_zorlugu * Time.deltaTime; 
        
        skor += artisMiktari * hiz * Time.deltaTime;

        skorText.text = "Skor: " + ((int)skor).ToString();

    }

    //player zemini terk etiiğinde çalışır.
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("zemin"))
        {
            StartCoroutine(yokEt(collision.gameObject));
            zemin_olusturr.zemin_Olustur();
        }
    }

    IEnumerator yokEt(GameObject obje)
    {
        yield return new WaitForSeconds(0.3f);
        obje.AddComponent<Rigidbody>();
        yield return new WaitForSeconds(1f);
        Destroy(obje);

    }
}
