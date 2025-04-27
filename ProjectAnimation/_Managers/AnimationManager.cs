using ProjectAnimation._Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAnimation._Managers
{
    public class AnimationManager
    {
        private readonly Dictionary<string, Animation> _animations = [];
        private string _lastKey;

        public void AddAnimation(string keyword, Animation animation)
        {
            _animations.Add(keyword , animation);
            _lastKey = keyword;
        }

        public void Update(string keyword)
        {
            if (_animations.TryGetValue(keyword, out Animation value))
            {
                value.Start();
                _animations[keyword].Update();
                _lastKey = keyword;
            }
            else
            {
                _animations[_lastKey].Stop();
                _animations[_lastKey].Reset();
            }
        }

        public void SetAnimation(string keyword)
        {
            if (!_animations.ContainsKey(keyword)) return;

            if (keyword != _lastKey)
            {
                _animations[_lastKey].Stop();
                _animations[_lastKey].Reset();
                _lastKey = keyword;
            }
        }

        public void Draw(Vector2 position)
        {
            _animations[_lastKey].Draw(position);
        }
    }
}
