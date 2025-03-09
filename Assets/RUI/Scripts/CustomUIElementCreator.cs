#if UNITY_EDITOR
using System.Diagnostics.CodeAnalysis;
using UnityEditor;
#endif

using UnityEngine;  // Добавляем импорт UnityEngine

public static class CustomUIElementCreator
{
    private const string prefabInputField = "Assets/RUI/InputField.prefab";
    private const string prefabDropdown = "Assets/RUI/Dropdown.prefab";
    private const string prefabFloatInputField = "Assets/RUI/FloatInputField.prefab";
    private const string prefabFloatDropdown = "Assets/RUI/FloatDropdown.prefab";
    private const string prefabDatePicker = "Assets/RUI/DatePicker.prefab";
    private const string prefabDeliter = "Assets/RUI/[deliter].prefab";

#if UNITY_EDITOR
    [MenuItem("GameObject/UI/RUI/InputField")]
    [SuppressMessage("NDepend", "ND1701:PotentiallyDeadMethods", Justification = "TODO")]
    private static void AddInputField(MenuCommand command)
    {
        AddUIPrefab(command, prefabInputField);
    }

    [MenuItem("GameObject/UI/RUI/Dropdown")]
    [SuppressMessage("NDepend", "ND1701:PotentiallyDeadMethods", Justification = "TODO")]
    private static void AddDropdown(MenuCommand command)
    {
        AddUIPrefab(command, prefabDropdown);
    }

    [MenuItem("GameObject/UI/RUI/FloatInputField")]
    [SuppressMessage("NDepend", "ND1701:PotentiallyDeadMethods", Justification = "TODO")]
    private static void AddFloatInputField(MenuCommand command)
    {
        AddUIPrefab(command, prefabFloatInputField);
    }

    [MenuItem("GameObject/UI/RUI/FloatDropdown")]
    [SuppressMessage("NDepend", "ND1701:PotentiallyDeadMethods", Justification = "TODO")]
    private static void AddFloatDropdown(MenuCommand command)
    {
        AddUIPrefab(command, prefabFloatDropdown);
    }

    [MenuItem("GameObject/UI/RUI/DatePicker")]
    [SuppressMessage("NDepend", "ND1701:PotentiallyDeadMethods", Justification = "TODO")]
    private static void AddDatePicker(MenuCommand command)
    {
        AddUIPrefab(command, prefabDatePicker);
    }

    [MenuItem("GameObject/UI/RUI/Deliter")]
    [SuppressMessage("NDepend", "ND1701:PotentiallyDeadMethods", Justification = "TODO")]
    private static void AddDeliter(MenuCommand command)
    {
        AddUIPrefab(command, prefabDeliter);
    }

    private static void AddUIPrefab(MenuCommand command, string path)
    {
        GameObject parent, prefab, instance;

        // Получение текущего активного объекта в иерархии
        parent = command.context as GameObject;

        // Загружаем префаб из ресурсов (если он находится там)
        prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

        // Если префаб найден, добавляем его на сцену
        if (prefab != null)
        {
            if (parent is not null)
            {
                instance = UnityEngine.Object.Instantiate(prefab, parent.transform);  // Используем Object.Instantiate
            }
            else
            {
                instance = UnityEngine.Object.Instantiate(prefab);  // Используем Object.Instantiate
            }

            instance.name = prefab.name;
        }
        else
        {
            Debug.LogError("UI Prefab not found!");
        }
    }
#endif
}