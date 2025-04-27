using ProjectAnimation._Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAnimation._Managers
{
    public class GameManager
    {
        private Ball _ball;

        public void Init()
        {
            _ball = new(new(300, 300));
        }

        public void Update()
        {
            _ball.Update();
        }

        public void Draw()
        {
            _ball.Draw();
        }

    }
}
