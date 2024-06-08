using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwordAttack : MonoBehaviour
{
    public GameObject damageTextPrefab; // Prefab for the damage text
    public Color flashColor = Color.red;
    private Color originalColor = Color.white;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;
    public Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default"); // or whatever sprite shader is being used
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DoDamage(float damage, float knockback, EnemyHealth enemy, Animator enemyAnim, bool isCritical)
    {
        enemy.health -= damage;
        enemyAnim.SetBool("isHit", true);

        if (enemy.getKnockback)
        {
            if (enemy.gameObject.GetComponent<Rigidbody2D>() == null)
            {
                Debug.LogWarning("NoRigidbody2d");
            }
            else
            {
                Rigidbody2D rb = enemy.gameObject.GetComponent<Rigidbody2D>();
                Vector2 attackerPosition = new Vector2(Player.position.x, Player.position.y);
                GetKnockback(rb, knockback, attackerPosition);
            }
            
        }
        // Flashing effect
        StartCoroutine(FlashEnemy(0.1f, enemy));

        if (isCritical == false)
        {
            DisplayDamageText(damage, enemy, false);
        }
        else
        {
            DisplayDamageText(damage, enemy, true);
        }
       

        yield return new WaitForSeconds(0.01f);
        enemyAnim.SetBool("isHit", false);

        //add knockback
    }

    private void GetKnockback(Rigidbody2D rb, float knockback, Vector2 attackPosition)
    {
        // Calculate the horizontal direction of the knockback
        Vector2 knockbackDirection = (rb.transform.position - new Vector3(attackPosition.x, attackPosition.y)).normalized;

        // Add an upward component to the knockback direction
        // Adjust the 'upwardForce' value to control how much the enemy is lifted
        float upwardForce = 0.5f; // This value can be adjusted based on your game's needs
        knockbackDirection += Vector2.up * upwardForce;
        knockbackDirection.Normalize(); // Ensure the direction vector has a magnitude of 1

        // Apply the knockback force with the added upward component
        rb.AddForce(knockbackDirection * knockback, ForceMode2D.Impulse);
    }

    private IEnumerator FlashEnemy(float duration, EnemyHealth enemy)
    {

        SpriteRenderer enemySpriteRenderer = enemy.gameObject.GetComponent<SpriteRenderer>();

        // Store the original color
        Color originalColor = enemySpriteRenderer.color;
        enemySpriteRenderer.material.shader = shaderGUItext;
        enemySpriteRenderer.color = flashColor;
  
        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Revert the sprite renderer's color back to the original color
        enemySpriteRenderer.material.shader = shaderSpritesDefault;
        enemySpriteRenderer.color = originalColor;
    }

    private void DisplayDamageText(float damage, EnemyHealth enemy,bool isCritical)
    {
        // Create a canvas
        GameObject canvasObject = new GameObject("DamageCanvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;

        // Set canvas position to the position of the enemy
        canvas.transform.position = enemy.gameObject.transform.position + Vector3.up * 1.5f; // Adjust height as needed

        // Add TextMeshPro text to the canvas
        GameObject textObject = new GameObject("DamageText");
        textObject.transform.SetParent(canvas.transform);
        TextMeshPro text = textObject.AddComponent<TextMeshPro>();
        text.text = damage.ToString();
        if (isCritical)
        {
            text.color = Color.red;
        }
        
        text.fontSize = 10; // Adjust font size as needed
        text.alignment = TextAlignmentOptions.Center;
        text.autoSizeTextContainer = true;
        // Set text position to the center of the canvas
        text.rectTransform.localPosition = Vector3.zero;
        // Start coroutine to gradually fade and move the text
        StartCoroutine(FadeAndMoveText(canvasObject, text, damage));
    }
    private IEnumerator FadeAndMoveText(GameObject canvasObject, TextMeshPro text, float damage)
    {
        Color textColor = text.color;
        float fadeDuration = 1.0f; // Time taken for the text to fade out
        float moveDuration = 1.0f; // Time taken for the text to move down
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            textColor.a = alpha;
            text.color = textColor;

            // Move text downwards
            text.rectTransform.localPosition -= Vector3.up * (Time.deltaTime / moveDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Destroy text and its canvas
        Destroy(text.gameObject);
        Destroy(canvasObject);
    }

}
