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

namespace HilamGhostPrototypes
{
    public static class ConeOverlapExtension
    {
        #region Overlap Cone All (Without Layer)
        public static Collider2D[] OverlapConeAll(this Transform _origin, float _originRotation, float _coneAngle,float _maxDistance)
        {
            Vector2 _originPosition = _origin.position;
            
            float _upPointAngleRaw = _coneAngle / 2;
            Vector2 _upPoint = CreateConeSides(_origin, _originRotation, _upPointAngleRaw, _maxDistance);

            float _downPointAngleRaw = 360 - (_coneAngle / 2);
            Vector2 _downPoint = CreateConeSides(_origin, _originRotation, _downPointAngleRaw, _maxDistance);



            Collider2D[] _collidersInsideCircle = Physics2D.OverlapCircleAll(_originPosition, _maxDistance);
            List<Collider2D> _collidersInsideView = new List<Collider2D>();

            for (int i = 0; i < _collidersInsideCircle.Length; i++)
            {
                if (_collidersInsideCircle[i].TryGetComponent(out Transform _seenOBJ))
                {
                    var _seenObjectPosXRaw = _seenOBJ.position.x;
                    var _seenObjectPosYRaw = _seenOBJ.position.y;

                    var _seenObjectPosWithOriginPos = new Vector2(_seenObjectPosXRaw, _seenObjectPosYRaw) + _originPosition;

                    bool _isInsideField =
                        (Vector2.Angle(_upPoint, _seenObjectPosWithOriginPos) < _coneAngle &&
                         Vector2.Angle(_downPoint, _seenObjectPosWithOriginPos) < _coneAngle);

                    if (_isInsideField) _collidersInsideView.Add(_collidersInsideCircle[i]);
                }
            }
            return _collidersInsideView.ToArray();
        }
        #endregion
        #region Overlap Cone All (With Layer)
        public static Collider2D[] OverlapConeAll(this Transform _origin, float _originRotation, float _coneAngle, float _maxDistance,LayerMask layer)
        {
            Vector2 _originPosition = _origin.position;


            float _upPointAngleRaw = _coneAngle / 2;
            Vector2 _upPoint = CreateConeSides(_origin, _originRotation, _upPointAngleRaw, _maxDistance);

            float _downPointAngleRaw = 360 - (_coneAngle / 2);
            Vector2 _downPoint = CreateConeSides(_origin, _originRotation, _downPointAngleRaw, _maxDistance);



            Collider2D[] _collidersInsideCircle = Physics2D.OverlapCircleAll(_originPosition, _maxDistance,layer);
            List<Collider2D> _collidersInsideView = new List<Collider2D>();

            for (int i = 0; i < _collidersInsideCircle.Length; i++)
            {
                if (_collidersInsideCircle[i].TryGetComponent(out Transform _seenOBJ))
                {
                    var _seenObjectPosXRaw = _seenOBJ.position.x;
                    var _seenObjectPosYRaw = _seenOBJ.position.y;

                    var _seenObjectPosWithOriginPos = new Vector2(_seenObjectPosXRaw, _seenObjectPosYRaw) + _originPosition;

                    bool _isInsideField =
                        (Vector2.Angle(_upPoint, _seenObjectPosWithOriginPos) < _coneAngle &&
                         Vector2.Angle(_downPoint, _seenObjectPosWithOriginPos) < _coneAngle);

                    if (_isInsideField) _collidersInsideView.Add(_collidersInsideCircle[i]);
                }
            }
            return _collidersInsideView.ToArray();
        }
        #endregion

        #region Overlap Cone (Without Layer)
        public static Collider2D OverlapCone(this Transform _origin, float _originRotation, float _coneAngle, float _maxDistance)
        {
            Vector2 _originPosition = _origin.position;


            float _upPointAngleRaw = _coneAngle / 2;
            Vector2 _upPoint = CreateConeSides(_origin, _originRotation, _upPointAngleRaw, _maxDistance);

            float _downPointAngleRaw = 360 - (_coneAngle / 2);
            Vector2 _downPoint = CreateConeSides(_origin, _originRotation, _downPointAngleRaw, _maxDistance);



            Collider2D[] _collidersInsideCircle = Physics2D.OverlapCircleAll(_originPosition, _maxDistance);
            List<Collider2D> _collidersInsideView = new List<Collider2D>();
            Collider2D _colliderNearest = new Collider2D();

            for (int i = 0; i < _collidersInsideCircle.Length; i++)
            {
                if (_collidersInsideCircle[i].TryGetComponent(out Transform _seenOBJ))
                {
                    var _seenObjectPosXRaw = _seenOBJ.position.x;
                    var _seenObjectPosYRaw = _seenOBJ.position.y;

                    var _seenObjectPosWithOriginPos = new Vector2(_seenObjectPosXRaw, _seenObjectPosYRaw) + _originPosition;

                    bool _isInsideField =
                        (Vector2.Angle(_upPoint, _seenObjectPosWithOriginPos) < _coneAngle &&
                         Vector2.Angle(_downPoint, _seenObjectPosWithOriginPos) < _coneAngle);

                    if (_isInsideField) _collidersInsideView.Add(_collidersInsideCircle[i]);
                }
            }
            if (_collidersInsideView.Count <= 0) return null;
            else 
            {
                for (int i = 0; i < _collidersInsideView.Count; i++)
                {
                    if (_colliderNearest == null)
                    {
                        _colliderNearest = _collidersInsideView[i];
                    }
                    else 
                    {
                        if(Vector2.Distance(_originPosition, _collidersInsideView[i].gameObject.transform.position) <
                            Vector2.Distance(_originPosition, _colliderNearest.gameObject.transform.position))
                        {
                            _colliderNearest = _collidersInsideView[i];
                        }
                    }
                }
                return _colliderNearest;
            }
        }
        #endregion
        #region Overlap Cone (With Layer)
        public static Collider2D OverlapCone(this Transform _origin, float _originRotation, float _coneAngle, float _maxDistance, LayerMask layer)
        {
            Vector2 _originPosition = _origin.position;


            float _upPointAngleRaw = _coneAngle / 2;
            Vector2 _upPoint = CreateConeSides(_origin, _originRotation, _upPointAngleRaw, _maxDistance);

            float _downPointAngleRaw = 360 - (_coneAngle / 2);
            Vector2 _downPoint = CreateConeSides(_origin, _originRotation, _downPointAngleRaw, _maxDistance);



            Collider2D[] _collidersInsideCircle = Physics2D.OverlapCircleAll(_originPosition, _maxDistance);
            List<Collider2D> _collidersInsideView = new List<Collider2D>();
            Collider2D _colliderNearest = new Collider2D();

            for (int i = 0; i < _collidersInsideCircle.Length; i++)
            {
                if (_collidersInsideCircle[i].TryGetComponent(out Transform _seenOBJ))
                {
                    var _seenObjectPosXRaw = _seenOBJ.position.x;
                    var _seenObjectPosYRaw = _seenOBJ.position.y;

                    var _seenObjectPosWithOriginPos = new Vector2(_seenObjectPosXRaw, _seenObjectPosYRaw) + _originPosition;

                    bool _isInsideField =
                        (Vector2.Angle(_upPoint, _seenObjectPosWithOriginPos) < _coneAngle &&
                         Vector2.Angle(_downPoint, _seenObjectPosWithOriginPos) < _coneAngle);

                    if (_isInsideField) _collidersInsideView.Add(_collidersInsideCircle[i]);
                }
            }
            if (_collidersInsideView.Count <= 0) return null;
            else
            {
                for (int i = 0; i < _collidersInsideView.Count; i++)
                {
                    if (_colliderNearest == null)
                    {
                        _colliderNearest = _collidersInsideView[i];
                    }
                    else
                    {
                        if (Vector2.Distance(_originPosition, _collidersInsideView[i].gameObject.transform.position) <
                            Vector2.Distance(_originPosition, _colliderNearest.gameObject.transform.position))
                        {
                            _colliderNearest = _collidersInsideView[i];
                        }
                    }
                }
                return _colliderNearest;
            }
        }
        #endregion

        #region Visualize Cone With Gizmos
        public static void VisualizeConeWithGizmos(this Transform _origin, float _originRotation, float _coneAngle,float _maxDistance,Color gizmosColor) 
        {
            Vector2 _originPosition = _origin.position;

            float _midPointAngleRaw = 0;
            Vector2 _midPoint = CreateConeSides(_origin, _originRotation, _midPointAngleRaw, _maxDistance);

            float _upPointAngleRaw = _coneAngle / 2;
            Vector2 _upPoint = CreateConeSides(_origin, _originRotation, _upPointAngleRaw, _maxDistance);

            float _downPointAngleRaw = 360 - (_coneAngle / 2);
            Vector2 _downPoint = CreateConeSides(_origin, _originRotation, _downPointAngleRaw, _maxDistance);

            float _upMidPointAngleRaw = _coneAngle / 4;
            Vector2 _upMidPoint = CreateConeSides(_origin, _originRotation, _upMidPointAngleRaw, _maxDistance);

            float _downMidPointAngleRaw = 360 - (_coneAngle / 4);
            Vector2 _downMidPoint = CreateConeSides(_origin, _originRotation, _downMidPointAngleRaw, _maxDistance);


            Gizmos.color = gizmosColor;
            Gizmos.DrawLine(_upPoint, _upMidPoint);
            Gizmos.DrawLine(_upMidPoint, _midPoint);

            Gizmos.DrawLine(_downPoint, _downMidPoint);
            Gizmos.DrawLine(_downMidPoint, _midPoint);

            Gizmos.DrawLine(_originPosition, _upPoint);
            Gizmos.DrawLine(_originPosition, _downPoint);

            
            
        }
        #endregion
        


        /// <summary>
        /// Creates cone's side points by circles
        /// </summary>
        /// <param name="_origin">The source of the cone</param>
        /// <param name="_originRotation">Rotation of the cone </param>
        /// <param name="_coneAngle">Size Of The Cone</param>
        /// <param name="_maxDistance">Lenght Of Cone </param>
        /// <returns></returns>
        static Vector2 CreateConeSides(Transform _origin, float _originRotation, float _coneAngle, float _maxDistance) 
        {
            Vector2 _originPosition = _origin.position;


            float _pointAngle = _originRotation + _coneAngle;
            var _pointX = Mathf.Cos(_pointAngle * Mathf.Deg2Rad);
            var _pointY = Mathf.Sin(_pointAngle * Mathf.Deg2Rad);

            Vector2 _pointRaw = new Vector2(_pointX, _pointY);
            Vector2 _point = _originPosition + _pointRaw * _maxDistance;

            return _point;
        }
        
    }
}
