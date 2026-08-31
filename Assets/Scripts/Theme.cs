using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theme : MonoBehaviour
{
    [SerializeField] private List<GameObject> nodeList;
    [SerializeField] private float zoneSize;
    [SerializeField] private float maxNodePerLine;
    [SerializeField] private Vector2 nodeSpacing;
    [SerializeField] private float disableDuration;
    [SerializeField] private float enableDuration;

    private Coroutine enableThemeRoutine;
    //
    
    public void Initialise()
    {
        enableThemeRoutine = StartCoroutine(ActiveTheme());
    }

    [ContextMenu("Replace Nodes")]
    void ReplaceNodes()
    {
        int estimedColumn = Mathf.CeilToInt(nodeList.Count / maxNodePerLine);

        Vector3 startPos = transform.position - new Vector3(zoneSize / 2, 0, -zoneSize / 2);
        for (int c = 0; c < estimedColumn; c++)
        {
            
            for (int i = 0; i < maxNodePerLine; i++)
            {
                int nodeIndex = c * (int)maxNodePerLine + i;
                if (nodeIndex >= nodeList.Count) continue;
                
                Vector3 nodePos = startPos + new Vector3(nodeSpacing.x * i, 0, 0);
                nodeList[nodeIndex].transform.position = nodePos;
            }
            startPos.z -= nodeSpacing.y;
        }

    }

    public IEnumerator DisableTheme()
    {
        if(enableThemeRoutine != null)
        {
            StopCoroutine(enableThemeRoutine);
            enableThemeRoutine = null;
        }
        float nodeDisablingDuration = disableDuration / nodeList.Count;
        for (int i = 0; i < nodeList.Count; i++)
        {
            nodeList[i].SetActive(false);
            yield return new WaitForSeconds(nodeDisablingDuration);
        }
        yield return null;
    }

    IEnumerator ActiveTheme()
    {
        float nodeEnablingDuration = enableDuration / nodeList.Count;
        for (int i = 0; i < nodeList.Count; i++)
        {
            nodeList[i].SetActive(true);
            yield return new WaitForSeconds(nodeEnablingDuration);
        }
        yield return null;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.darkGreen;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one * zoneSize);
    }
}
