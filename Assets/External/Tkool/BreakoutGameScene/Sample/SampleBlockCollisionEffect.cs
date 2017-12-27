
namespace Tkool.BreakoutGameScene.Sample
{
    class SampleBlockCollisionEffect : IBlockCollisionEffect
    {
        public bool DoErase { get; set; }

        public SampleBlockCollisionEffect(bool doErase)
        {
            DoErase = doErase;
        }
    }
}
