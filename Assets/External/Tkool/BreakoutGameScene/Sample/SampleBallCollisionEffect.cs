
namespace Tkool.BreakoutGameScene.Sample
{
    class SampleBallCollisionEffect : IBallCollisionEffect
    {
        public bool DoErase { get; set; }

        public SampleBallCollisionEffect(bool doErase)
        {
            DoErase = doErase;
        }
    }
}
