using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DestSelector : MonoBehaviour, IPointerClickHandler
{
    public TreeNode<Square> dest { get; set; }
    public UnityEvent<TreeNode<Square>> OnDestSelected { get; set; }

    public void OnPointerClick(PointerEventData pointerData){
        OnDestSelected.Invoke(dest);
	}
}
