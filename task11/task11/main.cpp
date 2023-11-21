#include <iostream>
#include <sstream>
#include <glad/glad.h>
#include <GLFW/glfw3.h>
#include <math.h>
#include <random>

void framebuffer_size_callback(GLFWwindow* window, int width, int height);

// settings
unsigned int SCR_WIDTH = 800;
unsigned int SCR_HEIGHT = 800;
float window_ratio = (SCR_WIDTH / SCR_HEIGHT);
bool redraw_after_size_change = true;

struct point {
    GLfloat x;
    GLfloat y;
    GLfloat r;
    GLfloat g;
    GLfloat b;
};

void set_color(point points[], int n, float r, float g, float b)
{
    for (int i = 0; i < n; i++)
    {
        points[i].r = r;
        points[i].g = g;
        points[i].b = b;
    }
}

void set_gradient(point points[], int n)
{
    for (int i = 0; i < n; i++)
    {
        points[i].r = (rand() % 11) / 10.0;
        points[i].g = (rand() % 11) / 10.0;
        points[i].b = (rand() % 11) / 10.0;
        //std::cout << points[i].r << " ";
    }
}

void set_smooth_gradient(point points[], int n)
{
    points[0].r = (rand() % 2 + 7) / 10.0;
    points[0].g = (rand() % 3 + 3) / 10.0;
    points[0].b = (rand() % 2 + 4) / 10.0;
    //std::cout << points[0].r << " ";
    for (int i = 1; i < n; i++)
    {
        points[i].r = points[i - 1].r + (rand() % 40 - 20) / 100.0;
        points[i].g = points[i - 1].g + (rand() % 40 - 10) / 100.0;
        points[i].b = points[i - 1].b + (rand() % 40 - 20) / 100.0;
        //std::cout << points[i].r << " ";
    }
}

void set_square(point points[])
{
    for (int i = 0; i < 4; i++)
    {
        points[i].r = 0.1;
        points[i].g = 0.9;
        points[i].b = 0.05;
    }
    points[0].x = -0.5;
    points[0].y = 0.5;
    points[1].x = 0.5;
    points[1].y = 0.5;
    points[2].x = -0.5;
    points[2].y = -0.5;
    points[3].x = 0.5;
    points[3].y = -0.5;
}

void set_pentagon(point points[])
{
    float pi = atan(1) * 4;
    float R = 0.7;
    float alpha = 2*pi/5;
    points[0].x = 0.0;
    points[0].y = 0.7;
    for (int i = 1; i < 5; i++)
    {
        points[i].x = cos(alpha) * points[i - 1].x - sin(alpha) * points[i - 1].y;
        points[i].y = sin(alpha) * points[i - 1].x + cos(alpha) * points[i - 1].y;
    }
    std::cout << pi << " " << cos(pi/3) << "\n";
    for (int j = 0; j < 5; j++)
    {
        std::cout << points[j].x << "-" << points[j].y << "\n";
    }
}

void set_fan(point points[], int n)
{
    points[0].x = -0.9;
    points[0].y = 0.0;

    //supposed n is even
    float increase_y = 0.1 * (18 / n);
    float d_y = 4 * (1.8 - increase_y * n + 2 * increase_y) / (n * n - 6 * n + 8);
    float increase_x = 0.1 * (30 / n);
    float d_x = 4 * (1.4 - increase_x * n + 2 * increase_x) / (n * n - 6 * n + 8);

    points[1].x = 0.2;
    points[1].y = 0.9;
    for (int i = 2; i < n; i++)
    {
        if (i <= n / 2)
        {
            points[i].x = points[i - 1].x + increase_x;
            if (i < n/2) increase_x += d_x;
        }
        else
        {
            points[i].x = points[i - 1].x - increase_x;
            increase_x -= d_x;
        }
        points[i].y = points[i - 1].y - increase_y;
        if (i < n / 2) increase_y += d_y;
        else if (i > n / 2) increase_y -= d_y;
    }
}

//Shaders which define one color for figure

const char* vertexShader1Source = "#version 330 core\n"
"layout(location = 0) attribute vec2 position;\n"
"void main(void)\n"
"{\n"
"gl_Position = vec4(position, 0.0, 1.0);\n"
"}\0";

const char* fragmentShader1Source = "#version 330 core\n"
"void main(void)\n"
"{\n"
"gl_FragColor = vec4(0.9, 0.1, 0.8, 1.0);\n"
"}\n\0";

//Shaders which receive colors from program through Uniform variable

const char* vertexShader2Source = "#version 330 core\n"
"layout(location = 0) attribute vec2 position;\n"
"uniform vec4 color;\n"
"void main(void)\n"
"{\n"
"gl_Position = vec4(position, 0.0, 1.0);\n"
"}\0";

const char* fragmentShader2Source = "#version 330 core\n"
"uniform vec4 color;\n"
"void main(void)\n"
"{\n"
"gl_FragColor = color;\n"
"}\n\0";

//Shaders which receive colors from program

const char* vertexShader3Source = "#version 330 core\n"
"layout(location = 0) attribute vec2 position;\n"
"layout(location = 1) attribute vec3 color;\n"
"out vec4 frag_color;\n"
"void main(void)\n"
"{\n"
"gl_Position = vec4(position, 0.0, 1.0);\n"
"frag_color = vec4(color.r, color.g, color.b, 1.0);\n"
"}\0";

const char* fragmentShader3Source = "#version 330 core\n"
"in vec4 frag_color;\n"
"void main(void)\n"
"{\n"
"gl_FragColor = frag_color;\n"
"}\n\0";

unsigned int build_and_compile_program(const char* v_source, const char* f_source)
{
    unsigned int vertexShader = glCreateShader(GL_VERTEX_SHADER);
    glShaderSource(vertexShader, 1, &v_source, NULL);
    glCompileShader(vertexShader);
    // check for shader compile errors
    int success;
    char infoLog[512];
    glGetShaderiv(vertexShader, GL_COMPILE_STATUS, &success);
    if (!success)
    {
        glGetShaderInfoLog(vertexShader, 512, NULL, infoLog);
        std::cout << "ERROR::SHADER::VERTEX::COMPILATION_FAILED\n" << infoLog << std::endl;
    }
    // fragment shader
    unsigned int fragmentShader = glCreateShader(GL_FRAGMENT_SHADER);
    glShaderSource(fragmentShader, 1, &f_source, NULL);
    glCompileShader(fragmentShader);
    // check for shader compile errors
    glGetShaderiv(fragmentShader, GL_COMPILE_STATUS, &success);
    if (!success)
    {
        glGetShaderInfoLog(fragmentShader, 512, NULL, infoLog);
        std::cout << "ERROR::SHADER::FRAGMENT::COMPILATION_FAILED\n" << infoLog << std::endl;
    }
    // link shaders
    unsigned int shaderProgram = glCreateProgram();
    glAttachShader(shaderProgram, vertexShader);
    glAttachShader(shaderProgram, fragmentShader);
    glLinkProgram(shaderProgram);
    // check for linking errors
    glGetProgramiv(shaderProgram, GL_LINK_STATUS, &success);
    if (!success) {
        glGetProgramInfoLog(shaderProgram, 512, NULL, infoLog);
        std::cout << "ERROR::SHADER::PROGRAM::LINKING_FAILED\n" << infoLog << std::endl;
    }
    glDeleteShader(vertexShader);
    glDeleteShader(fragmentShader);
    return shaderProgram;
}

int main()
{
    // glfw: initialize and configure
    // ------------------------------
    glfwInit();
    glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
    glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
    glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);

#ifdef __APPLE__
    glfwWindowHint(GLFW_OPENGL_FORWARD_COMPAT, GL_TRUE);
#endif

    // glfw window creation
    // --------------------
    GLFWwindow* window = glfwCreateWindow(SCR_WIDTH, SCR_HEIGHT, "11 LAB", NULL, NULL);
    if (window == NULL)
    {
        std::cout << "Failed to create GLFW window" << std::endl;
        glfwTerminate();
        return -1;
    }
    glfwMakeContextCurrent(window);
    glfwSetFramebufferSizeCallback(window, framebuffer_size_callback);

    // glad: load all OpenGL function pointers
    // ---------------------------------------
    if (!gladLoadGLLoader((GLADloadproc)glfwGetProcAddress))
    {
        std::cout << "Failed to initialize GLAD" << std::endl;
        return -1;
    }

    //----------------------build and compile our shader program---------------
    unsigned int shaderProgram_PredefColor = build_and_compile_program(vertexShader1Source, fragmentShader1Source);
    unsigned int shaderProgram_UniformColor = build_and_compile_program(vertexShader2Source, fragmentShader2Source);
    unsigned int shaderProgram_HandColor = build_and_compile_program(vertexShader3Source, fragmentShader3Source);

    //----------------------buffer initialization------------------------------

    unsigned int vboID, vaoID;
    glGenBuffers(1, &vboID);
    glGenVertexArrays(1, &vaoID);

    //----------------------create fan--------------------------------------

    const int n = 12; //for fan - 6, 8, 10, 12, 14 ...

    point figure[n];
    set_fan(figure, n);
    //set_square(figure);

    //----------------------create square--------------------------------------

    point square[4];
    set_square(square);
    //set_square(figure);

    //----------------------create pentagon--------------------------------------

    point pentagon[5];
    set_pentagon(pentagon);
    //set_square(figure);

    glUseProgram(shaderProgram_PredefColor);
    glBindVertexArray(vaoID);

    //----------------------set up buffer-----------------------------------

    glBindBuffer(GL_ARRAY_BUFFER, vboID);
    glBufferData(GL_ARRAY_BUFFER, sizeof(figure), figure, GL_STATIC_DRAW);
    glEnableVertexAttribArray(0);
    glVertexAttribPointer(0, 2, GL_FLOAT, GL_FALSE, 5 * sizeof(float), (void*)0);

    while (true)
    {
        // render
        // ------
        glClearColor(0.0f, 0.2f, 0.2f, 0.0f);
        glClear(GL_COLOR_BUFFER_BIT);

        glDrawArrays(GL_TRIANGLE_FAN, 0, n);

        glfwSwapBuffers(window);
        glfwPollEvents();
        if (glfwGetKey(window, GLFW_KEY_1) == GLFW_PRESS) break;
        if (glfwWindowShouldClose(window)) break;
    }

    glBufferData(GL_ARRAY_BUFFER, sizeof(square), square, GL_STATIC_DRAW);

    while (true)
    {
        // render
        // ------
        glClearColor(0.0f, 0.2f, 0.2f, 0.0f);
        glClear(GL_COLOR_BUFFER_BIT);

        glDrawArrays(GL_TRIANGLE_STRIP, 0, 4);

        glfwSwapBuffers(window);
        glfwPollEvents();
        if (glfwGetKey(window, GLFW_KEY_2) == GLFW_PRESS) break;
        if (glfwWindowShouldClose(window)) break;
    }

    glBufferData(GL_ARRAY_BUFFER, sizeof(pentagon), pentagon, GL_STATIC_DRAW);

    while (true)
    {
        // render
        // ------
        glClearColor(0.0f, 0.2f, 0.2f, 0.0f);
        glClear(GL_COLOR_BUFFER_BIT);

        glDrawArrays(GL_TRIANGLE_FAN, 0, 5);

        glfwSwapBuffers(window);
        glfwPollEvents();
        if (glfwGetKey(window, GLFW_KEY_3) == GLFW_PRESS) break;
        if (glfwWindowShouldClose(window)) break;
    }

    glUseProgram(shaderProgram_UniformColor);

    set_color(figure, n, 0.15, 0.9, 0.3);

    //----------------------set up buffer---------------

    glBufferData(GL_ARRAY_BUFFER, sizeof(figure), figure, GL_STATIC_DRAW);
    unsigned int location = glGetUniformLocation(shaderProgram_UniformColor, "color");
    glUniform4f(location, figure[0].r, figure[0].g, figure[0].b, 1.0);

    // render loop
    // -----------
    while (true)
    {
        // render
        // ------
        glClearColor(0.0f, 0.2f, 0.2f, 0.0f);
        glClear(GL_COLOR_BUFFER_BIT);

        glDrawArrays(GL_TRIANGLE_FAN, 0, n);

        glfwSwapBuffers(window);
        glfwPollEvents();
        if (glfwGetKey(window, GLFW_KEY_4) == GLFW_PRESS) break;
        if (glfwWindowShouldClose(window)) break;
    }

    glUseProgram(shaderProgram_HandColor);

    set_gradient(figure, n);

    //----------------------set up buffer---------------

    glBufferData(GL_ARRAY_BUFFER, sizeof(figure), figure, GL_STATIC_DRAW);
    glEnableVertexAttribArray(1);
    glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 5 * sizeof(float), (void*)(2 * sizeof(float)));

    // render loop
    // -----------
    while (true)
    {
        // render
        // ------
        glClearColor(0.0f, 0.2f, 0.2f, 0.0f);
        glClear(GL_COLOR_BUFFER_BIT);

        glDrawArrays(GL_TRIANGLE_FAN, 0, n);

        glfwSwapBuffers(window);
        glfwPollEvents();
        if (glfwGetKey(window, GLFW_KEY_5) == GLFW_PRESS) break;
        if (glfwWindowShouldClose(window)) break;
    }

    set_smooth_gradient(figure, n);

    //----------------------set up buffer---------------

    glBufferData(GL_ARRAY_BUFFER, sizeof(figure), figure, GL_STATIC_DRAW);

    // render loop
    // -----------
    while (true)
    {
        // render
        // ------
        glClearColor(0.0f, 0.2f, 0.2f, 0.0f);
        glClear(GL_COLOR_BUFFER_BIT);

        glDrawArrays(GL_TRIANGLE_FAN, 0, n);

        glfwSwapBuffers(window);
        glfwPollEvents();
        if (glfwWindowShouldClose(window)) break;
    }

    glDisableVertexAttribArray(0);
    glDisableVertexAttribArray(1);
    glBindBuffer(GL_ARRAY_BUFFER, 0);
    glBindVertexArray(0);

    glDeleteVertexArrays(1, &vaoID);
    glDeleteBuffers(1, &vboID);
    glDeleteProgram(shaderProgram_PredefColor);
    glDeleteProgram(shaderProgram_HandColor);

    glfwTerminate();
    return 0;
}

void framebuffer_size_callback(GLFWwindow* window, int width, int height)
{
    glViewport(0, 0, width, height);
    SCR_HEIGHT = height;
    SCR_WIDTH = width;
}

