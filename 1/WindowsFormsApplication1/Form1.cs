﻿#region File Description
//-----------------------------------------------------------------------------
// MainForm.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System.Windows.Forms;
#endregion

namespace WinFormsGraphicsDevice
{
    using GdiColor = System.Drawing.Color;
    using XnaColor = Microsoft.Xna.Framework.Color;
    using System.Diagnostics;
    using System;
    using System.Timers;
    using frogger;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Content;
    public partial class Form1 : Form
    {
        

        static Dictionary<string, Texture2D> sprites;
        static SpriteFont font;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Random rand;


        Player player;
        int score;
        int lives;
        public const int width = 800;
        public const int height = 600;
        public const int startingLives = 5;

        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
    
            frogger.Object.allObjects = new List<frogger.Object>();


            //graphics = new GraphicsDevice(this);
            //graphics.PreferredBackBufferWidth = width;
            //graphics.PreferredBackBufferHeight = height;

            //Content.RootDirectory = "Content";
            rand = new Random();
            sprites = new Dictionary<string, Texture2D>();

            Row.allRows = new List<Row>();
            new Row(64 * 0, 2.5f);
            new Row(64 * 1, 2);
            new Row(64 * 2, 1.5f);
            new Row(64 * 3, 1, Spawns.LOG);
            score = 0;
            lives = startingLives;
            //put the player at the bottom of the screen
            player = new Player(new Vector2(200, 256));
        }
        public void generateNewLevel()
        {
            //generate a new starting level
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
       

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        //protected  void LoadContent()
        //{
        //    // Create a new SpriteBatch, which can be used to draw textures.
        //    spriteBatch = new SpriteBatch();
        //    font = Content.Load<SpriteFont>("Segoe");
        //    sprites.Add("placeholder", this.Content.Load<Texture2D>("placeholder"));
        //    sprites.Add("frog", this.Content.Load<Texture2D>("frog"));
        //    sprites.Add("road", this.Content.Load<Texture2D>("road"));
        //    sprites.Add("water", this.Content.Load<Texture2D>("water"));
        //    player.setSprite("frog");
        //    //player.loadContent(this.Content, "test");
        //}

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
       

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
             //   this.Exit();

            // TODO: Add your update logic here
            for (int i = 0; i < Row.allRows.Count; i++)
            {
                /*
                for (int j = 0; j < allRows[i].objects.Count; j++)
                {
                    allRows[i].objects[j].update(elapsedTime);
                }
                */
                Row.allRows[i].update(elapsedTime);
            }
            player.update(elapsedTime);
            //base.Update(gameTime);
			//So here we should check if the player has reached a certain height
            if (player.getPosition().Y < height / 2)
            {
                //shift everything downward and create a new row
                //and delete the last row
                for (int i = 0; i < Row.allRows.Count; i++)
                {
                    Row.allRows[i].setPosition(Row.allRows[i].getPosition() + new Vector2(0, 64));             
                }
                    player.setPosition(player.getPosition() + new Vector2(0, 64));
                //randomly generate a row
                new Row(0, 1 , Spawns.LOG);
            }
            if (player.getPosition().X > width)
            {
                //player just died
                player.playerReset();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected  void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            for (int i = 0; i < Row.allRows.Count; i++)
            {
                Row.allRows[i].drawRow(this.spriteBatch);
            }
            player.draw(this.spriteBatch);
            //draw score and lives
            //use the difference at the bottom of the screen for this
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(0, (height-30)), Color.Red);
            spriteBatch.DrawString(font, "Lives Remaining: " + lives, new Vector2(200, (height-30)), Color.Red);
            spriteBatch.End();
            //base.Draw(gameTime);
            
        }

        /// <summary>
        /// Used for giving several classes access to the width
        /// of the screen.
        /// </summary>
        /// <returns>Returns the width of the screen</returns>
        public static int getWidth()
        {
            return width;
        }
        /// <summary>
        /// The bread and butter of our graphics drawings. Instead of
        /// letting each object load in it's own Texture2D, all of them
        /// are saved in the static "sprites" map. When objects need to
        /// be drawn they get their sprite from here.
        /// </summary>
        /// <param name="s">The key to the mapped sprite.</param>
        /// <returns>The sprite mapped at the key.</returns>
        public static Texture2D getSprite(string s)
        {
            return sprites[s];
        }
    }
}