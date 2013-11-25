using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BM_PhysicsEngine
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D satellite;
        Vector2 satellitePosition;

        SpriteFont font;

        float gEarth = 9.81f;
        Texture2D earth;
        Vector2 earthPosition = new Vector2(220, 130);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            satellite = Content.Load<Texture2D>(@"images\satellite");
            earth = Content.Load<Texture2D>(@"images\earth");
            font = Content.Load<SpriteFont>("SatInfo");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            float secondSinceBeginn = (float)gameTime.TotalGameTime.TotalSeconds;
            // TODO: Add your update logic here

            //arrowsGeschwindigkeit.Y = gEarth * secondSinceBeginn + 0f;

            float xErdMitte = earthPosition.X + earth.Height / 2;
            float yErdMitte = earthPosition.Y + earth.Width / 2;

            satellitePosition.Y = ((0.5f * gEarth * (secondSinceBeginn * secondSinceBeginn) + 0f * secondSinceBeginn + 0f) * 10f); // Das *10f sagt dass 10px 1m sind.

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            spriteBatch.Draw(satellite, satellitePosition, Color.White);
            spriteBatch.Draw(earth, earthPosition, Color.White);
            spriteBatch.DrawString(font, String.Format("GAMETIME: {3}s\n\nSattelite.Y: {0:0000.00} m\nSattelite.X: {1:0000.00} m\nSattelite.Velocity: {2:0000.00} m/s\n",
                (satellitePosition.X / 10), (satellitePosition.Y / 10), ((gEarth * (float)gameTime.TotalGameTime.TotalSeconds + 0f)).ToString(), gameTime.TotalGameTime.TotalSeconds),
                new Vector2(400, 45), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
