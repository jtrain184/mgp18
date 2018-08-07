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

    List<Song> playlist;

    bool doOnce;
    int songIndex;


    // Constructor:
    public S_AudioEngine(GameStateManager creator, float xPos, float yPos) : base(creator, xPos, yPos) {
      this.visible = false;

      this.songs = creator.game.songs;
      this.sfx = creator.game.sfx;

      playlist = new List<Song>();
      doOnce = true;
      songIndex = 0;

      // Music starts at 100% volume:
      MediaPlayer.Volume = 1.0f;
    }


    public override void Update(GameTime gameTime, KeyboardState ks) {
      base.Update(gameTime, ks);

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


    // Queue to next song possibly using fading:
    public void next(bool fade) {

      // ADD FADE <<----------------------TODO TODO

      // After fade, stop song (which will trigger next song):
      MediaPlayer.Stop();
    }


    public void addSongQueue(MGP_Constants.music song) {
      playlist.Add(songs[song]);
    }

    public void clearSongList() { playlist.Clear(); }

  }
}
