#region File Description
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
    public partial class Form1 : Form
    {
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

            frogger.Object.allObjects = new List<Object>();
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
    }
}
