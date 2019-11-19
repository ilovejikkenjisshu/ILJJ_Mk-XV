using UnityEngine;
using System.Collections;

// 以下で配布されているソース
// https://qiita.com/2dgames_jp/items/11bb76167fb44bb5af5f

public class Util {
    private static Rect _guiRect = new Rect();

    static Rect GetGUIRect()
    {
        return _guiRect;
    } 

    private static GUIStyle _guiStyle = null;

    static GUIStyle GetGUIStyle()
    {
      return _guiStyle ?? (_guiStyle = new GUIStyle());
    }

    public static void SetFontSize(int size)
    {
        GetGUIStyle().fontSize = size;
    }

    public static void SetFontColor(Color color)
    {
        GetGUIStyle().normal.textColor = color;
    }

    public static void SetFontAlignment(TextAnchor align)
    {
        GetGUIStyle().alignment = align;
    }

    public static void GUILabel(float x, float y, float w, float h, string text)
    {
        Rect rect = GetGUIRect();
        rect.x = x;
        rect.y = y;
        rect.width = w;
        rect.height = h;

        GUI.Label(rect, text, GetGUIStyle());
    }

    public static bool GUIButton(float x, float y, float w, float h, string text)
    {
        Rect rect = GetGUIRect();
        rect.x = x;
        rect.y = y;
        rect.width = w;
        rect.height = h;

        return GUI.Button(rect, text, GetGUIStyle());
    }
}
