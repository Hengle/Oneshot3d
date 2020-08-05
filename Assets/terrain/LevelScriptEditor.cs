using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UIElements;
using System.Linq;
//Made by A Blueberry#3141
[CustomEditor(typeof(LevelScript))]
public class LevelScriptEditor : Editor
{
    private bool mousedown;
    private void OnEnable()
    {
        if (!Application.isEditor)
        {
            Destroy(this);
        }
        SceneView.duringSceneGui += OnScene;
    }
    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnScene;
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelScript myScript = (LevelScript)target;
        int uielements = 0;
        while (uielements < myScript.obj.Length)
        {
            if (GUILayout.Button(myScript.obj[uielements].name))
            {
                myScript.whichobject = uielements;
                myScript.israndom = false;
            }
            uielements += 1;
        }
        if (GUILayout.Button("Random Pool"))
        {
            myScript.israndom = true;
        }
        uielements += 1;
    }
    void OnScene(SceneView scene)
    {

        LevelScript myScript = (LevelScript)target;

        Event e = Event.current;

        Vector3 mousePos = e.mousePosition;
        float ppp = EditorGUIUtility.pixelsPerPoint;
        mousePos.y = scene.camera.pixelHeight - mousePos.y * ppp;
        mousePos.x *= ppp;

        Ray ray = scene.camera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 holeAdjust = hit.point + hit.normal / 2f;
            Vector3 cubedpos = new Vector3(Mathf.Round(holeAdjust.x), Mathf.Round(holeAdjust.y), Mathf.Round(holeAdjust.z));
            myScript.spawnPoint = cubedpos;

            if (e.button == 0 && e.type == EventType.MouseDown)
            {
                if (hit.transform.gameObject.transform.position == cubedpos)
                    DestroyImmediate(hit.transform.gameObject);
                myScript.BuildObject();
            }
            if (e.button == 2 && e.type == EventType.MouseDown)
            {
                //you need to add a custom tag called "Cubes" and assign it to objects you want to break with middle click
                if (!myScript.obj.Contains(hit.transform.gameObject) && hit.transform.gameObject.tag == "Cubes")
                    DestroyImmediate(hit.transform.gameObject);
            }

        }

    }

}