using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp1
{   
    class Sound
    {
        private MediaPlayer m_mediaPlayer;

        public void Play(string filename)
        {
            m_mediaPlayer = new MediaPlayer();
            m_mediaPlayer.Open(new Uri(@"sounds/" + filename, UriKind.RelativeOrAbsolute));
            m_mediaPlayer.Play();
        }

        // `volume` is assumed to be between 0 and 100.
        public void SetVolume(int volume)
        {
            // MediaPlayer volume is a float value between 0 and 1.
            m_mediaPlayer.Volume = volume / 100.0f;
        }

        public void Stop()
        {
            m_mediaPlayer.Stop();
        }

        public bool HasAudio()
        {
            bool value;
            if (m_mediaPlayer.HasAudio)
            {
                value = true;
            }
            else
            {
                value = false;
            }
            return value;
        }
    }
}
