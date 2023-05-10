// 작성자 : 양세현
// 작성일 : 2022-10-31
// ntr : 박재만 야미
using UnityEngine;

/// <summary>
/// Interface for singleton properties.
/// <br/>
/// 싱글톤 interface.
/// </summary>
/// <typeparam name="T">
/// The type of component to singleton.
/// </typeparam>
public interface Singleton<T>
{
    /// <summary>
    /// The singleton instance.
    /// </summary>
    public static T m_Instance { get; set; }
    /// <summary>
    /// Handle for the instance.
    /// </summary>
    public static T Instance { get; }
}

/// <summary>
/// Generic singleton that all scene specific manager components inherit from.
/// i.e. GameManager, EventManager, etc.
/// <br/>
/// Scene과 관련된 싱글톤들이 inherit하는 class.
/// </summary>
/// <typeparam name="T">
/// The type of component to singleton.
/// </typeparam>
public class SceneSingleton<T> : MonoBehaviour, Singleton<T> where T : Component
{
    /// <summary>
    /// The singleton instance.
    /// </summary>
    private static T m_Instance = null;
    /// <summary>
    /// Handle for the instance.
    /// Return error if there is multiple instances, or if there is no SceneManager.
    /// Globally availible via SingletonName.Instance;
    /// </summary>
    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                // Very inefficient, but only run once, and there are only 3 scripts that call this, so it should be fine.
                var objs = FindObjectsOfType(typeof(T)) as T[];
                if (objs.Length == 1)
                {
                    m_Instance = objs[0];
                }
                else if (objs.Length > 1)
                {
                    // This should never run, since a singleton can only exist attached to a manager object.
                    Debug.LogError("There is more than one " + typeof(T) + " in the scene.");
                }
                else
                {
                    // This sometimes run when closing the application as destruction order is undefined.
                    //Debug.LogError("There is no " + typeof(T) + " in the scene.");
                    return null;
                }
                // Normally I would include this, but something weird with Unity's undefined gameobject initialization 
                // order causes this to trigger when loading Unity for the first time.
                /*else
                {
                    // This should never run, as there should always be one instance of a scenemanager object in a scene.
                    var obj = GameObject.FindGameObjectsWithTag("SceneManager");
                    Debug.Log(obj.Length);
                    if (obj.Length != 1)
                    {
                        Debug.LogError("Missing or multiple scene manager gameobjects.");
                    }
                    else
                    {
                        Debug.LogError("Something has gone VERY wrong. SceneManager object is missing " + typeof(T));
                    }
                }*/
            }
            return m_Instance;
        }
    }
}


/// <summary>
    /// Generic singleton that all global manager components inherit from.
    /// i.e. SeasonDatabase, etc.
/// <br/>
/// 게임 전체와 관련된 싱글톤들이 inherit하는 class.
/// </summary>
/// <typeparam name="T">
/// The type of component to singleton.
/// </typeparam>
public class GlobalSingleton<T> : MonoBehaviour, Singleton<T> where T : Component
{
    /// <summary>
    /// The singleton instance.
    /// </summary>
    private static T m_Instance = null;
    /// <summary>
    /// Handle for the instance.
    /// Return error if there is multiple instances, or if there is no SceneManager.
    /// Globally availible via SingletonName.Instance;
    /// </summary>
    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                var objs = FindObjectsOfType(typeof(T)) as T[];
                if (objs.Length == 1)
                {
                    m_Instance = objs[0];
                }
                else if (objs.Length > 1)
                {
                    // This should never run, as the only place manager components should be is attached to a manager gameobject.
                    Debug.LogError("There is more than one " + typeof(T) + " in the scene.");
                }
                else
                {
                    /*// This should never run, as there should always be only one instance of a global manager.
                    var obj = GameObject.FindGameObjectsWithTag("GlobalManager");
                    if (obj.Length != 1)
                    {
                        Debug.LogError("Missing or multiple global manager gameobjects.");
                    }
                    else
                    {
                        Debug.LogError("Something has gone VERY wrong. GlobalManager object is missing " + typeof(T));
                    }*/

                }
            }
            return m_Instance;
        }
    }

    protected virtual void Awake()
    {
        var objs = FindObjectsOfType(typeof(T)) as T[];

        if (objs.Length == 1)
        {
            m_Instance = objs[0];
            DontDestroyOnLoad(m_Instance.gameObject);
        }
        else if (objs.Length > 1)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i] != m_Instance)
                {
                    Destroy(objs[i].gameObject);
                }
            }
        }
        else
        {
            Debug.LogError("");
        }
    }
}