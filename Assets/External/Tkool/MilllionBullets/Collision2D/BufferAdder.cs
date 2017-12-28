
namespace Tkool.MilllionBullets.Collision2D
{
    public class BufferAdder
    {
        private CommonData _common;

        public BufferAdder(CommonData common)
        {
            _common = common;
        }

        public void AddBuffer<TOption>(BulletsBuffer<TOption> bulletsBuffer) where TOption:struct 
        {
            _common.BulletsBuffers.Add(bulletsBuffer.Data.StatesBuffer);
        }
    }
}
