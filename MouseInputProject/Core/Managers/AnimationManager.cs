namespace MouseInputProject
{
    public class AnimationManager
    {
        public Dictionary<string, Animation> Animations = [];
        public string LastKey; 


        public void AddAnimation(string key, Animation animation)
        {
            Animations.Add(key, animation);
            LastKey = key;
        }


        public void Update(string key)
        {
            foreach (var pair in Animations)
            {
                if (pair.Key != key)
                {
                    pair.Value.Stop();
                    pair.Value.Reset();
                }
            }

            if (Animations.TryGetValue(key, out var anim))
            {
                anim.Start();
                anim.Update();
                LastKey = key;
            }
        }


        public void Draw(Vector2 position, SpriteEffects flip = SpriteEffects.None)
        {
            Animations[LastKey].Draw(position, flip);
        }

    }
}

