#include <gl/glew.h>
#include <SFML/OpenGL.hpp>
#include <SFML/Window.hpp>
#include <SFML/Graphics.hpp>
#include <iostream>

// ID шейдерной программы
GLuint Program;
// ID атрибута
GLint Attrib_vertex;
// ID Vertex Buffer Object
GLuint VBO;
GLint move_x;
GLint move_z;
GLint move_y;

GLint UniformColor;

struct Vertex {
	GLfloat x;
	GLfloat y;
	GLfloat r;
	GLfloat z;
	GLfloat g;
	GLfloat b;
};

// Исходный код вершинного шейдера
const char* VertexShaderSource = R"(
#version 410 core
in vec3 coord;
in vec3 color; 

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
#version 410 core
in vec3 vertexColor; // Получаем цвет от вершинного шейдера
out vec4 color;
void main() {
    color = vec4(vertexColor, 1.0); // Используем цвет от вершины
}
)";

float moveX = 0;
float moveY = 0;
float moveZ = 0;
void moveShape(float moveXinc, float moveYinc, float moveZinc) {
	moveX += moveXinc;
	moveY += moveYinc;
	moveZ += moveZinc;
}

void ShaderLog(unsigned int shader)
{
	int infologLen = 0;
	glGetShaderiv(shader, GL_INFO_LOG_LENGTH, &infologLen);
	if (infologLen > 1)
	{
		int charsWritten = 0;
		std::vector<char> infoLog(infologLen);
		glGetShaderInfoLog(shader, infologLen, &charsWritten, infoLog.data());
		std::cout << "InfoLog: " << infoLog.data() << std::endl;
	}
}

void checkOpenGLerror()
{
	GLenum err;
	while ((err = glGetError()) != GL_NO_ERROR)
	{
		// Process/log the error.
		std::cout << err << std::endl;
	}
}

void InitShader(int num_task) {
	GLuint vShader = glCreateShader(GL_VERTEX_SHADER);
	glShaderSource(vShader, 1, &VertexShaderSource, NULL);
	glCompileShader(vShader);
	std::cout << "vertex shader \n";
	ShaderLog(vShader);
	GLuint fShader = glCreateShader(GL_FRAGMENT_SHADER);
	glShaderSource(fShader, 1, &FragShaderSource_Gradient, NULL);

	glCompileShader(fShader);
	std::cout << "fragment shader \n";
	ShaderLog(fShader);
	Program = glCreateProgram();
	glAttachShader(Program, vShader);
	glAttachShader(Program, fShader);
	glLinkProgram(Program);
	int link_ok;
	glGetProgramiv(Program, GL_LINK_STATUS, &link_ok);
	if (!link_ok) {
		std::cout << "error attach shaders \n";
		return;
	}
	// Вытягиваем ID атрибута из собранной программы
	const char* attr_name = "coord"; //имя в шейдере
	Attrib_vertex = glGetAttribLocation(Program, attr_name);
	if (Attrib_vertex == -1) {
		std::cout << "could not bind attrib " << attr_name << std::endl;
		return;
	}

	// Достаем ID юниформы
	const char* unif_name = "x_move";
	move_x = glGetUniformLocation(Program, unif_name);
	if (move_x == -1)
	{
		std::cout << "could not bind uniform " << unif_name << std::endl;
		return;
	}

	unif_name = "y_move";
	move_y = glGetUniformLocation(Program, unif_name);
	if (move_y == -1)
	{
		std::cout << "could not bind uniform " << unif_name << std::endl;
		return;
	}

	unif_name = "z_move";
	move_z = glGetUniformLocation(Program, unif_name);
	if (move_z == -1)
	{
		std::cout << "could not bind uniform " << unif_name << std::endl;
		return;
	}
	checkOpenGLerror();
}

void InitVBO(int num_task) {
	glGenBuffers(1, &VBO);
	//тетраэдр
	Vertex tetrahedron[] = {
	{ 0.3f, 0.675f, -0.75f, 0.0f, 0.0f, 1.5f },
	{ -0.9f, 0.f, -0.75f, 1.5f, 0.0f, 0.0f },
	{ 0.f, 0.f, 0.75f, 1.5f, 1.5f, 1.5f },

	{ -0.9f, 0.f, -0.75f, 1.5f, 0.0f, 0.0f },
	{ 0.f, 0.f, 0.75f, 1.5f, 1.5f, 1.5f },
	{ 0.3f, -0.675f, -0.75f, 0.0f, 1.5f, 0.0f },

	{ 0.f, 0.f, 0.75f, 1.5f, 1.5f, 1.5f },
	{ 0.3f, 0.675f, -0.75f, 0.0f, 0.0f, 1.5f },
	{ 0.3f, -0.675f, -0.75f, 0.0f, 1.5f, 0.0f },
	};

	// куб
	Vertex cube[] = {
		// Передняя грань
	  { -0.25f, -0.5f, 0.5f, 1.0f, 0.0f, 0.0f }, // левая низ
	  { 0.5f, -0.25f, 0.5f, 1.0f, 1.0f, 0.0f }, // правая низ
	  { 0.25f, 0.5f, 0.5f, 0.0f, 1.0f, 0.0f }, // правая вверх
	  { -0.5f, 0.25f, 0.5f, 0.0f, 0.0f, 1.0f }, // левая вверх

	  // Правая грань
	  { 0.5f, -0.25f, -0.5f, 1.0f, 1.0f, 0.0f  }, // левая низ
	  { 0.25f, 0.5f, -0.5f, 0.0f, 1.0f, 0.0f  }, // левая вверх
	  { 0.5f, 0.5f, -0.5f, 0.1f, 0.6f, 0.4f }, // правая вверх
	  { 0.75f, -0.25f, -0.5f, 0.90f, 0.50f, 0.70f }, // правая низ

	  // Нижняя грань
	  { -0.25f, -0.5f, 0.5f, 1.0f, 0.0f, 0.0f }, // левая низ 
	  { 0.5f, -0.25f, 0.5f, 1.0f, 1.0f, 0.0f }, // левая вверх
	  { 0.75f, -0.25f, -0.5f, 0.90f, 0.50f, 0.70f }, // правая вверх
	  { 0.0f, -0.5f, -0.5f, 1.0f, 1.0f, 0.0f }, // правая низ

	};



	// Передаем вершины в буфер
	glBindBuffer(GL_ARRAY_BUFFER, VBO);
	if (num_task == 1)
		glBufferData(GL_ARRAY_BUFFER, sizeof(tetrahedron), tetrahedron, GL_STATIC_DRAW);
	else if (num_task == 2)
		glBufferData(GL_ARRAY_BUFFER, sizeof(cube), cube, GL_STATIC_DRAW);

	checkOpenGLerror(); //Пример функции есть в лабораторной
}

void Init(int num_task) {
	// Шейдеры
	InitShader(num_task);
	// Вершинный буфер
	InitVBO(num_task);

	glEnable(GL_DEPTH_TEST);
}

void Draw(int num_task) {

	glUseProgram(Program); // Устанавливаем шейдерную программу текущей

	glUniform1f(move_x, moveX);
	glUniform1f(move_y, moveY);
	glUniform1f(move_z, moveZ);
	glEnableVertexAttribArray(0);
	glEnableVertexAttribArray(1);
	glBindBuffer(GL_ARRAY_BUFFER, VBO);
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 6 * sizeof(GLfloat), (GLvoid*)0);
	glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 6 * sizeof(GLfloat), (GLvoid*)(3 * sizeof(GLfloat)));

	if (num_task == 1) {
		glDrawArrays(GL_TRIANGLES, 0, 9);
	}
	else if (num_task == 2) {

		glDrawArrays(GL_QUADS, 0, 12);
	}


	glDisableVertexAttribArray(0);
	glDisableVertexAttribArray(1);
	glUseProgram(0);
	checkOpenGLerror();
}


// Освобождение буфера
void ReleaseVBO() {
	glBindBuffer(GL_ARRAY_BUFFER, 0);
	glDeleteBuffers(1, &VBO);
}

// Освобождение шейдеров
void ReleaseShader() {
	// Передавая ноль, мы отключаем шейдерную программу
	glUseProgram(0);
	// Удаляем шейдерную программу
	glDeleteProgram(Program);
}


void Release() {
	// Шейдеры
	ReleaseShader();
	// Вершинный буфер
	ReleaseVBO();
}

void Runner(int num_task) {
	sf::Window window(sf::VideoMode(800, 800), "Lab12", sf::Style::Default, sf::ContextSettings(24));
	window.setVerticalSyncEnabled(true);
	window.setActive(true);
	glewInit();
	Init(num_task);

	bool is_runner = true;
	while (is_runner) {
		sf::Event event;
		while (window.pollEvent(event)) {
			if (event.type == sf::Event::Closed) { is_runner = false; }
			else if (event.type == sf::Event::Resized)
			{
				glViewport(0, 0, event.size.width, event.size.height);
			}
			else if (event.type == sf::Event::KeyPressed) {
				switch (event.key.code) {
				case (sf::Keyboard::W): moveShape(0, 0.1, 0); break;
				case (sf::Keyboard::S): moveShape(0, -0.1, 0); break;
				case (sf::Keyboard::A): moveShape(-0.1, 0, 0); break;
				case (sf::Keyboard::D): moveShape(0.1, 0, 0); break;
				case (sf::Keyboard::Q): moveShape(0, 0, -0.2); break;
				case (sf::Keyboard::E): moveShape(0, 0, 0.2); break;
				default: break;
				}
			}
		}

		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
		Draw(num_task);
		window.display();
	}
	Release();

}

int main() {
	int num_task = 0;
	while (true) {
		std::cout << "Enter task: ";
		std::cin >> num_task;
		if (num_task == 4 || num_task == 2 || num_task == 3 || num_task == 1)
			Runner(num_task);
	}
	return 0;
}