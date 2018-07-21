using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid
{
    class Program
    {
        static void Main(string[] args)
        {
            RenderWindow app = new RenderWindow(new VideoMode(520, 450), "Arkanoid!");
            app.SetFramerateLimit(60);

            Texture t1, t2, t3, t4;
            t1 = new Texture("C:/Users/Gejmer/Documents/Visual Studio 2017/Projects/Arkanoid/Arkanoid/images/block01.png");
            t2 = new Texture("C:/Users/Gejmer/Documents/Visual Studio 2017/Projects/Arkanoid/Arkanoid/images/background.jpg");
            t3 = new Texture("C:/Users/Gejmer/Documents/Visual Studio 2017/Projects/Arkanoid/Arkanoid/images/ball.png");
            t4 = new Texture("C:/Users/Gejmer/Documents/Visual Studio 2017/Projects/Arkanoid/Arkanoid/images/paddle.png");

            Sprite sBackground = new Sprite(t2), sBall = new Sprite(t3), sPaddle = new Sprite(t4);
            sPaddle.Position = new Vector2f(300, 440);

            Sprite[] block = new Sprite[1000];

            for(int i = 0; i < block.Length; i++)
            {
                block[i] = new Sprite();
            }

            int n = 0;
            for (int i = 1; i <= 10; i++)
                for (int j = 1; j <= 10; j++)
                {
                    block[n] = new Sprite(t1);
                    block[n].Position = new Vector2f(i * 43, j * 20);
                    n++;
                }

            float dx = 6, dy = 5;
            float x = 300, y = 300;

            Random random = new Random();

            while (app.IsOpen)
            {
                x += dx;
                for (int i = 0; i < n; i++)
                    if (new FloatRect(x + 3, y + 3, 6, 6).Intersects(block[i].GetGlobalBounds()))
                    { block[i].Position = new Vector2f(-100, 0); dx = -dx; }

                y += dy;
                for (int i = 0; i < n; i++)
                    if (new FloatRect(x + 3, y + 3, 6, 6).Intersects(block[i].GetGlobalBounds()))
                    { block[i].Position = new Vector2f(-100, 0); dy = -dy; }

                if (x < 0 || x > 520) dx = -dx;
                if (y < 0 || y > 450) dy = -dy;

                if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                    sPaddle.Position = new Vector2f(sPaddle.Position.X + 6, sPaddle.Position.Y);
                if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                    sPaddle.Position = new Vector2f(sPaddle.Position.X - 6, sPaddle.Position.Y);

                if (new FloatRect(x, y, 12, 12).Intersects(sPaddle.GetGlobalBounds())) dy = -(random.Next() % 5 + 2);

                sBall.Position = new Vector2f(x, y);

                app.Clear();
                app.Draw(sBackground);
                app.Draw(sBall);
                app.Draw(sPaddle);

                for (int i = 0; i < n; i++)
                    app.Draw(block[i]);

                app.Display();
            }
        }
    }
}
