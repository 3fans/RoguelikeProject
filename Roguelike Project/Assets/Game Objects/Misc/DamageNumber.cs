using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour, ITextDisplay
{
    private TextMeshPro TextMeshPro;
    private Rigidbody2D RB;

    private float textTime = 1f;
    public float timer = 0;
    private void Awake()
    {
        TextMeshPro = GetComponent<TextMeshPro>();
        RB = GetComponent<Rigidbody2D>();
    }
    public void SetColor(Color color)
    {
        TextMeshPro.color = color;
    }

    public void SetText(string text)
    {
        TextMeshPro.text = text;
    }
    private void Start()
    {
        RB.velocity = Vector2.down;
        timer = textTime;

    }
    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
