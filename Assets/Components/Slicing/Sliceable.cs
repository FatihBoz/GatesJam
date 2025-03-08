using UnityEngine;

public class Sliceable : MonoBehaviour
{
    public Vector2 originalPosition;
    public Rect size;
    public bool IsMain;

    private GameObject forOutline;
    private bool hovered = false;

    private void OnMouseEnter()
    {
        if (forOutline == null && !hovered)
        {
            // Outline objesini oluştur
            forOutline = Instantiate(gameObject, transform.position, Quaternion.identity);

            // Outline objesindeki Sliceable komponentini kaldır
            Destroy(forOutline.GetComponent<Sliceable>());

            // Collider'ları devre dışı bırak
            foreach (var collider in forOutline.GetComponents<Collider2D>())
            {
                collider.enabled = false;
            }

            // Görsel ayarlamalar
            SpriteRenderer sr = forOutline.GetComponent<SpriteRenderer>();
            sr.color = Color.red;
            GetComponent<SpriteRenderer>().sortingOrder = 3;
            sr.sortingOrder = 2;
            forOutline.transform.localScale = transform.localScale * 1.02f;

            hovered = true;
        }
    }

    private void OnMouseExit()
    {
        if (forOutline != null && hovered)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 0;
            Destroy(forOutline);
            hovered = false;
        }
    }

    // Oyun objesi yok edilirken outline'ı da temizle
    private void OnDestroy()
    {
        if (forOutline != null)
            Destroy(forOutline);
    }
}