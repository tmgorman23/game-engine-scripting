using maze;
using NotTheBees;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceShooter
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] float shipSpeed = 6f;

        [SerializeField] GameObject bulletPrefab;

        private ShipControlMappings shipControlMappings;

        InputAction m_Move;

        InputAction m_Fire;


        private void Awake()
        {

            shipControlMappings = new ShipControlMappings();

            m_Move = shipControlMappings.Player.Move;

            m_Fire = shipControlMappings.Player.Fire;

            m_Fire.performed += Fire;
        }

        private void OnEnable()
        {
            m_Move.Enable();

            m_Fire.Enable();
        }

        private void OnDisable()
        {
            m_Move.Disable();

            m_Fire.Disable();
        }

        void Update()
        {
            HandleMove();
        }

        void HandleMove()
        {
            //Call ReadValue on m_Move and pass in a Vector3 datatype instead of Vector3 like we've done before
            //      and store it in a temporary Vector3 called input variable
            Vector3 inputVariable = m_Move.ReadValue<Vector2>();
            float amt = shipSpeed * Time.deltaTime;
            Vector3 direction = Vector3.right;

            transform.Translate(inputVariable.x * amt * direction);
        }

        void Fire(InputAction.CallbackContext context)
        {
            GameObject ship = Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
        }
    }
}