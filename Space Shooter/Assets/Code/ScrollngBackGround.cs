using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{

    public class ScrollngBackGround : MonoBehaviour
    {
        private Sprite _sprite;
        private float _yScale;

        [SerializeField]
        private float _speed = 1;
    
        SpriteRenderer sr;

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            _sprite = sr.sprite;
            _yScale = transform.localScale.y;
        }

        // Update is called once per frame
        void Update()
        {
            if(transform.position.y <= -30)
            {
                Vector3 localPosition = transform.localPosition;
                float spriteY = (_sprite.bounds.max.y - _sprite.bounds.min.y) * _yScale * 2;

                localPosition.y = spriteY;
                transform.localPosition = localPosition;

            }
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
    }
}
