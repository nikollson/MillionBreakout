
namespace Tkool.BreakoutGameScene.Sample
{
    public class SampleBallCollisionEffect : IBallCollisionEffect
    {
        public bool DoErase { get; set; }

        public SampleBallCollisionEffect(bool doErase)
        {
            DoErase = doErase;
        }
    }
}
