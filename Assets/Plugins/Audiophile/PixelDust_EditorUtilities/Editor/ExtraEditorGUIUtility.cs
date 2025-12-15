using UnityEditor;

public class ExtraEditorGUIUtility
{
    public static float SingleLineHeight()
    {
        return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
    }
}
