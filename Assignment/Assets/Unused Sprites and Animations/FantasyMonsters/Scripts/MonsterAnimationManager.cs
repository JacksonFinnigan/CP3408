using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.FantasyMonsters.Scripts
{
    /// <summary>
    /// Play monster animations
    /// </summary>
    public class MonsterAnimationManager : MonoBehaviour
    {
        public GameObject Placement;

        [Header("UI")]
        public Text ClipName;

        private readonly List<string> _animationClips = new List<string> { "Stand", "Move", "Attack", "Die" };
        private int _animationClipIndex;

        /// <summary>
        /// Called automatically on app start
        /// </summary>
        public void Start()
        {
            Reset();
        }

        /// <summary>
        /// Reset animation on start and weapon type change
        /// </summary>
        public void Reset()
        {
            PlayAnimation(0);
        }

        /// <summary>
        /// Change animation and play it
        /// </summary>
        /// <param name="direction">Pass 1 or -1 value to play forward / reverse</param>
        public void PlayAnimation(int direction)
        {
            _animationClipIndex += direction;

            if (_animationClipIndex < 0)
            {
                _animationClipIndex = _animationClips.Count - 1;
            }

            if (_animationClipIndex >= _animationClips.Count)
            {
                _animationClipIndex = 0;
            }

            var clipName = _animationClips[_animationClipIndex];
            var animator = Placement.GetComponentInChildren<Animator>();

            if (animator.HasState(0, Animator.StringToHash(clipName)))
            {
                animator.Play(clipName);
                ClipName.text = clipName;
            }
            else
            {
                ClipName.text = "-";
            }
        }
    }
}