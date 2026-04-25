using Source.Scripts.Interfaces;

namespace Source.Scripts.Other
{
    public class State : IEnterable, IExitable
    {
        public virtual void Enter() { }
        
        public virtual void Exit() { }
    }
}
