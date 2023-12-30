using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CursorManager : MonoBehaviour
{
    
    Image cursorImage;

    private void Awake() {
        cursorImage = GetComponent<Image>();
    }
    void Start()
    {
        Cursor.visible = false;

        if (Application.isPlaying) { //Application.isPlaying is TRUE when we're playing through the Unity Editor. It's false if we're playing through some other application (so it will be false once we build the project)
            Cursor.lockState = CursorLockMode.None; //In the unity editor, our cursor is allowed to leave the gamescreen (makes it easier to work with unity while developing)
        } else {
            Cursor.lockState = CursorLockMode.Confined;  // In the final build, the cursor will not be able to leave the gamewindow.
        }
    }

    void Update()
    {
        Vector2 cursorPosition = Input.mousePosition;
        cursorImage.rectTransform.position = cursorPosition;

        // if (!Application.isPlaying) {return;} //This block of commented out code ensures that after you hit "escape" in the gameview window, you can still re-left-click on the gameview window and the old cursor will be replaced by the game cursor.
        // Cursor.visible = false;
    }
}
