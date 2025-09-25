using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    public Transform[] backgrounds; // gắn 3 background: left - mid - right
    public float scrollSpeed = 2f;  // tốc độ cuộn, nhỏ hơn ground để tạo parallax

    private float length;  // chiều rộng của 1 background tile
    private float leftEdge;

    void Start()
    {
        if (backgrounds.Length < 3)
        {
            Debug.LogError("Cần ít nhất 3 background để lặp!");
            return;
        }

        // Lấy width từ sprite renderer của tile đầu tiên
        SpriteRenderer sr = backgrounds[0].GetComponent<SpriteRenderer>();
        length = sr.bounds.size.x;

        // Mép trái màn hình
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
    }

    void Update()
    {
        foreach (Transform bg in backgrounds)
        {
            // Di chuyển background sang trái
            bg.position += Vector3.left * scrollSpeed * Time.deltaTime;

            // Nếu ra ngoài màn hình bên trái → dịch sang phải để nối tiếp
            if (bg.position.x < leftEdge - length)
            {
                // tìm background bên phải nhất
                Transform rightmost = backgrounds[0];
                foreach (Transform other in backgrounds)
                {
                    if (other.position.x > rightmost.position.x)
                    {
                        rightmost = other;
                    }
                }

                // đặt bg ngay sau background bên phải
                bg.position = rightmost.position + Vector3.right * length;
            }
        }
    }
}
