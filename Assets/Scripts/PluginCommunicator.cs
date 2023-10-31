using UnityEngine;

public class PluginCommunicator : MonoBehaviour
{

    public AndroidJavaClass unityClass;
    public AndroidJavaObject currentActivity;
    public AndroidJavaObject _pluginInstance;


    public void CallInit()
    {
        InitializePlugin("com.cjhawk.unityapplication.TestApplication");
    }

    public void InitializePlugin(string pluginName)
    {
        unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        if (unityClass == null)
        {
            Debug.Log("Can't Find unityClass");
            return;
        }
        Debug.Log("1.Found Class");
        currentActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        if (currentActivity== null)
        {
            Debug.Log("Can't Find unityActivity");
            return;
        }
        Debug.Log("2.Found Activity");
        _pluginInstance = new AndroidJavaObject(pluginName);
        if (_pluginInstance != null)
        {
            Debug.Log($"2. Found Plugin Instance{pluginName}");
            _pluginInstance.CallStatic("receiveUnityAct",currentActivity);
        }
        else
            Debug.Log($"Can't Find Plugin Instance{pluginName}");
    }

    public void LaunchAtivity()
    {
        string activityClassName = "com.cjhawk.unitypluginmodule.TestActivity";
        AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent");

        intent.Call<AndroidJavaObject>("setClassName", currentActivity, activityClassName);
        currentActivity.Call("startActivity", intent);
    }

    public void Toast()
    {
        string msg = "Hello From Unity";
        Debug.Log(msg);
        _pluginInstance.Call("Toast", msg);
    }

}
