using UnityEditor;
using UnityEngine;

public class SpawnOnClick : EditorWindow
{
    [SerializeField] private GameObject prefabToSpawn;
    private bool isListening = false;

    [MenuItem("Tools/Spawn Prefab On Click")]
    public static void ShowWindow()
    {
        GetWindow<SpawnOnClick>("Spawn On Click");
    }

    private void OnGUI()
    {
        GUILayout.Label("Выберите префаб для спавна", EditorStyles.boldLabel);
        prefabToSpawn = EditorGUILayout.ObjectField("Префаб", prefabToSpawn, typeof(GameObject), false) as GameObject;

        if (GUILayout.Button(isListening ? "Остановить прослушивание" : "Начать прослушивание"))
        {
            isListening = !isListening;
            if (isListening)
                SceneView.duringSceneGui += OnSceneGUI;
            else
                SceneView.duringSceneGui -= OnSceneGUI;
        }
    }

    private void OnDestroy()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0 && prefabToSpawn != null)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Создаём объект из префаба на месте попадания
                GameObject spawned = (GameObject)PrefabUtility.InstantiatePrefab(prefabToSpawn);
                Undo.RegisterCreatedObjectUndo(spawned, "Создан объект");

                spawned.transform.position = hit.point;
                spawned.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                Debug.Log($"Объект создан в точке: {hit.point}");
                SceneView.RepaintAll();
                e.Use();
            }
            else
            {
                Debug.LogWarning("Рейкаст не попал ни во что.");
            }
        }
    }
}