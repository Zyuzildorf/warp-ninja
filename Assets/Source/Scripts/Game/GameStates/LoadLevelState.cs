namespace Source.Scripts.Game.GameStates
{
    public class LoadLevelState : GameState
    {
        //Экран загрузки. Загрузка уровня, сцены. Спавн врагов и игрока
        
        public LoadLevelState(Main main, GameStateMachine stateMachine) : base(main, stateMachine)
        {
        }
    }
}