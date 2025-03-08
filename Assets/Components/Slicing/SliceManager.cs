using System.Collections.Generic;
using UnityEngine;

public class SliceManager : MonoBehaviour
{
    public bool sliceEnabled = false;
    public LayerMask sliceableLayer;
    public GameObject spritePrefab;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && sliceEnabled)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, float.PositiveInfinity, sliceableLayer);

            if (hit.collider != null)
            {
                SliceObject(hit.collider.gameObject, worldPoint);
            }
        }
    }

    void SliceObject(GameObject obj, Vector2 slicePoint)
    {
        Sliceable sliceable = obj.GetComponent<Sliceable>();
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) return;

        Bounds bounds = sr.bounds;

        // Kesme çizgisini belirle
        float sliceX = slicePoint.x - obj.transform.position.x;
        float halfWidth = bounds.size.x / 2f;
        
        if (sliceable.IsMain)
        {
            sliceable.size = sr.sprite.rect;
            sliceable.originalPosition = Vector2.zero;
        }

        // Sol ve sağ parçalar için yeni GameObject oluştur
        GameObject leftPart = Instantiate(spritePrefab, obj.transform.position, Quaternion.identity);
        GameObject rightPart = Instantiate(spritePrefab, obj.transform.position, Quaternion.identity);
        leftPart.transform.localScale = obj.transform.localScale;
        rightPart.transform.localScale = obj.transform.localScale;

        // Sol ve sağ parçaların SpriteRenderer'larını al
        SpriteRenderer leftSR = leftPart.GetComponent<SpriteRenderer>();
        SpriteRenderer rightSR = rightPart.GetComponent<SpriteRenderer>();

        Sliceable leftSC = leftPart.GetComponent<Sliceable>();
        Sliceable rightSC = rightPart.GetComponent<Sliceable>();
        leftSC.IsMain = false;
        rightSC.IsMain = false;

        // Sprite'ı iki parçaya bölen Rect değerlerini ayarla
        Rect leftRect = new Rect(sliceable.originalPosition.x, sliceable.originalPosition.y, sliceable.size.width * (sliceX + halfWidth) / bounds.size.x, sliceable.size.height);
        Rect rightRect = new Rect(leftRect.width + sliceable.originalPosition.x, sliceable.originalPosition.y, sliceable.size.width - leftRect.width, sliceable.size.height);

        leftSC.size = leftRect;
        leftSC.originalPosition = new Vector2(sliceable.originalPosition.x, sliceable.originalPosition.y);

        rightSC.size = rightRect;
        rightSC.originalPosition = new Vector2(leftRect.width + sliceable.originalPosition.x, sliceable.originalPosition.y);


        try
        {
            leftSR.sprite = Sprite.Create(sr.sprite.texture, leftRect, new Vector2(0.5f, 0.5f));
            rightSR.sprite = Sprite.Create(sr.sprite.texture, rightRect, new Vector2(0.5f, 0.5f));
        }
        catch (System.Exception e)
        {
            Destroy(leftPart);
            Destroy(rightPart);
            return;
        }

        // Parçaları doğru konuma kaydır
        float sliceXLocal = slicePoint.x - obj.transform.position.x;
        leftPart.transform.position = new Vector3(
            obj.transform.position.x + (sliceXLocal - halfWidth) / 2f,
            obj.transform.position.y,
            obj.transform.position.z
        );
        rightPart.transform.position = new Vector3(
            obj.transform.position.x + (sliceXLocal + halfWidth) / 2f,
            obj.transform.position.y,
            obj.transform.position.z
        );
        // Collider ekle
        leftPart.AddComponent<BoxCollider2D>();
        rightPart.AddComponent<BoxCollider2D>();

        // Orijinal objeyi yok et
        Destroy(obj);
    }
}
