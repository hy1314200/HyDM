#include "OdPlatform.h"

#if defined(__POWERPC__) && defined(__GNUC__) /////////////////////////////////// MAC

#include <OpenGL/gl.h>
#include <OpenGL/glu.h>

#ifdef DD_USING_GLUT
#include <GLUT/glut.h>
#endif

#define DD_STD_CALL 

#elif defined sgi  ///////////////////////////////// SGI

#include <GL/gl.h>
#include <GL/glu.h>

#ifdef DD_USING_GLUT
#include <GL/glut.h>
#endif

#define DD_STD_CALL 

#else                  ////////////////////////////////// Windows

#include <gl/gl.h>
#include <gl/glu.h>

#ifdef DD_USING_GLUT
#include <GL/glut.h>
#endif

#define DD_STD_CALL __stdcall

#endif                 ////////////////////////////////// END





