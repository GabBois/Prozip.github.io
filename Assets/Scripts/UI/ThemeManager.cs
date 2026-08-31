using System;
using System.Collections;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    [SerializeField] private GameObject[] themePanelList;
    private Theme activeTheme;
    private Coroutine disableThemeRoutine;

    private void Start()
    {
        activeTheme = themePanelList[0].GetComponent<Theme>();
    }

    public void OpenTheme(int _index)
    {
        if (disableThemeRoutine != null)
        {
            StopCoroutine(disableThemeRoutine);
        }

        disableThemeRoutine = StartCoroutine(DisableThemeSequence(_index));

    }

    IEnumerator DisableThemeSequence(int _index)
    {
        if (activeTheme)
        {
            yield return activeTheme.DisableTheme();
        }
        themePanelList[_index].SetActive(true);
        themePanelList[_index].TryGetComponent(out activeTheme);
        activeTheme.Initialise();
    }
}
