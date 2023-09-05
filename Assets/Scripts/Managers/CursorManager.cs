using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : GlobalSingleton<CursorManager>
{ 
    [SerializeField] private Sprite _cursorImage;
    private Texture2D _cursorTexture;

    int _cursorSizeX = 64;  // Your cursor size x
    int _cursorSizeY = 64;  // Your cursor size y
    private float _angle = 0.0f;
    private Vector3 _screenCenter;

    private Scene _scene;

    protected override void Awake()
    {
        base.Awake();
        _cursorTexture = ConvertSpriteToTexture(_cursorImage);
    }

    private void Start()
    {
        Cursor.visible = false;
        _screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);

        _scene = SceneManager.GetActiveScene(); //함수 안에 선언하여 사용한다.


        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) =>
        {
            _scene = SceneManager.GetActiveScene(); //함수 안에 선언하여 사용한다.
        };
    }

    public static Texture2D ConvertSpriteToTexture(Sprite sprite)
    {
        try
        {
            if (sprite.rect.width != sprite.texture.width)
            {
                int x = Mathf.FloorToInt(sprite.textureRect.x);
                int y = Mathf.FloorToInt(sprite.textureRect.y);
                int width = Mathf.FloorToInt(sprite.textureRect.width);
                int height = Mathf.FloorToInt(sprite.textureRect.height);

                Texture2D newText = new Texture2D(width, height);
                Color[] newColors = sprite.texture.GetPixels(x, y, width, height);

                newText.SetPixels(newColors);
                newText.Apply();
                return newText;
            }
            else
                return sprite.texture;
        }
        catch
        {
            return sprite.texture;
        }
    }

    
    private void OnGUI()
    {
        float x = Event.current.mousePosition.x - _cursorSizeX / 2.0f;
        float y = Event.current.mousePosition.y - _cursorSizeY / 2.0f;

        Vector2 pivot = new Vector2(x + _cursorSizeX / 2.0f, y + _cursorSizeY / 2.0f);

        // 회전을 적용하기 전에 GUI 행렬을 백업합니다.
        Matrix4x4 matrixBackup = GUI.matrix;

        if (_scene.name == "GameCycle" || _scene.name == "TutorialScene")
        {
            if (InventoryManager.Instance._isOnInventory == false)
            {
                Vector3 mousePosition = Input.mousePosition;
                Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);

                // 화면의 Y축은 상하반전 되어 있으므로 이를 보정합니다.
                mousePosition.y = Screen.height - mousePosition.y;

                // 화면 중앙에서 마우스 방향을 계산합니다.
                Vector2 direction = (mousePosition - screenCenter).normalized;

                // atan2를 사용하여 회전 각도를 라디안으로 계산합니다.
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 240f; // -90을 추가하여 텍스쳐가 올바른 방향을 가리키도록 조정합니다.

                // 텍스쳐의 중앙을 회전의 피벗으로 사용하고 텍스쳐를 회전시킵니다.
                GUIUtility.RotateAroundPivot(angle, pivot);
            }
        }

        // 회전된 위치에 텍스쳐를 그립니다.
        GUI.DrawTexture(new Rect(x, y, _cursorSizeX, _cursorSizeY), _cursorTexture);

        // GUI 행렬을 원래 상태로 복원합니다.
        GUI.matrix = matrixBackup;
    }
}
