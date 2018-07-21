﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018 {

  public abstract class State {

    public GameStateManager parentManager;
    public State state;

    public bool active; // whether or not step function is run
    public bool visible; // whether or not draw function is run
    public bool isTopLayer; // Listen for keyboard input?
    public int player;
    public bool flagForDeletion; // at end of Update, delete me (sent to manager)
    public KeyboardManager km;

    // The 'state' will be drawn to the right and below this point
    public float xPos;
    public float yPos;

    // List of all Entity objects related to this State
    public List<Entity> eList = new List<Entity>();

    // CONSTRUCTOR:
    public State(GameStateManager creator, float xPos, float yPos) {

      // Passed in:
      this.parentManager = creator;
      this.state = this;
      this.xPos = xPos;
      this.yPos = yPos;

      // Default:
      this.active = true;
      this.visible = true;
      this.isTopLayer = false;
      this.flagForDeletion = false;

      // Makes referencing the keyboard manager easier
      this.km = parentManager.km;
    }

    // VIRTUAL Functions (will be overridden)
    public virtual void Update(GameTime gameTime, KeyboardState ks) { }
    public virtual void Draw(GameTime gameTime) { }

    // Add a new Entity to the State:
    public void addEntity(Entity e) { eList.Add(e); }

    // Remove Entity from the State:
    public void removeEntity(Entity e) { eList.Remove(e); }


  } // end State class



}
