using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
/// <summary>
/// ItemData용 커스텀 에디터 작성
/// </summary>
[CustomEditor(typeof(ItemData),true)]
public class ItemDataEditor : Editor
{
    ItemData itemData;

    private void OnEnable()
    {
        //target 은 에디터에서 선택한 오브젝트
        // ItemData로 캐스팅을 시도한 후 성공하면 반환 아니면 null
        itemData = target as ItemData;
    }

    public override void OnInspectorGUI()
    {
        if (itemData != null)
        {
            if (itemData.itemIcon != null)
            {
                EditorGUILayout.LabelField("Item Icon Preview");    //라벨 필드에 제목 출력
                // itemData.itemIcon 의 이미지를 텍스처에 저장
                Texture2D texture = AssetPreview.GetAssetPreview(itemData.itemIcon);   
                if (texture != null)
                {
                    GUILayout.Label("", GUILayout.Height(64), GUILayout.Width(64));
                    GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
                }
            }
        }
        base.OnInspectorGUI();  //기본적으로 인스펙터에서 표시되는 것들
    }
}
#endif