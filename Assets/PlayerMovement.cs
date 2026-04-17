using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource[] audios;
    private bool isEndingGame;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audios = GetComponents<AudioSource>();
    }

    void FixedUpdate()
    {
        float movHorizontal = Input.GetAxis("Horizontal");
        float movVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(movHorizontal, movVertical);
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coletavel")
        {
            if (!other.gameObject.activeSelf)
            {
                return;
            }

            Collider2D otherCollider = other.GetComponent<Collider2D>();
            if (otherCollider != null)
            {
                otherCollider.enabled = false;
            }

            if (!GameControler.IsCollectableCountInitialized())
            {
                GameControler.SetCollectableCount(GameObject.FindGameObjectsWithTag("Coletavel").Length);
            }

            other.gameObject.SetActive(false);
            audios[0].Play();
            GameControler.Collect();
            Destroy(other.gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo") && !isEndingGame)
        {
            StartCoroutine(HandleEnemyCollision());
        }
    }

    private IEnumerator HandleEnemyCollision()
    {
        isEndingGame = true;
        rb.linearVelocity = Vector2.zero;
        enabled = false;

        if (audios.Length > 1 && audios[1] != null)
        {
            audios[1].Play();

            AudioClip clip = audios[1].clip;
            if (clip != null)
            {
                yield return new WaitForSeconds(clip.length);
            }
            else
            {
                yield return new WaitForSeconds(0.25f);
            }
        }

        GameControler.Enemy();
    }

}
