using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float parallaxOffset = -0.15f;
    Camera cam;
    Vector2 startPos;
    Vector2 Travel => (Vector2)cam.transform.position - startPos; //we just made Travel into a property (it's like a function-variable hybrid. It changes everytime we use it)

    private void Awake() {
        cam = Camera.main;
    }

    private void Start() {
        startPos = transform.position;
    }

    private void FixedUpdate() {   //fixed update is great for moving things
      transform.position = startPos + Travel * parallaxOffset;
         
    }
}
