using System;
using System.Drawing;
using System.IO;
using System.Numerics;
using GLFW;
using OpenGL;
using static OpenGL.Gl;


namespace SharpEngine
{
    class Program
    {
        private static int _width = 1024;
        private static int _height = 768;
        public static bool toggle = false;

        private static Random random = new Random();
        float randomFloat = (float) random.NextDouble();
        
        //Creating 3 points for a triangle
        static float[] vertices = new float[]
        {
            //(X,Y,Z)
            //Vertex 1
            -.5f, -.5f, 0f,
            //Vertex 2
            .5f, -.5f, 0f,
            //Vertex 3
            0f, .5f, 0f,
            //Vertex 4
        };

        private const int VertexX = 0;
        private const int VertexY = 1;
        private const int VertexSize = 3;
         
        
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
                if (Glfw.GetKey(window, Keys.Escape) == InputState.Press)
                    Glfw.SetWindowShouldClose(window, true);
                ClearScreen();
                Render();


                MoveTriangleDown();
                //MoveTriangleRight();
                
                UpdateTriangleBuffer();



            }

        }

        private static void Render()
        {
            glDrawArrays(GL_LINE_LOOP, 0, vertices.Length/VertexSize);
            //GL_TRIANGLES for filled in triangle, GL_LINE_LOOP for outlined triangle
            //Executes the commands now
            glFlush();
        }

        private static void ClearScreen()
        {
            glClearColor(0.2f, .05f, .2f, .1f);
            glClear(GL_COLOR_BUFFER_BIT);
        }


        private static void MoveTriangleRight()
        {
            for (var i = VertexX; i < vertices.Length; i += VertexSize)
            {
                vertices[i] += 0.00001f;
            }
        }
        private static void MoveTriangleLeft()
        {
            for (var i = VertexX; i < vertices.Length; i += VertexSize)
            {
                vertices[i] -= 0.00001f;
            }
        }

        private static void MoveTriangleDown()
        {
            for (var i = VertexY; i < vertices.Length; i += VertexSize)
            {
                vertices[i] -= 0.00001f;
            }
        }
        private static void MoveTriangleUp()
        {
            for (var i = VertexY; i < vertices.Length; i += VertexSize)
            {
                vertices[i] += 0.00001f;
            }
        }

        private static void ScaleDownTriangle()
        {
            //Shrink Triangle
            if (vertices[7] > 0f)
            {
                vertices[7] -= 0.00001f;
            }

            if (vertices[0] < 0f)
            {
                vertices[0] += 0.00001f;
            }

            if (vertices[3] > 0f)
            {
                vertices[3] -= 0.00001f;
            }
        }

        private static void ScaleUpTriangle()
        {
            //Scale Triangle up
            vertices[7] += 0.00001f;
            vertices[0] -= 0.00001f;
            vertices[3] += 0.00001f;
        }

        private static unsafe void LoadTriangleIntoBuffer()
        {

            //Load the vertices into a buffer
            var vertexArray = glGenVertexArray();
            var vertexBuffer = glGenBuffer();

            glBindVertexArray(vertexArray);
            glBindBuffer(GL_ARRAY_BUFFER, vertexBuffer);

            //Here we disregard the safety features.
            UpdateTriangleBuffer();
            unsafe
            {
                glVertexAttribPointer(0, VertexSize, GL_FLOAT, false, VertexSize * sizeof(float), NULL);
            }

            glEnableVertexAttribArray(0);
        }

        static unsafe void UpdateTriangleBuffer()
        {
            fixed (float* vertex = &vertices[0])
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertices.Length, vertex, GL_STATIC_DRAW);
            }
        }

        private static void CreateShaderProgram()
        {
            // create vertex shader
            var vertexShader = glCreateShader(GL_VERTEX_SHADER);
            glShaderSource(vertexShader, File.ReadAllText("Shaders/screen-coordinates.vert"));
            glCompileShader(vertexShader);

            //Create fragment shader
            var fragmentShader = glCreateShader(GL_FRAGMENT_SHADER);
            glShaderSource(fragmentShader, File.ReadAllText("Shaders/green.frag"));
            glCompileShader(fragmentShader);

            //Create shader program - rendering pipeline
            var program = glCreateProgram();
            glAttachShader(program, vertexShader);
            glAttachShader(program, fragmentShader);
            //Shaders activated
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
