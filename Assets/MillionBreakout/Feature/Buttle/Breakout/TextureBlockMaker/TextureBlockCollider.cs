
using Tkool.BreakoutGameScene;
using UnityEngine;

namespace MillionBreakout
{
    public class TextureBlockCollider : BreakoutGridBlockCollider
    {
        public GridColorSetting ColorSetting;

        public Texture2D Texture;

        public int[,] HP;

        public override bool[,] GetInitialEnableArray()
        {
            var pixels = Texture.GetPixels();
            int h = Texture.height;
            int w = Texture.width;

            var ret = new bool[h, w];
            HP = new int[h, w];


            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    Color c = pixels[i * w + j];
                    var setting = ColorSetting.GetNearSetting(c);

                    if (setting.HP != 0)
                    {
                        HP[i, j] = setting.HP;
                        ret[i, j] = true;
                    }
                }
            }

            return ret;
        }

        public void AddDamage(int arrayX, int arrayY, int damage)
        {
            HP[arrayY, arrayX] -= Mathf.Min(HP[arrayY, arrayX], damage);

            if (HP[arrayY, arrayX] <= 0)
            {
                EnableArray[arrayY, arrayX] = false;
            }
        }
    }
}
