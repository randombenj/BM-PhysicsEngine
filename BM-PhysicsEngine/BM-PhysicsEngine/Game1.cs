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
        Vector2 satelliteVelocity;
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
            satelliteVelocity.X = 20;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        float relation = 0;
        double angle = 0;
        float secondsLastFrame;

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
            float timeSpanSincLastFrame = secondSinceBeginn - secondsLastFrame;
            secondsLastFrame = secondSinceBeginn;
            // TODO: Add your update logic here

            eartSatPos = earthPosition - satellitePosition;

            if (angle == 0)
            {
                angle = Math.Atan( eartSatPos.X / eartSatPos.Y);
                satelliteAcceleration.Y = ((float)Math.Cos(angle) * gEarth);
                satelliteAcceleration.X = ((float)Math.Sin(angle) * gEarth);
            }

            #region Change directory of Acceleration

            if (satellitePosition.X > earthPosition.X)
            {
                if (satelliteAcceleration.X > 0)
                {
                    satelliteAcceleration.X *= -1;
                }
            }
            else
            {
                if (satelliteAcceleration.X < 0)
                {
                    satelliteAcceleration.X *= -1;
                }
            }


            if (satellitePosition.Y > earthPosition.Y)
            {
                if (satelliteAcceleration.Y > 0)
                {
                    satelliteAcceleration.Y *= -1;
                }
            }
            else
            {
                if (satelliteAcceleration.Y < 0)
                {
                    satelliteAcceleration.Y *= -1;
                }
            }

            #endregion

            satelliteVelocity.X = satelliteAcceleration.X * timeSpanSincLastFrame + satelliteVelocity.X;
            satelliteVelocity.Y = satelliteAcceleration.Y * timeSpanSincLastFrame + satelliteVelocity.Y;

            satellitePosition.Y = (0.5f * satelliteAcceleration.Y * (timeSpanSincLastFrame * timeSpanSincLastFrame) + satelliteVelocity.Y * timeSpanSincLastFrame + satellitePosition.Y);
            satellitePosition.X = (0.5f * satelliteAcceleration.X * (timeSpanSincLastFrame * timeSpanSincLastFrame) + satelliteVelocity.X * timeSpanSincLastFrame + satellitePosition.X);

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
            spriteBatch.DrawString(font, String.Format("GAMETIME: {4}s\n\nSatelite Position X/Y: {0: .00} m / {1: .00} m\nSatelite Velocity X/Y: {2: .00} / {3: .00} m/s\nRelation: {5}\nAbstand X/Y: {6: .0} / {7: .0}\n Acceleration X/Y: {8: .0} / {9: .0}",
                satellitePosition.X, satellitePosition.Y, satelliteVelocity.X, satelliteVelocity.Y, gameTime.TotalGameTime.TotalSeconds, relation, eartSatPos.X, eartSatPos.Y, satelliteAcceleration.X, satelliteAcceleration.Y),
                new Vector2(400, 45), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
