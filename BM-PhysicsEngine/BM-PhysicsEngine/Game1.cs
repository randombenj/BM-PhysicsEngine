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
        Vector2 satelliteAcceleration;
        Vector2 eartSatPos;

        SpriteFont font;

        float gEarth = 9.81f;



        Texture2D earth;
        Vector2 earthPosition = new Vector2(250, 150);

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

        int count = 0;
        float relation = 0;
        double angle = 0;
        float velAtEarthX = 0;
        float velAtEarthY = 0;
        float earthPassTimeX = 0;
        float earthPassTimeY = 0;

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

            count++;

            float secondSinceBeginn = (float)gameTime.TotalGameTime.TotalSeconds;
            // TODO: Add your update logic here

            eartSatPos = earthPosition - satellitePosition;

            if (angle == 0)
            {
                angle = Math.Atan( eartSatPos.X / eartSatPos.Y);
                satelliteAcceleration.Y = ((float)Math.Cos(angle) * gEarth);
                satelliteAcceleration.X = ((float)Math.Sin(angle) * gEarth);
            }

            if (eartSatPos.Y > 0)
            {
                if (velAtEarthY != 0)
                {
                    velAtEarthY = 0;
                    earthPassTimeY = secondSinceBeginn;
                    satelliteAcceleration.Y *= -1;
                }
                satellitePosition.Y = (0.5f * satelliteAcceleration.Y * (secondSinceBeginn * secondSinceBeginn) + 0f * secondSinceBeginn + 0f); // Das *10f sagt dass 10px 1m sind.     
            }
            else
            {
                if (velAtEarthY == 0)
                {
                    velAtEarthY = satelliteAcceleration.Y * secondSinceBeginn + 0f;
                    earthPassTimeY = secondSinceBeginn;
                    satelliteAcceleration.Y *= -1;
                }

                satellitePosition.Y = (0.5f * satelliteAcceleration.Y * ((secondSinceBeginn - earthPassTimeY) * (secondSinceBeginn - earthPassTimeY)) + velAtEarthY * (secondSinceBeginn - earthPassTimeY) + earthPosition.Y); // Das *10f sagt dass 10px 1m sind.
            }

            if (eartSatPos.X > 0)
            {
                if (velAtEarthX != 0)
                {
                    velAtEarthX = 0;
                    earthPassTimeX = secondSinceBeginn;
                    satelliteAcceleration.X *= -1;
                }
                satellitePosition.X = (0.5f * satelliteAcceleration.X * (secondSinceBeginn * secondSinceBeginn) + 50f * secondSinceBeginn + 0f); //
            }
            else
            {
                if (velAtEarthX == 0)
                {
                    velAtEarthX = satelliteAcceleration.X * secondSinceBeginn + 50f;
                    earthPassTimeX = secondSinceBeginn;
                    satelliteAcceleration.X *= -1;
                }

                satellitePosition.X = (0.5f * satelliteAcceleration.X * ((secondSinceBeginn - earthPassTimeX) * (secondSinceBeginn - earthPassTimeX)) + velAtEarthX * (secondSinceBeginn - earthPassTimeX) + earthPosition.X); //
            }

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
            spriteBatch.DrawString(font, String.Format("GAMETIME: {3}s\n\nSatelite.Y: {0:0000.00} m\nSatelite.X: {1:0000.00} m\nSatelite.Velocity: {2:0000.00} m/s\nRelation: {4}\nAbstand X/Y: {5: .0} / {6: .0}\n Acceleration X/Y: {7: .0} / {8: .0}",
                satellitePosition.X, satellitePosition.Y, ((gEarth * (float)gameTime.TotalGameTime.TotalSeconds + 0f)).ToString(), gameTime.TotalGameTime.TotalSeconds, relation, eartSatPos.X, eartSatPos.Y, satelliteAcceleration.X, satelliteAcceleration.Y),
                new Vector2(400, 45), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
