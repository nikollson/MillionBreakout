
using System;
using UnityEngine;

namespace MillionBreakout
{


    [CreateAssetMenu(menuName = "MillionBreakout/GridColorSetting", fileName = "GridColorSetting")]
    public class GridColorSetting : ScriptableObject
    {
        public Element[] ElementSettings;

        public Element GetNearSetting(Color c)
        {
            float mini = 0;
            Element ret = null;

            foreach (var element in ElementSettings)
            {
                var d = element.Color;
                float dist = Math.Abs(d.a - c.a) + Math.Abs(d.b - c.b) + Math.Abs(d.g - c.g);

                if (ret == null || mini > dist)
                {
                    mini = dist;
                    ret = element;
                }
            }

            return ret;
        }


        [Serializable]
        public class Element
        {
            public Color Color;
            public int HP;
        }
    }
}
