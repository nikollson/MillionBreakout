using MillionBreakout;
using Tkool.BreakoutGameScene;

public class TestBlock : BlockBehaviour
{
    

    public override IBlockCollisionEffect GetCollisionEffect()
    {
        return new BlockCollisionEffect();
    }
}
