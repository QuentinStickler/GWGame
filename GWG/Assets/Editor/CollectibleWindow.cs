using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class CollectibleWindow : EditorWindow
{
   public GameObject collectibleObject;
   public GameObject UI;
   [MenuItem("Window/Collectible")]

   public static void ShowWindow()
   {
      GetWindow<CollectibleWindow>("Create Collectibles");
   }

   private void OnGUI()
   {
      GUILayout.Label("Create a collectible", EditorStyles.boldLabel);
      collectibleObject = (GameObject)EditorGUILayout.ObjectField("Collectible", collectibleObject, typeof(GameObject),false);
      UI = (GameObject)EditorGUILayout.ObjectField("UI", UI, typeof(GameObject),false);
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      Color sceneColor = new Color();
      switch (currentSceneIndex)
      {
         case 1:
            sceneColor = Color.magenta;
            break;
         case 2:
            sceneColor = Color.green;
            break;
         case 3:
            sceneColor = Color.yellow;
            break;
         case 4:
            sceneColor = Color.red;
            break;
      }
      GUILayout.Label(currentSceneIndex.ToString(), EditorStyles.boldLabel);
      collectibleObject.GetComponent<Renderer>().sharedMaterial.color = sceneColor;

      if (GUILayout.Button("Instantiate Collectible"))
      {
         GameObject parentObject = GameObject.Find("CollectibleParent");
         if (parentObject is null)
            parentObject = new GameObject("CollectibleParent");
         GameObject UIObject = GameObject.Find("UI");
         if (UIObject is null)
         {
            UIObject = Instantiate(UI);
            UIObject.name = "UI";
         }
         GameObject collectibleText = UIObject.transform.GetChild(1).gameObject;
         Debug.Log(collectibleText.GetComponent<TextMeshProUGUI>());
         collectibleText.GetComponent<TextMeshProUGUI>().color = sceneColor;
         GameObject instantiatedObject = Instantiate(collectibleObject, new Vector3(0, 0, 0), Quaternion.identity,parentObject.transform);
         instantiatedObject.AddComponent<CollectibleBehaviour>();
         instantiatedObject.GetComponent<CollectibleBehaviour>().collectibleText =
            collectibleText;
      }
   }
}
