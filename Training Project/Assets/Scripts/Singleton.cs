using UnityEngine;

/// <summary>
/// You can derive from this class to make an object that:
/// 1. will keep only one instance
/// 2. you can access through YourClassName.Instance
/// 3. will destroy self on awake if there is already an instance (see 1)
/// </summary>
/// <example>
/// // Derive like this:
/// public class YourClassName : Singleton<YourClassName> {
/// // Override Awake like this:
///     protected override void Awake() {
///         base.Awake();
///         //additional code
///     }
/// }
/// </example>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<T>();
                if (_instance == null)
                {
                    _instance = new GameObject(typeof(T) + " (singleton)").AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    /// <summary>
    /// If there is already an instance when this one is attempting
    /// to Awake, destroy self!
    /// </summary>
    protected virtual void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
    }
}


