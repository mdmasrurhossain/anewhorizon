using Microsoft.Xna.Framework.Audio;
using System;

namespace DhanmondiPong
{
    public class AudioSource
    {
        private int _sampleRate = 48000;
        private DynamicSoundEffectInstance _dsei;
        private byte[] _buffer;
        private int _bufferSize;
        private int _totalTime = 0;
        static Random Rand = new Random();

        public AudioSource()
        {
            _dsei = new DynamicSoundEffectInstance(_sampleRate, AudioChannels.Mono);
            _bufferSize = _dsei.GetSampleSizeInBytes(TimeSpan.FromMilliseconds(500));
            _buffer = new byte[_bufferSize];
            _dsei.Volume = 0.4f;
            _dsei.IsLooped = false ;
        }

        public void PlayWave(double freq, short durationInMilliseconds, WaveType waveType, float volume)
        {
            _dsei.Stop();

            _bufferSize = _dsei.GetSampleSizeInBytes(TimeSpan.FromMilliseconds(durationInMilliseconds));
            _buffer = new byte[_bufferSize];

            int size = _bufferSize - 1;

            for(int i = 0; i < size; i += 2)
            {
                double time = (double)_totalTime / (double)_sampleRate;

                short currentSample = 0;

                switch (waveType)
                {
                    case WaveType.Sin:
                        {
                            currentSample = (short)(Math.Sin(2 * Math.PI * freq * time) * (double)short.MaxValue * volume);
                            break;
                        }
                    case WaveType.Tan:
                        {
                            currentSample = (short)(Math.Tan(2 * Math.PI * freq * time) * (double)short.MaxValue * volume);
                            break;
                        }
                    case WaveType.Square:
                        {
                            currentSample = (short)(Math.Sign(Math.Sin(2 * Math.PI * freq * time)) * (double)short.MaxValue * volume);
                            break;
                        }
                    case WaveType.Noise:
                        {
                            currentSample = (short)(Rand.Next(-short.MaxValue, short.MaxValue) * volume);
                            break;
                        }
                }

                _buffer[i] = (byte)(currentSample & 0xFF);
                _buffer[i + 1] = (byte)(currentSample >> 8);
                _totalTime += 2;

            }

            _dsei.SubmitBuffer( _buffer );
            _dsei.Play();
        }

    }
}
