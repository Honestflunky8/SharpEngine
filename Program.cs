using System;
using System.Drawing;
using System.IO;
using System.Numerics;
using GLFW;
using static OpenGL.Gl;

namespace SharpEngine
{
    class Program
    {
        private static int _width = 1024;
        private static int _height = 768;
        
        static void Main(string[] args)
        {
            var window = CreateWindow();

            LoadTriangleIntoBuffer();

            CreateShaderProgram();

            //Engine Loop
            //Here we say if the window is closed, it should actually close.
            while (!Glfw.WindowShouldClose(window))
            {
                //This listens to events. Reacts to window changes like position etc.
                Glfw.PollEvents();
                glDrawArrays(GL_LINE_LOOP,0,3);
                //GL_TRIANGLES for filled in triangle, GL_LINE_LOOP for outlined triangle
                //Executes the commands now
                
                glFlush();
                
            }

        }

        private static unsafe void LoadTriangleIntoBuffer()
        {
            //Creating 3 points for a triangle
            float[] vertices = new float[]
            {
                -.5f, -.5f, 0f,
                .5f, -.5f, 0f,
                0f, .5f, 0f
            };

            //Load the vertices into a buffer
            //
            var vertexArray = glGenVertexArray();
            //
            var vertexBuffer = glGenBuffer();

            glBindVertexArray(vertexArray);
            glBindBuffer(GL_ARRAY_BUFFER, vertexBuffer);

            //Here we disregard the safety features.
            unsafe
            {
                fixed (float* vertex = &vertices[0])
                {
                    glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertices.Length, vertex, GL_STATIC_DRAW);
                }

                glVertexAttribPointer(0, 3, GL_FLOAT, false, 3 * sizeof(float), NULL);
            }

            glEnableVertexAttribArray(0);
        }

        private static void CreateShaderProgram()
        {
            // create vertex shader
            var vertexShader = glCreateShader(GL_VERTEX_SHADER);
            glShaderSource(vertexShader, File.ReadAllText("Shaders/red-triangle.vert"));
            glCompileShader(vertexShader);

            //Create fragment shader
            var fragmentShader = glCreateShader(GL_FRAGMENT_SHADER);
            glShaderSource(fragmentShader, File.ReadAllText("Shaders/green-triangle.frag"));
            glCompileShader(fragmentShader);

            //Create shader program - rendering pipeline
            var program = glCreateProgram();
            glAttachShader(program, vertexShader);
            glAttachShader(program, fragmentShader);
            glLinkProgram(program);
            glUseProgram(program);
        }

        private static Window CreateWindow()
        {
            Glfw.Init();
            Glfw.WindowHint(Hint.Doublebuffer, Constants.False);
            Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.Decorated, true);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.OpenglForwardCompatible, Constants.True);

            //Create and launch a window
            var window = Glfw.CreateWindow(_width, _height, "SharpEngine", Monitor.None, Window.None);
            Glfw.MakeContextCurrent(window);
            Import(Glfw.GetProcAddress);
            return window;
        }
    }
}
