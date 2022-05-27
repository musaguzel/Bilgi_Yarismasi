using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace BilgiYarismasi
{

    class GameMusic
    {

        WindowsMediaPlayer player = new WindowsMediaPlayer();
        

        public void setUrl(String url)
        {
            player.URL = url;
        }
        public void muzikAcKapat(Boolean musicOn)
        {
            
            if (musicOn == true)
            {
                player.controls.play();
            }
            else
            {
                player.controls.stop();
            }
            
        }
    }
}
