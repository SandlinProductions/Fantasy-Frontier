using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(GeometryGrassPainter))]
public class GrassPainterEditor : Editor
{
    GeometryGrassPainter grassPainter;
    readonly string[] toolbarStrings = { "Add", "Remove", "Edit" };
    private string shortcutText;

    private void OnEnable()
    {
        grassPainter = (GeometryGrassPainter)target;

        shortcutText = "[ Use Tool ]  Modifier Key + Right Click\n";
        shortcutText += "[ Remove Grass ]  ALT + Left Click\n";
        shortcutText += "[ Switch Tool ]  Modifier Key + Middle Click\n";
        shortcutText += "[ Brush Size ]  CTRL + Scroll\n";
        shortcutText += "[ Density ]  ALT/SHIFT/COMMAND + Scroll\n";
        shortcutText += "[ Height Multiplier ]  CTRL + ALT/SHIFT/COMMAND + Scroll\n";
    }

    void OnSceneGUI()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(grassPainter.hitPosGizmo, grassPainter.hitNormal,
            grassPainter.brushSize);
        Handles.color = new Color(0, 0.5f, 0.5f, 0.4f);
        Handles.DrawSolidDisc(grassPainter.hitPosGizmo, grassPainter.hitNormal,
            grassPainter.brushSize);

        if (grassPainter.toolbarInt == 1)
        {
            Handles.color = Color.red;
            Handles.DrawWireDisc(grassPainter.hitPosGizmo, grassPainter.hitNormal,
                grassPainter.brushSize);
            Handles.color = new Color(0.5f, 0f, 0f, 0.4f);
            Handles.DrawSolidDisc(grassPainter.hitPosGizmo, grassPainter.hitNormal,
                grassPainter.brushSize);
        }

        if (grassPainter.toolbarInt == 2)
        {
            Handles.color = Color.yellow;
            Handles.DrawWireDisc(grassPainter.hitPosGizmo, grassPainter.hitNormal,
                grassPainter.brushSize);
            Handles.color = new Color(0.5f, 0.5f, 0f, 0.4f);
            Handles.DrawSolidDisc(grassPainter.hitPosGizmo, grassPainter.hitNormal,
                grassPainter.brushSize);
        }
    }

    public override void OnInspectorGUI()
    {
        float barOffset = 20f;

        // Grass Limit
        EditorGUILayout.LabelField("Grass Limit", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUI.ProgressBar(
            new Rect(barOffset, barOffset + 10, EditorGUIUtility.currentViewWidth - barOffset * 2,
                20f),
            (float)grassPainter.currentGrassAmount / grassPainter.grassLimit,
            grassPainter.currentGrassAmount.ToString() + " / " + grassPainter.grassLimit);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        grassPainter.grassLimit =
            EditorGUILayout.IntField("Max Grass Amount", grassPainter.grassLimit);
        EditorGUILayout.Space();

        // Paint Settings
        EditorGUILayout.LabelField("Paint Status", EditorStyles.boldLabel);
        grassPainter.toolbarInt = GUILayout.Toolbar(grassPainter.toolbarInt, toolbarStrings,
            GUI.skin.button, GUILayout.Height(25));
        EditorGUILayout.LabelField(new GUIContent("Shortcuts (hover here)", shortcutText));
        EditorGUILayout.Space();

        // Brush Settings
        EditorGUILayout.LabelField("Brush Settings", EditorStyles.boldLabel);
        LayerMask tempMask = EditorGUILayout.MaskField("Hit Mask",
            InternalEditorUtility.LayerMaskToConcatenatedLayersMask(grassPainter.hitMask),
            InternalEditorUtility.layers);
        grassPainter.hitMask = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(tempMask);
        LayerMask tempMask2 = EditorGUILayout.MaskField("Painting Mask",
            InternalEditorUtility.LayerMaskToConcatenatedLayersMask(grassPainter.paintMask),
            InternalEditorUtility.layers);
        grassPainter.paintMask = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(tempMask2);

        grassPainter.brushSize =
            EditorGUILayout.Slider("Brush Size", grassPainter.brushSize, 0.1f, 10f);
        grassPainter.density = EditorGUILayout.Slider("Density", grassPainter.density, 0.1f, 10f);
        grassPainter.normalLimit =
            EditorGUILayout.Slider("Normal Limit", grassPainter.normalLimit, 0f, 1f);
        EditorGUILayout.Space();

        // Grass Size
        EditorGUILayout.LabelField("Grass Size", EditorStyles.boldLabel);
        grassPainter.heightMultiplier =
            EditorGUILayout.Slider("Height Multiplier", grassPainter.heightMultiplier, 0f, 2f);

        grassPainter.widthMultiplier =
            EditorGUILayout.Slider("Width Multiplier", grassPainter.widthMultiplier, 0f, 2f);
        EditorGUILayout.Space();

        // Color
        EditorGUILayout.LabelField("Color", EditorStyles.boldLabel);
        grassPainter.adjustedColor =
            EditorGUILayout.ColorField("Brush Color", grassPainter.adjustedColor);
        grassPainter.rangeR =
            EditorGUILayout.Slider("Random Red", grassPainter.rangeR, 0f, 1f);
        grassPainter.rangeG =
            EditorGUILayout.Slider("Random Green", grassPainter.rangeG, 0f, 1f);
        grassPainter.rangeB =
            EditorGUILayout.Slider("Random Blue", grassPainter.rangeB, 0f, 1f);

        EditorGUILayout.Space();

        // Clear Button
        GUI.backgroundColor = new Color(252 / 255f, 142 / 255f, 134 / 255f);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Clear Mesh", GUILayout.Width(150), GUILayout.Height(25)))
        {
            if (EditorUtility.DisplayDialog("Clear Painted Mesh?",
                "Are you sure you want to clear the mesh?", "Clear", "Don't Clear"))
            {
                grassPainter.ClearMesh();
            }
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
}
// Updated Version of The Grass Paint Editor by MinionsArt
// 1. Some renames
// 2. Added shortcut hint
// 3. Updated UI (progress bar, etc)
// Source: https://pastebin.com/Y7dCRAd3
// Tutorial: https://www.patreon.com/posts/geometry-grass-46836032