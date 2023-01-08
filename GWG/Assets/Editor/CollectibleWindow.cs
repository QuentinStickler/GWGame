using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class CollectibleWindow : EditorWindow
{
   public GameObject collectibleObject;
   [MenuItem("Window/Collectible")]

   public static void ShowWindow()
   {
      GetWindow<CollectibleWindow>("Create Collectibles");
   }

   private void OnGUI()
   {
      GUILayout.Label("Create a collectible", EditorStyles.boldLabel);
      collectibleObject = (GameObject)EditorGUILayout.ObjectField("Collectible", collectibleObject, typeof(GameObject),false);
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      Color sceneColor = new Color();
      switch (currentSceneIndex)
      {
         case 0:
            sceneColor = Color.magenta;
            break;
         case 1:
            sceneColor = Color.yellow;
            break;
         case 2:
            sceneColor = Color.blue;
            break;
      }
      GUILayout.Label(currentSceneIndex.ToString(), EditorStyles.boldLabel);
      collectibleObject.GetComponent<Renderer>().sharedMaterial.color = sceneColor;

      if (GUILayout.Button("Instantiate Collectible"))
      {
         GameObject instantiatedObject = Instantiate(collectibleObject, new Vector3(0, 0, 0), Quaternion.identity);
         instantiatedObject.AddComponent<CollectibleBehaviour>();
      }
   }
}
