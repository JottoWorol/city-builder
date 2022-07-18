using UnityEngine;

namespace Core.Character
{
    public class CharacterAnimation
    {
        private readonly Animator _animator;
        private static readonly int RunTop = Animator.StringToHash("run_top");
        private static readonly int RunBottom = Animator.StringToHash("run_bottom");

        public CharacterAnimation(Animator animator)
        {
            _animator = animator;
        }
        
        public void PlayRunTop()
        {
            _animator.SetTrigger(RunTop);
        }
        
        public void PlayRunBottom()
        {
            _animator.SetTrigger(RunBottom);
        }
    }
}