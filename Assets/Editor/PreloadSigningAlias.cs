using UnityEditor;


[InitializeOnLoad]
public class PreloadSigningAlias
{

    static PreloadSigningAlias()
    {
        PlayerSettings.Android.keystorePass = "at0micdex";
        PlayerSettings.Android.keyaliasName = "fetzerspaceshooter";
        PlayerSettings.Android.keyaliasPass = "Djb!18Pusi";
    }

}
