using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

[Serializable]
public class GameData{
    public string sceneName;
}

public class DataManager : MonoBehaviour
{
    //Singleton instance de lui meme
    //Accessible de n'importe tout sans créer d'instance (ce que fait static)
    public static DataManager instance = null;
    public static string dataPath{
        get{return Application.persistentDataPath + "/gameSave.dat";}
    }

    public static bool SaveFileExists{
        //Est-ce que j'ai bien une sauvegarde qui existe ?
        get{return File.Exists(dataPath);}
    }

    private void awake(){
        if(instance == null)
        {
            //Comme ça elle point sur l'objet lui même
            instance = this;
            //J'y passe mon game object -> eli l'objet pour dire toi tu n'es pas détruit quand on load une nouvelle scene
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this){
            Destroy(gameObject);
        }
    }


    public static void Save(){
        BinaryFormatter bf = new BinaryFormatter();
        //Je crée un fichier (le create va écraser la donnée précédemment)
        FileStream file = File.Create(dataPath);
        GameData data = new GameData();

        data.sceneName = SceneManager.GetActiveScene().name;

        bf.Serialize(file, data);
        file.Close();
        Debug.Log($"Save file {dataPath} as been created.");

    }


    public static void Load(){
        if(!SaveFileExists) return;
        BinaryFormatter bf =new BinaryFormatter();
        FileStream file = File.Open(dataPath, FileMode.Open);
        GameData data = bf.Deserialize(file) as GameData;

        SceneManager.LoadScene(data.sceneName);
    }
}
