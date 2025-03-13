using System.Collections;
using UnityEngine;

public class guard : MonoBehaviour
{
    public GameObject player;
    public GameObject Guard;
    public float fadeInTime = 0.3f;
    public float stayTime = 5.2f;
    public float flickerDuration = 2f; // Total duration of flickering
    public float flickerInterval = 0.1f; // Interval between flickers

    private SpriteRenderer spriteRenderer;
    private Player p;
    private Coroutine shieldCoroutine;

    private void Start()
    {
        p = player.GetComponent<Player>();
        spriteRenderer = Guard.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
    }

    public void ActivateShield()
    {
        if (shieldCoroutine != null)
        {
            StopCoroutine(shieldCoroutine);
        }

        shieldCoroutine = StartCoroutine(ShieldEvent());
    }

    private IEnumerator ShieldEvent()
    {
        p.SetInvincibility(true);

        // If fadeInTime is greater than 0, perform fade-in
        if (fadeInTime > 0f)
        {
            yield return FadeSpriteRenderer(spriteRenderer, new Color(1f, 1f, 1f, 0.9f), fadeInTime);
        }

        // Stay visible
        yield return new WaitForSeconds(stayTime);

        // Flicker
        float flickerTimeElapsed = 0f;
        while (flickerTimeElapsed < flickerDuration)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(flickerInterval);
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.8f);
            yield return new WaitForSeconds(flickerInterval);
            flickerTimeElapsed += flickerInterval * 2;
        }

        // Flicker out
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        yield return null;
        p.SetInvincibility(false);
    }

    private IEnumerator FadeSpriteRenderer(SpriteRenderer renderer, Color targetColor, float duration)
    {
        Color startColor = renderer.color;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            renderer.color = Color.Lerp(startColor, targetColor, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        renderer.color = targetColor;
    }
}