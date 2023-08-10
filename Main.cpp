#include <GLFW/glfw3.h>
#include <iostream>
#include <stdlib.h>
#include <stdio.h>
#include <glad/glad.h>
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>
#include "Shader.h"

#define GLFW_KEY_W 87
#define GLFW_KEY_A 65
#define GLFW_KEY_S 83
#define GLFW_KEY_D 68
#define GLFW_KEY_ESCAPE 256 
#define GLFW_KEY_SPACE 32 

const char* vertexShaderSourec = "version 330 core \n"; // OpenGL 3.3 

GLFWwindow * window;
int width = 640, height = 480; // 4:3 resolution is 1024x768
float playerX, playerY = 0; // player position 

int initialize(int x = width, int y = height) { // create window
     /* Initialize the library */
    if (!glfwInit()) // glfwInit();
        return -1;
    glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
    glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
    glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);
    /* Create a windowed mode window and its OpenGL context */
    window = glfwCreateWindow(640, 480, "OpenGL Graphics", NULL, NULL);
    if (!window)
    {
        std::cout << "Failed to create GLFW window" << std::endl;
        glfwTerminate();
        return -1;
    }
    glfwMakeContextCurrent(window);
    gladLoadGL(); // library configure OpenGL 
    glViewport(0, 0, width, height);
}

void drawPlayer() { // make into class later...
    
}

static void key_callback(GLFWwindow* window, int key, int scancode, int action, int mods) { // escape bring menu later
    if (key == GLFW_KEY_ESCAPE && action == GLFW_PRESS) glfwSetWindowShouldClose(window, GLFW_TRUE); 
}

// glfw: whenever the window size changed (by OS or user resize) this callback function executes
// ---------------------------------------------------------------------------------------------
void framebuffer_size_callback(GLFWwindow* window, int width, int height)
{
    // make sure the viewport matches the new window dimensions; note that width and 
    // height will be significantly larger than specified on retina displays.
    glViewport(0, 0, width, height);
}

int main(void)
{
   initialize(640, 480); // loads GL 
   glEnable(GL_DEPTH_TEST); // global opengl state 
   Shader shade; // hard coded shaders. :p 
     // world space positions of our cubes
   
   const char* rayShaderSource = "#version 330 core\n"
       "layout (location = 0) in vec3 aPos;\n"
       "layout (location = 1) in vec2 aTexCoord;\n"
       "out vec2 TexCoord;\n"
       "void main(){\n"
       "{\n"
       "   gl_Position = transform * vec4(aPos, 1.0f);\n"
           "TexCoord = vec2(aTexCoord.x, aTexCoord.y);\n"
       "}\0";
   glfwSetKeyCallback(window, key_callback); // exit program if "Esc" key pressed, only for testing purposes.
 
   GLfloat vertices[] =
   {
       -0.5f, -0.5f * float(sqrt(3)) / 3, 0.0f,
       0.5f, -0.5f * float(sqrt(3)) / 3, 0.0f,
       0.0f, 0.5 * float(sqrt(3)) * 2 / 3, 0.0f
   };
   // Vertex Shader source code
   const char* vertexShaderSource = "#version 330 core\n"
       "layout (location = 0) in vec3 aPos;\n"
       "void main()\n"
       "{\n"
       "   gl_Position = vec4(aPos.x, aPos.y, aPos.z, 1.0);\n"
       "}\0";
   //Fragment Shader source code
   const char* fragmentShaderSource = "#version 330 core\n"
       "out vec4 FragColor;\n"
       "void main()\n"
       "{\n"
       "   FragColor = vec4(0.8f, 0.3f, 0.02f, 1.0f);\n"
       "}\n\0";

   glfwMakeContextCurrent(window);
   gladLoadGL(); // library configure OpenGL 
   glViewport(0, 0, width, height); 

   GLuint vertexShader = glCreateShader(GL_VERTEX_SHADER);
   glShaderSource(vertexShader, 1, &vertexShaderSource, NULL);
   glCompileShader(vertexShader);

   GLuint fragmentShader = glCreateShader(GL_FRAGMENT_SHADER);
   glShaderSource(fragmentShader, 1, &fragmentShaderSource, NULL);
   glCompileShader(fragmentShader);

   GLuint shaderProgram = glCreateProgram();
   glAttachShader(shaderProgram, vertexShader);
   glAttachShader(shaderProgram, fragmentShader);

   glLinkProgram(shaderProgram);

   glDeleteShader(vertexShader);
   glDeleteShader(fragmentShader);

   GLuint VAO, VBO; // generate VAO b4 VBO , order important! 

   glGenVertexArrays(1, &VAO);
   glGenBuffers(1, &VBO); // 1 object generated 
   glBindVertexArray(VAO);
   glBindBuffer(GL_ARRAY_BUFFER, VBO);
   glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);

   glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 3 * sizeof(float), (void*)0);
   glEnableVertexAttribArray(0);
   glBindBuffer(GL_ARRAY_BUFFER, 0);
   glBindVertexArray(0);


   while (!glfwWindowShouldClose(window))
   {

       glClearColor(0.07f, 0.13f, 0.17f, 1.0f);
       glClear(GL_COLOR_BUFFER_BIT);
       glUseProgram(shaderProgram);
       glBindVertexArray(VAO);
       glDrawArrays(GL_TRIANGLES, 0, 3); // 3 vertices drawn
       glfwSwapBuffers(window); // UPDATE EACH FRAME 
       glfwPollEvents();
   }

   // clean up
   glDeleteVertexArrays(1, &VAO);
   glDeleteBuffers(1, &VBO);
   glDeleteProgram(shaderProgram);
   glfwDestroyWindow(window);
   glfwTerminate();
   return 0;
}