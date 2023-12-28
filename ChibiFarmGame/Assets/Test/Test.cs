using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform playerTransform;  // Player objesinin transform bileşeni
    public float egilmeMiktari = 30f;  // Fidanın eğilme miktarı
    public float maxUzaklik = 5f;      // Maksimum uzaklık
    public float egilmeHizi = 5f;      // Eğilme hızı

    void Update()
    {
        // Player ile fidan arasındaki mesafeyi hesapla
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        // Eğer mesafe maksimum uzaklıktan küçükse fidanı eğ
        if (distanceToPlayer < maxUzaklik)
        {
            Egi();
        }
        else
        {
            // Mesafe maksimum uzaklıktan büyükse, fidanın rotasyonunu sıfırla
           // fidanTransform.rotation = Quaternion.identity;
           // Döndürme işlemi
           transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, egilmeHizi * Time.deltaTime);
        }
    }

    void Egi()
    {
        // Player'a doğru bir vektör oluştur
        Vector3 directionToPlayer = transform.position - playerTransform.position;

        // Player'a doğru eğilmiş rotasyonu hesapla
        Quaternion egilmişRotation = Quaternion.LookRotation(directionToPlayer) * Quaternion.Euler(egilmeMiktari, 0, 0);

        
        // Döndürme işlemi
        transform.rotation = Quaternion.Lerp(transform.rotation, egilmişRotation, egilmeHizi * Time.deltaTime);
    }
}
