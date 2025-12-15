namespace Script.Behaviour
{
    using UnityEngine;

    public interface ITarget
    {
        public Transform Transform { get; }
        public Vector2 Position { get; }
    }
}