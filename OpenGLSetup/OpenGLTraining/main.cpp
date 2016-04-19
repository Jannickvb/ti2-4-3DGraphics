#include <gl\freeglut.h>
#include <stdio.h>

using namespace std;

void changeViewPort(int w, int h)
{
	glViewport(0, 0, w, h);

	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluPerspective(70, (float)w / (float)h, 0.1, 10.0);
	glMatrixMode(GL_MODELVIEW);
}

float rotation = 0;

void render()
{
	//set buffers
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	//Draw everything between glClear and glutSwapBuffers
	
	glLoadIdentity();
	glTranslatef(0.0, 0.0, -5.0);
	glRotatef(rotation, 0.0, 1.0, 0.0);
	rotation += 1.0;

	glBegin(GL_TRIANGLES);

	glColor3f(1.0, 1.0, 0.0);

	glVertex3f(-0.5, -0.5, 0.0);
	glVertex3f(0.5, -0.5, 0.0);
	glVertex3f(0.0, 0.5, 0.0);

	glEnd();

	//swaps color/depth buffers to refresh screen
	glutSwapBuffers();
}

void initOpenGL()
{
	glClearColor(0.0, 0.0, 0.0, 1.0);

	glEnable(GL_DEPTH_TEST);
	glDepthFunc(GL_LEQUAL);
}

int main(int argc, char* argv[]) {
	// Initialize GLUT
	glutInit(&argc, argv);
	// Set up some memory buffers for our display
	glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGBA | GLUT_DEPTH);
	// Set the window size and position
	glutInitWindowSize(800, 600);
	glutInitWindowPosition(100, 100);
	// Create the window with the title "Hello,GL"
	glutCreateWindow("Hello, GL");

	initOpenGL(); //init base settings (color etc.)

	// Bind the two functions (above) to respond when necessary
	glutReshapeFunc(changeViewPort); //called when window is created or reshaped
	glutDisplayFunc(render);
	glutIdleFunc(render);

	glutMainLoop();
	return 0;
}