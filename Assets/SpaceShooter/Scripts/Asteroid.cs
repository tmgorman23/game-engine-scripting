using UnityEngine;

namespace SpaceShooter
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] float speed = 5f;

        void Update()
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);

            if (transform.position.y < -6)
            {
                ResetPosition();
            }

        }

        void ResetPosition()
        {
            transform.position = new Vector2(transform.position.x, 10);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            ResetPosition();
            if (other.tag == "Bullet")
            {
                ScoreManager.IncrementScore();

            }
        }
    }
}