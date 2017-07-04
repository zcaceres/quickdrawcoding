1. Get free western theme for gallery
2. Use DontDestroyOnLoad() to select language and difficulty from main menu

GameControl with data that I want.
Make it into a singleton:

```csharp
public class GameControl : MonoBehaviour {
  public static GameControl control;
  void Awake() {
    if (control == null) {
      DontDestroyOnLoad(gameObject);
      control = this;
    } else if (control != this) {
      Destroy(gameObject);
    }
  }
}

```

Put a prefab with this script on it into all scenes.
