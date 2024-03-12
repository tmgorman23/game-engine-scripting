using Unity.VisualScripting;
using UnityEngine;

namespace SpaceShooter
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] float bulletSpeed = 5.0f;

        void Update()
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + bulletSpeed * Time.deltaTime);

            if (transform.position.y > 7)
            {
                GameObject.Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            GameObject.Destroy(gameObject);
        }
    }
}