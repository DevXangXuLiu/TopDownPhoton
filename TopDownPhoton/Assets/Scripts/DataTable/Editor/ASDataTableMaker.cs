#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

public class ASDataTableMaker : MonoBehaviour
{
    [MenuItem("Assets/Async Custom Tools/Make Data By CSV")]
    public static void CreateAsset()
    {
        foreach(UnityEngine.Object obj in Selection.objects)
        {
            TextAsset csvFile = (TextAsset)obj;
            string tableName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(csvFile));
            ScriptableObject scriptableObject = ScriptableObject.CreateInstance(tableName);
            if (scriptableObject == null)
                return;

            AssetDatabase.CreateAsset(scriptableObject, "Assets/Resources/DataTable/" + tableName + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            ASDataTableOrigin asDataTableOrigin = (ASDataTableOrigin)scriptableObject;
            asDataTableOrigin.ImportData(csvFile.text);
            EditorUtility.SetDirty(asDataTableOrigin);
        }
    }

    [MenuItem("Assets/Async Custom Tools/Create CSV File from ScriptableObject (Binary File)", false, 1)]
    private static void CreateCSVFile()
    {
        foreach(UnityEngine.Object obj in Selection.objects)
        {
            ASDataTableOrigin dataFile = (ASDataTableOrigin)obj;
            string tableName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(dataFile));
            string data = dataFile.GetCSVData();
            string filePath = "Assets/Data/DataTable/" + tableName + ".csv";
            FileUtil.DeleteFileOrDirectory(filePath);

            FileStream fs = new FileStream(filePath, FileMode.Create);
            StreamWriter sWriter = new StreamWriter(fs);
            sWriter.Write(data);
            sWriter.Flush();
            fs.Close();
            AssetDatabase.Refresh();
        }
    }
}

#endif
