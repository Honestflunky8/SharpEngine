using System;
using GLFW;

namespace SharpEngine
{
    class Program
    {
        public static int _width = 1024;
        public static int _height = 768;
        static void Main(string[] args)
        {
            Glfw.Init();

            var window = Glfw.CreateWindow(_width, _height, "SharpEngine", Monitor.None, Window.None);
            Glfw.MakeContextCurrent(window);

            
            //Here we say if the window is closed, it should actually close.
            while (!Glfw.WindowShouldClose(window))
            {
                //This listens to events. Reacts to window changes like position etc.
                Glfw.PollEvents();
            }

        }
    }
}
