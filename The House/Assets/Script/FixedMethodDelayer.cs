namespace Script
{
    using System;
    using KarpysDev.KarpysUtils;
    using KarpysDev.KarpysUtils.MethodDelay;

    public class FixedMethodDelayer : SingletonMonoBehavior<FixedMethodDelayer>
    {
        private IMethodDelayer m_MethodDelayer = new PoolMethodDelayer(10);

        public void FixedUpdate()
        {
            m_MethodDelayer.Update();
        }

        public void AddMethod(Action method, float delay)
        {
            m_MethodDelayer.AddDelayMethod(method,delay);
        }
    }
}