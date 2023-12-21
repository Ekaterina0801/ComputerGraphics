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
#include <assimp/Importer.hpp>
#include <assimp/scene.h>
#include <assimp/postprocess.h>
#include <iostream>
#include "Model.h"
using namespace std;

enum axis {
    OX,
    OY,
    OZ,
    NUL
};


//тип шейдера
enum type_shader_label {
    fong,
    toon, 
    rim
};

//режим направления или позиции
enum dir_or_pos_label {
    d,
    p,
};




enum type_light_label {
    dir,//направленный
    point, //точечный
    spot //прожектор
};

//default 
type_light_label type_light = dir;
axis ax = NUL;
dir_or_pos_label repl = p;
type_shader_label sh = fong;

//камера
glm::vec3 cameraPos = glm::vec3(0.0f, 4.0f, 35.0f);
glm::vec3 cameraFront = glm::vec3(0.0f, 0.0f, -1.0f);
glm::vec3 cameraUp = glm::vec3(0.0f, 1.0f, 0.0f);



// время для обработки кадров
float deltaTime = 0.0f;
float lastFrame = 0.0f;

//вращение
bool firstMouse = true;
float yaw = -90.0f;
float pitch = 0.0f;
float last_x = 450.0f;
float last_y = 450.0f;

// ID шейдерной программы
GLuint Phong_mode;
GLuint Toon_mode;
GLuint Rim_mode;
GLuint LightingCube_mode;

// ID атрибута
GLint Attrib_vertex;


//положение света
glm::vec3 lightPos(0.0f, 2.0f, 10.0f);
glm::vec3 lightDirection(0.0f, 0.0f, -1.0f);
glm::vec3 lightness(1.0f, 1.0f, 1.0f);
float conus = 12.5f;


// Исходный код вершинного шейдера для Фонга
const char* VertexShaderPhong = R"(
    #version 330 core

    layout (location = 0) in vec3 coord_pos;
    layout (location = 1) in vec3 normal_in;
    layout (location = 2) in vec2 tex_coord_in;

    out vec2 coord_tex;
	out vec3 normal;
	out vec3 frag_pos;

    uniform mat4 model;
    uniform mat4 view;
    uniform mat4 projection;
    
    void main() 
    { 
        gl_Position = projection * view * model * vec4(coord_pos, 1.0);
        coord_tex = tex_coord_in;
		frag_pos = vec3(model * vec4(coord_pos, 1.0));
		normal =  mat3(transpose(inverse(model))) * normal_in;
        //coord_tex = vec2(tex_coord_in.x, 1.0f - tex_coord_in.y); //если текстуры ннеправильно наложились
    }
    )";

// Исходный код фрагментного шейдера для Фонга

const char* FragShaderPhong = R"(
    #version 330 core

	struct Light {
		vec3 position;
		vec3 direction; //dir and spot
  
		vec3 ambient;
		vec3 diffuse;
		vec3 specular;

	//point (для затухания)
		float constant;
		float linear;
		float quadratic;

	//spot
		float cutOff;
		float outerCutOff;
	};

	uniform Light light;  

    in vec2 coord_tex;
    in vec3 frag_pos;
    in vec3 normal;

	out vec4 frag_color;

    uniform sampler2D texture_diffuse1;
	uniform vec3 viewPos;
	uniform int shininess;
	uniform int type_light;

    void main()  
    {
		vec3 norm = normalize(normal);
		vec3 lightDir;

		if(type_light == 0)
		{
            //from light to object
			lightDir = normalize(-light.direction);  //dir
		}
		else
		{
			lightDir = normalize(light.position - frag_pos);  //point and spot
		}
		
		vec3 ambient = light.ambient * texture(texture_diffuse1, coord_tex).rgb; 

		float diff = max(dot(norm, lightDir), 0.0);
		vec3 diffuse = light.diffuse * (diff * texture(texture_diffuse1, coord_tex).rgb); 

		vec3 viewDir = normalize(viewPos - frag_pos);
		vec3 reflectDir = reflect(-lightDir, norm);  

		float spec = pow(max(dot(viewDir, reflectDir), 0.0), shininess);
		vec3 specular = light.specular * (spec * texture(texture_diffuse1, coord_tex).rgb); 

		if(type_light == 1 || type_light == 2)
		{
			    //точечный и прожекторный
				float distance = length(light.position - frag_pos);
				float attenuation = 1.0 / (light.constant + light.linear * distance 
									+ light.quadratic * (distance * distance));
				if(type_light == 1)
				{
					ambient *= attenuation; //point
				}
				diffuse *= attenuation;
				specular *= attenuation;   
			    //end точечный и прожекторный

				if(type_light == 2)
				{	//прожектор
						float theta = dot(lightDir, normalize(-light.direction)); 
						float epsilon   = light.cutOff - light.outerCutOff;
						float intensity = clamp((theta - light.outerCutOff) / epsilon, 0.0, 1.0);

						diffuse *= intensity;
						specular *= intensity;
					//end прожектор
				}
		}

		vec3 result = (ambient + diffuse + specular);
		frag_color = vec4(result, 1.0);
    } 
)";

//Исходный код вершинного шейдера для Toon
const char* VertexShaderToon = R"(
    #version 330 core

    layout (location = 0) in vec3 coord_pos;
    layout (location = 1) in vec3 normal_in;
    layout (location = 2) in vec2 tex_coord_in;

    out vec2 coord_tex;
	out vec3 normal;
	out vec3 frag_pos;

    uniform mat4 model;
    uniform mat4 view;
    uniform mat4 projection;
    
    void main() 
    { 
        gl_Position = projection * view * model * vec4(coord_pos, 1.0);
        coord_tex = tex_coord_in;
		frag_pos = vec3(model * vec4(coord_pos, 1.0));
		normal =  mat3(transpose(inverse(model))) * normal_in;
        //coord_tex = vec2(tex_coord_in.x, 1.0f - tex_coord_in.y); //если текстуры ннеправильно наложились
    }
    )";

//Исходный код фрагментного шейдера для Toon
const char* FragShaderToon = R"(
    #version 330 core

	struct Light {
		vec3 position;
		vec3 direction; //направленный и прожекторный

	//point (для затухания)
		float constant;
		float linear;
		float quadratic;

	//spot
		float cutOff;
	};

	uniform Light light;  

    in vec2 coord_tex;
    in vec3 frag_pos;
    in vec3 normal;

	out vec4 frag_color;

    uniform sampler2D texture_diffuse1;
	uniform vec3 viewPos;
	uniform int type_light;

    void main()  
    {
		vec3 norm = normalize(normal);
		vec3 lightDir;

		if(type_light == 0)
		{
            //from light to object
			lightDir = normalize(-light.direction);  //dir
		}
		else
		{
			lightDir = normalize(light.position - frag_pos);  //point and spot
		}
		
       float intensity = max(dot(norm, lightDir), 0.0);
       vec3 result;
	   if (intensity > 0.95)
       {
          result = vec3(1.0,1.0,1.0)*texture(texture_diffuse1, coord_tex).rgb;
       }
       else if (intensity > 0.5)
       {
         result = vec3(0.6,0.6,0.6) * texture(texture_diffuse1, coord_tex).rgb;
       }
       else if (intensity > 0.25)
       {
         result = vec3(0.4,0.4,0.4) * texture(texture_diffuse1, coord_tex).rgb;
       }
       else 
         result = vec3(0.2,0.2,0.2) * texture(texture_diffuse1, coord_tex).rgb;
        float theta = dot(lightDir, normalize(-light.direction)); 
		frag_color = vec4(result, 1.0);
        if(type_light == 1 || type_light == 2)
		{
			//точечный и прожекторный
				float distance    = length(light.position - frag_pos);
				float attenuation = 1.0 / (light.constant + light.linear * distance 
									+ light.quadratic * (distance * distance));
				result *= attenuation;   
			//end точечный м прожекторный

				if(type_light == 2 && theta <= light.cutOff)
				{	
					//прожектор
						result = vec3(0.2, 0.2, 0.2) * texture(texture_diffuse1, coord_tex).rgb;	
					//end прожектор
				}	
		}

		frag_color = vec4(result, 1.0);
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


void InitShaderPhong()
{
    GLuint vShaderPhong = glCreateShader(GL_VERTEX_SHADER);
    //компиляция шейдера
    glShaderSource(vShaderPhong, 1, &VertexShaderPhong, NULL);
    glCompileShader(vShaderPhong);
    std::cout << "vertex shader \n";
    ShaderLog(vShaderPhong);

    GLuint fShaderPhong = glCreateShader(GL_FRAGMENT_SHADER);
    glShaderSource(fShaderPhong, 1, &FragShaderPhong, NULL);
    glCompileShader(fShaderPhong);
    std::cout << "fragment shader \n";
    // Функция печати лога шейдера
    ShaderLog(fShaderPhong);
    Phong_mode = glCreateProgram();
    glAttachShader(Phong_mode, vShaderPhong);
    glAttachShader(Phong_mode, fShaderPhong);
    // Линкуем шейдерную программу
    glLinkProgram(Phong_mode);
    // Проверяем статус сборки
    int link_ok;
    glGetProgramiv(Phong_mode, GL_LINK_STATUS, &link_ok);

    if (!link_ok)
    {
        std::cout << "error attach shaders \n";
        return;
    }
    checkOpenGLerror();
}

void InitShaderToon()
{
    GLuint vShaderToon = glCreateShader(GL_VERTEX_SHADER);
    //компиляция шейдера
    glShaderSource(vShaderToon, 1, &VertexShaderToon, NULL);
    glCompileShader(vShaderToon);
    std::cout << "vertex shader \n";
    ShaderLog(vShaderToon);

    GLuint fShaderToon = glCreateShader(GL_FRAGMENT_SHADER);
    glShaderSource(fShaderToon, 1, &FragShaderToon, NULL);
    glCompileShader(fShaderToon);
    std::cout << "fragment shader \n";
    // Функция печати лога шейдера
    ShaderLog(fShaderToon);
    Toon_mode = glCreateProgram();
    glAttachShader(Toon_mode, vShaderToon);
    glAttachShader(Toon_mode, fShaderToon);
    // Линкуем шейдерную программу
    glLinkProgram(Toon_mode);
    // Проверяем статус сборки
    int link_ok;
    glGetProgramiv(Toon_mode, GL_LINK_STATUS, &link_ok);

    if (!link_ok)
    {
        std::cout << "error attach shaders \n";
        return;
    }
    checkOpenGLerror();
}
void InitShader()
{
    InitShaderPhong();
    InitShaderToon();
}


void Init()
{
    // Шейдеры
    InitShader();
    
    //включаем тест глубины
    glEnable(GL_DEPTH_TEST);
}


void Draw(sf::Clock clock, vector<Model> mod, GLint shader, int count)
{
    glm::mat4 view = glm::mat4(1.0f);
    glm::mat4 model = glm::mat4(1.0f);
    glm::mat4 projection = glm::perspective(glm::radians(45.0f), 900.0f / 900.0f, 0.1f, 100.0f);
    glUseProgram(shader);
    view = glm::lookAt(cameraPos, cameraPos + cameraFront, cameraUp);
    glUniform3f(glGetUniformLocation(shader, "light.position"), lightPos.x, lightPos.y, lightPos.z);
    glUniform3f(glGetUniformLocation(shader, "viewPos"), cameraPos.x, cameraPos.y, cameraPos.z);
    glUniform1i(glGetUniformLocation(shader, "type_light"), type_light);
    glUniform3f(glGetUniformLocation(shader, "light.ambient"), 0.2f, 0.2f, 0.2f);
    glUniform3f(glGetUniformLocation(shader, "light.diffuse"), 0.9f, 0.9f, 0.9);
    glUniform3f(glGetUniformLocation(shader, "light.specular"), 1.0f, 1.0f, 1.0f);
    glUniform1i(glGetUniformLocation(shader, "shininess"), 16);
    glUniform3f(glGetUniformLocation(shader, "light.direction"), lightDirection.x, lightDirection.y, lightDirection.z);
    glUniform1f(glGetUniformLocation(shader, "light.constant"), 1.0f);
    glUniform1f(glGetUniformLocation(shader, "light.linear"), 0.045f);
    glUniform1f(glGetUniformLocation(shader, "light.quadratic"), 0.0075f);
    glUniform1f(glGetUniformLocation(shader, "light.cutOff"), glm::cos(glm::radians(conus)));
    glUniform1f(glGetUniformLocation(shader, "light.outerCutOff"), glm::cos(glm::radians(conus * 1.4f)));
    glUniformMatrix4fv(glGetUniformLocation(shader, "view"), 1, GL_FALSE, glm::value_ptr(view));
    glUniformMatrix4fv(glGetUniformLocation(shader, "projection"), 1, GL_FALSE, glm::value_ptr(projection));
    glUniformMatrix4fv(glGetUniformLocation(shader, "model"), 1, GL_FALSE, glm::value_ptr(model));

    float angle = 90.0f;
    //--------------------------ChristmasTree----------------------------------
    model = glm::scale(model, glm::vec3(2.5f, 2.5f, 2.5f));
    model = glm::translate(model, glm::vec3(0.0f, 0, 0.0f));
    //model = glm::rotate(model, glm::radians(angle), glm::vec3(1.0f, 0.0f, 0.0f));
    model = glm::rotate(model, glm::radians(angle), glm::vec3(0.0f, 1.0f, 0.0f));
    glUniformMatrix4fv(glGetUniformLocation(shader, "model"), 1, GL_FALSE, glm::value_ptr(model));
    mod[0].Draw(shader, count);

    //--------------------------ChristmasTree----------------------------------
    model = glm::rotate(model, glm::radians(angle), glm::vec3(0.0f, 1.0f, 0.0f));
    model = glm::mat4(1.0f);
    model = glm::translate(model, glm::vec3(5.0f, 5, 5.0f));
    glUniformMatrix4fv(glGetUniformLocation(shader, "model"), 1, GL_FALSE, glm::value_ptr(model));
    mod[1].Draw(shader, count);

    //--------------------------ChristmasTree----------------------------------
    model = glm::mat4(1.0f);
    model = glm::translate(model, glm::vec3(4.0f, 1.4f, 2.0f));
    glUniformMatrix4fv(glGetUniformLocation(shader, "model"), 1, GL_FALSE, glm::value_ptr(model));
    mod[2].Draw(shader, count);

    //--------------------------ChristmasTree----------------------------------
    model = glm::mat4(1.0f);
    model = glm::translate(model, glm::vec3(2.5f, 0.0f, 3.0f));
    glUniformMatrix4fv(glGetUniformLocation(shader, "model"), 1, GL_FALSE, glm::value_ptr(model));
    mod[3].Draw(shader, count);

    //--------------------------Pinguin----------------------------------
    model = glm::mat4(1.0f);
    model = glm::translate(model, glm::vec3(7.0f, 0.0f, 2.0f));
    model = glm::rotate(model, glm::radians(-angle), glm::vec3(1.0f, 0.0f, 0.0f));
    glUniformMatrix4fv(glGetUniformLocation(shader, "model"), 1, GL_FALSE, glm::value_ptr(model));
    mod[4].Draw(shader, count);

    glUseProgram(0); // Отключаем шейдерную программу
    checkOpenGLerror();
}


// Освобождение буфера
void ReleaseVBO()
{
    glBindBuffer(GL_ARRAY_BUFFER, 0);

}

// Освобождение шейдеров
void ReleaseShader()
{
    // Передавая ноль, мы отключаем шейдерную программу
    glUseProgram(0);

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
    sf::Window window(sf::VideoMode(900, 900), "Lab13", sf::Style::Default, sf::ContextSettings(24));
    window.setVerticalSyncEnabled(true);
    window.setActive(true);
    glewInit();
    glGetError(); // сброс флага GL_INVALID_ENUM
    Init();
    std::cout << "Гайд" << std::endl;
    std::cout << "1 - Фонг, 2 - Toon, 3 - Рим, 4 - направленный, 5 - точечный, 6 - прожектор" << std::endl;
    std::cout << "R - смена режима изменения параметров источника, по дефолту меняем направление, можно сменить на положение" << std::endl;
    std::cout << "Для прожектора: K - увеличить радиус конуса, L - уменьшить радиус конуса" << std::endl;
    std::cout << "Изменение параметров: стрелочка вверх - увеличение по Z" << std::endl;
    std::cout << "Изменение параметров: стрелочка вверх - уменьшение по Z" << std::endl;
    std::cout << "Изменение параметров: стрелочка вправо - увеличение по X" << std::endl;
    std::cout << "Изменение параметров: стрелочка влево - уменьшение по X" << std::endl;
    std::cout << "Изменение параметров: запятая - уменьшение по Y" << std::endl;
    std::cout << "Изменение параметров: точка - увеличение по Y" << std::endl;
    std::cout << "Камера: перемещение - WSAD, Q - приближение, E - отдаление" << std::endl;
    sf::Clock clock;
    srand(static_cast<unsigned int>(clock.getElapsedTime().asSeconds())); // initialize random seed
    vector<Model> models;

    Model tree("tree/source/Christmas_tree.obj");
    Model pin("planet1/penguin02.fbx");
    models.push_back(tree);
    models.push_back(tree);
    models.push_back(tree);
    models.push_back(tree);
    models.push_back(pin);

    
    while (window.isOpen())
    {
        sf::Event event;

        while (window.pollEvent(event))
        {

            float camera_speed = 0.5f;

            if (event.type == sf::Event::Closed)
                window.close();
            if (event.type == sf::Event::KeyPressed)
            {
                switch (event.key.code)
                {
                case sf::Keyboard::Escape:
                    window.close();
                    break;
                case sf::Keyboard::W:
                    cameraPos += camera_speed * cameraFront;
                    break;
                case sf::Keyboard::S:
                    cameraPos -= camera_speed * cameraFront;
                    break;
                case sf::Keyboard::A:
                    cameraPos -= glm::normalize(glm::cross(cameraFront, cameraUp)) * camera_speed;
                    break;
                case sf::Keyboard::D:
                    cameraPos += glm::normalize(glm::cross(cameraFront, cameraUp)) * camera_speed;
                    break;
                case sf::Keyboard::Q:
                    cameraPos += camera_speed * cameraUp;
                    break;
                case sf::Keyboard::E:
                    cameraPos -= camera_speed * cameraUp;
                    break;
                case sf::Keyboard::K:
                    conus += 0.5f;
                    std::cout << conus << std::endl;
                    break;
                case sf::Keyboard::L:
                    conus -= 0.5f;
                    std::cout << conus << std::endl;
                    break;
                case sf::Keyboard::R:
                    if (repl == p)
                        repl = d;
                    else
                        repl = p;
                    break;
                case sf::Keyboard::Up:
                    if (repl == p)
                    {
                        lightPos -= glm::vec3(0.0f, 0.0f, 0.5f);
                        std::cout << lightPos.z << std::endl;
                    }
                    else
                    {
                        lightDirection += glm::vec3(0.0f, 0.0f, 0.5f);
                        std::cout << lightDirection.z << std::endl;
                    }
                    break;
                case sf::Keyboard::Down:
                    if (repl == p)
                    {
                        lightPos += glm::vec3(0.0f, 0.0f, 0.5f);
                        std::cout << lightPos.z << std::endl;
                    }
                    else
                    {
                        lightDirection -= glm::vec3(0.0f, 0.0f, 0.5f);
                        std::cout << lightDirection.z << std::endl;
                    }
                    break;
                case sf::Keyboard::Right:
                    if (repl == p)
                    {
                        lightPos += glm::vec3(0.5f, 0.0f, 0.0f);
                        std::cout << lightPos.x << std::endl;
                    }
                    else
                    {
                        lightDirection -= glm::vec3(0.5f, 0.0f, 0.0f);
                        std::cout << lightDirection.x << std::endl;
                    }
                    break;
                case  sf::Keyboard::Left:
                    if (repl == p)
                    {
                        lightPos -= glm::vec3(0.5f, 0.0f, 0.0f);
                        std::cout << lightPos.x << std::endl;
                    }
                    else
                    {
                        lightDirection += glm::vec3(0.5f, 0.0f, 0.0f);
                        std::cout << lightDirection.x << std::endl;
                    }
                case  sf::Keyboard::Comma:
                    if (repl == p)
                    {
                        lightPos += glm::vec3(0.0f, 0.5f, 0.0f);
                        std::cout << lightPos.y << std::endl;
                    }
                    else
                    {
                        lightDirection -= glm::vec3(0.0f, 0.5f, 0.0f);
                        std::cout << lightDirection.y << std::endl;
                    }
                    break;
                case sf::Keyboard::Period:
                    if (repl == p)
                    {
                        lightPos -= glm::vec3(0.0f, 0.5f, 0.0f);
                        std::cout << lightPos.y << std::endl;
                    }
                    else
                    {
                        lightDirection += glm::vec3(0.0f, 0.5f, 0.0f);
                        std::cout << lightDirection.y << std::endl;
                    }
                    break;
                case sf::Keyboard::Num1:
                    sh = rim;
                    std::cout << "rim" << std::endl;
                    break;
                case sf::Keyboard::Num2:
                    sh = toon;
                    std::cout << "toon" << std::endl;
                    break;
                case sf::Keyboard::Num3:
                    sh = fong;
                    std::cout << "fong" << std::endl;
                    break;
                case sf::Keyboard::Num4:
                    type_light = dir;
                    std::cout << "dir" << std::endl;
                    break;
                case sf::Keyboard::Num5:
                    type_light = point;
                    std::cout << "point" << std::endl;
                    break;
                case sf::Keyboard::Num6:
                    type_light = spot;
                    std::cout << "spot" << std::endl;
                    break;
                }
            }
            if (event.type == sf::Event::MouseMoved)
            {
                float xpos = static_cast<float>(event.mouseMove.x);
                float ypos = static_cast<float>(event.mouseMove.y);

                if (firstMouse)
                {
                    last_x = xpos;
                    last_y = ypos;
                    firstMouse = false;
                }

                float xoffset = xpos - last_x;
                float yoffset = last_y - ypos;
                last_x = xpos;
                last_y = ypos;

                float sensitivity = 0.1f;
                xoffset *= sensitivity;
                yoffset *= sensitivity;

                yaw += xoffset;
                pitch += yoffset;

                if (pitch > 89.0f)
                    pitch = 89.0f;
                if (pitch < -89.0f)
                    pitch = -89.0f;

                glm::vec3 front;
                front.x = cos(glm::radians(yaw)) * cos(glm::radians(pitch));
                front.y = sin(glm::radians(pitch));
                front.z = sin(glm::radians(yaw)) * cos(glm::radians(pitch));
                cameraFront = glm::normalize(front);
            }
            else if (event.type == sf::Event::Resized)
                glViewport(0, 0, event.size.width, event.size.height);
        }

        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

        GLint shader = Phong_mode;

        if (sh == rim)
            shader = Rim_mode;
        if (sh == toon)
            shader = Toon_mode;
        if (sh == fong)
            shader = Phong_mode;

        Draw(clock, models, shader, 1);

        window.display();
    }
}

int main()
{
    runner();
    Release();
    return 0;
}

