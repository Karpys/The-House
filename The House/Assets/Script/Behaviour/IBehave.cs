namespace Script.Behaviour
{
    using UnityEngine;

    public interface IBehave
    {
        public void Behave();
    }

    public interface IController
    {
        public void Move(Vector2 position);
        public void MoveTowards(Vector2 position);
        public void MoveForward();
    }
}