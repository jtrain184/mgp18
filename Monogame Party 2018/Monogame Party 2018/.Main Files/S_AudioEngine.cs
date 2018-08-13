using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;


namespace Monogame_Party_2018 {
  public class S_AudioEngine : State {

    public Dictionary<MGP_Constants.music, Song> songs;
    public Dictionary<MGP_Constants.soundEffects, SoundEffect> sfx;

    //bool doOnce;
    //int songIndex;

    Song nextSong;
    int fadeTimer;
    bool playNext;
    bool silence;


    // Constructor:
    public S_AudioEngine(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos) {
      this.visible = false;

      this.songs = creator.game.songs;
      this.sfx = creator.game.sfx;

      // Music starts at 100% volume:
      //curVolume = 1.0f;
      //MediaPlayer.Volume = curVolume;

      //playlist = new List<Song>();
      //doOnce = true;
      //songIndex = 0;

      fadeTimer = 0;
      silence = false;
      playNext = false;
    }


    public override void Update(GameTime gameTime, KeyboardState ks) {
      base.Update(gameTime, ks);

      /*
      // If end of song queue for next song:
      if ((playlist.Count > 0) && (MediaPlayer.State != MediaState.Playing)) {
        if (doOnce) {
          doOnce = false;
          songIndex++;
          if(songIndex > playlist.Count - 1) { songIndex = 0; }

          if (playlist.Count > 0) {
            MediaPlayer.Play(playlist[songIndex]);
          }
        }
      }

      if (MediaPlayer.State == MediaState.Playing) {
        doOnce = true;
      }

  */

      // adjust volume based on the fade time:
      if (fadeTimer > -1) {
        if (fadeTimer > 0) {
          float newVolume = (1 - (1 / fadeTimer));
          newVolume = MathHelper.Clamp(newVolume, 0.0001f, 1.0f);
          MediaPlayer.Volume = newVolume;
        } // somewhere between 100% and 0%
        fadeTimer--;
      }


      // Stop the current song
      if (fadeTimer == 0) {
        MediaPlayer.Stop();

        if (!silence)
          playNext = true;
      }

      // Play the next song:
      if (playNext) {
        MediaPlayer.Volume = 1.0f;
        MediaPlayer.Play(nextSong); // remains the same until someone changes it
        playNext = false;
      }

    }




    // DRAW
    public override void Draw(GameTime gameTime) {
        base.Draw(gameTime);
    }


    // Play a sound effect:
    public void playSound(MGP_Constants.soundEffects sound, float volume) {

      // Boundary checks:
      if (volume > 1.0f)
        volume = 1.0f;
      else if (volume < 0.0f) {
        volume = 0.0f;
      }

      // Change volume:
      SoundEffect.MasterVolume = volume;

      sfx[sound].Play();
    }


    public void setNextSong(MGP_Constants.music song) {
      nextSong = songs[song];
    }

    public void playNextSong(int fadeTime) {
      if (fadeTime < 0) { fadeTime = 1; }
      fadeTimer = fadeTime;
      silence = false;
    }

    public void stopMusic(int fadeTime) {
      if (fadeTime < 0) { fadeTime = 1; }
      fadeTimer = fadeTime;
      silence = true;
    }


  }
}
