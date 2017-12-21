namespace AI
{
    public interface State {
        void Init(EnemyAI ai);
        void Act(EnemyAI ai);
    }
}