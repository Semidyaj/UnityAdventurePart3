namespace DZ_11
{
    public class AgentRotatableController : Controller
    {
        private AgentPlayer _agentPlayer;

        public AgentRotatableController(AgentPlayer agentPlayer)
        {
            _agentPlayer = agentPlayer;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            _agentPlayer.SetRotationDirection(_agentPlayer.CurrentVelocity);
        }
    }
}