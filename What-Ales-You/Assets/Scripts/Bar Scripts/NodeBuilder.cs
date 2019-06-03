using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBuilder : MonoBehaviour
{
    public int xSize;
    public int zSize;
    public float nodeSize;
    public GameObject node;



    private void Awake()
    {
        

        for(int x = 0; x < xSize; x++)
        {
            for(int z = 0; z < zSize; z++)
            {
                Vector3 pos = this.transform.position + new Vector3(x, 0, z) * nodeSize;

                Instantiate(node, pos, Quaternion.identity, this.transform).GetComponent<PlaceNode>(); 
               
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                Vector3 pos = this.transform.position + new Vector3(x, 0, z) * nodeSize;

                Gizmos.DrawCube(pos, new Vector3(nodeSize, .01f, nodeSize));

            }
        }
    }
}
