#include <GL/glew.h>
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>
#include <SOIL/SOIL.h> 
#include <SFML/OpenGL.hpp>
#include <SFML/Window.hpp>
#include <SFML/Graphics.hpp>
#include <iostream>
#include <vector>


enum axis {
    OX,
    OY,
    OZ,
    NUL
};

axis ax = NUL;

enum modeFigure {
    tetrahedron,
    cube,
    circle
};
struct VertexG {
    GLfloat x;
    GLfloat y;
    GLfloat z;
    GLfloat r;
    GLfloat g;
    GLfloat b;
};
struct Vertex
{
    glm::vec3 position;
    glm::vec3 color;
    glm::vec2 texCoords;
};
modeFigure mode = tetrahedron;

int sectors = 0;
bool two_tex = false;

float mixValue = 0.4f;

float stepByX = 0.0f;
float stepByY = 0.0f;
float stepByZ = -5.0f;

float scaleByX = 1.0f;
float scaleByY = 1.0f;
float scaleByZ = 1.0f;
GLint Unif_xmove;
GLint Unif_zmove;
GLint Unif_ymove;
// ID шейдерной программы
GLuint Gradient_mode;
GLuint Texture_mode;
GLuint GradientCircle_mode;

// ID атрибута
GLint Attrib_vertex;
// ID Vertex Buffer Object
GLuint VBO_tetrahedron;
GLuint VBO_cube;
GLuint VBO_circle;

//текстуры
GLuint texture1;
GLuint texture2;


// Исходный код вершинного шейдера
const char* VertexShaderSource = R"(
#version 330 core
layout (location = 0) in vec3 coord;
layout (location = 1) in vec3 color; 

uniform float x_move;
uniform float y_move;
uniform float z_move;

out vec3 vertexColor; 

void main() {
	vec3 position = vec3(coord) + vec3(x_move, y_move, z_move);
    gl_Position = vec4(position, 1.0);
    vertexColor = color; // Передача цвета во фрагментный шейдер
}
)";

// Исходный код фрагментного шейдера для градиентного закрашивания
const char* FragShaderSource_Gradient = R"(
#version 330 core
in vec3 vertexColor; // Получаем цвет от вершинного шейдера
out vec4 color;
void main() {
    color = vec4(vertexColor, 1.0); // Используем цвет от вершины
}
)";

// Исходный код вершинного шейдера
const char* VertexShaderGradient = R"(
#version 330 core

    layout (location = 0) in vec3 coord_pos;
    layout (location = 1) in vec3 color_value;

    out vec3 frag_color;

    uniform mat4 model;
    uniform mat4 view; 
    uniform mat4 projection;
    
    void main() 
    {
        gl_Position = projection * view * model * vec4(coord_pos, 1.0);;
        frag_color = color_value;
    }
    )";

const char* VertexShaderTextures = R"(
    #version 330 core

    layout (location = 0) in vec3 coord_pos;
    layout (location = 1) in vec3 color_value;
    layout (location = 2) in vec2 tex_coord_in;

    out vec3 frag_color;
    out vec2 coord_tex;

    uniform mat4 model;
    uniform mat4 view;
    uniform mat4 projection;
    
    void main() 
    { 
        gl_Position = projection * view * model * vec4(coord_pos, 1.0);
        frag_color = color_value;
        coord_tex = tex_coord_in;
    }
    )";

// Исходный код фрагментного шейдера
const char* FragShaderGradient = R"(
    #version 330 core
    
    in vec3 frag_color;
    in vec2 coord_tex;

    out vec4 color;

    uniform float mixValue;
    
    uniform sampler2D texture1;
    uniform sampler2D texture2;

    void main() 
    {
        color = vec4(frag_color, 1);
    }
)";

const char* FragShaderTextures = R"(
    #version 330 core
    
    in vec3 frag_color;
    in vec2 coord_tex;

    out vec4 color;

    uniform float mixValue;
    
    uniform bool two_tex;

    uniform sampler2D texture1;
    uniform sampler2D texture2;

    void main()  
    {
       if(two_tex)
           color = mix(texture(texture1, coord_tex), texture(texture2, coord_tex), mixValue);
       else
           color = mix(texture(texture1, coord_tex), vec4(frag_color, 1.0), mixValue);
    } 
)";


void checkOpenGLerror()
{
    GLenum err = glGetError();
    if (err != GL_NO_ERROR)
    {
        std::cout << "OpenGL error " << err << std::endl;
    }
}

void ShaderLog(unsigned int shader)
{
    int infologLen = 0;
    glGetShaderiv(shader, GL_INFO_LOG_LENGTH, &infologLen);
    GLint vertex_compiled;
    glGetShaderiv(shader, GL_COMPILE_STATUS, &vertex_compiled);

    if (infologLen > 1)
    {
        int charsWritten = 0;
        std::vector<char> infoLog(infologLen);
        glGetShaderInfoLog(shader, infologLen, &charsWritten, infoLog.data());
        std::cout << "InfoLog: " << infoLog.data() << std::endl;
    }

    if (vertex_compiled != GL_TRUE)
    {
        GLsizei log_length = 0;
        GLchar message[1024];
        glGetShaderInfoLog(shader, 1024, &log_length, message);
        std::cout << "InfoLog2: " << message << std::endl;
    }

}
void InitShaderTetrahedron() {
    //Тетраэдр
    GLuint vShader = glCreateShader(GL_VERTEX_SHADER);
    glShaderSource(vShader, 1, &VertexShaderGradient, NULL);
    glCompileShader(vShader);
    std::cout << "vertex shader \n";
    ShaderLog(vShader);

    GLuint fShader = glCreateShader(GL_FRAGMENT_SHADER);
    glShaderSource(fShader, 1, &FragShaderGradient, NULL);
    glCompileShader(fShader);
    std::cout << "fragment shader \n";
    // Функция печати лога шейдера
    ShaderLog(fShader);
    Gradient_mode = glCreateProgram();
    glAttachShader(Gradient_mode, vShader);
    glAttachShader(Gradient_mode, fShader);
    // Линкуем шейдерную программу
    glLinkProgram(Gradient_mode);
    // Проверяем статус сборки
    int link_ok;
    glGetProgramiv(Gradient_mode, GL_LINK_STATUS, &link_ok);

    if (!link_ok)
    {
        std::cout << "error attach shaders \n";
        return;
    }
    checkOpenGLerror();
}

void InitShaderCube()
{
    //Куб
    GLuint vShaderTex = glCreateShader(GL_VERTEX_SHADER);
    // Передаем исходный код
    glShaderSource(vShaderTex, 1, &VertexShaderTextures, NULL);
    // Компилируем шейдер
    glCompileShader(vShaderTex);
    std::cout << "vertex shader t\n";
    // Функция печати лога шейдера
    ShaderLog(vShaderTex);

    // Создаем фрагментный шейдер
    GLuint fShaderTex = glCreateShader(GL_FRAGMENT_SHADER);
    // Передаем исходный код
    glShaderSource(fShaderTex, 1, &FragShaderTextures, NULL);
    // Компилируем шейдер
    glCompileShader(fShaderTex);
    std::cout << "fragment shader \n";
    // Функция печати лога шейдера
    ShaderLog(fShaderTex);
    Texture_mode = glCreateProgram();
    glAttachShader(Texture_mode, vShaderTex);
    glAttachShader(Texture_mode, fShaderTex);

    // Линкуем шейдерную программу
    glLinkProgram(Texture_mode);
    int link_ok;
    // Проверяем статус сборки
    glGetProgramiv(Texture_mode, GL_LINK_STATUS, &link_ok);

    if (!link_ok)
    {
        std::cout << "error attach shaders \n";
        return;
    }
    checkOpenGLerror();
}
void InitShaderCircle()
{
    //Окружность
    GLuint vShaderGradCircle = glCreateShader(GL_VERTEX_SHADER);
    // Передаем исходный код
    glShaderSource(vShaderGradCircle, 1, &VertexShaderGradient, NULL);
    // Компилируем шейдер
    glCompileShader(vShaderGradCircle);
    std::cout << "vertex shader g\n";
    // Функция печати лога шейдера
    ShaderLog(vShaderGradCircle);

    // Создаем фрагментный шейдер
    GLuint fShaderGradCircle = glCreateShader(GL_FRAGMENT_SHADER);
    // Передаем исходный код
    glShaderSource(fShaderGradCircle, 1, &FragShaderGradient, NULL);
    // Компилируем шейдер
    glCompileShader(fShaderGradCircle);
    std::cout << "fragment shader \n";
    // Функция печати лога шейдера
    ShaderLog(fShaderGradCircle);
    GradientCircle_mode = glCreateProgram();
    glAttachShader(GradientCircle_mode, vShaderGradCircle);
    glAttachShader(GradientCircle_mode, fShaderGradCircle);

    // Линкуем шейдерную программу
    glLinkProgram(GradientCircle_mode);
    // Проверяем статус сборки
    int link_ok;
    glGetProgramiv(GradientCircle_mode, GL_LINK_STATUS, &link_ok);

    if (!link_ok)
    {
        std::cout << "error attach shaders \n";
        return;
    }
    checkOpenGLerror();
}
void InitShader()
{

    InitShaderTetrahedron();
    InitShaderCube();
    InitShaderCircle();
}


void InitVBO()
{
    //тетраэдр

    float a = 3;
    //https://www.cyberforum.ru/opengl/thread718294.html?ysclid=lpoa7mua4z890811023
    float tetrahedron[] =
    {
        0.0f, 0.0f, a * sqrt(6) / 4, 1.0f, 1.0f, 0.0f,
        a / sqrt(3), 0.0f, -a * sqrt(6) / 12, 0.0f, 1.0f, 1.0f,
        -a / sqrt(12), a / 2, -a * sqrt(6) / 12,1.0f, 0.0f, 1.0f,

        0.0f, 0.0f, a * sqrt(6) / 4, 1.0f, 1.0f, 0.0f,
        -a / sqrt(12),  -a / 2, -a * sqrt(6) / 12, 0.0f, 0.0f, 1.0f,
        a / sqrt(3), 0.0f, -a * sqrt(6) / 12, 0.0f, 1.0f, 1.0f,

        0.0f, 0.0f, a * sqrt(6) / 4, 1.0f, 1.0f, 0.0f,
        -a / sqrt(12), a / 2, -a * sqrt(6) / 12,1.0f, 0.0f, 1.0f,
        -a / sqrt(12),  -a / 2, -a * sqrt(6) / 12,0.0f, 0.0f, 1.0f,

        a / sqrt(3), 0.0f, -a * sqrt(6) / 12,0.0f, 1.0f, 1.0f,
        -a / sqrt(12),  -a / 2, -a * sqrt(6) / 12,0.0f, 0.0f, 1.0f,
        -a / sqrt(12), a / 2, -a * sqrt(6) / 12,1.0f, 0.0f, 1.0f,

    };
    //объявляем массив атрибутов и буфер
    glGenBuffers(1, &VBO_tetrahedron);

    // передаем вершины в буфер
    glBindBuffer(GL_ARRAY_BUFFER, VBO_tetrahedron);
    glBufferData(GL_ARRAY_BUFFER, sizeof(tetrahedron), tetrahedron, GL_STATIC_DRAW);


    
    float cube[] = {
        -0.5f, -0.5f, -0.5f,  1.0f, 1.0f, 0.0f,  0.0f, 0.0f,
         0.5f, -0.5f, -0.5f,  0.0f, 1.0f, 1.0f,  1.0f, 0.0f,
         0.5f,  0.5f, -0.5f,  1.0f, 0.0f, 1.0f,  1.0f, 1.0f,
         0.5f,  0.5f, -0.5f,  1.0f, 0.0f, 1.0f,  1.0f, 1.0f,
        -0.5f,  0.5f, -0.5f,  0.0f, 0.0f, 1.0f,  0.0f, 1.0f,
        -0.5f, -0.5f, -0.5f,  1.0f, 1.0f, 0.0f,  0.0f, 0.0f,

        -0.5f, -0.5f,  0.5f,   0.0f, 1.0f, 1.0f,  0.0f, 0.0f,
         0.5f, -0.5f,  0.5f,   1.0f, 1.0f, 0.0f,  1.0f, 0.0f,
         0.5f,  0.5f,  0.5f,   0.0f, 0.0f, 1.0f,  1.0f, 1.0f,
         0.5f,  0.5f,  0.5f,   0.0f, 0.0f, 1.0f,  1.0f, 1.0f,
        -0.5f,  0.5f,  0.5f,   1.0f, 0.0f, 1.0f,  0.0f, 1.0f,
        -0.5f, -0.5f,  0.5f,   0.0f, 1.0f, 1.0f,  0.0f, 0.0f,

        -0.5f,  0.5f,  0.5f,   1.0f, 0.0f, 1.0f,  1.0f, 0.0f,
        -0.5f,  0.5f, -0.5f,  0.0f, 0.0f, 1.0f,  1.0f, 1.0f,
        -0.5f, -0.5f, -0.5f,  1.0f, 1.0f, 0.0f,  0.0f, 1.0f,
        -0.5f, -0.5f, -0.5f,  1.0f, 1.0f, 0.0f,  0.0f, 1.0f,
        -0.5f, -0.5f,  0.5f,   0.0f, 1.0f, 1.0f,  0.0f, 0.0f,
        -0.5f,  0.5f,  0.5f,   1.0f, 0.0f, 1.0f,  1.0f, 0.0f,

         0.5f,  0.5f,  0.5f,   0.0f, 0.0f, 1.0f,  1.0f, 0.0f,
         0.5f,  0.5f, -0.5f,  1.0f, 0.0f, 1.0f,  1.0f, 1.0f,
         0.5f, -0.5f, -0.5f,  0.0f, 1.0f, 1.0f,  0.0f, 1.0f,
         0.5f, -0.5f, -0.5f,  0.0f, 1.0f, 1.0f,  0.0f, 1.0f,
         0.5f, -0.5f,  0.5f,   1.0f, 1.0f, 0.0f,  0.0f, 0.0f,
         0.5f,  0.5f,  0.5f,   0.0f, 0.0f, 1.0f,  1.0f, 0.0f,

        -0.5f, -0.5f, -0.5f,  1.0f, 1.0f, 0.0f,  0.0f, 1.0f,
         0.5f, -0.5f, -0.5f,  0.0f, 1.0f, 1.0f,  1.0f, 1.0f,
         0.5f, -0.5f,  0.5f,   1.0f, 1.0f, 0.0f,  1.0f, 0.0f,
         0.5f, -0.5f,  0.5f,   1.0f, 1.0f, 0.0f,  1.0f, 0.0f,
        -0.5f, -0.5f,  0.5f,   0.0f, 1.0f, 1.0f,  0.0f, 0.0f,
        -0.5f, -0.5f, -0.5f,  1.0f, 1.0f, 0.0f,  0.0f, 1.0f,

        -0.5f,  0.5f, -0.5f,  0.0f, 0.0f, 1.0f,  0.0f, 1.0f,
         0.5f,  0.5f, -0.5f,  1.0f, 0.0f, 1.0f,  1.0f, 1.0f,
         0.5f,  0.5f,  0.5f,   0.0f, 0.0f, 1.0f,  1.0f, 0.0f,
         0.5f,  0.5f,  0.5f,   0.0f, 0.0f, 1.0f,  1.0f, 0.0f,
        -0.5f,  0.5f,  0.5f,   1.0f, 0.0f, 1.0f,  0.0f, 0.0f,
        -0.5f,  0.5f, -0.5f,  0.0f, 0.0f, 1.0f,  0.0f, 1.0f
    };

    //объявляем массив атрибутов и буфер
    glGenBuffers(1, &VBO_cube);

    // передаем вершины в буфер
    glBindBuffer(GL_ARRAY_BUFFER, VBO_cube);
    glBufferData(GL_ARRAY_BUFFER, sizeof(cube), cube, GL_STATIC_DRAW);


    //окружность

    const int step = 360;
    float angle = 3.1415f * 2.0f / step;
    float radius = 0.8f;
    const int vert = step + 2;
    sectors = vert;

    //вершины круга
    float circle[vert * 6] = { 0.0f, 0.0f, 0.0f,1.0f,1.0f,1.0f };
    int range = vert / 3;
    float colorStep = 2.0f / range;
    std::vector<float> colors = { 1.0f, 0.0f, 0.0f };

    for (int i = 1; i < vert; i++)
    {
        circle[i * 6] = radius * cos(angle * (i - 1));
        circle[(i * 6) + 1] = radius * sin(angle * (i - 1));
        circle[(i * 6) + 2] = 0.0f;

        if (i > 0 && i <= range)
        {
            circle[(i * 6) + 3] = colors[0];
            circle[(i * 6) + 4] = colors[1];
            circle[(i * 6) + 5] = colors[2];

            if (colors[1] < 1.0f)
                colors[1] += colorStep;
            else
                colors[0] -= colorStep;
        }
        else if (i > range && i <= range * 2)
        {
            circle[(i * 6) + 3] = colors[0];
            circle[(i * 6) + 4] = colors[1];
            circle[(i * 6) + 5] = colors[2];


            if (colors[2] < 1.0f)
                colors[2] += colorStep;
            else
                colors[1] -= colorStep;
        }
        else
        {
            circle[(i * 6) + 3] = colors[0];
            circle[(i * 6) + 4] = colors[1];
            circle[(i * 6) + 5] = colors[2];

            if (colors[0] < 1.0f)
                colors[0] += colorStep;
            else
                colors[2] -= colorStep;
        }
    }

    //объявляем массив атрибутов и буфер
    glGenBuffers(1, &VBO_circle);

    // Передаем вершины в буфер
    glBindBuffer(GL_ARRAY_BUFFER, VBO_circle);
    glBufferData(GL_ARRAY_BUFFER, sizeof(circle), circle, GL_STATIC_DRAW);

    checkOpenGLerror();

}


void InitTextures()
{
    int width, height;
    // создаем текстуру
    glGenTextures(1, &texture1);

    // связываем с типом текступы
    glBindTexture(GL_TEXTURE_2D, texture1);

    // настроки отображения текстуры при выходе за пределы диапазона текстуры
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);

    // настройки отображения текстуры в зависимости от удаления или приближения обьекта
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

    // загружаем изображение

    unsigned char* image = SOIL_load_image("text1.png", &width, &height, 0, SOIL_LOAD_RGB);

    //создаем текстуру
    glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, width, height, 0, GL_RGB, GL_UNSIGNED_BYTE, image);
    SOIL_free_image_data(image);

    //отключаем привязку к текстуре
    glBindTexture(GL_TEXTURE_2D, 0);

    //вторая текстура//


    // создаем текстуру
    glGenTextures(1, &texture2);

    // связываем с типом текступы
    glBindTexture(GL_TEXTURE_2D, texture2);

    // настроки отображения текстуры при выходе за пределы диапазона текстуры
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);

    // настройки отображения текстуры в зависимости от удаления или приближения обьекта
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

    // загружаем изображение
    image = SOIL_load_image("text2.png", &width, &height, 0, SOIL_LOAD_RGB);

    //создаем текстуру
    glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, width, height, 0, GL_RGB, GL_UNSIGNED_BYTE, image);
    SOIL_free_image_data(image);

    //отключаем привязку к текстуре
    glBindTexture(GL_TEXTURE_2D, 0);

}

void Init()
{
    // Шейдеры
    InitShader();

    // Вершинный буфер
    InitVBO();

    //подгрузка текстур
    InitTextures();

    //включаем тест глубины
    glEnable(GL_DEPTH_TEST);
}
float angle = 25.0f;
void Draw(sf::Clock clock)
{
    glm::mat4 view = glm::mat4(1.0f); 
    glm::mat4 projection;
    glm::mat4 model = glm::mat4(1.0f);
    switch (mode)
    {
    case (tetrahedron):
        glUseProgram(Gradient_mode); // Устанавливаем шейдерную программу текущей

        model = glm::translate(model, glm::vec3(0.0f, 0.0f, 0.0f));

        if (ax == OX)
            model = glm::rotate(model, glm::radians(angle), glm::vec3(1.0f, 0.0f, 0.0f));
        else if (ax == OY)
            model = glm::rotate(model, glm::radians(angle), glm::vec3(0.0f, 1.0f, 0.0f));
        else if (ax == OZ)
            model = glm::rotate(model, glm::radians(angle), glm::vec3(0.0f, 0.0f, 1.0f));
        model = glm::scale(model, glm::vec3(scaleByX, scaleByY, scaleByZ));

        view = glm::translate(view, glm::vec3(stepByX, stepByY, stepByZ));

        projection = glm::perspective(glm::radians(45.0f), 900.0f / 900.0f, 0.1f, 100.0f);

        glUniformMatrix4fv(glGetUniformLocation(Gradient_mode, "view"), 1, GL_FALSE, glm::value_ptr(view));
        glUniformMatrix4fv(glGetUniformLocation(Gradient_mode, "projection"), 1, GL_FALSE, glm::value_ptr(projection));
        glUniformMatrix4fv(glGetUniformLocation(Gradient_mode, "model"), 1, GL_FALSE, glm::value_ptr(model));

        // подключаем VBO

        glBindBuffer(GL_ARRAY_BUFFER, VBO_tetrahedron);

        // Подключаем массив аттрибутов с указанием на каких местах кто находится
        glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 6 * sizeof(float), (void*)0);
        glEnableVertexAttribArray(0);
        glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 6 * sizeof(float), (void*)(3 * sizeof(float)));
        glEnableVertexAttribArray(1);

        glBindBuffer(GL_ARRAY_BUFFER, 0); // Отключаем VBO

        //Рисуем 

        glDrawArrays(GL_TRIANGLES, 0, 12);

        // Отключаем массивы атрибутов
        glDisableVertexAttribArray(0);
        glDisableVertexAttribArray(1);


        glUseProgram(0); // Отключаем шейдерную программу
        break;
    case (cube):
    {
        glUseProgram(Texture_mode); // Устанавливаем шейдерную программу текущей 

        //связываем текстуры с переменными в шейдере
        glActiveTexture(GL_TEXTURE0);
        glBindTexture(GL_TEXTURE_2D, texture1);
        glUniform1i(glGetUniformLocation(Texture_mode, "texture1"), 0);

        glActiveTexture(GL_TEXTURE1);
        glBindTexture(GL_TEXTURE_2D, texture2);
        glUniform1i(glGetUniformLocation(Texture_mode, "texture2"), 1);

        glUniform1f(glGetUniformLocation(Texture_mode, "mixValue"), mixValue);
        glUniform1i(glGetUniformLocation(Texture_mode, "two_tex"), two_tex);


        model = glm::translate(model, glm::vec3(0.0f, 0.0f, 0.0f));

        //float angle = 25.0f;
        //float elapsed1 = clock.getElapsedTime().asSeconds();
        model = glm::rotate(model, glm::radians(angle), glm::vec3(1.0f, 1.0f, 0.0f));
        //model = glm::rotate(model, elapsed1 * glm::radians(10.0f) / 2, glm::vec3(1.0f, 0.0f, 0.0f));

        model = glm::scale(model, glm::vec3(scaleByX, scaleByY, scaleByZ));

        view = glm::translate(view, glm::vec3(stepByX, stepByY, stepByZ));

        projection = glm::perspective(glm::radians(45.0f), 900.0f / 900.0f, 0.1f, 100.0f);

        glUniformMatrix4fv(glGetUniformLocation(Texture_mode, "view"), 1, GL_FALSE, glm::value_ptr(view));
        glUniformMatrix4fv(glGetUniformLocation(Texture_mode, "projection"), 1, GL_FALSE, glm::value_ptr(projection));
        glUniformMatrix4fv(glGetUniformLocation(Texture_mode, "model"), 1, GL_FALSE, glm::value_ptr(model));


        // подключаем VBO
        glBindBuffer(GL_ARRAY_BUFFER, VBO_cube);

        // Подключаем массив аттрибутов с указанием места
        glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)0);
        glEnableVertexAttribArray(0);

        glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(3 * sizeof(float)));
        glEnableVertexAttribArray(1);

        glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(6 * sizeof(float)));
        glEnableVertexAttribArray(2);

        glBindBuffer(GL_ARRAY_BUFFER, 0);  // Отключаем VBO

        //Рисуем 
        glDrawArrays(GL_TRIANGLES, 0, 36);

        // Отключаем массивы атрибутов
        glDisableVertexAttribArray(0);
        glDisableVertexAttribArray(1);
        glDisableVertexAttribArray(2);

        glUseProgram(0); // Отключаем шейдерную программу

    }
    case(circle):
        {
            //окружность

            glUseProgram(GradientCircle_mode); // Устанавливаем шейдерную программу текущей

            model = glm::translate(model, glm::vec3(0.0f, 0.0f, 0.0f));

            model = glm::rotate(model, glm::radians(angle), glm::vec3(-1.0f, 0.0f, 0.0f));

            model = glm::scale(model, glm::vec3(scaleByX, scaleByY, scaleByZ));

            view = glm::translate(view, glm::vec3(stepByX, stepByY, stepByZ));

            projection = glm::perspective(glm::radians(45.0f), 1.0f, 0.1f, 100.0f);

            glUniformMatrix4fv(glGetUniformLocation(GradientCircle_mode, "view"), 1, GL_FALSE, glm::value_ptr(view));
            glUniformMatrix4fv(glGetUniformLocation(GradientCircle_mode, "projection"), 1, GL_FALSE, glm::value_ptr(projection));
            glUniformMatrix4fv(glGetUniformLocation(GradientCircle_mode, "model"), 1, GL_FALSE, glm::value_ptr(model));

            //подключаем VBO
            glBindBuffer(GL_ARRAY_BUFFER, VBO_circle);

            // Подключаем массив аттрибутов с указанием на каких местах кто находится
            glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 6 * sizeof(float), (void*)0);
            glEnableVertexAttribArray(0);
            glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 6 * sizeof(float), (void*)(3 * sizeof(float)));
            glEnableVertexAttribArray(1);

            glBindBuffer(GL_ARRAY_BUFFER, 0); // Отключаем VBO

            //Рисуем в зависимости от выбранной фигуры
            glDrawArrays(GL_TRIANGLE_FAN, 0, sectors);

            // Отключаем массивы атрибутов
            glDisableVertexAttribArray(0);
            glDisableVertexAttribArray(1);


            glUseProgram(0); // Отключаем шейдерную программу

        }
    }
    checkOpenGLerror();
}


// Освобождение буфера
void ReleaseVBO()
{
    glBindBuffer(GL_ARRAY_BUFFER, 0);
    glDeleteBuffers(1, &VBO_tetrahedron);
    glDeleteBuffers(1, &VBO_cube);
    glDeleteBuffers(1, &VBO_circle);
}

// Освобождение шейдеров
void ReleaseShader()
{
    // Передавая ноль, мы отключаем шейдерную программу
    glUseProgram(0);
    // Удаляем шейдерные программы
    glDeleteProgram(Gradient_mode);
    glDeleteProgram(Texture_mode);
    glDeleteProgram(GradientCircle_mode);
}


void Release()
{
    // Шейдеры
    ReleaseShader();
    // Вершинный буфер
    ReleaseVBO();
}



//https://russianblogs.com/article/455565970/
void runner() {
    std::setlocale(LC_ALL, "Russian");
    sf::Window window(sf::VideoMode(1000, 1000), "Lab12", sf::Style::Default, sf::ContextSettings(24));
    window.setVerticalSyncEnabled(true);
    window.setActive(true);
    glewInit();
    glGetError(); // сброс флага GL_INVALID_ENUM
    Init();
    std::cout << "T - режим тетраэдра, C - режим куба, O - режим окружности" << std::endl;
    sf::Clock clock;
    while (window.isOpen()) {
        sf::Event event;
        while (window.pollEvent(event)) {
            if (event.type == sf::Event::Closed)
                window.close();
            else if (event.type == sf::Event::Resized)
                glViewport(0, 0, event.size.width, event.size.height);
            else if (event.type == sf::Event::KeyPressed)
                switch (event.key.code) {
                case sf::Keyboard::Escape:
                    window.close();
                    break;
                case sf::Keyboard::Down:
                    if (mode == cube) {
                        mixValue -= 0.01f;
                        if (mixValue <= 0.0f)
                            mixValue = 0.0f;
                    }
                    break;
                case sf::Keyboard::Up:
                    if (mode == cube) {
                        mixValue += 0.01f;
                        if (mixValue >= 1.0f)
                            mixValue = 1.0f;
                    }
                    break;
                case sf::Keyboard::Right:
                    if (ax == OX)
                        stepByX += 0.1f;
                    else if (ax == OY)
                        stepByY += 0.1f;
                    else if (ax == OZ)
                        stepByZ += 0.5f;
                    break;
                case sf::Keyboard::Left:
                    if (ax == OX)
                        stepByX -= 0.1f;
                    else if (ax == OY)
                        stepByY -= 0.1f;
                    else if (ax == OZ)
                        stepByZ -= 0.5f;
                    break;
                case sf::Keyboard::X:
                    ax = OX;
                    break;
                case sf::Keyboard::Y:
                    ax = OY;
                    break;
                case sf::Keyboard::Z:
                    ax = OZ;
                    break;
                case sf::Keyboard::C:
                    mode = cube;
                    std::cout << "Гайд для куба" << std::endl;
                    std::cout << "Стрелочка вверх - коэффициент смешения увеличивается" << std::endl;
                    std::cout << "Стрелочка вниз - коэффициент смешения уменьшается" << std::endl;
                    std::cout << "Клавиша M - включаем/отключаем режим двух текстур" << std::endl;
                    std::cout << "Клавиши X,Y,Z - выбираем ось вращения" << std::endl;
                    std::cout << "Клавиша R - вращаем вокруг выбранной оси" << std::endl;
                    break;
                case sf::Keyboard::T:
                    mode = tetrahedron;
                    std::cout << "Гайд для тетраэдра" << std::endl;
                    std::cout << "X - режим перемещения по Ox и ось вращения" << std::endl;
                    std::cout << "Y - режим перемещения по Oy и ось вращения" << std::endl;
                    std::cout << "Z - режим перемещения по Oz и ось вращения" << std::endl;
                    std::cout << "Стрелочка влево - уменьшение" << std::endl;
                    std::cout << "Стрелочка вправо - увеличение" << std::endl;
                    std::cout << "Клавиша R - вращаем вокруг выбранной оси" << std::endl;
                    break;
                case sf::Keyboard::O:
                    mode = circle;
                    std::cout << "Гайд" << std::endl;
                    std::cout << "Клавиша 1 - сжатие по Ox" << std::endl;
                    std::cout << "Клавиша 2 - растяжение по Ox" << std::endl;
                    std::cout << "Клавиша 3 - сжатие по Oу" << std::endl;
                    std::cout << "Клавиша 4 - растяжение по Oу" << std::endl;
                    std::cout << "Клавиша 5 - сжатие по Oz" << std::endl;
                    std::cout << "Клавиша 6 - растяжение по Oz" << std::endl;
                    break;
                case sf::Keyboard::Num1:
                    if (mode == circle)
                        scaleByX -= 0.1f;
                    break;
                case sf::Keyboard::Num2:
                    if (mode == circle)
                        scaleByX += 0.1f;
                    break;
                case sf::Keyboard::Num3:
                    if (mode == circle)
                        scaleByY -= 0.1f;
                    break;
                case sf::Keyboard::Num4:
                    if (mode == circle)
                        scaleByY += 0.1f;
                    break;
                case sf::Keyboard::Num5:
                    if (mode == circle)
                        scaleByZ -= 0.1f;
                    break;
                case sf::Keyboard::Num6:
                    if (mode == circle)
                        scaleByZ += 0.1f;
                    break;
                case sf::Keyboard::M:
                    if (mode == cube)
                        two_tex = !two_tex;
                    break;
                case (sf::Keyboard::R):
                    if (mode == cube)
                            angle += 15.0f;
                    if (mode == tetrahedron)
                        angle += 15.0f;
                        break;
                default:
                    break;
                }

        }
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
        Draw(clock);
        window.display();
    }
}

int main()
{
    runner();
    Release();
    return 0;
}