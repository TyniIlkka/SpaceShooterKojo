using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
	public class PlayerSpaceShip : SpaceShipBase
	{
        public static LevelContoller current;

        [SerializeField, Tooltip("How many lives player have.")]
        public int _lives;
        [SerializeField]
        public float _respawnTimer = 0.5f;
        
        //Collider and Renderer are used to simulate dying.
        public CircleCollider2D coll;
        public Renderer rend;

        public const string HorizontalAxis = "Horizontal";
		public const string VerticalAxis = "Vertical";
		public const string FireButtonName = "Fire1";

		public override Type UnitType
		{
			get { return Type.Player; }
		}
        private void Start()
        {
            coll = GetComponent<CircleCollider2D>();
            rend = GetComponent<Renderer>();
            rend.enabled = true;
        }

        private Vector3 GetInputVector()
		{
			float horizontalInput = Input.GetAxis(HorizontalAxis);
			float verticalInput = Input.GetAxis(VerticalAxis);

			return new Vector3(horizontalInput, verticalInput);
		}

		protected override void Update()
		{
			base.Update();

			if(Input.GetButton(FireButtonName))
			{
				Shoot();
			}
		}

		protected override void Move()
		{
			Vector3 inputVector = GetInputVector();
			Vector2 movementVector = inputVector * Speed;
			transform.Translate(movementVector * Time.deltaTime);
		}
        protected override void Die()
        {
            rend.enabled = false;
            coll.enabled = false;
            StartCoroutine(Respawn(_respawnTimer));
     
        }

        protected IEnumerator Respawn(float respawnTimer)
        {
            yield return new WaitForSeconds(respawnTimer);
            if (_lives > 0)
            {
                rend.enabled = false;
                coll.enabled = false;
                transform.position = new Vector3(0, -4, 0);
                Health.IncreaseHealth(Health.InitialHealth);
                _lives--;              
                StartCoroutine(Invulnerability());
            }
            else
            {
                Debug.Log("Out of lives");
                SceneManager.LoadScene(0);

            }
        }

        protected IEnumerator Invulnerability()
        {
            coll.enabled = false;
            bool flashing = true;
            bool flashAlpha = true;
            int flashes = 0;
            while (flashing)
            {
                for (int i = 0; i < 10; i++)
                {
                    flashAlpha = !flashAlpha;
                    yield return new WaitForSeconds(0.04f);
                    rend.enabled = true;
                    yield return new WaitForSeconds(0.04f);
                    rend.enabled = false;
                
                    flashes++;
                    if (flashes >= 10)
                    {
                        flashing = false;
                    }
                }
            }
            rend.enabled = true;
            coll.enabled = true;
        }
    }
}
