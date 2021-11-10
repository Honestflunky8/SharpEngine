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

        struct Vector
        {
            public float x, y, z;

            public Vector(float x, float y, float z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
            public Vector(float x, float y)
            {
                this.x = x;
                this.y = y;
                this.z = 0;
            }
            
        }
        
        //Creating 3 points for a triangle
        static Vector[] vertices = new Vector[]
        {
            //Vertex 1
            new Vector(-.5f, -.5f, 0f),
            //Vertex 2
           new Vector(.5f, -.5f, 0f),
            //Vertex 3
            new Vector(0f, .5f, 0f),
            
            //Vertex 4
            new Vector(.4f, .4f, 0f),
            //Vertex 5
            new Vector(.6f, .4f, 0f),
            //Vertex 6
            new Vector( .5f, .6f, 0f),
            
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
                Render(window);


                //ScaleTriangleUp();
                //ScaleUpTriangle();
                //MoveTriangleDown();
                MoveTriangleRight();
                //ScaleDownTriangle();
                UpdateTriangleBuffer();



            }

        }

         static void Render(Window window)
        {
            glDrawArrays(GL_TRIANGLES, 0, vertices.Length);
            //GL_TRIANGLES for filled in triangle, GL_LINE_LOOP for outlined triangle
            //Executes the commands now
            //glFlush();
            Glfw.SwapBuffers(window);
        }

        private static void ClearScreen()
        {
            glClearColor(0.2f, .05f, .2f, .1f);
            glClear(GL_COLOR_BUFFER_BIT);
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
                glVertexAttribPointer(0, VertexSize, GL_FLOAT, false, sizeof(Vector), NULL);
            }

            glEnableVertexAttribArray(0);
        }

        static unsafe void UpdateTriangleBuffer()
        {
            fixed (Vector* vertex = &vertices[0])
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(Vector) * vertices.Length, vertex, GL_DYNAMIC_DRAW);
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
            Glfw.WindowHint(Hint.Doublebuffer, Constants.True);
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
        
        //************************************************//
        private static void MoveTriangleRight()
        {
            for (var i = VertexX; i < vertices.Length; i += VertexSize)
            {
                vertices[i].x += 0.001f;
            }
        }
        private static void MoveTriangleLeft()
        {
            for (var i = VertexX; i < vertices.Length; i += VertexSize)
            {
                vertices[i].x -= 0.001f;
            }
        }

        private static void MoveTriangleDown()
        {
            for (var i = VertexY; i < vertices.Length; i += VertexSize)
            {
                vertices[i].y -= 0.001f;
                
            }
        }
        private static void MoveTriangleUp()
        {
            for (var i = VertexY; i < vertices.Length; i += VertexSize)
            {
                vertices[i].y += 0.001f;
            }
        }

        private static void ScaleTriangleUp()
        {
            for (var i = VertexY; i < vertices.Length; i += VertexSize)
            {
                vertices[i].x *= 1.001f;
            }
        }

        private static void ScaleDownTriangle()
        {

            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].x *= 0.99f;
            }
        }

        private static void ScaleUpTriangle()
        {
            //Scale Triangle up
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].x *= 1.001f;
            }
        }
        //************************************************//
        
        
        
    }
}
