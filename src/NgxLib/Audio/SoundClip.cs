using System;
using Microsoft.Xna.Framework.Audio;
using NgxLib.Serialization;

namespace NgxLib.Audio
{
    public class SoundBank : IDisposable
    {
        public bool Enabled = true;
        private const string PathTemplate = "Content/Sounds/{0}.wav";
        private readonly Index<SoundEffect> _effects = new Index<SoundEffect>();

        public SoundEffect Get(int handle)
        {
            if (!_effects.ContainsKey(handle))
            {
                return Load(handle);
            }
            return _effects[handle];
        }

        public SoundEffect Load(int handle)
        {
            var path = string.Format(PathTemplate, handle);
            var stream = Serializer.OpenReadStream(path);
            var snd = SoundEffect.FromStream(stream);
            _effects.Add(handle, snd);
            return snd;
        }

        public void Dispose()
        {
            foreach (var effect in _effects)
            {
                effect.Value.Dispose();
            }  
            _effects.Clear();
        }

        public void Play(int handle)
        {
            if (!Enabled) return;
            
            try
            {
                Get(handle).Play();                
            }
            catch (Exception ex)
            {
                Logger.Log("Audio system crash! -> " + ex.Message);
                Enabled = false;
            }
        }
    }
}
