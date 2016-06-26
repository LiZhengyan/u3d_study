using UnityEngine;
using System.Collections;

/*
这个脚本定义了一个Mapdata类，。它只有一个fieldtype属性，用来表示单元格可放置状态
因为Mapdata类并非继承自MonoBehaviour，所以不可以在inspector中被编辑，所以需要添加Serializable
来保证它被序列话
*/

public class GridNode : MonoBehaviour {
	public MapData _mapData;

	void OnDrawGizmos(){
		Gizmos.DrawIcon(this.transform.position,"gridnode.tif");
	}

}
[System.Serializable]
public class MapData
{
	public enum FieldTypeID{
		GuardPosition,     //可以放置防守
		CanNotStand,
	}
	public FieldTypeID fieldtype=FieldTypeID.GuardPosition;
}
