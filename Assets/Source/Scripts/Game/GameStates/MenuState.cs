using Source.Scripts.Other;

namespace Source.Scripts.Game.GameStates
{
    public class MenuState : GameState
    {
        //UI менюшки. Выбор локации, уровня, настройки, покупка, кастомизация
        public MenuState(Main main, GameStateMachine stateMachine) : base(main, stateMachine)
        {
        }
    }
}