using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Scene_Event : MonoBehaviour
{
    public abstract IEnumerator ExecuteSceneCode();
}
