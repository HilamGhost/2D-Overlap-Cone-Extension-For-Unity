/*```
⠀⠀⠀⠀⠀⢀⡠⠔⠂⠉⠉⠉⠉⠐⠦⡀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⢀⠔⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⡄⠀⠀⠀⠀⠀
⠀⠀⢠⠋⠀⠀⠀⠀⠖⠉⢳⠀⠀⢀⠔⢢⠸⠀⠀⠀⠀⠀
⠀⢠⠃⠀⠀⠀⠀⢸⠀⢀⠎⠀⠀⢸⠀⡸⠀⡇⠀⠀⠀⠀
⠀⡜⠀⠀⠀⠀⠀⠀⠉⠁⠾⠭⠕⠀⠉⠀⢸⠀⢠⢼⣱⠀
⠀⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡌⠀⠈⠉⠁⠀  hilam was here
⢸⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡇⠀⠀⣖⡏⡇  
⢸⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⢄⠀⠀⠈⠀
⢸⠀⢣⠀⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⡬⠇⠀⠀⠀
⠀⡄⠘⠒⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢣⠀⠀⠀⠀
⠀⢇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⡀⠀⠀⠀
⠀⠘⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡤⠁⠀⠀⠀
⠀⠀⠘⠦⣀⠀⢀⡠⣆⣀⣠⠼⢀⡀⠴⠄⠚⠀⠀⠀⠀⠀
```*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace HilamGhostPrototypes
{
    public class ConeExtensionExample : MonoBehaviour
    {
        [SerializeField] Transform originGO;
        [SerializeField] float maxDistance;
        [SerializeField,Range(0,120)] float angle;

        [SerializeField] float MouseAngle;

        [SerializeField] Collider2D[] pointedObjects;
        [SerializeField] Collider2D mostClosedObject;
        // Update is called once per frame
        void Update()
        {
            RotatePlayer();
            if (Input.GetKeyDown(KeyCode.A)) 
            {
                pointedObjects = originGO.OverlapConeAll(MouseAngle, angle, maxDistance);
                StartCoroutine(ChangeColliderColorsMultiple(pointedObjects));
            }
            if (Input.GetKeyDown(KeyCode.S)) 
            { 
                mostClosedObject = originGO.OverlapCone(MouseAngle, angle, maxDistance);
                StartCoroutine(ChangeCollidersColor(mostClosedObject));
            }

        }
        
        void RotatePlayer() 
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MouseAngle = Mathf.Atan2(mousePos.y,mousePos.x)*Mathf.Rad2Deg;

            transform.localRotation = Quaternion.Euler(0, 0, MouseAngle);

        }

        private void OnDrawGizmosSelected()
        {
            originGO.VisualizeConeWithGizmos(MouseAngle,angle,maxDistance,Color.white);
        }

        IEnumerator ChangeColliderColorsMultiple(Collider2D[] _colliders) 
        {
            for (int i = 0; i < _colliders.Length; i++)
            {
                if(_colliders[i].TryGetComponent(out SpriteRenderer spriteRenderer)) 
                {
                    spriteRenderer.color = Color.blue;
                }
            }
            yield return new WaitForSeconds(1);

            for (int i = 0; i < _colliders.Length; i++)
            {
                if (_colliders[i].TryGetComponent(out SpriteRenderer spriteRenderer))
                {
                    spriteRenderer.color = Color.red; 
                }
            }
        }
        IEnumerator ChangeCollidersColor(Collider2D _collider)
        {
            if (_collider == null) yield return null;
            else
            if (_collider.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.color = Color.blue;
                yield return new WaitForSeconds(1);
                spriteRenderer.color = Color.red;
            }
        }
    }
}
