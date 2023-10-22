// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;


// public class ScreenBounds : MonoBehaviour
// {
//     private Vector2 screenBound;

//     void Start()
//     {
//         screenBound = _Camera.main.ScreeToWorldPoint(new Vector3(screenBound.width, screenBound.height, Camera.main.transform.position));
//     }
//     void LateUpdate()
//     {
//         Vector3 viewPos = transform.position;
//         viewPos.x = Math.Clamp(viewPos.x, screenBound.x, screenBound.x *-1);
//         viewPos.y = Math.Clamp(viewPos.y, screenBound.y, screenBound.y *-1);
//         transform.position = viewPos;

//     }
// }
