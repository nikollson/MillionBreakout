
namespace Tkool.BreakoutGameScene.Sample
{
    public class SampleBlockCollisionEffect : IBlockCollisionEffect
    {
        public bool DoErase { get; set; }

        public SampleBlockCollisionEffect(bool doErase)
        {
            DoErase = doErase;
        }
    }
}
