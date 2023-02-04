using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public enum HitSurfaceType
    {
        Dirt = 0,
        Blood = 1,
    }

    [System.Serializable]
public class HitEffectMapper
{ 
    public HitSurfaceType surface;
    public GameObject effectPrefab;
}
public class HitEffectManager : Singleton<HitEffectManager>
{
    public HitEffectMapper[] effectMap;

    public GameObject GetEffectPrefab(HitSurfaceType SurfaceType)
    {
        HitEffectMapper mapper = System.Array.Find(effectMap, x => x.surface == SurfaceType);
        return mapper ?.effectPrefab;
    }
}
